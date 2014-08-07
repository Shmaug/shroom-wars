using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

// this class for easy porting between platforms
namespace shroom_wars
{
    static class Input
    {
        public static Vector2 currentPos;
        public static Vector2 lastPos;
        public static bool currentPress;
        public static bool lastPress;

        public static void Update()
        {
            lastPos = currentPos;
            lastPress = currentPress;
            currentPos = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
            currentPress = Mouse.GetState().LeftButton == ButtonState.Pressed;
        }
    }
}
