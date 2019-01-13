using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game;
using Microsoft.Xna.Framework.Content;

namespace NUKE.SCREENS
{
    //nothing presently being used forXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
    //DEAD
    class help : IScreens
    {
        public help(ContentManager content)
        {

        }
        public NUKE.Game1.GameState update(Microsoft.Xna.Framework.GameTime gametime)
        {
            return Game1.GameState.HELP_MENU;
        }

        public void Draw(Microsoft.Xna.Framework.GameTime gametime, Microsoft.Xna.Framework.Graphics.SpriteBatch spritebatch)
        {
            spritebatch.Begin();
            spritebatch.End();
        }

    }
}
