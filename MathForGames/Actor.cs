using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace MathForGames
{
    class Actor
    {
        private string _name;
        private bool _started;
        private Collider _collider;
        private Vector2 _forward = new Vector2(1, 0);
        private Matrix3 _globalTransform = Matrix3.Identity;
        private Matrix3 _localTransform = Matrix3.Identity;
        private Matrix3 _translation = Matrix3.Identity;
        private Matrix3 _rotation = Matrix3.Identity;
        private Matrix3 _scale = Matrix3.Identity;
        private Actor[] _children = new Actor[0];
        private Actor _parent;
        private Sprite _sprite;

        /// <summary>
        /// True if the start function has been called for this actor
        /// </summary>
        public bool Started
        {
            get { return _started; }
        }

        /// <summary>
        /// Position of actor
        /// </summary>
        public Vector2 LocalPosition
        {
            get { return new Vector2(_translation.M02, _translation.M12); }
            set 
            {
                SetTranslation(value.X, value.Y);
            }
        }

        /// <summary>
        /// The position of this actor in the world
        /// </summary>
        public Vector2 WorldPosition
        {
            //return the global transform's T column
            get { return new Vector2(_globalTransform.M02, _globalTransform.M12); }
            set
            {
                //If the actor has a parent...
                if (Parent != null)
                {
                    //...convert the world cooridinates into local coordiniates and translate the actor
                    float xOffSet = (value.X - Parent.WorldPosition.X) / new Vector2(_globalTransform.M00, _globalTransform.M10).Magnitude;
                    float yOffSet = (value.Y - Parent.WorldPosition.Y) / new Vector2(_globalTransform.M10, _globalTransform.M11).Magnitude;
                    SetTranslation(xOffSet, yOffSet);
                }
                //If this actor doesn't have a parent...
                else
                    //...set local position to be the given value
                    LocalPosition = value;
            }
        }

        public Matrix3 GlobalTransform
        {
            get { return _globalTransform; }
            private set { _globalTransform = value; }
        }

        public Matrix3 LocalTransform
        {
            get { return _localTransform; } 
            private set { _localTransform = value; }
        }

        public Actor Parent
        {
            get { return _parent; } 
            set { _parent = value; }
        }

        public Actor[] Children
        {
            get { return _children; }
        }

        /// <summary>
        /// Size of actor
        /// </summary>
        public Vector2 Size
        {
            get 
            {
                float xScale = new Vector2(_scale.M00, _scale.M10).Magnitude;
                float yScale = new Vector2(_scale.M01, _scale.M11).Magnitude;

                return new Vector2(xScale, yScale); 
            }
            set { SetScale(value.X, value.Y); }
        }

        public Vector2 Forward
        {
            get { return new Vector2(_rotation.M00, _rotation.M10); }
            set 
            { 
                Vector2 point = value.Normalized + LocalPosition;
                LookAt(point);
            }
        }

        /// <summary>
        /// The collider attached to this actor
        /// </summary>
        public Collider Collider
        {
            get { return _collider; }
            set { _collider = value; }
        }

        /// <summary>
        /// The sprite attached to the actor
        /// </summary>
        public Sprite Sprite
        {
            get { return _sprite; }
            set { _sprite = value; }
        }

        public Actor(float x, float y, string name = "Actor", string path = "") :
            this( new Vector2 { X = x, Y = y}, name, path) {}


        public Actor(Vector2 position, string name = "Actor", string path = "")
        {
            SetTranslation(position.X, position.Y);
            _name = name;

            if (path != "")
                _sprite = new Sprite(path);
        }

        /// <summary>
        /// 
        /// </summary>
        public void UpdateTransforms()
        {
            //If
            if (Parent != null)
            {
                //...
                GlobalTransform = Parent.GlobalTransform * LocalTransform;
            }
            //Otherwise
            else
            {
                //...
                GlobalTransform = LocalTransform;
            }

        }

        /// <summary>
        /// Adds a child to the actor array list
        /// </summary>
        /// <param name="child">The child to be added to the array</param>
        public void AddChild(Actor child)
        {
            //Create a temporary array larger than the original
            Actor[] tempArray = new Actor[_children.Length + 1];

            //Copy all values from the original array into the temporary array
            for (int i = 0; i < _children.Length; i++)
            {
                tempArray[i] = _children[i];
            }

            //Add the new child to the end of the new array
            tempArray[_children.Length] = child;

            //Set the parent of the actor to be this actor
            child.Parent = this;

            //Set the old array to be the new array
            _children = tempArray;
        }

        /// <summary>
        /// Removes a child from the actor array list
        /// </summary>
        /// <param name="child">The child to be removed from the array</param>
        /// <returns>True if child was successfuly removed from the array</returns>
        public bool RemoveChild(Actor child)
        {
            //Create a variable to store if the removal was successful
            bool actorRemoved = false;

            //Create a new array that is smaller than the original
            Actor[] tempArray = new Actor[_children.Length - 1];

            //Copy all values except the child we don't want into the new array
            int j = 0;
            for (int i = 0; i < tempArray.Length; i++)
            {
                //If the child that the loop is on is not the one to remove...
                if (_children[i] != child)
                {
                    //...add the child into the new array and increment the temp array counter
                    tempArray[j] = _children[i];
                    j++;
                }
                //Otherwise if this child is the one to remove...
                else
                {
                    //...set childRemoved to true
                    actorRemoved = true;
                }
            }

            //If the child removal was successful...
            if (actorRemoved)
            {
                //...set the old array to be the new array
                _children = tempArray;

                //Set the parent of the child to be nothing
                child.Parent = null;
            }

            return actorRemoved;
        }

        public virtual void Start()
        {
            _started = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="deltaTime"></param>
        public virtual void Update(float deltaTime)
        {
            //Call funtion to update transforms
            UpdateTransforms();

            //If the actor is not the player...
            if(_name != "Player")
                //...rotate the actor
                Rotate(deltaTime);

            _localTransform = _translation * _rotation * _scale;

            Console.WriteLine(_name + ": " + LocalPosition.X + ", " + LocalPosition.Y);
        } 
        
        public virtual void Draw()
        {
            if (_sprite != null)
                _sprite.Draw(GlobalTransform);

            //Collider.Draw();
        }

        public void End()
        {

        }

        public virtual void OnCollision(Actor actor)
        {
            
        }

        /// <summary>
        /// Checks if this actor collided with another actor
        /// </summary>
        /// <param name="other">The actor to check collision against</param>
        /// <returns>True if the distance between the actors is less than the radii of the two combined</returns>
        public virtual bool CheckForCollision(Actor other)
        {
            //Return false if either actor doesn't have a collider attached
            if (Collider == null || other.Collider == null)
                return false;

            return Collider.CheckCollision(other);
        }

        /// <summary>
        /// Sets the position of the actor
        /// </summary>
        /// <param name="translationX">The new x position </param>
        /// <param name="translationY">The new y position</param>
        public void SetTranslation(float translationX, float translationY)
        {
            _translation = Matrix3.CreateTranslation(translationX, translationY);
        }

        /// <summary>
        /// Applies the given values to the current translation
        /// </summary>
        /// <param name="translationX">The amount to move on the x</param>
        /// <param name="translationY">The amount to move on the y</param>
        public void Translate(float translationX, float translationY)
        {
            _translation *= Matrix3.CreateTranslation(translationX, translationY);
        }

        /// <summary>
        /// Set the rotation of the actor
        /// </summary>
        /// <param name="radians">The angle of the new rotation in radians</param>
        public void SetRotation(float radians)
        { 
            _rotation = Matrix3.CreateRotation(radians);
        }

        /// <summary>
        /// Adds a rotation to the current transform's rotation
        /// </summary>
        /// <param name="radians">The angle in raidans</param>
        public void Rotate(float radians)
        {
            _rotation *= Matrix3.CreateRotation(radians);
        }

        /// <summary>
        /// Sets the scale of the actor
        /// </summary>
        /// <param name="x">The value to scale on the x axis</param>
        /// <param name="y">The value to scale on the y axis</param>
        public void SetScale(float x, float y)
        {
            _scale = Matrix3.CreateScale(x, y);
        }

        /// <summary>
        /// Scales the actor by the given amount
        /// </summary>
        /// <param name="x">The value to scale on the x axis</param>
        /// <param name="y">The value to scale on the y axis</param>
        public void Scale(float x, float y)
        {
            _scale *= Matrix3.CreateScale(x, y);
        }

        /// <summary>
        /// Rotates the actor to face the given position
        /// </summary>
        /// <param name="position">The position the actor should be looking towards</param>
        public void LookAt(Vector2 position)
        {
            //Find the direction the actor should look in
            Vector2 direction = (position - LocalPosition).Normalized;

            //Use the dot product to find the angle the actor needs to rotate
            float dotProd = Vector2.DotProduct(direction, Forward);

            if (dotProd > 1)
                dotProd = 1;

            float angle = (float)Math.Acos(dotProd);

            //Find the perpindiculer vector to the direction
            Vector2 perpDirection = new Vector2(direction.Y, -direction.X);

            //Find the dot product of the perpindicular vector and the current forward
            float perpDot = Vector2.DotProduct(perpDirection, Forward);

            //If the result isn't 0, use it to change the sign of the angle to be either positive or negative
            if (perpDot != 0)
                angle *= -perpDot / Math.Abs(perpDot);

            Rotate(angle);
        }
    }
}
