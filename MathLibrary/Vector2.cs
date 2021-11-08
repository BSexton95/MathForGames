using System;

namespace MathLibrary
{
    public struct Vector2
    {
        public float X;
        public float Y;

        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Gets the length of the vector
        /// </summary>
        public float Magnitude
        {
            get
            {
                return (float)Math.Sqrt(X * X + Y * Y);
            }
        }

        /// <summary>
        /// Gets the normalized version of this vector without changing it
        /// </summary>
        public Vector2 Normalized
        {
            get
            {
                Vector2 value = this;
                return value.Normalize();
            }
        }

        /// <summary>
        /// Changes this vector to have a magnitude that is equal to one
        /// </summary>
        /// <returns>The result of the normalization. Returns an emply vector if the magnitude is zero</returns>
        public Vector2 Normalize()
        {
            if (Magnitude == 0)
                return new Vector2();

            return this /= Magnitude;
        }

        /// <param name="lhs">The left hand side of the operation</param>
        /// <param name="rhs">The right hand side of the operation</param>
        /// <returns>The dot product of the first vector on to the second </returns>
        public static float DotProduct(Vector2 lhs, Vector2 rhs)
        {
            return (lhs.X * rhs.X) + (lhs.Y * rhs.Y);
        }

        /// <summary>
        /// Finds the distance from the first vector to the second 
        /// </summary>
        /// <param name="lhs">The starting point</param>
        /// <param name="rhs">The ending point</param>
        /// <returns>A scalar representing the distance</returns>
        public static float Distance(Vector2 lhs, Vector2 rhs)
        {
            return (rhs - lhs).Magnitude;
        }

        /// <summary>
        /// Adds the x value of the second vector to the first, and adds the y value of the second vector to the first.
        /// </summary>
        /// <param name="lhs">The vector that is increasing</param>
        /// <param name="rhs">The vector used to increase the 1st vectre</param>
        /// <returns>The reult of the vector addition</returns>
        public static Vector2 operator +(Vector2 lhs, Vector2 rhs)
        {
            return new Vector2 { X = lhs.X + rhs.X, Y = lhs.Y + rhs.Y };
        }

        ///<summary>
        /// Subtracts the x value of the second vector to the first, and Sibtracts the y value of the second vector to the first.
        /// </summary>
        /// <param name="lhs">The vector that is being subtracted from</param>
        /// <param name="rhs">The vector used to subtract from the first</param>
        /// <returns>The reult of the vector subtraction</returns>
        public static Vector2 operator -(Vector2 lhs, Vector2 rhs)
        {
            return new Vector2 { X = lhs.X - rhs.X, Y = lhs.Y - rhs.Y };
        }

        /// <summary>
        /// Multiplies the vector's x and y values by the scalar
        /// </summary>
        /// <param name="vector">The vector that is being scaled</param>
        /// <param name="scalar">The value to scale the vector by</param>
        /// <returns>The result of the vector scaling</returns>
        public static Vector2 operator *(Vector2 vector, float scalar)
        {
            return new Vector2 { X = vector.X * scalar, Y = vector.Y * scalar };
        }

        /// <summary>
        /// Multiplies the vector's x and y values by the scalar
        /// </summary>
        /// <param name="vector">The vector that is being scaled</param>
        /// <param name="scalar">The value to scale the vector by</param>
        /// <returns>The result of the vector scaling</returns>
        public static Vector2 operator *(float scalar, Vector2 vector)
        {
            return new Vector2 { X = vector.X * scalar, Y = vector.Y * scalar };
        }

        /// <summary>
        /// Divides the vector's x and y values by the scalar given
        /// </summary>
        /// <param name="vector">The vector that is being scaled</param>
        /// <param name="scalar">The value to scale the vector by</param>
        /// <returns>The result of the vector scaling</returns>
        public static Vector2 operator /(Vector2 vector, float scalar)
        {
            return new Vector2 { X = vector.X / scalar, Y = vector.Y / scalar };
        }

        /// <summary>
        /// Compares the x and y values of two vectors
        /// </summary>
        /// <param name="lhs">The left side of the comparison</param>
        /// <param name="rhs">The right side of the comparison</param>
        /// <returns>True if the x values of both vectors match and the y values match</returns>
        public static bool operator ==(Vector2 lhs, Vector2 rhs)
        {
            return lhs.X == rhs.X && lhs.Y == rhs.Y;
        }

        /// <summary>
        /// Compares the x and y values of two vectors
        /// </summary>
        /// <param name="lhs">The left side of the comparison</param>
        /// <param name="rhs">The right side of the comparison</param>
        /// <returns>True if the x values of both vectors don't match and the y values don't match</returns>
        public static bool operator !=(Vector2 lhs, Vector2 rhs)
        {
            return lhs.X != rhs.X || lhs.Y != rhs.Y;
        }

    }
}
