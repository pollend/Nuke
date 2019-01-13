using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace NUKE.SCREENS
{
    class Menu : IScreens
    {
        //the beginning menu class
        double Lasttime;
    
        public enum Menu_Switch{HighScore, Logo, MainMenu,Help};
        private Menu_Switch MenuSwitch = Menu_Switch.Logo;
        Vector2 scrolling_credits = new Vector2();
       
        //background/////////////////////////////
        Texture2D Billboard, Lights_Billboard;
        Texture2D Help_front;
        Texture2D background;
        Texture2D clouds;
        Gamescreen.Buildings_Randomiser buildings = new Gamescreen.Buildings_Randomiser();
        Gamescreen.Nuke_Randomiser nukes = new Gamescreen.Nuke_Randomiser();

        //logo/////////////////////////////
        Texture2D Title;
        int logo_Value;
        byte Menu_Alpha = 0;
        Texture2D[] Logo = new Texture2D[117];
        
        //Select///////////////////////////////////////    
        Game.TextureSelection menu = new Game.TextureSelection(new Vector2(600, 440), new Vector2(100, 25), 255);
        Game.TextureSelection Start = new Game.TextureSelection(new Vector2(400, 260), new Vector2(100,25),0);
        Game.TextureSelection Help = new Game.TextureSelection(new Vector2(400, 320), new Vector2(100, 25), 0);
        Game.TextureSelection Highscores = new Game.TextureSelection(new Vector2(400, 380), new Vector2(100, 25), 0);
        Game.TextureSelection Quit = new Game.TextureSelection(new Vector2(400, 440), new Vector2(100, 25), 0);
        //Select///////////////////////////////////////
        private SpriteFont font;
        private HighScores.HighScores highScores = new HighScores.HighScores();
        private const string HighScoreFile = "high_scores.bin";
        private const string credits = "CREDIT \n pollend";
        private int credit_Y = 600;

        public Menu(ContentManager content, bool showIntro)
        {
            background = content.Load<Texture2D>("Background");
            font = content.Load<SpriteFont>("Bomb_Font");
            if (showIntro)
            {
                for (int i = 0; i <= 116; i++)
                {
                    if (i < 8)
                    {
                        Logo[i] = content.Load<Texture2D>("Logo//logo 00" + (i + 2));
                    }
                    else if (i < 98)
                    {
                        Logo[i] = content.Load<Texture2D>("Logo//logo 0" + (i + 2));
                    }
                    else
                    {
                        Logo[i] = content.Load<Texture2D>("Logo//logo " + (i + 2));
                    }
                }
            }
            else
            {
                Logo = null;
                MenuSwitch = Menu_Switch.MainMenu;
            }
            ///////////
            //remove when finish game

            ////////////////////////
            Help_front = content.Load<Texture2D>("Menu_variables//help_menu_front");
            menu.loadingContent("Menu_Variables//Menu", content);
            clouds = content.Load<Texture2D>("clouds");
            Highscores.loadingContent("Menu_Variables//HighScore", content);
            Billboard = content.Load<Texture2D>("Billboards//billboard");
            Title = content.Load<Texture2D>("MENU//Title");
            Quit.loadingContent("Menu_Variables//quit", content);
            Lights_Billboard = content.Load<Texture2D>("Billboards//Lights");
            Start.loadingContent("Menu_Variables//Start",content);
            Help.loadingContent("Menu_Variables//Help", content);
            buildings.Building_Type(content);
            nukes.contents(content);
            if (!System.IO.File.Exists(HighScoreFile))
            {
                //18
                highScores.Add(new HighScores.Score("sam", 2123));
                highScores.Add(new HighScores.Score("roudy", 9293));
                highScores.Add(new HighScores.Score("mike", 2345));
                highScores.Add(new HighScores.Score("MajorSlack", 32342));
                highScores.Add(new HighScores.Score("michael", 3564));
                highScores.Add(new HighScores.Score("Gcan", 9384));
                highScores.Add(new HighScores.Score("dan", 25785));
                highScores.Add(new HighScores.Score("Steve", 32345));
                highScores.Add(new HighScores.Score("michael", 34756));
                highScores.Add(new HighScores.Score("bob", 1453));
                highScores.Add(new HighScores.Score("George", 2534));
                highScores.Add(new HighScores.Score("louis", 9234));
                highScores.Add(new HighScores.Score("bobby", 34123));

                highScores.WriteScores(HighScoreFile);
            }
            highScores.ReadScores(HighScoreFile);
        }
        public NUKE.Game1.GameState update(Microsoft.Xna.Framework.GameTime gametime)
        {

            if ((font.MeasureString(credits).Y + credit_Y) < 0)
            {
                credit_Y = 600;
            }
            else
            {
                credit_Y--;
            }
            switch (MenuSwitch)
            {
                case Menu_Switch.Help:
                    if (menu.interseciton())
                    {
                        menu.getfloat = 1.3f;
                        if (menu.click())
                        {

                            MenuSwitch = Menu_Switch.MainMenu;
                        }
                    }
                    else
                    {
                        menu.getfloat = 1.0f;
                    }
                    break;
                    case Menu_Switch.HighScore:
                    if (menu.interseciton())
                    {
                        menu.getfloat = 1.3f;
                        if (menu.click())
                        {
                           
                            MenuSwitch = Menu_Switch.MainMenu;
                        }
                    }
                    else
                    {
                        menu.getfloat = 1.0f;
                    }
                    break;

                case Menu_Switch.Logo:
                    if ((!(logo_Value >= 116)))
                    {
                        if (gametime.TotalGameTime.TotalMilliseconds - Lasttime > 33)
                        {
                            Lasttime = gametime.TotalGameTime.TotalMilliseconds;
                            logo_Value += 1;
                        }
                    }
                    else
                    {
                        MenuSwitch = Menu_Switch.MainMenu;
                    }
                    break;

                case Menu_Switch.MainMenu:
                    if (!(Menu_Alpha >= 255))
                    {
                        Menu_Alpha += 1;
                    }
                    Start.Alpha = Menu_Alpha;
                    Help.Alpha = Menu_Alpha;
                    Quit.Alpha = Menu_Alpha;
                    Highscores.Alpha = Menu_Alpha;
                    if (Logo != null)
                    {
                        Logo = null;

                    }
                    {
                        if (Start.interseciton())
                        {
                            Start.getfloat = 1.3f;

                            if (Start.click())
                            {
                                return Game1.GameState.GAME_SCREEN;

                            }
                        }
                        else
                        {
                            Start.getfloat = 1.0f;
                        }
                        if (Highscores.interseciton())
                        {
                            if (Highscores.click())
                            {
                                MenuSwitch = Menu_Switch.HighScore;
                            }
                            Highscores.getfloat = 1.3f;
                        }
                        else
                        {
                            Highscores.getfloat = 1.0f;
                        }
                        if (Help.interseciton())
                        {
                            Help.getfloat = 1.3f;
                            if (Start.click())
                            {
                                MenuSwitch = Menu_Switch.Help;
                            }
                        }
                        else
                        {
                            Help.getfloat = 1.0f;
                        }
                        if (Quit.interseciton())
                        {
                            Quit.getfloat = 1.3f;
                            if (Quit.click())
                            {

                                return Game1.GameState.QUIT;
                            }
                        }
                        else
                        {
                            Quit.getfloat = 1.0f;
                        }
                    }
                    break;
            }
            
            nukes.update(gametime);
            buildings.update(gametime,Vector2.Zero);
            return Game1.GameState.MENU_SCREEN;
        }

        public void Draw(Microsoft.Xna.Framework.GameTime gametime, Microsoft.Xna.Framework.Graphics.SpriteBatch spritebatch)
        {
            spritebatch.Begin();
            spritebatch.Draw(background, Vector2.Zero, Color.White);
            nukes.Draw(spritebatch);
            nukes.Particle_Draw(spritebatch);
            spritebatch.Draw(clouds, Vector2.Zero, Color.White);
            buildings.Draw(spritebatch);
            spritebatch.Draw(Billboard, Vector2.Zero, Color.White);
            switch (MenuSwitch)
            {
                case Menu_Switch.HighScore:
                    {
                        int y = 0;
                        foreach (HighScores.Score s in highScores.Scores)
                        {
                            menu.SpriteBatch(spritebatch);
                            spritebatch.DrawString(font, s.ToString(), new Vector2(100, 100 + (y * 20)), Color.Black);
                            y += 1;
                            if (y > 20)
                            {
                                y = 0;
                            }
                        }
                    }
                    break;

                case Menu_Switch.Logo:
                    if(Logo != null)
                    {
                      spritebatch.Draw(Logo[logo_Value], new Vector2(75, 80), Color.White);
                    }
                break;

                case Menu_Switch.MainMenu:
                Start.SpriteBatch(spritebatch);
                Help.SpriteBatch(spritebatch);
                Quit.SpriteBatch(spritebatch);
                Highscores.SpriteBatch(spritebatch);
                spritebatch.Draw(Title, new Vector2(400, 20), null, new Color(255, 255, 255, Menu_Alpha), 0.0f, new Vector2(200, 0), 0.9f, SpriteEffects.None, 0);
                spritebatch.DrawString(font,credits, new Vector2(0,credit_Y), Color.Black);

                break;
                case Menu_Switch.Help:
                spritebatch.Draw(Help_front, new Vector2(51, 50), Color.White);
                menu.SpriteBatch(spritebatch);
                break;
            }
            spritebatch.Draw(Lights_Billboard, Vector2.Zero, Color.White);
      
            spritebatch.End();
        }

    }
}
