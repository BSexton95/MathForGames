using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Diagnostics;
using MathLibrary;
using Raylib_cs;

namespace MathForGames
{
    class Engine
    {
        private static bool _applicationShouldClose;
        private static int _currentSceneIndex;
        private Scene[] _scenes = new Scene[0];
        private Stopwatch _stopWatch = new Stopwatch();
        private Camera3D _camera = new Camera3D();

        /// <summary>
        /// Called to begin the application
        /// </summary>
        public void Run()
        {
            //Call start for the entire application
            Start();

            float currentTime = 0;
            float lastTime = 0;
            float deltaTime = 0;

            //Loop until the application is told to close
            while (!_applicationShouldClose && !Raylib.WindowShouldClose())
            {
                //Get how much time has passed since the application started
                currentTime = _stopWatch.ElapsedMilliseconds / 1000.0f;

                //Set delta time to be the difference in time from the last time recorded to the current time
                deltaTime = currentTime - lastTime;

                //Update the application
                Update(deltaTime);
                //Draw all items
                Draw();

                //Set the last time recorded to be the current time
                lastTime = currentTime;
            }

            //Call end for the entire application
            End();
        }

        /// <summary>
        /// Initializes the camera
        /// </summary>
        private void InitializeCamera()
        {
            _camera.position = new System.Numerics.Vector3(0, 10, 10); //Camera position
            _camera.target = new System.Numerics.Vector3(0, 0, 0); //Point the camera is focused on
            _camera.up = new System.Numerics.Vector3(0, 1, 0); //Camera up vector (rotation towards target)
            _camera.fovy = 45; //Camera field of view on the Y
            _camera.projection = CameraProjection.CAMERA_PERSPECTIVE; //Camera mode type
        }

        /// <summary>
        /// Called when the application starts
        /// </summary>
        private void Start()
        {
            _stopWatch.Start();

            //Create a window using raylib
            Raylib.InitWindow(800, 450, "Math for Games");
            Raylib.SetTargetFPS(0);

            //Call initialize camera function
            InitializeCamera();

            Scene scene = new Scene();
            
            //Player
            Player player = new Player(0, 0, 1, "Player", Shape.CUBE);
            player.SetScale(1, 1, 1);
            /*
            //Player's collider
            CircleCollider playerCircleCollider = new CircleCollider(20, player);
            AABBCollider playerBoxCollider = new AABBCollider(50, 50, player);
            player.Collider = playerCircleCollider;
            //Add player to scene
            */

            /*
            /////////////////Testing matrix hierarchie//////////////////////
            //Parent
            Actor parent = new Actor(100, 100, "Parent", "Images/sun.png");
            parent.SetScale(100, 100);
            parent.SetTranslation(375, 200);

            //Child
            Actor child = new Actor(1, 1, "Child", "Images/rick.png");
            child.SetScale(.7f, .7f);
            child.SetTranslation(1, 1);

            
            //Child2
            Actor child2 = new Actor(.8f, .8f, "Child2", "Images/morty.png");
            child2.SetScale(.5f, .5f);
            
            
            child.AddChild(child2);
            parent.AddChild(child);
            
            scene.AddActor(child);
            scene.AddActor(child2);
            scene.AddActor(parent);
            scene.AddActor(player);
            /////////////////Testing matrix hierarchie//////////////////////
            */
            /*
            //Enemy
            Actor enemy = new Actor(400, 400, "Enemy", "Images/enemy.png");
            enemy.SetScale(50, 50);
            enemy.SetTranslation(100, 100);
            //Enemy's Collider
            CircleCollider enemyCircleCollider = new CircleCollider(10, enemy);
            AABBCollider enemyBoxCollider = new AABBCollider(50, 50, enemy);
            enemy.Forward = new Vector2(700, 900);
            enemy.Collider = enemyBoxCollider;
            //Add enemy to scene
            scene.AddActor(enemy);
            */

            scene.AddActor(player);
            _currentSceneIndex = AddScene(scene);
            _scenes[_currentSceneIndex].Start();

            Console.CursorVisible = false;
        }

        /// <summary>
        /// Called evertime the game loops
        /// </summary>
        private void Update(float deltaTime)
        {
            _scenes[_currentSceneIndex].Update(deltaTime);
        }

        /// <summary>
        /// Called every time the game loops to update visuals
        /// </summary>
        private void Draw()
        {
            Raylib.BeginDrawing();
            Raylib.BeginMode3D(_camera);

            //Color of the background
            Raylib.ClearBackground(Color.RAYWHITE);
            //Draw a grid to console
            Raylib.DrawGrid(50, 1);

            //Adds all actor icons to buffer
            _scenes[_currentSceneIndex].Draw();

            Raylib.EndMode3D();
            Raylib.EndDrawing();
        }

        /// <summary>
        /// Called when the apllication exits
        /// </summary>
        private void End()
        {
            _scenes[_currentSceneIndex].End();
            Raylib.CloseWindow();

            while (Console.KeyAvailable)
                Console.ReadKey(true);
        }

        /// <summary>
        /// Adds a scene to the engine's scene array
        /// </summary>
        /// <param name="scene">The scene that will be added to the scene array</param>
        /// <returns>The index that the new scene is located</returns>
        public int AddScene(Scene scene)
        {
            //Create a new temporary array
            Scene[] tempArray = new Scene[_scenes.Length + 1];

            //Copy all values from old array into the new array
            for (int i = 0; i < _scenes.Length; i ++)
            {
                tempArray[i] = _scenes[i];
            }

            //Set the last index to be the new scene
            tempArray[_scenes.Length] = scene;

            //Set the old array to be the new array
            _scenes = tempArray;

            //Return the last index
            return _scenes.Length - 1;
        }

        /// <summary>
        /// Gets the next key in the input stream 
        /// </summary>
        /// <returns>The key that was pressed</returns>
        public static ConsoleKey GetNextKey()
        {
            //If there is no key being pressed...
            if (!Console.KeyAvailable)
                //...return
                return 0;

            //Return the current key being pressed
            return Console.ReadKey(true).Key;
        }

        /// <summary>
        /// Ends the application
        /// </summary>
        public static void CloseApplication()
        {
            _applicationShouldClose = true;
        }
    }
}
