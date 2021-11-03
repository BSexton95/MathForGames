using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace MathForGames
{
    class Player : Actor
    {
        private float _speed;
        private Vector3 _velocity;

        public float Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        public Vector3 Velocity
        {
            get { return _velocity; }
            set { _velocity = value; }
        }

        public Player(float x, float y, float speed, string name = "Actor", Shape shape = Shape.CUBE)
            : base(x, y, name, shape)
        {
            _speed = speed;
        }

        public override void Start()
        {
            base.Start();
        }

        public override void Update(float deltaTime)
        {
            //Get the player input direction
            int xDirection = -Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_A))
                + Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_D));
            int zDirection = -Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_W))
                + Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_S));

            //Create a vector that stores the move input
            Vector3 moveDirection = new Vector3(xDirection, 0, zDirection);

            Velocity = moveDirection.Normalized * Speed * deltaTime;

            LocalPosition += Velocity;

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
            //Collider.Draw();
        }
    }
}
