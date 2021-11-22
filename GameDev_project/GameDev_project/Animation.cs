using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameDev_project
{
    class Animation
    {
        public AnimationFrame CurrenFrame { get; set; }
        private List<AnimationFrame> frames;
        private int counter;

        public Animation()
        {
            frames = new List<AnimationFrame>();
        }
        public void AddFrame(AnimationFrame frame)
        {
            frames.Add(frame);
            CurrenFrame = frames[0];
        }
        public void Update()
        {
            CurrenFrame = frames[counter];
            counter++;
            if (counter >= frames.Count)
                counter = 0;
        }
        public void GetFrameFromProperties(int width, int height, int numberOfWidthSprites, int numberOfHeightSprites)
        {
            int widthOfFrame = width / numberOfWidthSprites;
            int heightOfFrame = height / numberOfHeightSprites;

            for (int y = 0; y <= height - heightOfFrame; y += heightOfFrame)
            {
                for (int x = 0; x <= width - widthOfFrame; x+= widthOfFrame)
                {
                    frames.Add(new AnimationFrame(new Rectangle(x, y, widthOfFrame, heightOfFrame)));
                }
            }
        }
    }
}
