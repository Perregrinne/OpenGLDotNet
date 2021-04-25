using GLFW;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using OpenGLDotNet.OpenGL;
using System.Xml.Serialization;
using static OpenGLDotNet.OpenGL.GL;
using System.Drawing;

namespace OpenGLDotNet.Render.Display
{
    static class DisplayManager
    {
		public static Window Window { get; set; }
		public static Vector2 WindowSize { get; set; }
		public static void CreateWindow(int width, int height, string title)
		{
			WindowSize = new Vector2(width, height);

			Glfw.Init();
			//OpenGL 3.3
			Glfw.WindowHint(Hint.ContextVersionMajor, 3);
			Glfw.WindowHint(Hint.ContextVersionMinor, 3);
			Glfw.WindowHint(Hint.OpenglProfile, Profile.Core);

			Glfw.WindowHint(Hint.Focused, true);
			Glfw.WindowHint(Hint.Resizable, false);

			Window = Glfw.CreateWindow(width, height, title, Monitor.None, Window.None);

			//Error checking
			if (Window == Window.None)
			{
				return;
			}

			Rectangle screen = Glfw.PrimaryMonitor.WorkArea;
			int x = (screen.Width) / 2;
			int y = (screen.Height) / 2;

			Glfw.MakeContextCurrent(Window);
			Import(Glfw.GetProcAddress);

			glViewport(0, 0, width, height);
			Glfw.SwapInterval(0); //No V-Sync

		}

		public static void CloseWindow()
		{
			Glfw.Terminate();
		}
	}
}
