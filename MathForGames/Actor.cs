using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;

namespace MathForGames
{
    class Actor
    {
        private char _icon;
        private string _name;
        private Vector2 _position;
        private bool _started;

        /// <summary>
        /// True if the start function has been called for this actor
        /// </summary>
        public bool Started
        {
            get { return _started; }
        }

        public Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public Actor(char icon, Vector2 position, string name = "Actor")
        {
            _icon = icon;
            _position = position;
            _name = name;
        }

        public virtual void Start()
        {
            _started = true;
        }

        public virtual void Update()
        {
            _position.X = Position.X + 1;
        } 
        
        public virtual void Draw()
        {
            Console.SetCursorPosition((int)Position.X, (int)Position.Y);
            Console.Write(_icon);
        }

        public void End()
        {

        }
    }
}
