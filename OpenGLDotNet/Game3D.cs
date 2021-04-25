using GLFW;
using OpenGLDotNet.Gameloop;
using OpenGLDotNet.Render.Display;
using OpenGLDotNet.Render.Shader;
using OpenGLDotNet.Render.Camera3D;
using System;
using System.Collections.Generic;
using System.Text;
using static OpenGLDotNet.OpenGL.GL;
using System.Numerics;

namespace OpenGLDotNet
{
    class Game3D : Game
    {
        uint vbo;
        uint vao;

        Shader shader;

        Camera3D camera;

        public Game3D(int initialWindowWidth, int initialWindowHeight, string initialWindowTitle) : base(initialWindowWidth, initialWindowHeight, initialWindowTitle)
        {

        }

        protected override void Initialize()
        {

        }

        protected unsafe override void LoadContent()
        {
            string vertexShader = @"#version 330 core
                                    layout (location = 0) in vec3 aPosition;
                                    layout (location = 1) in vec3 aColor;
                                    out vec4 vertexColor;

                                    uniform mat4 projection;
                                    uniform mat4 model;
                                    
                                    void main()
                                    {
                                        vertexColor = vec4(aColor.rgb, 1.0);
                                        gl_Position = projection * model * vec4(aPosition.xyz, 1.0);
                                    }";
            string fragmentShader = @"#version 330 core
                                      out vec4 FragColor;
                                      in vec4 vertexColor;
                                      
                                      void main()
                                      {
                                          FragColor = vertexColor;
                                      }";

            shader = new Shader(vertexShader, fragmentShader);
            shader.Load();

            //Create vertex array object and vertex buffer object
            vao = glGenVertexArray();
            vbo = glGenBuffer();

            glBindVertexArray(vao);
            glBindBuffer(GL_ARRAY_BUFFER, vbo);

            //Vertex data:
            float[] vertices =
            {
                //Left Triangle
                -0.5f, 0.5f, 333.0f, 1.0f, 0.0f, 0.0f, //Top Left, Red
                0.5f, 0.5f, 333.0f, 1f, 1f, 0f, //Top Right, Yellow
                -0.5f, -0.5f, 333.0f, 0f, 0f, 1f, //Bottom Left, Blue
                //Right Triangle
                0.5f, 0.5f, 333.0f, 1.0f, 1f, 0f, //Top Right, Yellow
                0.5f, -0.5f, 333.0f, 0f, 1f, 0f, //Bottom Right, Green
                -0.5f, -0.5f, 333.0f, 0f, 0f, 1f, //Bottom Left, Blue
            };

            fixed (float* v = &vertices[0])
            {
                glBufferData(GL_ARRAY_BUFFER, sizeof(float) * vertices.Length, v, GL_STATIC_DRAW);
            }

            //Vertex Position Data:
            glVertexAttribPointer(0, 2, GL_FLOAT, false, 6 * sizeof(float), (void*)0);
            glEnableVertexAttribArray(0);
            //Vertex Color Data:
            glVertexAttribPointer(1, 3, GL_FLOAT, false, 6 * sizeof(float), (void*)(3 * sizeof(float)));
            glEnableVertexAttribArray(1);

            glBindBuffer(GL_ARRAY_BUFFER, 0);
            glBindVertexArray(0);

            camera = new Camera3D(DisplayManager.WindowSize / 2.0f, 2.5f);
        }

        protected override void Update()
        {

        }

        protected override void Render()
        {
            glClearColor(0.0f, 0.0f, 0.0f, 0.0f);
            glClear(GL_COLOR_BUFFER_BIT);

            Vector2 camPosition = new Vector2(300, 300);
            Vector2 camScale = new Vector2(150, 100);
            float camRotation = MathF.Sin(Gametime.TotalElapsedSeconds) * MathF.PI * 2.0f;

            Matrix4x4 translate = Matrix4x4.CreateTranslation(camPosition.X, camPosition.Y, 0.0f);
            Matrix4x4 scale = Matrix4x4.CreateScale(camScale.X, camScale.Y, 1.0f);
            Matrix4x4 rotate = Matrix4x4.CreateRotationZ(camRotation);
            rotate += Matrix4x4.CreateRotationX(camRotation);
            rotate -= Matrix4x4.CreateRotationY(camRotation);

            shader.SetMatrix4x4("model", scale * rotate * translate);

            //Apply basic shader to the vertices:
            shader.Use();
            shader.SetMatrix4x4("projection", camera.GetProjectionMatrix());

            glBindVertexArray(vao);
            glDrawArrays(GL_TRIANGLES, 0, 6);
            glBindVertexArray(0);

            Glfw.SwapBuffers(DisplayManager.Window);
        }
    }
}
