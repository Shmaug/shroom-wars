using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace shroom_wars
{
    class Map
    {
        private static int infoUIPos = Main.screenHeight;
        private static Vector2 playBtn;
        private static Village dragStart;

        public static List<Map> maps = new List<Map>();
        public static Vector2 cameraPosition;
        public static bool mapSelected;
        public static Map mapLoaded;

        public Vector2 mapPos; // position of icon on storyMap
        public List<Village> villages;
        public int stars; // score (in stars)
        public string name;
        public bool selected;
        private float iconRot;

        public Map(Vector2 mapPos, List<Village> villages, string name)
        {
            this.mapPos = mapPos;
            this.villages = villages;
            this.name = name;
            maps.Add(this);
        }

        private void Load()
        {
            Village.villages = this.villages;
            Guy.guys.Clear();
        }

        public static void Update(GameTime gameTime)
        {
            if (Main.gameState == gameState.inGame)
                UpdateInGame(gameTime);
            else
                UpdateMapScreen(gameTime);
        }

        private static void UpdateInGame(GameTime gameTime)
        {
            foreach (Village v in Village.villages)
            {
                if (Vector2.Distance(Input.currentPos + cameraPosition, v.position) < Main.villageTexture.Width * .5f)
                {
                    if (!Input.lastPress && Input.currentPress)
                        dragStart = v;
                    else if (Input.lastPress && !Input.currentPress)
                    {
                        dragStart.sendPeople(v);
                        dragStart = null;
                    }
                }
            }
            if (Input.currentPress && Input.lastPress && dragStart == null)
                cameraPosition -= Input.currentPos - Input.lastPos;
        }

        private static void UpdateMapScreen(GameTime gameTime)
        {
            mapSelected = false;
            Map sel = null;
            foreach (Map m in maps)
            {
                // rotate selected map
                if (m.selected)
                {
                    m.iconRot += MathHelper.ToRadians(30) * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    mapSelected = true;
                    sel = m;
                }
                else
                    m.iconRot = 0;

                // detect click
                if (Input.currentPress && !Input.lastPress)
                    if (Vector2.Distance(Input.currentPos, new Vector2(Main.screenWidth / 2, infoUIPos + Main.playBtnTexture.Height)) >= Main.playBtnTexture.Width / 2)
                        m.selected = Vector2.Distance(Input.currentPos + cameraPosition, m.mapPos) <= 64;
            }

            if (Input.currentPress && Input.lastPress)
                cameraPosition -= Input.currentPos - Input.lastPos;

            if (mapSelected && Input.lastPress && !Input.currentPress && Vector2.Distance(Input.currentPos, new Vector2(Main.screenWidth / 2, infoUIPos + Main.playBtnTexture.Height)) < Main.playBtnTexture.Width / 2)
            {
                Main.gameState = gameState.inGame;
                cameraPosition = Vector2.Zero;
                mapLoaded = sel;
                sel.Load();
            }
        }
        
        public static void drawMaps(SpriteBatch spriteBatch)
        {
            // draw ground
            for (int x = (int)(Math.Floor(cameraPosition.X / Main.grassTexture.Width) * Main.grassTexture.Width - cameraPosition.X); x < Main.screenWidth; x += Main.grassTexture.Width)
                for (int y = (int)(Math.Floor(cameraPosition.Y / Main.grassTexture.Height) * Main.grassTexture.Height - cameraPosition.Y); y < Main.screenHeight; y += Main.grassTexture.Height)
                    spriteBatch.Draw(Main.grassTexture, new Rectangle(x, y, Main.grassTexture.Width, Main.grassTexture.Height), null, Color.White);


            Map selected = null;
            foreach (Map m in maps)
            {
                if (m.selected)
                    selected = m;

                // draw dashed circle
                spriteBatch.Draw(Main.circleTexture, m.mapPos - cameraPosition, null, (m.stars != 0) ? Color.Green : Color.Red, m.iconRot, new Vector2(Main.circleTexture.Width, Main.circleTexture.Height) * .5f, (m.selected) ? 1.25f : 1f, SpriteEffects.None, 0f);
                // draw map icon
                spriteBatch.Draw(Main.mapTexture, m.mapPos - cameraPosition, null, Color.White, 0f, new Vector2(Main.mapTexture.Width, Main.mapTexture.Height) * .5f, (m.selected) ? 1.25f : 1f, SpriteEffects.None, 0f);
            }
            
            if (mapSelected)
            {
                if (infoUIPos > Main.screenHeight - 200)
                    infoUIPos -= 10;
            }
            else
                if (infoUIPos < Main.screenHeight)
                    infoUIPos += 10;

            SpriteEffects fx = SpriteEffects.None;
            if (Input.currentPress && Vector2.Distance(Input.currentPos, new Vector2(Main.screenWidth / 2, infoUIPos + Main.playBtnTexture.Height)) < Main.playBtnTexture.Width / 2)
                fx = SpriteEffects.FlipVertically;

            spriteBatch.Draw(Main.playBtnTexture, new Vector2(Main.screenWidth / 2, infoUIPos + Main.playBtnTexture.Height), null, Color.White, 0f, new Vector2(Main.playBtnTexture.Width, Main.playBtnTexture.Height) * .5f, 1f, fx, 0f);
            if (selected != null)
            {
                spriteBatch.DrawString(Main.fonts[3], selected.name, new Vector2(Main.screenWidth / 2 + 2, infoUIPos), Color.Black * .5f, 0f, Main.fonts[1].MeasureString(selected.name) * new Vector2(.5f, -.5f), 1f, SpriteEffects.None, 0f);
                spriteBatch.DrawString(Main.fonts[3], selected.name, new Vector2(Main.screenWidth / 2 - 2, infoUIPos), Color.Black * .5f, 0f, Main.fonts[1].MeasureString(selected.name) * new Vector2(.5f, -.5f), 1f, SpriteEffects.None, 0f);
                spriteBatch.DrawString(Main.fonts[3], selected.name, new Vector2(Main.screenWidth / 2, infoUIPos + 2), Color.Black * .5f, 0f, Main.fonts[1].MeasureString(selected.name) * new Vector2(.5f, -.5f), 1f, SpriteEffects.None, 0f);
                spriteBatch.DrawString(Main.fonts[3], selected.name, new Vector2(Main.screenWidth / 2, infoUIPos - 2), Color.Black * .5f, 0f, Main.fonts[1].MeasureString(selected.name) * new Vector2(.5f, -.5f), 1f, SpriteEffects.None, 0f);

                spriteBatch.DrawString(Main.fonts[3], selected.name, new Vector2(Main.screenWidth / 2, infoUIPos), Color.White, 0f, Main.fonts[1].MeasureString(selected.name) * new Vector2(.5f, -.5f), 1f, SpriteEffects.None, 0f);
            }
        }
    }
}
