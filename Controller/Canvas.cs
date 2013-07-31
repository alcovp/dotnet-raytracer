using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataStructure;
using Solver;
using System.Drawing;
using System.Threading.Tasks;

namespace Controller
{
    public class Canvas
    {
        private static Queue<Pixel> pixelQueue = new Queue<Pixel>();
        private static Pixel[][] Pixels;
        private static XYZ[][] rays;
        public static int allPixelsCount = 0;
        public static int donePixelsCount = 0;

        public static void CreateFrame(
            int xResolution, int yResolution,
            double viewAngle, XYZ eye_p,
            XYZ view_v, XYZ up_v, int antialiasing, int recursion)
        {
            allPixelsCount = xResolution * yResolution;
            donePixelsCount = 0;
            Pixels = new Pixel[xResolution][];
            // объявим двумерный массив под
            //лучи для каждого пикселя
            for (int i = 0; i < xResolution; i++)
            {
                Pixels[i] = new Pixel[yResolution];
            }


            rays = new XYZ[xResolution][];
            // объявим двумерный массив под лучи
            //для каждого пикселя
            for (int i = 0; i < xResolution; i++)
            {
                rays[i] = new XYZ[yResolution];
            }
            // найдем вектор "влево"
            var left_v = view_v.
                OuterProduct(up_v).
                Normalize();
            // ширина экрана камеры
            var b = Math.Tan((viewAngle / 2) *
                Math.PI / 180) * 2;
            // размер одного пикселя на экране
            //камеры
            var pixelSize = b / xResolution;
            // высота экрана камеры
            var a = yResolution * pixelSize;
            // для начала найдем луч, 
            //проходящий через левый-верхний
            //пиксель экрана камеры
            XYZ zeroRay = view_v
                        .Add(left_v.
                        Product(b / 2))
                        .Add(up_v.
                        Product(a / 2))
                        .Substract(left_v.
                        Product(pixelSize / 2))
                        .Substract(up_v.
                        Product(pixelSize / 2));
            // заполнение матрицы лучей 
            //происходит по столбцам
            for (int y = 0; y < yResolution; y++)
            {
                XYZ row = zeroRay
                    .Substract(up_v.
                    Product(pixelSize * y));
                for (int x = 0;
                    x < xResolution;
                    x++)
                {
                    rays[x][y] =
                        row.Substract(
                        left_v.Product(
                        pixelSize * x)).
                        Normalize();
                }
            }

            Parallel.For(0, rays[0].Length,
                delegate(int y)
                {
                    Parallel.For(0, rays.Length,
                        delegate(int x)
                        {
                            // случай без сглаживания
                            if (antialiasing == 0)
                            {
                                var pixel = Computer.
                                    ComputePixelColor(x, y,
                                    eye_p, rays[x][y],
                                    new List<IContainer>(),
                                    1, recursion);
                                Pixels[x][y] = pixel;
                            }
                            // сглаживание с помощью 
                            //размножения луча на 4 
                            else if (antialiasing == 1)
                            {
                                var shift =
                                    pixelSize / 4;
                                var r1 = rays[x][y].Add(left_v.Product(shift)).Normalize().Add(up_v.Product(shift)).Normalize();
                                var r2 = rays[x][y].Substract(left_v.Product(shift)).Normalize().Add(up_v.Product(shift)).Normalize();
                                var r3 = rays[x][y].Substract(left_v.Product(shift)).Normalize().Substract(up_v.Product(shift)).Normalize();
                                var r4 = rays[x][y].Add(left_v.Product(shift)).Normalize().Substract(up_v.Product(shift)).Normalize();
                                var p1 = Computer.ComputePixelColor(x, y, eye_p, r1, new List<IContainer>(), 1, recursion);
                                var p2 = Computer.ComputePixelColor(x, y, eye_p, r2, new List<IContainer>(), 1, recursion);
                                var p3 = Computer.ComputePixelColor(x, y, eye_p, r3, new List<IContainer>(), 1, recursion);
                                var p4 = Computer.ComputePixelColor(x, y, eye_p, r4, new List<IContainer>(), 1, recursion);
                                // смешивание цветов лучей
                                var pixel = new Pixel
                                {
                                    X = x,
                                    Y = y,
                                    Color = Color.FromArgb(
                                        (p1.Color.R + p2.Color.R + p3.Color.R + p4.Color.R) / 4,
                                        (p1.Color.G + p2.Color.G + p3.Color.G + p4.Color.G) / 4,
                                        (p1.Color.B + p2.Color.B + p3.Color.B + p4.Color.B) / 4
                                        )
                                };
                                Pixels[x][y] = pixel;
                            }
                            // сглаживание с помощью размножения луча на 9
                            else if (antialiasing == 2)
                            {
                                var shift = pixelSize / 3;
                                var r1 = rays[x][y].Add(left_v.Product(shift)).Normalize().Add(up_v.Product(shift)).Normalize();
                                var r2 = rays[x][y].Add(up_v.Product(shift)).Normalize();
                                var r3 = rays[x][y].Substract(left_v.Product(shift)).Normalize().Add(up_v.Product(shift)).Normalize();
                                var r4 = rays[x][y].Add(left_v.Product(shift)).Normalize();
                                var r5 = rays[x][y];
                                var r6 = rays[x][y].Substract(left_v.Product(shift)).Normalize();
                                var r7 = rays[x][y].Add(left_v.Product(shift)).Normalize().Substract(up_v.Product(shift)).Normalize();
                                var r8 = rays[x][y].Substract(up_v.Product(shift)).Normalize();
                                var r9 = rays[x][y].Substract(left_v.Product(shift)).Normalize().Substract(up_v.Product(shift)).Normalize();
                                var p1 = Computer.ComputePixelColor(x, y, eye_p, r1, new List<IContainer>(), 1, recursion);
                                var p2 = Computer.ComputePixelColor(x, y, eye_p, r2, new List<IContainer>(), 1, recursion);
                                var p3 = Computer.ComputePixelColor(x, y, eye_p, r3, new List<IContainer>(), 1, recursion);
                                var p4 = Computer.ComputePixelColor(x, y, eye_p, r4, new List<IContainer>(), 1, recursion);
                                var p5 = Computer.ComputePixelColor(x, y, eye_p, r5, new List<IContainer>(), 1, recursion);
                                var p6 = Computer.ComputePixelColor(x, y, eye_p, r6, new List<IContainer>(), 1, recursion);
                                var p7 = Computer.ComputePixelColor(x, y, eye_p, r7, new List<IContainer>(), 1, recursion);
                                var p8 = Computer.ComputePixelColor(x, y, eye_p, r8, new List<IContainer>(), 1, recursion);
                                var p9 = Computer.ComputePixelColor(x, y, eye_p, r9, new List<IContainer>(), 1, recursion);
                                // смешивание цветов лучей
                                var pixel = new Pixel
                                {
                                    X = x,
                                    Y = y,
                                    Color = Color.FromArgb(
                                        (p1.Color.R + p2.Color.R + p3.Color.R + p4.Color.R + p5.Color.R + p6.Color.R + p7.Color.R + p8.Color.R + p9.Color.R) / 9,
                                        (p1.Color.G + p2.Color.G + p3.Color.G + p4.Color.G + p5.Color.G + p6.Color.G + p7.Color.G + p8.Color.G + p9.Color.G) / 9,
                                        (p1.Color.B + p2.Color.B + p3.Color.B + p4.Color.B + p5.Color.B + p6.Color.B + p7.Color.B + p8.Color.B + p9.Color.B) / 9
                                        )
                                };
                                Pixels[x][y] = pixel;
                            }
                            donePixelsCount++;
                        });
                });
            allPixelsCount = 0;
        }

        public static Pixel[][] GetComputedPixels()
        {
            return Pixels;
        }
    }
}
