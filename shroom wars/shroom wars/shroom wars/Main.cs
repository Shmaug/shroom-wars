using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace shroom_wars
{
    public enum gameState
    {
        mainMenu,
        storyMap,
        inGame
    }

    public class Main : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public static int screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        public static int screenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

        public static Texture2D grassTexture;
        public static Texture2D guyTexture;
        public static Texture2D villageTexture;
        public static Texture2D dirtTexture;
        public static SpriteFont font0;

        public static gameState gameState = gameState.mainMenu;

        public Main()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.PreferredBackBufferHeight = screenHeight;
            graphics.PreferMultiSampling = true;
            this.IsMouseVisible = true;
        }

        protected override void Initialize()
        {

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            grassTexture = Content.Load<Texture2D>("grass");
            dirtTexture = Content.Load<Texture2D>("dirt");
            villageTexture = Content.Load<Texture2D>("village");
            guyTexture = Content.Load<Texture2D>("guy");
            font0 = Content.Load<SpriteFont>("font0");
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            Village.Update(gameTime);
            Guy.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            switch (gameState)
            {
                case gameState.mainMenu:
                    {

                        break;
                    }
                case gameState.inGame:
                    {
                        spriteBatch.Begin();
                        for (int x = 0; x < screenWidth; x += grassTexture.Width)
                        {
                            for (int y = 0; y < screenWidth; y += grassTexture.Width)
                            {
                                spriteBatch.Draw(grassTexture, new Rectangle(x, y, 64, 64), null, Color.White);
                            }
                        }
                        spriteBatch.End();

                        spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
                        Village.Draw(spriteBatch);
                        Guy.Draw(spriteBatch);
                        spriteBatch.End();
                        break;
                    }
            }

            base.Draw(gameTime);
        }
    }
}
