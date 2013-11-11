using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataStructure
{
    public class QuadricSurface : Entity, IContainer
    {
        public double[][] QuadricFormMatrix { get; set; }
        public double[] LinearFormVector { get; set; }
        public double AbsoluteTerm { get; set; }
        public virtual Material Material { get; set; }
        public double[][] Transformation { get; set; }
        public double[][] Inverse { get; set; }

        public override Result GetIntersectionResult(XYZ eye_p, XYZ ray_v, double n1, int recursion)
        {
            var eye = eye_p.Transform(Inverse);
            var ray = ray_v;
            // a*x^2 + 2*b*x + c = 0
            // возможна оптимизация: считать только 1 раз все это для одной итерации
            var a = Phi(ray.X, ray.Y, ray.Z);
            var f1 = F1(eye.X, eye.Y, eye.Z);
            var f2 = F2(eye.X, eye.Y, eye.Z);
            var f3 = F3(eye.X, eye.Y, eye.Z);
            var b = f1 * ray.X
                + f2 * ray.Y
                + f3 * ray.Z;
            var c = F(eye.X, eye.Y, eye.Z);
            var discriminant = Math.Pow(b * 2, 2) - 4 * a * c;
            double x1;
            double x2;
            if (discriminant >= 0)
            {
                x1 = (-2 * b + Math.Sqrt(discriminant)) / (2 * a);
                x2 = (-2 * b - Math.Sqrt(discriminant)) / (2 * a);
                if (x1 > 0 || x2 > 0)
                {
                    XYZ p1 = null;
                    XYZ p2 = null;
                    XYZ intersectionPoint = null;
                    if (x1 > 0 && x2 > 0)
                    {
                        // переписать это в векторных операциях XYZ
                        p1 = new XYZ { X = eye.X + ray.X * x1, Y = eye.Y + ray.Y * x1, Z = eye.Z + ray.Z * x1 };
                        p2 = new XYZ { X = eye.X + ray.X * x2, Y = eye.Y + ray.Y * x2, Z = eye.Z + ray.Z * x2 };
                        // выберем точку первого пересечения (ближайшая к eye_p)
                        var p1Distance = p1.Substract(eye).ScalarOfVector();
                        var p2Distance = p2.Substract(eye).ScalarOfVector();
                        intersectionPoint = p1Distance < p2Distance ? p1 : p2;
                    }
                    else
                    {
                        if (x1 > 0)
                        {
                            intersectionPoint = new XYZ { X = eye.X + ray.X * x1, Y = eye.Y + ray.Y * x1, Z = eye.Z + ray.Z * x1 };
                        }
                        if (x2 > 0)
                        {
                            intersectionPoint = new XYZ { X = eye.X + ray.X * x2, Y = eye.Y + ray.Y * x2, Z = eye.Z + ray.Z * x2 };
                        }
                    }
                    XYZ normal = null;
                    XYZ reflectedRay = null;
                    XYZ refractedRay = null;
                    // найдем нормаль в точке пересечения
                    normal = new XYZ { X = F1(intersectionPoint.X, intersectionPoint.Y, intersectionPoint.Z) * 2, Y = F2(intersectionPoint.X, intersectionPoint.Y, intersectionPoint.Z) * 2, Z = F3(intersectionPoint.X, intersectionPoint.Y, intersectionPoint.Z) * 2 }.Normalize();

                    // найдем направление отраженного луча
                    reflectedRay = ray.Substract(normal.Product(2).Product(ray.ScalarProduct(normal)));
                    if (Material.Reflectivity > 0 || Material.Refractivity > 0)
                    {
                        //normal = new XYZ { X = F1(intersectionPoint.X, intersectionPoint.Y, intersectionPoint.Z) * -2, Y = F2(intersectionPoint.X, intersectionPoint.Y, intersectionPoint.Z) * -2, Z = 1 }.Normalize();
                        
                        //normal = new XYZ { X = f1 * -2, Y = f2 * -2, Z = 1 }.Normalize();
                        // найдем направление преломленного луча
                        if (Material.Refractivity > 0)
                        {
                            var n2 = Material.RefractiveIndex;
                            if (n1 == n2)
                            {
                                refractedRay = ray;
                            }
                            else
                            {
                                var cosine = ray.ScalarProduct(normal);
                                var sineSqr = Math.Pow(n1 / n2, 2) * (1 - Math.Pow(cosine, 2));
                                if (Math.Sqrt(sineSqr) <= n2 / n1)
                                {
                                    refractedRay = ray.Product(n1 / n2).Add(normal.Product((n1 / n2) * cosine + Math.Sqrt(1 + sineSqr))).Normalize();
                                }
                                else
                                {

                                }
                            }
                            //else
                            //{
                            //    var I = ray.Negate();
                            //    var nT = n1 / n2;
                            //    var sqrt = 1 - nT * nT * (1 - Math.Pow(normal.ScalarProduct(I), 2));
                            //    if (sqrt < 0)
                            //    {

                            //    }
                            //    else
                            //    {
                            //        refractedRay = normal.Product(nT * (normal.ScalarProduct(I)) - Math.Sqrt(sqrt)).Substract(I.Product(nT)).Negate().Normalize();
                            //    }
                            //}
                        }
                    }
                    var color = Material.Color;
                    // найдем цвет в точке пересечения
                    //if (Material.Texture != null)
                    //{
                    //    Complex z0 = null;
                    //    Complex z1 = null;
                    //    Complex z = null;
                    //    if (intersectionPoint.Z == 0)
                    //    {
                    //        z0 = new Complex { A = 0, B = 0 };
                    //        z1 = new Complex { A = 1, B = 0 };
                    //    }
                    //    else
                    //    {
                    //        z0 = new Complex { A = intersectionPoint.Z - 4, B = 0 };
                    //        z1 = new Complex { A = intersectionPoint.X, B = intersectionPoint.Y };
                    //    }
                    //    z = z1.Division(z0);
                    //    color = Material.Texture.GetPixel((int)(z.A + Material.Texture.Width / 2), (int)(z.B + Material.Texture.Height / 2));
                    //}

                    intersectionPoint = intersectionPoint.Transform(Transformation);
                    return new Result
                    {
                        Point = intersectionPoint,
                        Color = color,
                        Material = Material,
                        ReflectedRay = reflectedRay,
                        RefractedRay = refractedRay,
                        Normal = normal,
                        Container = this
                    };
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        private double F(double x, double y, double z)
        {
            return QuadricFormMatrix[0][0] * Math.Pow(x, 2) + 2 * QuadricFormMatrix[0][1] * x * y
                + QuadricFormMatrix[1][1] * Math.Pow(y, 2) + 2 * QuadricFormMatrix[0][2] * x * z
                + QuadricFormMatrix[2][2] * Math.Pow(z, 2) + 2 * QuadricFormMatrix[1][2] * y * z
                + AbsoluteTerm;
        }

        private double Phi(double x, double y, double z)
        {
            return QuadricFormMatrix[0][0] * Math.Pow(x, 2) + 2 * QuadricFormMatrix[0][1] * x * y
                + QuadricFormMatrix[1][1] * Math.Pow(y, 2) + 2 * QuadricFormMatrix[0][2] * x * z
                + QuadricFormMatrix[2][2] * Math.Pow(z, 2) + 2 * QuadricFormMatrix[1][2] * y * z;
        }

        private double F1(double x, double y, double z)
        {
            return QuadricFormMatrix[0][0] * x
                + QuadricFormMatrix[0][1] * y
                + QuadricFormMatrix[0][2] * z
                + LinearFormVector[0];
        }

        private double F2(double x, double y, double z)
        {
            return QuadricFormMatrix[1][0] * x
                + QuadricFormMatrix[1][1] * y
                + QuadricFormMatrix[1][2] * z
                + LinearFormVector[1];
        }

        private double F3(double x, double y, double z)
        {
            return QuadricFormMatrix[2][0] * x
                + QuadricFormMatrix[2][1] * y
                + QuadricFormMatrix[2][2] * z
                + LinearFormVector[2];
        }

        public double GetRefractionIndex()
        {
            return Material.RefractiveIndex;
        }
    }
}
