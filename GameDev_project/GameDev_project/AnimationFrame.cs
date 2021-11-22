using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameDev_project
{
    class AnimationFrame
    {
        public Rectangle SourceRectangle { get; set; }
        public AnimationFrame(Rectangle sourceRectangle)
        {
            sourceRectangle = SourceRectangle;
        }
    }
}
