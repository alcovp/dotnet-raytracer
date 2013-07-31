using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace DataStructure
{
    public class Material
    {
        public Color Color { get; set; }
        public Bitmap Texture { get; set; }
        public double Reflectivity { get; set; }
        public double Refractivity { get; set; }
        public double RefractiveIndex { get; set; }
    }
}
