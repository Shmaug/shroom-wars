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
        public static List<Map> maps = new List<Map>();

        public Vector2 mapPos; // position of icon on storyMap
        public List<Village> villages;
        public int stars; // score (in stars)
        public bool selected;
        private float iconRot;

        public Map(Vector2 mapPos, List<Village> villages)
        {
            this.mapPos = mapPos;
            this.villages = villages;
            maps.Add(this);
        }

        public static void Update(GameTime gameTime)
        {
            foreach (Map m in maps)
            {
                // rotate selected map
                if (m.selected)
                    m.iconRot += MathHelper.ToRadians(30) * (float)gameTime.ElapsedGameTime.TotalSeconds;
                else
                    m.iconRot = 0;

                // detect click
                if (Input.currentPress && !Input.lastPress)
                    m.selected = Vector2.Distance(Input.currentPos, m.mapPos) <= 64;
            }
            //if (c && !s)
                //selectedMap = null;
        }
        
        public static void drawMaps(SpriteBatch spriteBatch)
        {
            foreach (Map m in maps)
            {
                // draw dashed circle
                spriteBatch.Draw(Main.circleTexture, m.mapPos, null, (m.stars != 0) ? Color.Green : Color.Red, m.iconRot, new Vector2(Main.circleTexture.Width, Main.circleTexture.Height) * .5f, (m.selected) ? 1.25f : 1f, SpriteEffects.None, 0f);
                spriteBatch.Draw(Main.mapTexture, m.mapPos, null, Color.White, 0f, new Vector2(Main.mapTexture.Width, Main.mapTexture.Height) * .5f, (m.selected) ? 1.25f : 1f, SpriteEffects.None, 0f);
            }
        }
    }
}
