using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Game;
using NUKE;

namespace GameScreen.PauseScreen
{
    public class PauseScreen
    {
        public bool Switch_State2 { get; set; }
        public bool Animation { get; set; }
        static Texture2D billboard;
        public enum pause_state{HighScore,Menuselect};
        public enum Pause{ Paused,unpaused };
        private Texture2D logo;
        private SpriteFont font;
        //High score/////////////////////////////////////////////////////////
        private Texture2D NewHighScore;
        private Texture2D HighScore;
        private HighScores.HighScores Scores = new HighScores.HighScores();
        // ////////////////////////////////////////////////////////////////////
        // Keypresses\//////////////////////////////////////////
        private TryNativeWindow.MyNativeWindow nativeWindow;
        string output = "NULL";
        //\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
        public Pause pause { get; set; }
        public pause_state Pause_State { get; set; }
        Vector2 Postion = new Vector2(0,600);
        public bool finish { get; set; }
        float Y = 600;
        private String Score_Name = "Score";
        public NUKE.Game1.GameState Returns { get; set; }
        Game.TextureSelection Restart = new Game.TextureSelection(new Vector2(400+800, 800), new Vector2(100, 25), 255);
        Game.TextureSelection Menu = new Game.TextureSelection(new Vector2(400 + 800, 800), new Vector2(100, 25), 255);
        Game.TextureSelection quite = new Game.TextureSelection(new Vector2(400+800, 260), new Vector2(100, 25), 255);

        public int Getscore{get;set;}

        public void Content(ContentManager content)
        {
            font = content.Load<SpriteFont>("Bomb_Font");
            finish = false;
            logo = content.Load<Texture2D>("MENU//Title");
            NewHighScore = content.Load<Texture2D>("Menu_Variables//NewHighScore");
            Pause_State = pause_state.Menuselect;
            HighScore = content.Load<Texture2D>("Menu_Variables//High score");
            quite.loadingContent("Menu_Variables//quit", content);
            Restart.loadingContent("Menu_Variables//Restart", content);
            billboard = content.Load<Texture2D>("billboards//billboard");
            Menu.loadingContent("Menu_Variables//Menu", content);
            if (!System.IO.File.Exists("high_scores.bin"))
            {
                //18
                Scores.Add(new HighScores.Score("sam", 2123));
                Scores.Add(new HighScores.Score("roudy", 9293));
                Scores.Add(new HighScores.Score("mike", 2345));
                Scores.Add(new HighScores.Score("MajorSlack", 932342));
                Scores.Add(new HighScores.Score("michael", 3564));
                Scores.Add(new HighScores.Score("Gcan", 9384));
                Scores.Add(new HighScores.Score("dan", 25785));
                Scores.Add(new HighScores.Score("Steve", 32345));
                Scores.Add(new HighScores.Score("michael", 34756));
                Scores.Add(new HighScores.Score("bob", 1453));
                Scores.Add(new HighScores.Score("George", 2534));
                Scores.Add(new HighScores.Score("louis", 9234));
                Scores.Add(new HighScores.Score("bobby", 34123));

                Scores.WriteScores("high_scores.bin");
            }
            Scores.ReadScores("high_scores.bin");
        }
        public PauseScreen(TryNativeWindow.MyNativeWindow nativeWindow)
        {
            this.nativeWindow = nativeWindow;
            Returns = Game1.GameState.GAME_SCREEN;
            this.nativeWindow.Attach();
        }

