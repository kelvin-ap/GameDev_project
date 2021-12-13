using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameDev_project
{
    class Hero : IGameObject
    {
        private Texture2D texture;
        Animation animation;
        private Vector2 positie;
        private Vector2 snelheid;
        Vector2 versnelling = new Vector2(0.1f, 0.1f);

        public Hero(Texture2D texture)
        {
            this.texture = texture;
            animation = new Animation();
            animation.GetFrameFromTextureProperties(texture.Width, texture.Height, 10, 4);
            snelheid = new Vector2(1, 1);
            positie = new Vector2(0, 0);

        }

        public void Draw(SpriteBatch spriteBatch)
        {            
            //positie = new Vector2(0,0);
            //snelheid = new Vector2(1, 1);
            spriteBatch.Draw(texture, positie, animation.CurrenFrame.SourceRectangle, Color.White);
        }

        public void Update(GameTime gameTime)
        {
            animation.Update(gameTime);
            Move();
        }
        private Vector2 Limit(Vector2 v, float max)
        {
            if (v.Length() > max)
            {
                var ratio = max / v.Length();
                v.X *= ratio;
                v.Y *= ratio;
            }
            return v;
        }
        private void Move()
        {
            positie += snelheid;
            snelheid += versnelling;
            float maximaleSnelheid = 10;
            snelheid = Limit(snelheid, maximaleSnelheid);

            if (positie.X > 800 - 102|| positie.X < 0)
            {
                snelheid.X *= -1;
                versnelling.X *= -1;

            }
            if (positie.Y < 0 || positie.Y > 480 - 129)
            {
                snelheid.Y *= -1;
                versnelling.Y *= -1;
            }
        }
    }
}
