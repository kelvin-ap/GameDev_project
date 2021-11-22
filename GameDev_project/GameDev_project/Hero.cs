using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameDev_project
{
    class Hero: IGameObject
    {
        private Texture2D texture;
        Animation animation;
        private Rectangle _deelRectangleChar;        
        private int schuifOp_X = 0;

        public Hero(Texture2D texture)
        {
            this.texture = texture;
            animation = new Animation();
            animation.GetFrameFromProperties(texture.Width, texture.Height, 5, 2);

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Vector2(0, 0), animation.CurrenFrame.SourceRectangle, Color.White);
        }
        public void Update()
        {
            animation.Update();
        }
    }
}
