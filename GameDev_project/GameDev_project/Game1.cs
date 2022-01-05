using GameDev_project.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace GameDev_project
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public Texture2D texture;
        private Texture2D achtergrond;

        //GameGlobals global;
        Camera camera;
        Map map;
        Character character;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            map = new Map();
            //character.Initialize();
            //character = new Character(texture);
            base.Initialize();
        }
        private void InitializeGameObjects()
        {
            character = new Character(texture);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Tiles.Content = Content;

            texture = Content.Load<Texture2D>("idle");
            achtergrond = Content.Load<Texture2D>("background");
            camera = new Camera(GraphicsDevice.Viewport);

            map.Generate(new int[,]{
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,2,2,2,3,0,0,1,2,2,2,2,2,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,2,4,4,4,8,0,0,9,4,4,4,4,4,4,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {2,2,2,2,4,4,4,4,8,0,0,9,4,4,4,4,4,4,4,2,2,0,0,2,0,0,2,0,0,0,2,0,0,0,2,0,0,0,2,2,2,2,2,2},
            }, 64);

            InitializeGameObjects();
            character.Load(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            character.Update(gameTime);
            foreach (CollisionTiles tile in map.CollisionTiles)
            {
                character.Collision(tile.Rectangle, map.Width, map.Height);
                camera.Update(character.Positie, map.Width, map.Height);
            }

            //nog te verplaatsen
            if(character.snelheid.X < 0)
                texture = Content.Load<Texture2D>("runLeft");
            else if (character.snelheid.X > 0)
                texture = Content.Load<Texture2D>("run");

            if (character.snelheid.X < 0 && character.jumped == true)
                texture = Content.Load<Texture2D>("jumpLeft");
            else if ((character.snelheid.X >= 0 && character.jumped == true) || character.jumped == true)
                texture = Content.Load<Texture2D>("jump");

            if (character.snelheid.X == 0 && character.jumped == false)
                texture = Content.Load<Texture2D>("idle");

            //load finish
            if(character.Positie.X == 2797)
                throw new Exception("finsih");

            //load GameOver screen
            if (character.health == 0)
                throw new Exception("Dood");
            //Time.timeScale = 0;

            character.sprite = texture;
            //LoadContent();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null,
                camera.Transform);
            _spriteBatch.Draw(achtergrond, new Vector2(0, -520), Color.White);
            _spriteBatch.Draw(achtergrond, new Vector2(1920, -520), Color.White);
            map.Draw(_spriteBatch);
            character.Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}