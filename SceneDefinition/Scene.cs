using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataStructure;
using System.Drawing;

namespace SceneDefinition
{
    public class Scene
    {
        public static IList<IIntersectable> objects = new List<IIntersectable>();
        public static IList<Light> lights = new List<Light>();

        public static void NewScene()
        {
            objects.Clear();
            lights.Clear();
        }

        public static void AddQuadricSurface(double[][] quadricFormMatrix, double[] linearFormVector, double absoluteTerm, Material material)
        {
            objects.Add(new QuadricSurfaceOld
            {
                QuadricFormMatrix = quadricFormMatrix,
                LinearFormVector = linearFormVector,
                AbsoluteTerm = absoluteTerm, 
                Material = material
            }); 
        }

        public static void AddBoundingBox(double left_v, double right, double bottom, double top, double face, double back, Material material)
        {
            objects.Add(new BoundingBox
            {
                Top = top,
                Bottom = bottom,
                Left = left_v,
                Right = right,
                Face = face,
                Back = back,
                Material = material
            });
        }

        public static void AddPlain(XYZ normal_v, double d, Material material)
        {
            objects.Add(new Plain
            {
                Normal = normal_v,
                D = d,
                Material = material
            });
        }

        public static void AddSphere(XYZ center, double radius, Material material)
        {
            objects.Add(new Sphere
            {
                Center = center,
                Radius = radius,
                Material = material
            });
        }

        public static void AddSphere2(XYZ center, double radius, Material material)
        {
            double[][] quadricFormMatrix = new double[][]
            {
                new double[] { 1, 0, 0 },
                new double[] { 0, 1, 0 },
                new double[] { 0, 0, 1 }
            };
            double[] linearFormVector = new double[] { 0, 0, 0 };
            double absoluteTerm = -1;

            double[][] transformation = new double[][]
            {
                new double[] { radius, 0, 0, center.X},
                new double[] { 0, radius, 0, center.Y},
                new double[] { 0, 0, radius, center.Z},
                new double[] { 0, 0, 0, 1}
            };

            objects.Add(new QuadricSurface
            {
                Material = material,
                AbsoluteTerm = absoluteTerm,
                LinearFormVector = linearFormVector,
                QuadricFormMatrix = quadricFormMatrix,
                Transformation = transformation,
                Inverse = Matrices.InvertMatrix(transformation)
            });
        }

        public static void AddLight(XYZ center, double weight, Color color, double ambient)
        {
            lights.Add(new Light
            {
                Center = center,
                Weight = weight,
                Color = color,
                Abmient = ambient
            });
        }
    }
}