        public void Update(GameTime gametime, NUKE.SCREENS.GameScreen gs)
        {
            switch (pause)
            {
                case Pause.unpaused:
                    if (Y > 60)
                    {
                        finish = false;
                        Y -= 10;
                    }
                    else
                    {
                        finish = true;
                  
                    }
                    break;

                case Pause.Paused:
                    if (Y < 600)
                    {
                        finish = false;
                        Y += 10;

                    }
                    else
                    {
                        finish = true;
                        Pause_State = pause_state.Menuselect;
                    }
                    break;

            }

        switch (Pause_State)
        {
            case pause_state.Menuselect:

                Restart.location = new Vector2(400, Y + 260);
                Menu.location = new Vector2(400, Y + 320);
                quite.location = new Vector2(400, Y + 380);
            if (Menu.interseciton())
            {
                Menu.getfloat = 1.3f;
                if (Menu.click())
                {
                   
                    Returns = Game1.GameState.MENU_SCREEN;
                }
            }
            else
            {
                Menu.getfloat = 1.0f;
            }
            if (quite.interseciton())
            {
                quite.getfloat = 1.3f;
                if (quite.click())
                {
 
                    Returns = Game1.GameState.QUIT;
                }
            }
            else
            {
                quite.getfloat = 1.0f;
            }
            if (Restart.interseciton())
            {
                Restart.getfloat = 1.3f;
                if (Restart.click())
                {
                    gs.Kill_all_building();
                }
            }
            else
            {
                Restart.getfloat = 1.0f;
            }
            break;

            case pause_state.HighScore:
            Restart.location = new Vector2(150, Y + 400);
            Menu.location = new Vector2(600, Y+400);
            if (Scores.LastScore() < Getscore)
            {
            //high
                 output += nativeWindow.Chars;
                 string newOutput = "";
                 for (int i = 0; i < output.Length; ++i)
                 {
                     if (char.IsLetterOrDigit(output[i]))
                     {
                         if (i+1 < output.Length && output[i+1] == '\b' || i > 11)
                         {
                             ++i;
                         }
                         else
                         {
                                 newOutput += output[i];  
                         }
                     }
                 }
                 output = newOutput;
            }
            if (Menu.interseciton())
            {
                Menu.getfloat = 1.3f;
                if (Menu.click())
                {
                    this.nativeWindow.Detach();
                    if (Scores.LastScore() < Getscore)
                    {
                        if (output.Length == 0)
                        {
                            output = "NULL";
                        }
                        Scores.Add(new HighScores.Score(output, Getscore));
                        Scores.WriteScores("high_scores.bin");
                    }
                    Returns = Game1.GameState.MENU_SCREEN;
                }
            }
            else
            {
                Menu.getfloat = 1.0f;
            }
            if (Restart.interseciton())
            {
                this.nativeWindow.Detach();
                Restart.getfloat = 1.3f;
                if (Restart.click())
                {
                    if (Scores.LastScore() < Getscore)
                    {
                        if (output.Length == 0)
                        {
                            output = "NULL";
                        }
                        Scores.Add(new HighScores.Score(output, Getscore));
                        Scores.WriteScores("high_scores.bin");
                    }
                    pause = Pause.Paused;
                    Returns = Game1.GameState.GAME_SCREEN;
             
                }
            }
            else
            {
                Restart.getfloat = 1.0f;
            }   
                break;
        }

            //quite.location = new Vector2( 400,Y+190);
            Postion = new Vector2(0, Y);
        }

        public void Draw(SpriteBatch sprites)
        {
            sprites.Draw(billboard, Postion, null, Color.White, 0.0f, new Vector2(12, 45), 1.0f, SpriteEffects.None, 0);

            switch (Pause_State)
           {
           case pause_state.Menuselect:
            quite.SpriteBatch(sprites);
            Restart.SpriteBatch(sprites);
            Menu.SpriteBatch(sprites);
            sprites.Draw(logo, new Vector2(400-(logo.Width/2), 0 + Y),Color.White);

            break;
               case pause_state.HighScore:
                
            if (Scores.LastScore() < Getscore)
            {
                sprites.Draw(NewHighScore, new Vector2((NewHighScore.Width / 2), Y + 50), Color.White);
                sprites.DrawString(font,output, new Vector2(400-(output.Length * 12), 250 + Y), Color.Black, 0.0f, new Vector2(0, 0), 3.0f, SpriteEffects.None, 0);
            }
            else
            {
                sprites.Draw(HighScore, new Vector2((NewHighScore.Width / 2), Y + 50), Color.White);
            }
            Restart.SpriteBatch(sprites);
            Menu.SpriteBatch(sprites);
            sprites.DrawString(font, Getscore.ToString(), new Vector2(400, 150 + Y), Color.Black, 0.0f, new Vector2(font.MeasureString(Getscore.ToString()).X / 2, 0), 3.0f, SpriteEffects.None, 0);
            break;
        }
        }
    }
}
