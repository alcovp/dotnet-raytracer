using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataStructure
{
    public class QuadricSurfaceOld : Entity, IContainer
    {
        public double[][] QuadricFormMatrix { get; set; }
        public double[] LinearFormVector { get; set; }
        public double AbsoluteTerm { get; set; }
        public virtual Material Material { get; set; }

        public override Result GetIntersectionResult(XYZ eye_p, XYZ ray_v, double n1, int recursion)
        {
            //test 
            var a11 = QuadricFormMatrix[0][0];
            var a12 = QuadricFormMatrix[0][1];
            var a13 = QuadricFormMatrix[0][2];
            var a22 = QuadricFormMatrix[1][1];
            var a23 = QuadricFormMatrix[1][2];
            var a33 = QuadricFormMatrix[2][2];
            var b1 = LinearFormVector[0];
            var b2 = LinearFormVector[1];
            var b3 = LinearFormVector[2];
            // a*x^2 + 2*b*x + c = 0
            // возможна оптимизация: считать только 1 раз все это для одной итерации
            var a = Phi(ray_v.X, ray_v.Y, ray_v.Z);
            var f1 = F1(eye_p.X, eye_p.Y, eye_p.Z);
            var f2 = F2(eye_p.X, eye_p.Y, eye_p.Z);
            var f3 = F3(eye_p.X, eye_p.Y, eye_p.Z);
            var b = f1 * ray_v.X
                + f2 * ray_v.Y
                + f3 * ray_v.Z;
            var c = F(eye_p.X, eye_p.Y, eye_p.Z);
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
                        p1 = new XYZ { X = eye_p.X + ray_v.X * x1, Y = eye_p.Y + ray_v.Y * x1, Z = eye_p.Z + ray_v.Z * x1 };
                        p2 = new XYZ { X = eye_p.X + ray_v.X * x2, Y = eye_p.Y + ray_v.Y * x2, Z = eye_p.Z + ray_v.Z * x2 };
                        // выберем точку первого пересечения (ближайшая к eye_p)
                        var p1Distance = p1.Substract(eye_p).ScalarOfVector();
                        var p2Distance = p2.Substract(eye_p).ScalarOfVector();
                        intersectionPoint = p1Distance < p2Distance ? p1 : p2;
                    }
                    else
                    {
                        if (x1 > 0)
                        {
                            intersectionPoint = new XYZ { X = eye_p.X + ray_v.X * x1, Y = eye_p.Y + ray_v.Y * x1, Z = eye_p.Z + ray_v.Z * x1 };
                        }
                        if (x2 > 0)
                        {
                            intersectionPoint = new XYZ { X = eye_p.X + ray_v.X * x2, Y = eye_p.Y + ray_v.Y * x2, Z = eye_p.Z + ray_v.Z * x2 };
                        }
                    }
                    XYZ normal = null;
                    XYZ reflectedRay = null;
                    XYZ refractedRay = null;
                    // найдем нормаль в точке пересечения
                    if (Material.Reflectivity > 0 || Material.Refractivity > 0)
                    {
                        normal = new XYZ { X = f1 * -2, Y = f2 * -2, Z = 1 }.Normalize();
                        // найдем направление отраженного луча
                        if (Material.Reflectivity > 0)
                        {
                            reflectedRay = ray_v.Substract(normal.Product(2).Product(ray_v.ScalarProduct(normal))).Normalize();
                        }
                        // найдем направление преломленного луча
                        if (Material.Refractivity > 0)
                        {
                            var n2 = Material.RefractiveIndex;
                            if (n1 == n2)
                            {
                                refractedRay = ray_v;
                            }
                            else
                            {
                                var cosine = ray_v.ScalarProduct(normal);
                                var sineSqr = Math.Pow(n1 / n2, 2) * (1 - Math.Pow(cosine, 2));
                                if (Math.Sqrt(sineSqr) <= n2 / n1)
                                {
                                    refractedRay = ray_v.Product(n1 / n2).Add(normal.Product((n1 / n2) * cosine + Math.Sqrt(1 - sineSqr))).Normalize();
                                }
                                else
                                {

                                }
                            }
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
