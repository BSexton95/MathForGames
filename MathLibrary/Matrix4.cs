using System;
using System.Collections.Generic;
using System.Text;

namespace MathLibrary
{
    public struct Matrix4
    {
        public float M00, M01, M02, M03, M10, M11, M12, M13, M20, M21, M22, M23, M30, M31, M32, M33;

        public Matrix4(float m00, float m01, float m02, float m03,
                       float m10, float m11, float m12, float m13,
                       float m20, float m21, float m22, float m23,
                       float m30, float m31, float m32, float m33)
        {
            M00 = m00; M01 = m01; M02 = m02; M03 = m03;
            M10 = m10; M11 = m11; M12 = m12; M13 = m13;
            M20 = m20; M21 = m21; M22 = m22; M23 = m23;
            M30 = m30; M31 = m31; M32 = m32; M33 = m33;
        }

        public static Matrix4 Identity
        {
            get
            {
                return new Matrix4(1, 0, 0, 0,
                                   0, 1, 0, 0,
                                   0, 0, 1, 0,
                                   0, 0, 0, 1);
            }
        }


        /// <summary>
        /// Creates a new matrix that has been rotated by the given value in radians
        /// </summary>
        /// <param name="radians">The result of the rotation</param>
        /// <returns></returns>
        public static Matrix4 CreateRotationZ(float radians)
        {
            return new Matrix4((float)Math.Cos(radians), -(float)Math.Sin(radians), 0, 0,
                               (float)Math.Sin(radians), (float)Math.Cos(radians), 0, 0,
                                0, 0, 1, 0,
                                0, 0, 0, 1);


        }

        public static Matrix4 CreateRotationY(float radians)
        {
            return new Matrix4((float)Math.Cos(radians), 0, (float)Math.Sin(radians), 0,
                                0, 1, 0, 0,
                                -(float)Math.Sin(radians), 0, (float)Math.Cos(radians), 0,
                                0, 0, 0, 1);
        }

        public static Matrix4 CreateRotationX(float radians)
        {
            return new Matrix4(1, 0, 0, 0,
                               0, (float)Math.Cos(radians), -(float)Math.Sin(radians), 0,
                               0, (float)Math.Sin(radians), (float)Math.Cos(radians), 0,
                               0, 0, 0, 1);
        }

        /// <summary>
        /// Creates a new matrix that has been translated by the given value
        /// </summary>
        /// <param name = "x" >The x position of the new matrix</param>
        /// <param name = "y" >The y position of the new matrix</param>
        /// <returns></returns>
        public static Matrix4 CreateTranslation(float x, float y, float z)
        {
            return new Matrix4(1, 0, 0, x,
                               0, 1, 0, y,
                               0, 0, 1, z,
                               0, 0, 0, 1);
        }

        /// <summary>
        /// Creates a new matrix that has been translated by the given value
        /// </summary>
        /// <param name = "x" >The x position of the new matrix</param>
        /// <param name = "y" >The y position of the new matrix</param>
        /// <returns></returns>
        public static Matrix4 CreateTranslation(Vector3 vector)
        {
            return new Matrix4(1, 0, 0, vector.X,
                               0, 1, 0, vector.Y,
                               0, 0, 1, vector.Z,
                               0, 0, 0, 1);
        }

        /// <summary>
        /// Creates a new matrix that has been scaled by the given value
        /// </summary>
        /// <param name="x">The value to use to scale the matrix in the x axis</param>
        /// <param name="y">The value to use to scale the matrix in the y axis</param>
        /// <returns>The result of the scale</returns>
        public static Matrix4 CreateScale(float x, float y, float z)
        {
            return new Matrix4(x, 0, 0, 0,
                               0, y, 0, 0,
                               0, 0, z, 0,
                               0, 0, 0, 1);
        }

        /// <summary>
        /// Adds two matrices together
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        public static Matrix4 operator +(Matrix4 lhs, Matrix4 rhs)
        {
            return new Matrix4(lhs.M00 + rhs.M00, lhs.M01 + rhs.M01, lhs.M02 + rhs.M02, lhs.M03 + rhs.M03,
                               lhs.M10 + rhs.M10, lhs.M11 + rhs.M11, lhs.M12 + rhs.M12, lhs.M13 + rhs.M13,
                               lhs.M20 + rhs.M20, lhs.M21 + rhs.M21, lhs.M22 + rhs.M22, lhs.M23 + rhs.M23,
                               lhs.M30 + rhs.M30, lhs.M31 + rhs.M31, lhs.M32 + rhs.M32, lhs.M33 + rhs.M33);
        }

        /// <summary>
        /// Subtracts two matrices
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        public static Matrix4 operator -(Matrix4 lhs, Matrix4 rhs)
        {
            return new Matrix4(lhs.M00 - rhs.M00, lhs.M01 - rhs.M01, lhs.M02 - rhs.M02, lhs.M03 - rhs.M03,
                               lhs.M10 - rhs.M10, lhs.M11 - rhs.M11, lhs.M12 - rhs.M12, lhs.M13 - rhs.M13,
                               lhs.M20 - rhs.M20, lhs.M21 - rhs.M21, lhs.M22 - rhs.M22, lhs.M23 - rhs.M23,
                               lhs.M30 - rhs.M30, lhs.M31 - rhs.M31, lhs.M32 - rhs.M32, lhs.M33 - rhs.M33);
        }

