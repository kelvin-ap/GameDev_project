using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameDev_project
{
    public class Camera
    {
        private Matrix transform;
        public Matrix Transform
        {
            get { return transform; }
        }
        private Vector2 center;
        private Viewport viewport;

        public Camera(Viewport newViewport)
        {
            viewport = newViewport;
        }
        public void Update(Vector2 positie, int xOffset, int Yoffset)
        {
            if (positie.X < viewport.Width / 2)
            {
                center.X = viewport.Width / 2;
            }
            else if (positie.X > xOffset - (viewport.Width / 2))
            {
                center.X = xOffset - (viewport.Width / 2);
            }
            else
            {
                center.X = positie.X;
            }

            if (positie.Y < viewport.Height / 2)
            {
                center.Y = viewport.Height / 2;
            }
            else if (positie.Y > Yoffset - (viewport.Height / 2))
            {
                center.Y = Yoffset - (viewport.Height / 2);
            }
            else
            {
                center.Y = positie.Y;
            }
            transform = Matrix.CreateTranslation(new Vector3(-center.X + (viewport.Width / 2),
                    -center.Y + (viewport.Height / 2), 0));
        }
    }
}
