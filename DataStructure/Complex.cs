using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataStructure
{
    public class Complex
    {
        public double A { get; set; }
        public double B { get; set; }

        public Complex Division(Complex z)
        {
            return new Complex
            {
                A = (this.A * z.A + this.B * z.B) / (z.A * z.A + z.B * z.B),
                B = (this.B * z.A - this.A * z.B) / (z.A * z.A + z.B * z.B)
            };
        }
    }
}
