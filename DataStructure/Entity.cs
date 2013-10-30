using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace DataStructure
{
    public abstract class Entity : IIntersectable
    {
        public abstract Result GetIntersectionResult(XYZ eye_p, XYZ ray_v, double n1, int recursion);
    }
}
