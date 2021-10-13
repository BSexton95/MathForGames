using System;

namespace MathLibrary
{
    public struct Vector2
    {
        public float X;
        public float Y;

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
            return new Vector2 { X = lhs.X + rhs.X, Y = lhs.Y + rhs.Y };
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
