using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection.Metadata;
using System.Text;
using SharpDX.Direct3D9;


namespace GameDev_project
{
    class Character
    {
        //private SpriteBatch _spriteBatch;
        //private Texture2D stilstaan;
        //private Texture2D rennen;
        //private Texture2D springen;
        //private Texture2D slaan;
        //GameGlobals global;

        Animation animation;
        private Texture2D texture;
        private Vector2 positie = new Vector2(64, 384);
        public Vector2 snelheid;
        public Rectangle CollisionRectangle { get; set; }
        private Rectangle _collisionRectangle;
        private Rectangle charRectangle;
        private int schuifop = 0;

        public bool jumped = false;

        //moet in GameGlobals
        public Texture2D sprite;
        public int health;

        public Vector2 Positie
        {
            get { return positie; }
        }

        public Character(Texture2D texture)
        {
            this.texture = texture;
            health = 100;
            animation = new Animation();
            animation.GetFrameFromTextureProperties(texture.Width, texture.Height, 4, 1);
            charRectangle = new Rectangle(schuifop, 0, 16, 32);
        }

        public void Initialize()
        {
            charRectangle = new Rectangle(schuifop, 0, 16, 32);
        }

        public void Load(ContentManager Content)
        {
            
        }

        public void Update(GameTime gameTime)
        {
            positie += snelheid;
            _collisionRectangle = new Rectangle((int)positie.X, (int)positie.Y, 18, 32);

            Input(gameTime);

            if (snelheid.Y < 10)
            {
                snelheid.Y += 0.4f;
            }
            animation.Update(gameTime);

            //dood
            if (positie.Y >= 488)
                health = 0;

            //testing
            Debug.WriteLine("X: " + positie.X);
            Debug.WriteLine("Y: " + positie.Y);
        }

        //input nog veranderen naar keyBoardReader volgens SOLID principe
        private void Input(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Right) || Keyboard.GetState().IsKeyDown(Keys.D))
            {
                snelheid.X = (float)gameTime.ElapsedGameTime.TotalMilliseconds / 3;
                 
            }
            else if (positie.X >= 1 && (Keyboard.GetState().IsKeyDown(Keys.Left) || Keyboard.GetState().IsKeyDown(Keys.Q)))
            {
                snelheid.X = -(float)gameTime.ElapsedGameTime.TotalMilliseconds / 3;
            }
            else
            {
                snelheid.X = 0f;
            }
            if ((Keyboard.GetState().IsKeyDown(Keys.Up) || Keyboard.GetState().IsKeyDown(Keys.Z) || Keyboard.GetState().IsKeyDown(Keys.Space)) && jumped == false)
            {
                positie.Y -= 5f;
                snelheid.Y = -8f;
                jumped = true;
            }
        }

        public void Collision(Rectangle newRectangle, int xOffset, int yOffset)
        {
            if (_collisionRectangle.TouchTopOf(newRectangle))
            {
                _collisionRectangle.Y = newRectangle.Y - _collisionRectangle.Height;
                snelheid.Y = 0f;
                jumped = false;
            }
            if (_collisionRectangle.touchLeftOf(newRectangle))
            {
                positie.X = newRectangle.X - _collisionRectangle.Width - 2;
            }
            if (_collisionRectangle.touchRightOf(newRectangle))
            {
                positie.X = newRectangle.X + newRectangle.Width + 2;
            }
            if (_collisionRectangle.touchBottomOf(newRectangle))
            {
                snelheid.Y = 1f;
            }

            if (positie.X < 0)
            {
                positie.X = 0;
            }
            if (positie.X > xOffset - _collisionRectangle.Width)
            {
                positie.X = xOffset - _collisionRectangle.Width;
            }
            if (_collisionRectangle.Y < 0)
            {
                snelheid.Y = 1f;
            }
            if (positie.Y > yOffset - _collisionRectangle.Height)
            {
                positie.Y = yOffset - _collisionRectangle.Height;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (sprite == null)
                sprite = texture;

            spriteBatch.Draw(sprite, positie, animation.CurrenFrame.SourceRectangle, Color.White);
            
            schuifop += 16;
            if (schuifop > 64)
            {
                schuifop = 1;
            }
            _collisionRectangle.X = schuifop;
        }
    }
}
