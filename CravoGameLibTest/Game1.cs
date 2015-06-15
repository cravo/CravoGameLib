using CravoGameLib.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CravoGameLibTest
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        DataManager dataManager;
        Texture2D texture;
        Vector2 position = Vector2.Zero;
        Vector2 velocity = Vector2.One * 128.0f;
        CravoGameLib.TileMap.Map map;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            dataManager = new DataManager("GameData", GraphicsDevice);

            texture = dataManager.Load<Texture2D>("test");

            map = new CravoGameLib.TileMap.Map();
            map.Load("GameData\\TileMap\\desert.tmx", GraphicsDevice);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here
            position += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if ( position.X > GraphicsDevice.Viewport.Width - texture.Width )
            {
                position = new Vector2(GraphicsDevice.Viewport.Width - texture.Width, position.Y);
                velocity = new Vector2(-velocity.X, velocity.Y);
            }
            if (position.X < 0)
            {
                position = new Vector2(0, position.Y);
                velocity = new Vector2(-velocity.X, velocity.Y);
            }
            if (position.Y > GraphicsDevice.Viewport.Height - texture.Height)
            {
                position = new Vector2(position.X, GraphicsDevice.Viewport.Height - texture.Height);
                velocity = new Vector2(velocity.X, -velocity.Y);
            }
            if (position.Y < 0)
            {
                position = new Vector2(position.X, 0);
                velocity = new Vector2(velocity.X, -velocity.Y);
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            map.Draw();

            spriteBatch.Begin();
            spriteBatch.Draw(texture, position, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
