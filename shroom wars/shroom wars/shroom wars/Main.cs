using System;
using System.Collections.Generic;
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
        inGame,
        mapCompleted
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
        public static Texture2D circleTexture;
        public static Texture2D mapTexture;
        public static Texture2D playBtnTexture;

        public static SpriteFont[] fonts = new SpriteFont[4];

        public static gameState gameState = gameState.mainMenu;

        public Main()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.PreferredBackBufferHeight = screenHeight;
            graphics.PreferMultiSampling = true;
            this.IsMouseVisible = true;
            try
            {
                IntPtr hWnd = this.Window.Handle;
                var control = System.Windows.Forms.Control.FromHandle(hWnd);
                var form = control.FindForm();
                form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                form.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            }
            catch { }
        }

        protected override void Initialize()
        {

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            grassTexture = Content.Load<Texture2D>("tex/grass");
            dirtTexture = Content.Load<Texture2D>("spr/dirt");
            villageTexture = Content.Load<Texture2D>("spr/village");
            guyTexture = Content.Load<Texture2D>("spr/guy");
            circleTexture = Content.Load<Texture2D>("ui/circle");
            mapTexture = Content.Load<Texture2D>("ui/map");
            playBtnTexture = Content.Load<Texture2D>("ui/play");

            for (int i = 0; i < fonts.Length; i++)
                fonts[i] = Content.Load<SpriteFont>("fon/font" + i);

            #region maps
            List<Village> v1 = new List<Village>();
            v1.Add(new Village(new Vector2(300, 100), 1, Color.Red, 10));
            v1.Add(new Village(new Vector2(300, 100), 1, Color.Blue, 10));
            Map.maps.Add(new Map(new Vector2(256, 200), v1, "Tutorial"));

            List<Village> v2 = new List<Village>();
            v2.Add(new Village(new Vector2(50, 100), 1, Color.Red, 10));
            v2.Add(new Village(new Vector2(150, 100), 1, Color.Red, 10));
            v2.Add(new Village(new Vector2(300, 100), 1, Color.Blue, 10));

            Map.maps.Add(new Map(new Vector2(340, 500), v2, "First Base"));
            #endregion
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            Input.UpdateBefore();
            switch (gameState)
            {
                case gameState.mainMenu:
                    {
                        if (Input.currentPress && !Input.lastPress)
                        {
                            gameState = gameState.storyMap;
                        }
                        break;
                    }
                case gameState.storyMap:
                    {
                        Map.Update(gameTime);
                        break;
                    }
                case gameState.inGame:
                    {
                        Village.Update(gameTime);
                        Guy.Update(gameTime);
                        break;
                    }
            }
            Input.UpdateAfter();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            switch (gameState)
            {
                case gameState.mainMenu:
                    {
                        spriteBatch.DrawString(fonts[1], "Tap anywhere to start", new Vector2(screenWidth, screenHeight) * .5f, Color.White, 0f, fonts[1].MeasureString("Tap anywhere to start") * .5f, 1f, SpriteEffects.None, 0f);
                        break;
                    }
                case gameState.storyMap:
                    {
                        Map.drawMaps(spriteBatch);
                        break;
                    }
                case gameState.inGame:
                    {
                        // draw ground
                        for (int x = (int)(Math.Floor(Map.cameraPosition.X / Main.grassTexture.Width) * Main.grassTexture.Width - Map.cameraPosition.X); x < Main.screenWidth; x += Main.grassTexture.Width)
                            for (int y = (int)(Math.Floor(Map.cameraPosition.Y / Main.grassTexture.Height) * Main.grassTexture.Height - Map.cameraPosition.Y); y < Main.screenHeight; y += Main.grassTexture.Height)
                                spriteBatch.Draw(Main.grassTexture, new Rectangle(x, y, Main.grassTexture.Width, Main.grassTexture.Height), null, Color.White);
                        spriteBatch.End();

                        // draw game objects in order of bottom to top
                        spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
                        Village.Draw(spriteBatch);
                        Guy.Draw(spriteBatch);
                        break;
                    }
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
