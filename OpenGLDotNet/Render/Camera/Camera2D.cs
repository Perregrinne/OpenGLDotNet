using OpenGLDotNet.Render.Display;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace OpenGLDotNet.Render.Camera2D
{
    class Camera2D
    {
        public Vector2 FocusPosition { get; set; }
        public float Zoom { get; set; }

        public Camera2D(Vector2 focusPosition, float zoom)
        {
            this.FocusPosition = focusPosition;
            this.Zoom = zoom;
        }

        public Matrix4x4 GetProjectionMatrix()
        {
            float left = FocusPosition.X - DisplayManager.WindowSize.X / 2.0f;
            float right = FocusPosition.X + DisplayManager.WindowSize.X / 2.0f;
            float top = FocusPosition.Y - DisplayManager.WindowSize.Y / 2.0f;
            float bottom = FocusPosition.Y + DisplayManager.WindowSize.Y / 2.0f;

            //Orthographic matrix: the last two elements are near plane and far plane!
            Matrix4x4 orthoMatrix = Matrix4x4.CreateOrthographicOffCenter(left, right, bottom, top, 0.01f, 100.0f);
            Matrix4x4 zoomMatrix = Matrix4x4.CreateScale(Zoom);

            return orthoMatrix * zoomMatrix;
        }
    }
}
