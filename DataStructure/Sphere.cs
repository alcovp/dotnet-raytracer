using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace DataStructure
{
    /// <summary>
    /// нужно выпилить класс
    /// </summary>
    public class Sphere : Entity
    {
        public XYZ Center { get; set; }
        public double Radius { get; set; }

        public Material Material { get; set; }

        private XYZ CheckForIntersection(XYZ eye_p, XYZ ray_v)
        {
            //var distance = eye_Center.ScalarProduct(ray_v) / ray_v.ScalarOfVector(); // косячная формула почему-то

            //var distance = Math.Sqrt(
            //    Math.Pow(((Center.X - eye_p.X) * ray_v.Y) - ((Center.Y - eye_p.Y) * ray_v.X), 2)
            //    + Math.Pow(((Center.Y - eye_p.Y) * ray_v.Z) - ((Center.Z - eye_p.Z) * ray_v.Y), 2)
            //    + Math.Pow(((Center.X - eye_p.X) * ray_v.Z) - ((Center.Z - eye_p.Z) * ray_v.X), 2))
            //    /
            //    Math.Sqrt(Math.Pow(ray_v.X, 2) + Math.Pow(ray_v.Y, 2) + Math.Pow(ray_v.Z, 2));

            
            // найдем точку пересечения
            var b = 2 * (ray_v.X * (eye_p.X - Center.X) + ray_v.Y * (eye_p.Y - Center.Y) + ray_v.Z * (eye_p.Z - Center.Z));
            var c = Math.Pow(eye_p.X - Center.X, 2) + Math.Pow(eye_p.Y - Center.Y, 2) + Math.Pow(eye_p.Z - Center.Z, 2) - Math.Pow(Radius, 2);
            var discriminant = Math.Pow(b, 2) - 4 * c;
            var x1 = (-b + Math.Sqrt(discriminant)) / 2;
            var x2 = (-b - Math.Sqrt(discriminant)) / 2;

            if (discriminant >= 0 && (x1 > 0 || x2 > 0))
            {
                var p1 = new XYZ { X = eye_p.X + ray_v.X * x1, Y = eye_p.Y + ray_v.Y * x1, Z = eye_p.Z + ray_v.Z * x1 };
                var p2 = new XYZ { X = eye_p.X + ray_v.X * x2, Y = eye_p.Y + ray_v.Y * x2, Z = eye_p.Z + ray_v.Z * x2 };
                // выберем точку первого пересечения (ближайшая к eye_p)
                var p1Distance = p1.Substract(eye_p).ScalarOfVector();
                var p2Distance = p2.Substract(eye_p).ScalarOfVector();
                return p1Distance < p2Distance ? p1 : p2;
            }
            else
            {
                return null;
            }
        }

        public override Result GetIntersectionResult(XYZ eye_p, XYZ ray_v, double n1, int recursion)
        {
            var normal = eye_p.Substract(Center).Normalize();
            var intersectionPoint = CheckForIntersection(eye_p, ray_v);
            if (intersectionPoint != null)
            {
                return new Result
                {
                    //stub
                    ReflectedRay = null,
                    Color = Material.Color,
                    Point = intersectionPoint,
                    Material = Material,
                    Normal = normal
                };
            }
            else
            {
                return null;
            }
        }
    }
}
