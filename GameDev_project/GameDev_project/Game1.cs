using GameDev_project.Collision;
using GameDev_project.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace GameDev_project
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D achtergrond;

        // Updated upstream
        Camera camera;

        Map map;
        Character character;

        private Texture2D texture;
        private Hero hero;
        CollisionManager collisionManager;
        // Stashed changes

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            map = new Map();
            character = new Character();
            //character.Initialize();
            base.Initialize();
            //updated
            //hero = new Hero(texture, new KeyBoardReader());
            collisionManager = new CollisionManager();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Tiles.Content = Content;
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
                {2,2,2,2,4,4,4,4,8,0,0,9,4,4,4,4,4,4,4,2,2,0,0,2,0,0,2,0,0,0,2,0,0,0,2,0,0,0,0,2,2,2,2,2},
            }, 64);
            character.Load(Content);
            InitializeGameObjects();
        }

        private void InitializeGameObjects()
        {
            hero = new Hero(texture, new KeyBoardReader());
        }

        protected override void Update(GameTime gameTime)
        {
            character.Update(gameTime);
            foreach (CollisionTiles tile in map.CollisionTiles)
            {
                character.Collision(tile.Rectangle, map.Width, map.Height);
                camera.Update(character.Positie, map.Width, map.Height);
            }

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            hero.Update(gameTime);
            //blok.update()

            /*if (collisionManager.CheckCollision(hero.CollisionRectangle, blok.CollisionRectangle))
            {
                Debug.WriteLine("botsing");
            }*/

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