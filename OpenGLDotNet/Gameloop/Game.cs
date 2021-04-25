using OpenGLDotNet.Render.Display;
using System;
using System.Collections.Generic;
using System.Text;
using GLFW;

namespace OpenGLDotNet.Gameloop
{
    abstract class Game
    {

        protected int InitialWindowWidth { get; set; }
        protected int InitialWindowHeight { get; set; }
        protected string InitialWindowTitle { get; set; }

        public Game(int initialWindowWidth, int initialWindowHeight, string initialWindowTitle)
        {
            InitialWindowWidth = initialWindowWidth;
            InitialWindowHeight = initialWindowHeight;
            InitialWindowTitle = initialWindowTitle;
        }

        public void Run()
        {
            Initialize();

            DisplayManager.CreateWindow(InitialWindowWidth, InitialWindowHeight, InitialWindowTitle);

            LoadContent();

            //Actual Game Loop:
            while(!Glfw.WindowShouldClose(DisplayManager.Window))
            {
                Gametime.DeltaTime = (float)Glfw.Time - Gametime.TotalElapsedSeconds;
                Gametime.TotalElapsedSeconds = (float)Glfw.Time;
                Update();

                Glfw.PollEvents();

                Render();
            }
            DisplayManager.CloseWindow();

        }

        protected abstract void Initialize();
        protected abstract void LoadContent();

        protected abstract void Update();
        protected abstract void Render();
    }
}
