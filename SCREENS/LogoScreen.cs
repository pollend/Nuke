using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game;
using Microsoft.Xna.Framework.Content;

namespace NUKE.SCREENS
{
//Dead class////////////////////////////////////////
    //was for logo but is placed in the Menu class
    class LogoScreen: IScreens
    {
        public LogoScreen(ContentManager content)
        {

        }
        public NUKE.Game1.GameState update(Microsoft.Xna.Framework.GameTime gametime)
        {
            return Game1.GameState.MENU_SCREEN;
        }

        public void Draw(Microsoft.Xna.Framework.GameTime gametime, Microsoft.Xna.Framework.Graphics.SpriteBatch spritebatch)
        {
            throw new NotImplementedException();
        }

    }
}
