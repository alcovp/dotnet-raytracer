using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataStructure
{
    public class Matrices
    {
        public static double[][] InvertMatrix(double[][] matrix)
        {
            var det = Determinant4x4(matrix);
            return new double[][]
            {
                new double[] { Determinant3x3(matrix, 0, 0) / det, -Determinant3x3(matrix, 1, 0) / det, Determinant3x3(matrix, 2, 0) / det, -Determinant3x3(matrix, 3, 0) / det},
                new double[] { -Determinant3x3(matrix, 0, 1) / det, Determinant3x3(matrix, 1, 1) / det, -Determinant3x3(matrix, 2, 1) / det, Determinant3x3(matrix, 3, 1) / det},
                new double[] { Determinant3x3(matrix, 0, 2) / det, -Determinant3x3(matrix, 1, 2) / det, Determinant3x3(matrix, 2, 2) / det, -Determinant3x3(matrix, 3, 2) / det},
                new double[] { -Determinant3x3(matrix, 0, 3) / det, Determinant3x3(matrix, 1, 3) / det, -Determinant3x3(matrix, 2, 3) / det, Determinant3x3(matrix, 3, 3) / det}
            };
        }

        private static double Determinant4x4(double[][] matrix)
        {
            return matrix[0][0] * Determinant3x3(matrix, 0, 0) - matrix[0][1] * Determinant3x3(matrix, 0, 1) + matrix[0][2] * Determinant3x3(matrix, 0, 2) - matrix[0][3] * Determinant3x3(matrix, 0, 3);
        }

        private static double Determinant3x3(double[][] matrix, int i, int j)
        {
            double[][] newMatrix = new double[][]
            {
                new double[] { 0, 0, 0 },
                new double[] { 0, 0, 0 },
                new double[] { 0, 0, 0 }
            };
            int jFlag = 0;
            int iFlag = 0;
            for (int I = 0; I < 3; I++)
            {
                jFlag = 0;
                for (int J = 0; J < 3; J++)
                {
                    if (i == I && j == J)
                    {
                        jFlag = 1;
                        iFlag = 1;
                        newMatrix[I][J] = matrix[I + iFlag][J + jFlag];
                    }
                    else if (i == I)
                    {
                        iFlag = 1;
                        newMatrix[I][J] = matrix[I + iFlag][J + jFlag];
                    }
                    else if (j == J)
                    {
                        jFlag = 1;
                        newMatrix[I][J] = matrix[I + iFlag][J + jFlag];
                    }
                    else
                    {
                        newMatrix[I][J] = matrix[I + iFlag][J + jFlag];
                    }
                }
            }

            var result = newMatrix[0][0] * newMatrix[1][1] * newMatrix[2][2] +
                newMatrix[0][1] * newMatrix[1][2] * newMatrix[2][0] + 
                newMatrix[1][0] * newMatrix[2][1] * newMatrix[0][2] -
                newMatrix[2][0] * newMatrix[1][1] * newMatrix[2][0] -
                newMatrix[0][0] * newMatrix[1][2] * newMatrix[2][1] -
                newMatrix[2][2] * newMatrix[0][1] * newMatrix[1][0];
            return result;
        }
    }
}
