using System;
using System.Collections.Generic;
using System.Text;
using GameDev_project.Input;
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
        private Vector2 versnelling;
        private Vector2 mouseVector;

        public Rectangle CollisionRectangle { get; set; }
        private Rectangle _collisionRectangle;

        IInputReader inputReader;


        public Hero(Texture2D texture, IInputReader reader)
        {
            this.texture = texture;
            animation = new Animation();
            animation.GetFrameFromTextureProperties(texture.Width, texture.Height, 10, 4);

            snelheid = new Vector2(1, 1);
            positie = new Vector2(0, 0);
            versnelling = new Vector2(0.1f, 0.1f);

            //Read input for my hero class
            this.inputReader = reader;

            //waarden aanpassen aan sprite
            _collisionRectangle = new Rectangle((int)positie.X, (int)positie.Y, 130, 385);
        }            

        public void Update(GameTime gameTime)
        {
            //geen scherm begrenzing, anders staat kan hero niet bewegen een dat deze aan de grens komt of kan achtergrond niet bewegen
            var direction = inputReader.ReadInput();
            direction *= 4;
            /*geeft bugs in code aan de grenzen en niet bruikzaam in deze context
            if (positie.X > 800 - 102 || positie.X < 0)
            {
                direction *= -1;
            }*/
            positie += direction;

            //Move(MoveWithMouse());
            animation.Update(gameTime);

            _collisionRectangle.X = (int)positie.X;
            CollisionRectangle = _collisionRectangle;
        }

        private Vector2 MoveWithMouse()
        {
            MouseState state = Mouse.GetState();
            mouseVector = new Vector2(state.X, state.Y);

            return mouseVector;
        }

        private void Move(Vector2 mouse)
        {
            var direction = Vector2.Add(mouse, -positie);
            direction.Normalize();
            direction = Vector2.Multiply(direction, 0.1f);

            snelheid += direction;
            snelheid = Limit(snelheid, 5);
            positie += snelheid;

            if (positie.X + snelheid.X > 800 - 102|| positie.X + snelheid.X < 0)
            {
                //snelheid.X *= -1;
                versnelling.X *= -1;
                snelheid = new Vector2(snelheid.X < 0 ? 1 : -1, snelheid.Y);
            }
            if (positie.Y + snelheid.Y < 0 || positie.Y + snelheid.Y > 480 - 129)
            {
                snelheid = new Vector2(snelheid.X, snelheid.Y < 0 ? 1 : -1);
                versnelling.Y *= -1;
            }
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
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, positie, animation.CurrenFrame.SourceRectangle, Color.White);
        }

    }
}
