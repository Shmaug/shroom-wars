using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace shroom_wars
{
    class guy
    {
        public static List<guy> guys = new List<guy>();

        public Village destination;
        public Vector2 pos;
        public Color color;

        private float time;
        private float timer = 0f;
        private Vector2 start;
        private Vector2 offset;

        public guy(Vector2 pos, Village dest, Color col, Vector2 offset)
        {
            this.pos = pos;
            this.destination = dest;
            this.color = col;
            guys.Add(this);
            this.start = this.pos;
            this.time = Vector2.Distance(this.pos, this.destination.position + this.offset) / 125f;
            this.offset = offset;
        }

        public static void Update(GameTime time)
        {
            List<guy> remove = new List<guy>();
            foreach (guy g in guys)
            {
                g.timer += (float)time.ElapsedGameTime.TotalSeconds;
                g.pos = Vector2.Lerp(g.start, g.destination.position + g.offset, g.timer / g.time);
                if (g.timer >= g.time)
                {
                    remove.Add(g);
                    if (g.destination.color == g.color)
                        g.destination.guys++;
                    else
                    {
                        if (g.destination.guys > 0)
                            g.destination.guys--;
                        else
                        {
                            g.destination.color = g.color;
                            g.destination.guys++;
                        }
                    }
                }
            }

            foreach (guy g in remove)
            {
                guys.Remove(g);
            }
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            foreach (guy g in guys)
            {
                spriteBatch.Draw(Main.guyTexture, g.pos, null, g.color, 0f, new Vector2(Main.guyTexture.Width * .5f, Main.guyTexture.Height), 1f, SpriteEffects.None, (Main.screenHeight - g.pos.Y) / (float)Main.screenHeight);
            }
        }
    }
}
