using System;
using MathLibrary;

namespace MathLibrary
{
    public struct Vector3
    {
        public float X;
        public float Y;
        public float Z;

        public Vector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        /// <summary>
        /// Gets the length of the vector
        /// </summary>
        public float Magnitude
        {
            get
            {
                return (float)Math.Sqrt(X * X + Y * Y + Z * Z);
            }
        }

        /// <summary>
        /// Gets the normalized version of this vector without changing it
        /// </summary>
        public Vector3 Normalized
        {
            get
            {
                Vector3 value = this;
                return value.Normalize();
            }
        }

        /// <summary>
        /// Changes this vector to have a magnitude that is equal to one
        /// </summary>
        /// <returns>The result of the normalization. Returns an empty vector if the magnitude is zero</returns>
        public Vector3 Normalize()
        {
            if (Magnitude == 0)
                return new Vector3();

            return this /= Magnitude;
        }

        /// <param name="lhs">The left hand side of the operation</param>
        /// <param name="rhs">The right hand side of the operation</param>
        /// <returns>The dot product of the first vector on to the second </returns>
        public static float DotProduct(Vector3 lhs, Vector3 rhs)
        {
            return (lhs.X * rhs.X) + (lhs.Y * rhs.Y) + (lhs.Z * rhs.Z);
        }

        public static Vector3 CrossProduct(Vector3 lhs, Vector3 rhs)
        {
            return new Vector3((lhs.Y * rhs.Z) - (lhs.Z * rhs.Y),
                               (lhs.Z * rhs.X) - (lhs.X * rhs.Z),
                               (lhs.X * rhs.Y) - (lhs.Y * rhs.X));
        }

        /// <summary>
        /// Finds the distance from the first vector to the second 
        /// </summary>
        /// <param name="lhs">The starting point</param>
        /// <param name="rhs">The ending point</param>
        /// <returns>A scalar representing the distance</returns>
        public static float Distance(Vector3 lhs, Vector3 rhs)
        {
            return (rhs - lhs).Magnitude;
        }

        /// <summary>
        /// Adds the x value of the second vector to the first, adds the y value of the second vector to the first,
        /// and adds the z value of the second vector to the firs.
        /// </summary>
        /// <param name="lhs">The vector that is increasing</param>
        /// <param name="rhs">The vector used to increase the 1st vectre</param>
        /// <returns>The reult of the vector addition</returns>
        public static Vector3 operator +(Vector3 lhs, Vector3 rhs)
        {
            return new Vector3 { X = lhs.X + rhs.X, Y = lhs.Y + rhs.Y, Z = lhs.Z + rhs.Z };
        }

        ///<summary>
        /// Subtracts the x value of the second vector to the first, subtracts the y value of the second vector to the first,
        /// and subracts the z value of the second vector to the first.
        /// </summary>
        /// <param name="lhs">The vector that is being subtracted from</param>
        /// <param name="rhs">The vector used to subtract from the first</param>
        /// <returns>The reult of the vector subtraction</returns>
        public static Vector3 operator -(Vector3 lhs, Vector3 rhs)
        {
            return new Vector3 { X = lhs.X - rhs.X, Y = lhs.Y - rhs.Y, Z = lhs.Z - rhs.Z };
        }

        /// <summary>
        /// Multiplies the vector's x, y, and z values by the scalar
        /// </summary>
        /// <param name="vector">The vector that is being scaled</param>
        /// <param name="scalar">The value to scale the vector by</param>
        /// <returns>The result of the vector scaling</returns>
        public static Vector3 operator *(Vector3 vector, float scalar)
        {
            return new Vector3 { X = vector.X * scalar, Y = vector.Y * scalar, Z = vector.Z * scalar };
        }

        /// <summary>
        /// Multiplies the vector's x and y values by the scalar
        /// </summary>
        /// <param name="vector">The vector that is being scaled</param>
        /// <param name="scalar">The value to scale the vector by</param>
        /// <returns>The result of the vector scaling</returns>
        public static Vector3 operator *(float scalar, Vector3 vector)
        {
            return new Vector3 { X = vector.X * scalar, Y = vector.Y * scalar, Z = vector.Z * scalar };
        }

        /// <summary>
        /// Divides the vector's x, y, and z values by the scalar given
        /// </summary>
        /// <param name="vector">The vector that is being scaled</param>
        /// <param name="scalar">The value to scale the vector by</param>
        /// <returns>The result of the vector scaling</returns>
        public static Vector3 operator /(Vector3 vector, float scalar)
        {
            return new Vector3 { X = vector.X / scalar, Y = vector.Y / scalar, Z = vector.Z / scalar };
        }

        /// <summary>
        /// Compares the x, y, and z values of two vectors
        /// </summary>
        /// <param name="lhs">The left side of the comparison</param>
        /// <param name="rhs">The right side of the comparison</param>
        /// <returns>True if the x values of both vectors match, the y values match, and the z values match</returns>
        public static bool operator ==(Vector3 lhs, Vector3 rhs)
        {
            return lhs.X == rhs.X && lhs.Y == rhs.Y && lhs.Z == rhs.Z;
        }

        /// <summary>
        /// Compares the x, y, and z values of two vectors
        /// </summary>
        /// <param name="lhs">The left side of the comparison</param>
        /// <param name="rhs">The right side of the comparison</param>
        /// <returns>True if the x values of both vectors don't match, the y values don't match, and the z values match</returns>
        public static bool operator !=(Vector3 lhs, Vector3 rhs)
        {
            return lhs.X != rhs.X || lhs.Y != rhs.Y || lhs.Z != rhs.Z;
        }

    }
}
