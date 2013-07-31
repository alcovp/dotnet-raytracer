using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataStructure;
using System.Threading;
using System.Drawing;
using SceneDefinition;

namespace Solver
{
    public class Computer
    {
        public static Pixel ComputePixelColor(int x, int y, XYZ eye_p, XYZ ray_v, IList<IContainer> containingObjects, double n1, int recursion)
        {
            if (x == 180 && y == 120)
            {

            }
            recursion--;
            Result result = null;
            IList<Result> results = new List<Result>();
            double minDistanceToPoint = double.MaxValue;
            double n2 = n1;

            #region Результаты пересечения с объектом
            // для каждого объекта сцены
            foreach (var obj in Scene.objects)
            {
                // проверяем, не пересекается ли текущий луч с ним
                var nextResult = obj.GetIntersectionResult(eye_p, ray_v, n1);
                if (nextResult != null)
                {
                    
                    results.Add(nextResult);
                }
                
            }
            // найдем самый первый(ближний) пересеченный объект
            foreach (var i in results)
            {
                var distance = i.Point.Substract(eye_p).ScalarOfVector();
                if (distance < minDistanceToPoint)
                {
                    minDistanceToPoint = distance;
                    result = i;
                }
            }
            
            if (x == 180 && y == 120)
            {

            }
            if (result == null)
            {
                // в случае ухода луча в космос
                return new Pixel { Color = Color.FromArgb(0, 0, 0), X = x, Y = y };
            }
            #endregion

            #region Показатель преломления среды
            // проверяем, были ли мы внутри объекта до его пересечения (для расчета лучей преломления)
            if (containingObjects.Contains(result.Container))
            {
                // выход из объекта
                containingObjects.Remove(result.Container);
                // берем показатель преломления у объекта в который мы вошли последним
                if (containingObjects.Count > 0)
                {
                    n2 = containingObjects.Last().GetRefractionIndex();
                }
                else
                {
                    // если вышли из всех объектов, берем показатель преломления воздуха
                    n2 = 1;
                }
            }
            else
            {
                // вход в новый объект
                if (result.Container != null)
                {
                    containingObjects.Add(result.Container);
                    n2 = result.Container.GetRefractionIndex();
                }
            }
            #endregion

            #region Освещение
            // Расчет освещения
            IList<Illumination> illumination = new List<Illumination>();
            if (result != null)
            {
                // для каждого источника света в сцене
                foreach (var light in Scene.lights)
                {
                    bool softShadowed = false;
                    bool fullShadowed = false;
                    double totalRefractivity = 1;
                    var newIllumination = new Illumination();
                    var voxel_Light = light.Center.Substract(result.Point);
                    var directionToLight = voxel_Light.Normalize();
                    var distanceToLight = voxel_Light.ScalarOfVector();
                    // проверим каждый объект на следующие случаи
                    foreach (var obj in Scene.objects)
                    {
                        var possibleBlockingPoint = obj.GetIntersectionResult(result.Point.Add(directionToLight.Product(0.001)), directionToLight, n1);
                        // если полностью прозрачный - не считать затенение
                        if (possibleBlockingPoint != null && possibleBlockingPoint.Material.Refractivity != 1)
                        {
                            var distanceToBlockingVoxel = possibleBlockingPoint.Point.Substract(result.Point).ScalarOfVector();
                            // проверка на неполную затененность
                            if (distanceToBlockingVoxel < distanceToLight)
                            {
                                softShadowed = true;
                                // проверка на полную затененность
                                if (possibleBlockingPoint.Material.Refractivity == 0)
                                {
                                    fullShadowed = true;
                                    break;
                                }
                                else
                                {
                                    totalRefractivity *= possibleBlockingPoint.Material.Refractivity;
                                }
                            }
                        }
                    }
                    newIllumination.Abmient = light.Abmient;
                    // если точка полностью затенена
                    if (fullShadowed)
                    {
                        newIllumination.Diffuse = 0;
                        newIllumination.Specular = 0;
                    }
                    else
                    { 
                        // если точка затенена неполностью
                        if (softShadowed)
                        {
                            newIllumination.Diffuse = light.GetDiffuse(result.Point, result.Normal, eye_p) * totalRefractivity;
                            newIllumination.Specular = light.GetSpecular(result.Point, result.Normal, eye_p, result.ReflectedRay) * totalRefractivity;
                        }
                        else
                        {
                            // если между точкой и источником света нет препятствий
                            newIllumination.Diffuse = light.GetDiffuse(result.Point, result.Normal, eye_p);
                            newIllumination.Specular = light.GetSpecular(result.Point, result.Normal, eye_p, result.ReflectedRay);
                        }
                    }
                    illumination.Add(newIllumination);
                }
            }
            #endregion

            #region Смешение составляющих в конечный цвет итерации
            if (result == null)
            {
                // в случае ухода луча в космос
                return new Pixel { Color = Color.FromArgb(0, 0, 0), X = x, Y = y };
            }
            else
            {
                // подготовка выходных данных
                Pixel reflectedPixel = null;
                Pixel refractedPixel = null;
                // освещение
                double totalIllumination = 0;
                foreach (var i in illumination)
                {
                    totalIllumination += i.Abmient + i.Diffuse + i.Specular;
                }
                if (totalIllumination > 1)
                {
                    totalIllumination = 1;
                }
                if (totalIllumination < 0)
                {
                    totalIllumination = 0;
                }
                result.Color = Color.FromArgb((int)(result.Color.R * totalIllumination), (int)(result.Color.G * totalIllumination), (int)(result.Color.B * totalIllumination));
                // расчет преломления и отражения 
                if (recursion > 0)
                {
                    if (result.ReflectedRay != null && result.Material.Reflectivity > 0)
                    {
                        reflectedPixel = ComputePixelColor(x, y, result.Point.Add(result.ReflectedRay.Product(0.001)), result.ReflectedRay, containingObjects, n1, recursion);
                    }
                    if (result.RefractedRay != null && result.Material.Refractivity > 0)
                    {
                        refractedPixel = ComputePixelColor(x, y, result.Point.Add(result.RefractedRay.Product(0.001)), result.RefractedRay, containingObjects, n2, recursion);
                    }
                }
                // смешивение состовляющих цвета конечного пикселя
                if (reflectedPixel != null && refractedPixel != null)
                {
                    // stub for mixing colors. now it always mixes 1:1:1
                    var newColor = Color.FromArgb(
                        (result.Color.R + reflectedPixel.Color.R + refractedPixel.Color.R) / 3,
                        (result.Color.G + reflectedPixel.Color.G + refractedPixel.Color.G) / 3,
                        (result.Color.B + reflectedPixel.Color.B + refractedPixel.Color.B) / 3
                        );
                    return new Pixel { Color = newColor, X = x, Y = y };
                }
                // случай без преломления
                else if (reflectedPixel != null)
                {
                    // stub for mixing colors. now it always mixes 1:1
                    var newColor = Color.FromArgb(
                        (result.Color.R + reflectedPixel.Color.R) / 2,
                        (result.Color.G + reflectedPixel.Color.G) / 2,
                        (result.Color.B + reflectedPixel.Color.B) / 2
                        );
                    return new Pixel { Color = newColor, X = x, Y = y };
                }
                // случай без отражения
                else if (refractedPixel != null)
                {
                    // stub for mixing colors. now it always mixes 1:1
                    var newColor = Color.FromArgb(
                        (result.Color.R + refractedPixel.Color.R) / 2,
                        (result.Color.G + refractedPixel.Color.G) / 2,
                        (result.Color.B + refractedPixel.Color.B) / 2
                        );
                    return new Pixel { Color = newColor, X = x, Y = y };
                }
                else
                {
                    return new Pixel { Color = result.Color, X = x, Y = y };
                }
            }
            #endregion
        }
    }
}