        /// <summary>
        /// Multiplys two matrices
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        public static Matrix4 operator *(Matrix4 lhs, Matrix4 rhs)
        {
            Matrix4 newMatrix4;

            //Row1, Column1
            newMatrix4.M00 = (lhs.M00 * rhs.M00) + (lhs.M01 * rhs.M10) + (lhs.M02 * rhs.M20) + (lhs.M03 * rhs.M30);
            //Row1, Column2
            newMatrix4.M01 = (lhs.M00 * rhs.M01) + (lhs.M01 * rhs.M11) + (lhs.M02 * rhs.M21) + (lhs.M03 * rhs.M31);
            //Row1, Column3
            newMatrix4.M02 = (lhs.M00 * rhs.M02) + (lhs.M01 * rhs.M12) + (lhs.M02 * rhs.M22) + (lhs.M03 * rhs.M32);
            //Row1, Column4
            newMatrix4.M03 = (lhs.M00 * rhs.M03) + (lhs.M01 * rhs.M13) + (lhs.M02 * rhs.M23) + (lhs.M03 * rhs.M33);
            //Row2, Column1
            newMatrix4.M10 = (lhs.M10 * rhs.M00) + (lhs.M11 * rhs.M10) + (lhs.M12 * rhs.M20) + (lhs.M13 * rhs.M30);
            //Row2, Column2
            newMatrix4.M11 = (lhs.M10 * rhs.M01) + (lhs.M11 * rhs.M11) + (lhs.M12 * rhs.M21) + (lhs.M13 * rhs.M31);
            //Row2, Column3
            newMatrix4.M12 = (lhs.M10 * rhs.M02) + (lhs.M11 * rhs.M12) + (lhs.M12 * rhs.M22) + (lhs.M13 * rhs.M32);
            //Row3, Column4
            newMatrix4.M13 = (lhs.M10 * rhs.M03) + (lhs.M11 * rhs.M13) + (lhs.M12 * rhs.M23) + (lhs.M13 * rhs.M33);
            //Row3, Column1
            newMatrix4.M20 = (lhs.M20 * rhs.M00) + (lhs.M21 * rhs.M10) + (lhs.M22 * rhs.M20) + (lhs.M23 * rhs.M30);
            //Row3, Column2
            newMatrix4.M21 = (lhs.M20 * rhs.M01) + (lhs.M21 * rhs.M11) + (lhs.M22 * rhs.M21) + (lhs.M23 * rhs.M31);
            //Row3, Column3
            newMatrix4.M22 = (lhs.M20 * rhs.M02) + (lhs.M21 * rhs.M12) + (lhs.M22 * rhs.M22) + (lhs.M23 * rhs.M32);
            //Row3, Column4
            newMatrix4.M23 = (lhs.M20 * rhs.M03) + (lhs.M21 * rhs.M13) + (lhs.M22 * rhs.M23) + (lhs.M23 * rhs.M33);
            //Row4, Column1
            newMatrix4.M30 = (lhs.M30 * rhs.M00) + (lhs.M31 * rhs.M10) + (lhs.M32 * rhs.M20) + (lhs.M33 * rhs.M30);
            //Row4, Column2
            newMatrix4.M31 = (lhs.M30 * rhs.M01) + (lhs.M31 * rhs.M11) + (lhs.M32 * rhs.M21) + (lhs.M33 * rhs.M31);
            //Row4, Column3
            newMatrix4.M32 = (lhs.M30 * rhs.M02) + (lhs.M31 * rhs.M12) + (lhs.M32 * rhs.M22) + (lhs.M33 * rhs.M32);
            //Row4, Column4
            newMatrix4.M33 = (lhs.M30 * rhs.M03) + (lhs.M31 * rhs.M13) + (lhs.M32 * rhs.M23) + (lhs.M33 * rhs.M33);

            return newMatrix4;
        }

        public static Vector4 operator *(Matrix4 lhs, Vector4 rhs)
        {
            return new Vector4((lhs.M00 * rhs.X) + (lhs.M01 * rhs.Y) + (lhs.M02 * rhs.Z) + (lhs.M03 * rhs.W),
                               (lhs.M10 * rhs.X) + (lhs.M11 * rhs.Y) + (lhs.M12 * rhs.Z) + (lhs.M13 * rhs.W),
                               (lhs.M20 * rhs.X) + (lhs.M21 * rhs.Y) + (lhs.M22 * rhs.Z) + (lhs.M23 * rhs.W),
                               (lhs.M30 * rhs.X) + (lhs.M31 * rhs.Y) + (lhs.M32 * rhs.Z) + (lhs.M33 * rhs.W));
        }
    }
}
