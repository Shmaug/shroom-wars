using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace shroom_wars
{
    class Village
    {
        public static List<Village> villages = new List<Village>();

        public Vector2 position;
        public int level;
        public float productionRate;
        public float productionTimer;
        public Color color;
        public int guys;

        public Village(Vector2 pos, int level, Color col, int guys)
        {
            this.position = pos;
            this.level = level;
            this.productionRate = 1f / level * 3;
            this.color = col;
            this.guys = guys;
        }

        public void sendPeople(Village dest)
        {
            if (this.guys <= 0)
                return;
            Random r = new Random();
            int g = this.guys;
            
            for (int i = 0; i < g; i++)
            {
                if (this.guys - 1 >= 0)
                {
                    Vector2 off = new Vector2(r.Next(-100, 100), r.Next(-100, 100)); // randomly offset so guys dont bunch up
                    new Guy(this.position + off, dest, this.color, off);
                    this.guys--;
                }
                else
                    break;
            }
        }

        public static void Update(GameTime gameTime)
        {
            // add guys based off production rate
            foreach (Village v in villages)
            {
                v.productionTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (v.productionTimer > v.productionRate)
                {
                    v.productionTimer = 0f;
                    v.guys++;
                }
            }
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            foreach (Village v in villages)
            {
                spriteBatch.Draw(Main.villageTexture, v.position - Map.cameraPosition, null, v.color, 0f, new Vector2(Main.villageTexture.Width * .5f, Main.villageTexture.Height), 1f, SpriteEffects.None, (Main.screenHeight - v.position.Y) / (float)Main.screenHeight);
                spriteBatch.Draw(Main.dirtTexture, v.position - Map.cameraPosition, null, Color.White, 0f, new Vector2(Main.dirtTexture.Width, Main.dirtTexture.Height) * .5f, 1f, SpriteEffects.None, (Main.screenHeight - v.position.Y + 1f) / (float)Main.screenHeight);
                Vector2 o = Main.fonts[0].MeasureString(v.guys.ToString()) * .5f;
                //outline
                spriteBatch.DrawString(Main.fonts[0], v.guys.ToString(), v.position - Map.cameraPosition + new Vector2(2, -Main.villageTexture.Height), Color.Black, 0f, o, 1f, SpriteEffects.None, (Main.screenHeight - v.position.Y) / (float)Main.screenHeight);
                spriteBatch.DrawString(Main.fonts[0], v.guys.ToString(), v.position - Map.cameraPosition + new Vector2(-2, -Main.villageTexture.Height), Color.Black, 0f, o, 1f, SpriteEffects.None, (Main.screenHeight - v.position.Y) / (float)Main.screenHeight);
                spriteBatch.DrawString(Main.fonts[0], v.guys.ToString(), v.position - Map.cameraPosition + new Vector2(0, -Main.villageTexture.Height + 2), Color.Black, 0f, o, 1f, SpriteEffects.None, (Main.screenHeight - v.position.Y) / (float)Main.screenHeight);
                spriteBatch.DrawString(Main.fonts[0], v.guys.ToString(), v.position - Map.cameraPosition + new Vector2(0, -Main.villageTexture.Height - 2), Color.Black, 0f, o, 1f, SpriteEffects.None, (Main.screenHeight - v.position.Y) / (float)Main.screenHeight);
                spriteBatch.DrawString(Main.fonts[0], v.guys.ToString(), v.position - Map.cameraPosition + new Vector2(0, -Main.villageTexture.Height), Color.White, 0f, o, 1f, SpriteEffects.None, (Main.screenHeight - v.position.Y - 1f) / (float)Main.screenHeight);
            }
        }
    }
}
