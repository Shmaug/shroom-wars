using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;

namespace Shroom_Wars
{
    public class GameState : State
    {
        int stage = -1;
        List<Stage> stages;

        public GameState(GraphicsDeviceManager g, ContentManager c, Viewport v) : base(g, c, v)
        {
            stages = new List<Stage>();
            Stage stage1 = new Stage(g, c, v);
            stages.Add(stage1);
        }

        public override void Initialize()
        {
        }

        public override void LoadContent()
        {
        }

        public override State Update(GameTime gameTime)
        {
            if (stage == -1)
                return this;
            else
                return stages.ElementAt(stage);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.End();
        }
    }
}
