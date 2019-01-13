using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using Game;
using Gamescreen;
using NUKE;
using GameScreen.PauseScreen;

//Gamescreen//////////////////////////////////////////////////////////////////////////////////////////////////////
//acual game is played in
namespace NUKE.SCREENS
{
    public class GameScreen : IScreens
    {
        private int Score = 0;
        private Buildings_Randomiser buildings = new Buildings_Randomiser();
        private Nuke_Randomiser nukes = new Nuke_Randomiser();
        private SpriteFont font;
        private KeyboardState oldState;
       
        private Texture2D clouds;
        private Texture2D background;
        private PauseScreen pause;

        public void Kill_all_building ()
        {
            buildings.Kill_building();
            buildings.building_count = 0;

            Score = 0;
        }
        public GameScreen(ContentManager content, TryNativeWindow.MyNativeWindow nativeWindow)
        {
            pause = new PauseScreen(nativeWindow);
            pause.Content(content);
            nukes.contents(content);
            buildings.Building_Type(content);
            clouds = content.Load<Texture2D>("clouds");
            background=content.Load<Texture2D>("Background");
            font = content.Load<SpriteFont>("Bomb_Font");

        }
 

        public NUKE.Game1.GameState update(Microsoft.Xna.Framework.GameTime gametime)
        {

            nukes.buidlingCount=buildings.building_count;
            if (buildings.building_count <= 0)
            {
                pause.Pause_State = PauseScreen.pause_state.HighScore;
                pause.pause = PauseScreen.Pause.unpaused ;
                pause.Getscore = Score;
                buildings.reset();
                buildings.building_count = 20;
                nukes.Reset_location();
               
            }
            Score = (nukes.Points);
            KeyboardState newState = Keyboard.GetState();
            
            if ((oldState.IsKeyUp(Keys.Escape) || oldState.IsKeyUp(Keys.Up)) && (newState.IsKeyDown(Keys.Escape) || newState.IsKeyDown(Keys.Up)))
            {
                if (pause.finish == true)
                {
                    if (pause.pause == PauseScreen.Pause.Paused)
                    {
                        pause.pause = PauseScreen.Pause.unpaused;
                    }
                    else
                    {
                        pause.pause = PauseScreen.Pause.Paused;
                    }
                }
            }

            pause.Update(gametime, this);
         switch(pause.pause)
         {
             case PauseScreen.Pause.Paused:
                 buildings.update(gametime, nukes.destruciton);
                 nukes.update(gametime);
                 break;
             case PauseScreen.Pause.unpaused:
          
                 break;
         }

        
            oldState = newState;

                return pause.Returns;
            

        }
        
        public void Draw(Microsoft.Xna.Framework.GameTime gametime, Microsoft.Xna.Framework.Graphics.SpriteBatch spritebatch)
        {
            spritebatch.Begin();
            spritebatch.Draw(background, Vector2.Zero, Color.White);
            buildings.Back_Draw(spritebatch);
            nukes.Draw(spritebatch);
           
            spritebatch.End();
          
            spritebatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Deferred, SaveStateMode.None);
            nukes.Particle_Draw(spritebatch);
            spritebatch.End();

            spritebatch.Begin();
            spritebatch.Draw(clouds, new Vector2(0, -250), Color.White);
            buildings.Draw(spritebatch);
            pause.Draw(spritebatch);
            spritebatch.DrawString(font, "Score:" + Score, Vector2.Zero, Color.Black);
            spritebatch.End();
        }


    }
}
