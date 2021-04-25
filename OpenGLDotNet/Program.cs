using System;
using System.Drawing;
using System.Numerics;
using GLFW;
using OpenGLDotNet.Gameloop;
//using static OpenGL.GL;
using OpenGLDotNet.OpenGL;
using static OpenGLDotNet.OpenGL.GL;

namespace OpenGLDotNet
{
	class Program
	{
		public static void Main(string[] args)
		{
			Game game = new Game3D(600, 600, "Hello world");
			game.Run();
		}
	}
}