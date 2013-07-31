using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataStructure
{
    public class Plain : Entity
    {
        public XYZ Normal { get; set; }
        public double D { get; set; }
        public virtual Material Material { get; set; }

        public override Result GetIntersectionResult(XYZ eye_p, XYZ ray_v, double n1)
        {
            XYZ intersectionPoint = null;
            var t = -(eye_p.ScalarProduct(Normal) + D) / (ray_v.ScalarProduct(Normal));
            intersectionPoint = eye_p.Add(ray_v.Product(t));

            if (double.IsInfinity(t) || t <= 0)
            {
                return null;
            }

            XYZ reflectedRay = null;
            XYZ refractedRay = null;
            var left = ray_v.OuterProduct(Normal);
            if (Material.Reflectivity > 0)
            {
                reflectedRay = ray_v.Substract(Normal.Product(2).Product(ray_v.ScalarProduct(Normal))).Normalize();
            }
            if (Material.Refractivity > 0)
            {
                var n2 = Material.RefractiveIndex;
                if (n1 == n2)
                {
                    refractedRay = ray_v;
                }
                else
                {
                    var cosine = ray_v.ScalarProduct(Normal);
                    var sineSqr = Math.Pow(n1 / n2, 2) * (1 - Math.Pow(cosine, 2));
                    if (Math.Sqrt(sineSqr) <= n2 / n1)
                    {
                        refractedRay = ray_v.Product(n1 / n2).Add(Normal.Product((n1 / n2) * cosine + Math.Sqrt(1 - sineSqr))).Normalize();
                    }
                    else
                    {

                    }
                }
            }

            return new Result
            {
                ReflectedRay = reflectedRay,
                RefractedRay = refractedRay,
                Normal = Normal,
                Color = Material.Color,
                Point = intersectionPoint,
                Material = Material
            };
        }
    }
}
