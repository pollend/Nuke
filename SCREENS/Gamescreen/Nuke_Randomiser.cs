using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;
using System.Collections.Generic;

using System.Data;

//creates nukes that float down from the screen
namespace Gamescreen
{
    class Bombes
    {
        public Texture2D nuke { get; set; }
        public Vector2 Location { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float speed { get; set; }
        public Keys key { get; set; }
        public string Letter { get; set; }

    }
    class Nuke_Randomiser
    {
        public Vector2 destruciton { get; set; }
        public int Points {get;set;}
        bool Unlocked_PointsLost = true;
        double Bombe_Lock_Lastime = 0;
        private List<Particle> particle = new List<Particle>();
        private Random rand = new Random((int)DateTime.Now.Ticks);
        List<Bombes> bombes = new List<Bombes>();
        public int buidlingCount { get; set; }
        
        Texture2D nuke1;
        SpriteFont font;
        ContentManager content;
        private KeyboardState keyboard;
        //24
        Keys[] key = { Keys.A, Keys.B, Keys.C, Keys.D, Keys.E, Keys.F, Keys.G, Keys.H, Keys.J, Keys.K, Keys.L, Keys.M, Keys.N, Keys.O, Keys.P, Keys.Q, Keys.R, Keys.S, Keys.T, Keys.U, Keys.V,Keys.W, Keys.X, Keys.Y, Keys.Z };
        string[] letters = { "A", "B", "C", 
                               "D", "E", "F",
                               "G", "H", "J", 
                               "K", "L", "M", 
                               "N", "O", "P", 
                               "Q", "R", "S",
                               "T", "U", "V","W",
                               "X", "Y", "Z" };
        //24
        public void contents(ContentManager content)
        {
            this.content = content;
            font = content.Load<SpriteFont>("Bomb_Font");
            nuke1 = content.Load<Texture2D>("Nukes//bomb 1");
            
            for (int i = 0; i < 10; i++)
            {
     
                int letter_position = rand.Next(0, 25);
                bombes.Add(new Bombes());
                bombes[i].nuke = nuke1;
                bombes[i].Y = -50;
                bombes[i].key = key[letter_position];
                bombes[i].Letter = letters[letter_position];
                bombes[i].X = rand.Next(0, 800);
                bombes[i].speed = (float)rand.NextDouble() * 1;
                if (bombes[i].speed < 0.2f)
                {
                    bombes[i].speed =  0.3f;
                }
                
            }

        }

        public void Reset_location()
        {
            int reset;
            Points = 0;
            for (int i = 0; i < bombes.Count; i++)
            {
                reset = rand.Next(-50, -10);
                bombes[i].Y = reset;
        bombes[i].speed = (float)rand.NextDouble() * 1;
            }
        }


        public void update(GameTime gametime)
        {
            int totalLossed = 0;
            for (int i = 0; i < particle.Count; i++)
            {
                particle[i].Spawn_Points(gametime, 20);
                if (particle[i].Delete == true)
                {
                    particle.RemoveAt(i);
                }
              
            }
            keyboard = Keyboard.GetState();
            {
                totalLossed = 0;
                Dictionary<Keys, int> pressed = new Dictionary<Keys, int>();
                foreach (Keys k in key)
                {
                    if (keyboard.IsKeyDown(k))
                    {
                        pressed[k] = 1;
                    }
                }
                foreach (Bombes b in bombes)
                {
                    pressed[b.key] = 0;
                
                    if (keyboard.IsKeyDown(b.key))
                    {
                       float buildingPercent = (buidlingCount/20.0f);
                        Points = Points + (int)(40 * buildingPercent);
                        particle.Add(new Particle());
                        particle[particle.Count - 1].Particle_Content(content,new Vector2(22+b.Location.X,27+b.Location.Y));
                        int Letter_type = rand.Next(0, 25);
                        b.Y = -50;
                        b.speed += 0.1f;
                        b.X = rand.Next(0, 800-47);
                        b.Letter = letters[Letter_type];
                        b.key = key[Letter_type];
                        Unlocked_PointsLost = false;
                    }
                 
                }
                foreach (int s in pressed.Values)
                {
                    totalLossed += s;
                }
                if (Unlocked_PointsLost && totalLossed > 0)
                {
                    Points = (Points + ((totalLossed * 100) * -1));
                    Unlocked_PointsLost = false;
                }
                if (Unlocked_PointsLost == false)
                {
                    if (gametime.TotalGameTime.TotalMilliseconds - Bombe_Lock_Lastime > 500)
                    {
                        Unlocked_PointsLost = true;
                        Bombe_Lock_Lastime = gametime.TotalGameTime.TotalMilliseconds;
                    }
                }
                else
                {
                    Bombe_Lock_Lastime = gametime.TotalGameTime.TotalMilliseconds;
                }

            }
            {
                for (int i = 0; i < 10; i++)
                {
                    bombes[i].Location = new Vector2(bombes[i].X, bombes[i].Y);
                    bombes[i].Y += bombes[i].speed;
                    if (bombes[i].Y >570)
                    {
                        destruciton = new Vector2(bombes[i].Location.X, 600);
                        int letter_position = rand.Next(0, 25);
                        bombes[i].Y = -50;
                        //bombes[i].speed = (float)rand.NextDouble() * 2;
                        bombes[i].X = rand.Next(0, 800);
                        bombes[i].key = key[letter_position];
                        bombes[i].Letter = letters[letter_position];
                        if (bombes[i].speed < 0.2f)
                        {
                            bombes[i].speed = 0.3f;
                        }
                        particle.Add(new Particle());
                        particle[particle.Count - 1].Particle_Content(content,new Vector2(22+bombes[i].Location.X,20+bombes[i].Location.Y));
                    }
                }
            }
        }

        public void Particle_Draw(SpriteBatch sprites)
        {
            for (int i = 0; i < bombes.Count; i++)
            {
              for (int x = 0; x < particle.Count; x++)
                {
                    particle[x].Draw(sprites);

                }
            }
        }
        public void Draw(SpriteBatch sprites)
        {
            for (int i = 0; i < bombes.Count; i++)
            {
                sprites.Draw(bombes[i].nuke, bombes[i].Location, null, Color.White, 0.0f, new Vector2(18,17), 1.6f, SpriteEffects.None, 1);
                sprites.DrawString(font, bombes[i].Letter, bombes[i].Location, Color.LightGreen,0.0f,Vector2.Zero,new Vector2(0.9f,1f),SpriteEffects.None,0);
            }
        }
    }
}
