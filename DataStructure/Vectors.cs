using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace DataStructure
{
    public class Pixel
    {
        public Color Color { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }

    public class XYZ
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        /// <summary>
        /// OK
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public XYZ OuterProduct(XYZ v)
        {
            return new XYZ
            {
                X = this.Y * v.Z - this.Z * v.Y,
                Y = -(this.X * v.Z - this.Z * v.X),
                Z = this.X * v.Y - this.Y * v.X
            };

            //var normThis = this.Normalize();
            //v = v.Normalize();
            //return new XYZ
            //{
            //    X = normThis.Y * v.Z - normThis.Z * v.Y,
            //    Y = -(normThis.X * v.Z - normThis.Z * v.X),
            //    Z = normThis.X * v.Y - normThis.Y * v.X
            //};
        }
        /// <summary>
        /// OK
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public XYZ Add(XYZ v)
        {
            return new XYZ
            {
                X = this.X + v.X,
                Y = this.Y + v.Y,
                Z = this.Z + v.Z
            };
        }
        /// <summary>
        /// OK
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public XYZ Substract(XYZ v)
        {
            return new XYZ
            {
                X = this.X - v.X,
                Y = this.Y - v.Y,
                Z = this.Z - v.Z
            };
        }
        /// <summary>
        /// OK
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public XYZ Product(double s)
        {
            return new XYZ
            {
                X = this.X * s,
                Y = this.Y * s,
                Z = this.Z * s
            };
        }
        /// <summary>
        /// OK
        /// </summary>
        /// <returns></returns>
        public XYZ Normalize()
        {
            double scalar = this.ScalarOfVector();
            return new XYZ
            {
                X = this.X / scalar,
                Y = this.Y / scalar,
                Z = this.Z / scalar
            };
        }
        /// <summary>
        /// OK
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public double ScalarProduct(XYZ v)
        {
            return this.X * v.X + this.Y * v.Y + this.Z * v.Z;
        }
        /// <summary>
        /// OK
        /// </summary>
        /// <returns></returns>
        public double ScalarOfVector()
        {
            return Math.Sqrt(Math.Pow(this.X, 2) + Math.Pow(this.Y, 2) + Math.Pow(this.Z, 2));
        }
        /// <summary>
        /// OK
        /// </summary>
        /// <returns></returns>
        public XYZ Negate()
        {
            return new XYZ
            {
                X = -this.X,
                Y = -this.Y,
                Z = -this.Z
            };
        }

        public XYZ Transform(double[][] matrix)
        {
            return new XYZ
            {
                X = matrix[0][0] * this.X + matrix[0][1] * this.Y + matrix[0][2] * this.Z + matrix[0][3],
                Y = matrix[1][0] * this.X + matrix[1][1] * this.Y + matrix[1][2] * this.Z + matrix[1][3],
                Z = matrix[2][0] * this.X + matrix[2][1] * this.Y + matrix[2][2] * this.Z + matrix[2][3]
            };
        }
    }
}
