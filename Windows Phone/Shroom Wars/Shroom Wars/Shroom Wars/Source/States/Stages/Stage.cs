using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Shroom_Wars
{
    class Stage : State
    {
        Viewport viewport;

        Texture2D grass;
        Rectangle grassRectangle;
        

        public Stage(GraphicsDeviceManager g, ContentManager c, Viewport v) : base(g, c, v)
        {
            viewport = v;
            grass = c.Load<Texture2D>("grass");
            grassRectangle = new Rectangle(0, 0, viewport.Height / 512, viewport.Height / 512);
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState state = Keyboard.GetState();

            //if (something)
            //    return new GameState(graphics, content, viewport);
        }

        public override void Draw(GameTime gameTime, SpriteBatch s)
        {
            s.Begin();
                for (int x = 0; x < viewport.Width / 64; ++)
                {
                    for (int y = 0; y < viewport.Height / 64; y++)
                    {
                        s.Draw(grass, new Rectangle(x * 64, y * 64, grassRectangle.Width, grassRectangle.Height), Color.Green);
                    }
                }
            s.End();
        }
    }
}
