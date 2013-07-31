using DataStructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMath
{
    class Program
    {
        static void Main(string[] args)
        {
            XYZ v1 = new XYZ() { X = 0.55, Y = 4.97, Z = -17.2 };
            XYZ v2 = new XYZ() { X = -2.57, Y = 12.97, Z = -0.16 };
            double s1 = 0.88;
            var outer12 = v1.OuterProduct(v2);
            var outer21 = v2.OuterProduct(v1);
            var norm1 = v1.Normalize();
            var norm2 = v2.Normalize();
            var scalar12 = v1.ScalarProduct(v2);//65
            var scalar21 = v2.ScalarProduct(v1); //65
            var scalar1 = v1.ScalarOfVector(); // 17
            var scalar2 = v2.ScalarOfVector(); // 13
        }
    }
}
