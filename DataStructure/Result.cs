using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace DataStructure
{
    public class Result
    {
        public XYZ Point { get; set; }
        public XYZ ReflectedRay { get; set; }
        public XYZ RefractedRay { get; set; }
        public XYZ Normal { get; set; }
        public Material Material { get; set; }
        public Color Color { get; set; }
        public IContainer Container { get; set; }
    }
}
