using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Game
{
    public interface IScreens
    {
        NUKE.Game1.GameState update(GameTime gametime);
        void Draw(GameTime gametime, SpriteBatch spritebatch);
    }
}
