using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Tester
{
    public class RayTracer
    {
        private IDictionary<int, int> points = new Dictionary<int, int>();

        public void AddPoint(int x, int y)
        {
            points.Add(x, y);
        }

        public IDictionary<int, int> GetPoints()
        {
            return points;
        }

        public void TraceRay(Bitmap image)
        {
            foreach (var point in points)
            {
                image.SetPixel(point.Key, point.Value, Color.White);
            }
        }
    }
}
