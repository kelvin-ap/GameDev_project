using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameDev_project
{
    class Character
    {
        private SpriteBatch _spriteBatch;
        private Texture2D stilstaan;
        private Texture2D rennen;
        private Texture2D springen;
        private Texture2D slaan;
        private Texture2D texture;
        private Vector2 positie = new Vector2(64, 384);
        private Vector2 snelheid;
        private Rectangle rectangle;
        private Rectangle deelRectangle;
        private int schuifop = 0;

        private bool jumped = false;


        public Vector2 Positie
        {
            get { return positie; }
        }
        public Character() { }
        public void Initialize()
        {
            deelRectangle = new Rectangle(schuifop, 0, 16, 32);
        }
        public void Load(ContentManager Content)
        {
            texture = Content.Load<Texture2D>("run");

            /*
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                texture = Content.Load<Texture2D>("run");
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                texture = Content.Load<Texture2D>("run");
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                texture = Content.Load<Texture2D>("jump");
            }
            else
            {
                texture = Content.Load<Texture2D>("idle");
            }
            */
        }
        public void Update(GameTime gameTime)
        {
            positie += snelheid;
            rectangle = new Rectangle((int)positie.X, (int)positie.Y, texture.Width, texture.Height);

            Input(gameTime);

            if (snelheid.Y < 10)
            {
                snelheid.Y += 0.4f;
            }
        }
        private void Input(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                snelheid.X = (float)gameTime.ElapsedGameTime.TotalMilliseconds / 3;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                snelheid.X = -(float)gameTime.ElapsedGameTime.TotalMilliseconds / 3;
            }
            else
            {
                snelheid.X = 0f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up) && jumped == false)
            {
                positie.Y -= 5f;
                snelheid.Y = -8f;
                jumped = true;
            }
        }
        public void Collision(Rectangle newRectangle, int xOffset, int yOffset)
        {
            if (rectangle.TouchTopOf(newRectangle))
            {
                rectangle.Y = newRectangle.Y - rectangle.Height;
                snelheid.Y = 0f;
                jumped = false;
            }
            if (rectangle.touchLeftOf(newRectangle))
            {
                positie.X = newRectangle.X - rectangle.Width - 2;
            }
            if (rectangle.touchRightOf(newRectangle))
            {
                positie.X = newRectangle.X + newRectangle.Width + 2;
            }
            if (rectangle.touchBottomOf(newRectangle))
            {
                snelheid.Y = 1f;
            }

            if (positie.X < 0)
            {
                positie.X = 0;
            }
            if (positie.X > xOffset - rectangle.Width)
            {
                positie.X = xOffset - rectangle.Width;
            }
            if (rectangle.Y < 0)
            {
                snelheid.Y = 1f;
            }
            if (positie.Y > yOffset - rectangle.Height)
            {
                positie.Y = yOffset - rectangle.Height;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, positie, Color.White);
            
            schuifop += 16;
            if (schuifop > 64)
            {
                schuifop = 0;
            }
            deelRectangle.X = schuifop;
        }
    }
}
