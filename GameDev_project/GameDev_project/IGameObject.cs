using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameDev_project
{
    interface IGameObject
    {
        void Update();
        void Draw(SpriteBatch spriteBatch);
    }
}
