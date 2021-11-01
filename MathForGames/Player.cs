﻿using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace MathForGames
{
    class Player : Actor
    {
        private float _speed;
        private Vector2 _velocity;

        public float Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        public Vector2 Velocity
        {
            get { return _velocity; }
            set { _velocity = value; }
        }

        public Player(float x, float y, float speed, string name = "Actor", string path = "")
            : base(x, y, name, path)
        {
            _speed = speed;
        }

        public override void Update(float deltaTime)
        {
            //Get the player input direction
            int xDirection = -Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_A))
                + Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_D));
            int yDirection = -Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_W))
                + Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_S));

            //Create a vector that stores the move input
            Vector2 moveDirection = new Vector2(xDirection, yDirection);

            Velocity = moveDirection.Normalized * Speed * deltaTime;

            Translate(Velocity.X, Velocity.Y);

            //If player moves to the right...
            if (xDirection > 0)
                //...sprite will rotate to the right
                SetRotation(0);
            //If player moves to the left...
            if (xDirection < 0)
                //...sprite rotates to the left
                SetRotation((float)Math.PI);
            //If player moves up...
            if (yDirection > 0)
                //...sprite will rotate up
                SetRotation(3 * ((float)Math.PI / 2));
            //If player moves down...
            if (yDirection < 0)
                //...sprite will rotate down.
                SetRotation((float)Math.PI / 2);
            


            //Prints players position
            base.Update(deltaTime);
        }

        public override void OnCollision(Actor actor)
        {
            Console.WriteLine("Collision occured");
        }

        public override void Draw()
        {
            base.Draw();
            Collider.Draw();
        }
    }
}
