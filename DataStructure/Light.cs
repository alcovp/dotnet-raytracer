using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace DataStructure
{
    public class Light
    {
        public Color Color { get; set; }
        public XYZ Center { get; set; }
        public double Weight { get; set; }
        public double Abmient { get; set; }

        public double GetDiffuse(XYZ point, XYZ normal, XYZ eye_p)
        {
            // check for distance 
            var distance = Center.Substract(point).ScalarOfVector();
            if (Weight - distance <= 0)
            {
                return 0;
            }
            else
            {
                if (normal != null)
                {
                    var eye_point = eye_p.Substract(point).Normalize();
                    var cosine = normal.OuterProduct(eye_point).OuterProduct(normal).ScalarProduct(eye_point); // чет не пашет, как надо
                    if (cosine > 1 || cosine < 0)
                    {

                    }
                }
                var lightFactor = /*Weight / Math.Pow(distance, 2);*/ (Weight - distance) * 1 / Weight;
                if (lightFactor > 1)
                {
                    lightFactor = 1;
                }
                return lightFactor;
            }
        }

        public double GetSpecular(XYZ point, XYZ reflectedRay, double exponent)
        {
            // check for distance 
            // TODO не работает
            var distance = Center.Substract(point).ScalarOfVector();
            if (reflectedRay == null)
            {
                return 0;
            }
            else
            {
                var lightToPoint = Center.Substract(point).Normalize();
                //var lightFactor = lightToPoint.ScalarProduct(reflectedRay);
                var lightFactor = Math.Pow(lightToPoint.ScalarProduct(reflectedRay), exponent);

                if (lightFactor > 1)
                {
                    lightFactor = 1;
                }
                if (lightFactor < 0)
                {
                    lightFactor = 0;
                }
                return lightFactor;
            }
        }

        public Color ModifyColor(Color color, XYZ point)
        {
            // check for distance 
            var distance = Center.Substract(point).ScalarOfVector();
            if (Weight - distance <= 0)
            {
                return Color.FromArgb(0, 0, 0);
            }
            else
            {
                var lightFactor = /*Weight / Math.Pow(distance, 2);*/ (Weight - distance) / Weight;
                if (lightFactor > 1)
                {
                    lightFactor = 1;
                }
                var lightRedFactor = Color.R / 255;
                var lightGreenFactor = Color.G / 255;
                var lightBlueFactor = Color.B / 255;
                var newColor = Color.FromArgb(
                    (int)(color.R * lightFactor * lightRedFactor),
                    (int)(color.G * lightFactor * lightGreenFactor),
                    (int)(color.B * lightFactor * lightBlueFactor)
                    );
                return newColor;
            }
        }
    }
}
