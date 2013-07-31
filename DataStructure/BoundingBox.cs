using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace DataStructure
{
    public class BoundingBox : Entity
    {
        public double Top { get; set; } // equation: y = Top, normal: (0, -1, 0)
        public double Bottom { get; set; } // y = Bottom
        public double Left { get; set; } // x = Left
        public double Right { get; set; }
        public double Face { get; set; }
        public double Back { get; set; }
        public virtual Material Material { get; set; }

        public override Result GetIntersectionResult(XYZ eye_p, XYZ ray_v, double n1)
        {
            XYZ intersectionPoint = new XYZ { X = 0, Y = 0, Z = 0 };
            XYZ normal_v = null;
            // for back
            normal_v = new XYZ { X = 0, Y = 0, Z = -1 };
            var t = -(eye_p.ScalarProduct(normal_v) + Back) / (ray_v.ScalarProduct(normal_v));
            intersectionPoint = eye_p.Add(ray_v.Product(t));

            // stub
            return new Result
            {
                ReflectedRay = null,
                Color = Material.Color,
                Point = intersectionPoint,
                Material = Material
            };
        }
    }
}
