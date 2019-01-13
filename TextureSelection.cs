
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;

namespace Game
{
    class TextureSelection
    {
        private ContentManager content;
        private string assets;
        private SpriteBatch spritebatch;
        private Texture2D texture;
        private Vector2 origin;
        private MouseState mouse;
        MouseState OldMouseState;
        public float getfloat { get; set; }
        public Vector2 location { get; set; }
        public byte Alpha { get; set; }
        public TextureSelection(Vector2 loc,Vector2 origin, byte alpha)
        {
            this.Alpha = alpha;
            getfloat = 1.0f;
            this.origin = origin;
            this.location = loc;
        }
        public bool interseciton()
        {
        
            mouse = Mouse.GetState();

            if ( mouse.X >= location.X - origin.X &&
                mouse.Y >= location.Y - origin.Y &&
                mouse.X <=  texture.Width  +location.X &&
                mouse.Y  <= texture.Height + location.Y) 
            {
                return true;
            }
            return false;
        }


        public bool click()
        {
            if (mouse.LeftButton == ButtonState.Released && OldMouseState.LeftButton == ButtonState.Pressed)
            {
                OldMouseState = mouse;
                return true;  
            }
             OldMouseState = mouse;
            return false;
        }
        
        public void loadingContent(string assets, ContentManager content )
        {
  
            this.assets = assets;
            this.content = content;
            texture = content.Load<Texture2D>(assets);
            
        }
        public void SpriteBatch(SpriteBatch sprites)
        {

            this.spritebatch = sprites;
            spritebatch.Draw(texture,location,null,new Color(255,255,255,Alpha),0.0f,origin,getfloat,SpriteEffects.None,1);

        }
    }
}
