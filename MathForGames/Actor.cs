using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace MathForGames
{
    public enum Shape
    {
        CUBE,
        SPHERE
    }

    class Actor
    {
        private string _name;
        private bool _started;
        private Collider _collider;
        private Vector3 _forward = new Vector3(0, 0, 1);
        private Matrix4 _globalTransform = Matrix4.Identity;
        private Matrix4 _localTransform = Matrix4.Identity;
        private Matrix4 _translation = Matrix4.Identity;
        private Matrix4 _rotation = Matrix4.Identity;
        private Matrix4 _scale = Matrix4.Identity;
        private Actor[] _children = new Actor[0];
        private Actor _parent;
        private Shape _shape;

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
        public Vector3 LocalPosition
        {
            get { return new Vector3(_translation.M03, _translation.M13, _translation.M23); }
            set 
            {
                SetTranslation(value.X, value.Y, value.Z);
            }
        }

        /// <summary>
        /// The position of this actor in the world
        /// </summary>
        public Vector3 WorldPosition
        {
            //return the global transform's T column
            get { return new Vector3(_globalTransform.M03, _globalTransform.M13, _globalTransform.M23); }
            set
            {
                //If the actor has a parent...
                if (Parent != null)
                {
                    //...convert the world cooridinates into local coordiniates and translate the actor
                    float xOffSet = (value.X - Parent.WorldPosition.X) / new Vector3(GlobalTransform.M00, GlobalTransform.M10, GlobalTransform.M20).Magnitude;
                    float yOffSet = (value.Y - Parent.WorldPosition.Y) / new Vector3(GlobalTransform.M01, GlobalTransform.M11, GlobalTransform.M21).Magnitude;
                    float zOffSet = (value.Z - Parent.WorldPosition.Z) / new Vector3(GlobalTransform.M02, GlobalTransform.M12, GlobalTransform.M22).Magnitude;
                    SetTranslation(xOffSet, yOffSet, zOffSet);
                }
                //If this actor doesn't have a parent...
                else
                    //...set local position to be the given value
                    LocalPosition = value;
            }
        }

        public Matrix4 GlobalTransform
        {
            get { return _globalTransform; }
            private set { _globalTransform = value; }
        }

        public Matrix4 LocalTransform
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
        public Vector3 Size
        {
            get 
            {
                float xScale = new Vector3(_scale.M00, _scale.M10, _scale.M20).Magnitude;
                float yScale = new Vector3(_scale.M01, _scale.M11, _scale.M21).Magnitude;
                float zScale = new Vector3(_scale.M02, _scale.M12, _scale.M22).Magnitude;

                return new Vector3(xScale, yScale, zScale); 
            }
            set { SetScale(value.X, value.Y, value.Z); }
        }

        public Vector3 Forward
        {
            get { return new Vector3(_rotation.M02, _rotation.M12, _rotation.M22); }
        }

        /// <summary>
        /// The collider attached to this actor
        /// </summary>
        public Collider Collider
        {
            get { return _collider; }
            set { _collider = value; }
        }

        public Actor() { }

        public Actor(float x, float y, string name = "Actor", Shape shape = Shape.CUBE) :
            this( new Vector3 { X = x, Y = y}, name, shape) {}


        public Actor(Vector3 position, string name = "Actor", Shape shape = Shape.CUBE)
        {
            LocalPosition = position;
            _name = name;
            _shape = shape;
        }

        /// <summary>
        /// 
        /// </summary>
        public void UpdateTransforms()
        {
            //If...
            if (Parent != null)
            {
                //...
                GlobalTransform = Parent.GlobalTransform * LocalTransform;
            }
            //Otherwise
            else
            {
                //...set 
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
            //Multiplys the translation matrix with the rotation matrix and scale matrix to get the local transform
            _localTransform = _translation * _rotation * _scale;

            //Call funtion to update transforms
            UpdateTransforms();

            /*
            //If the actor is not the player...
            if(_name != "Player")
                //...rotate the actor
                Rotate(deltaTime);
                */
            

            Console.WriteLine(_name + ": " + LocalPosition.X + ", " + LocalPosition.Y);
        } 
        
        public virtual void Draw()
        {
            System.Numerics.Vector3 position = new System.Numerics.Vector3(WorldPosition.X, WorldPosition.Y, WorldPosition.Z);
            switch (_shape)
            {
                case Shape.CUBE:
                    float sizeX = new Vector3(GlobalTransform.M00, GlobalTransform.M10, GlobalTransform.M20).Magnitude;
                    float sizeY = new Vector3(GlobalTransform.M01, GlobalTransform.M11, GlobalTransform.M21).Magnitude;
                    float sizeZ = new Vector3(GlobalTransform.M02, GlobalTransform.M12, GlobalTransform.M22).Magnitude;
                    Raylib.DrawCube(position, sizeX, sizeY, sizeZ, Color.BLUE);
                    break;
                case Shape.SPHERE:
                    sizeX = new Vector3(GlobalTransform.M00, GlobalTransform.M10, GlobalTransform.M20).Magnitude;
                    Raylib.DrawSphere(position, sizeX, Color.BLUE);
                    break;
                    /*
                case Shape.CUBE:
                    Raylib.DrawCube(position, Size.X, Size.Y, Size.Z, Color.Blue);
                    break;
                case Shape.SPHERE:
                    Raylib.DrawSphere(position, Size.X, Color.BLUE);
                    break;
                    */
            }
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
        public void SetTranslation(float translationX, float translationY, float translationZ)
        {
            _translation = Matrix4.CreateTranslation(translationX, translationY, translationZ);
        }

        /// <summary>
        /// Applies the given values to the current translation
        /// </summary>
        /// <param name="translationX">The amount to move on the x</param>
        /// <param name="translationY">The amount to move on the y</param>
        public void Translate(float translationX, float translationY, float translationZ)
        {
            _translation *= Matrix4.CreateTranslation(translationX, translationY, translationZ);
        }

        /// <summary>
        /// Set the rotation of the actor
        /// </summary>
        /// <param name="radians">The angle of the new rotation in radians</param>
        public void SetRotation(float radiansX, float radiansY, float radiansZ)
        {
            Matrix4 rotationX = Matrix4.CreateRotationX(radiansX);
            Matrix4 rotationY = Matrix4.CreateRotationY(radiansY);
            Matrix4 rotationZ = Matrix4.CreateRotationZ(radiansZ);
            _rotation = rotationX * rotationY * rotationZ;
        }

        /// <summary>
        /// Adds a rotation to the current transform's rotation
        /// </summary>
        /// <param name="radians">The angle in raidans</param>
        public void Rotate(float radiansX, float radiansY, float radiansZ)
        {
            Matrix4 rotationX = Matrix4.CreateRotationX(radiansX);
            Matrix4 rotationY = Matrix4.CreateRotationY(radiansY);
            Matrix4 rotationZ = Matrix4.CreateRotationZ(radiansZ);
            _rotation *= rotationX * rotationY * rotationZ;
        }

        /// <summary>
        /// Sets the scale of the actor
        /// </summary>
        /// <param name="x">The value to scale on the x axis</param>
        /// <param name="y">The value to scale on the y axis</param>
        public void SetScale(float x, float y, float z)
        {
            _scale = Matrix4.CreateScale(x, y, z);
        }

        /// <summary>
        /// Scales the actor by the given amount
        /// </summary>
        /// <param name="x">The value to scale on the x axis</param>
        /// <param name="y">The value to scale on the y axis</param>
        public void Scale(float x, float y, float z)
        {
            _scale *= Matrix4.CreateScale(x, y, z);
        }

        /// <summary>
        /// Rotates the actor to face the given position
        /// </summary>
        /// <param name="position">The position the actor should be looking towards</param>
        public void LookAt(Vector3 position)
        {
            //Find the direction the actor should look in
            Vector3 direction = (position - WorldPosition).Normalized;

            if (direction.Magnitude == 0)
                direction = new Vector3(0, 0, 1);

            Vector3 alignAxis = new Vector3(0, 1, 0);

            Vector3 newYAxis = new Vector3(0, 1, 0);
            Vector3 newXAxis = new Vector3(1, 0, 0);



            //////////////////////////////////2D Look at function/////////////////////////////
            /*
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
            */
        }
    }
}
