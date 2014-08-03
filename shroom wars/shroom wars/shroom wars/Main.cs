using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace shroom_wars
{
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

            Village v1 = new Village(new Vector2(screenWidth * .25f, screenHeight * .3f), 1, Color.Blue, 10);
            Village v2 = new Village(new Vector2(screenWidth * .75f, screenHeight * .6f), 2, Color.Green, 10);
            Village v3 = new Village(new Vector2(screenWidth * .9f, screenHeight * .6f), 2, Color.Green, 10);
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            Village.Update(gameTime);
            guy.Update(gameTime);

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                Village.villages[1].sendPeople(Village.villages[0]);
                Village.villages[2].sendPeople(Village.villages[0]);
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

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
            guy.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
