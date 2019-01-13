using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;
using System.Collections.Generic;


namespace Gamescreen
{
    public class Particle_Variables
    {
        public float x { get; set; }
        public float y { get; set; }
        public Vector2 Location { get; set; }
        public float rotation { get; set; }
        public float size { get; set; }
        public Color C { get; set; }
        public float Speed { get; set; }
        public float SpeedX { get; set; }
        public float SpeedY { get; set; }
        public float Angle { get; set; }
        public float Speed_decrease { get; set; }
     
    }
    class Particle
    {
     //   private Effect bloom;
        private Random rand = new Random((int)DateTime.Now.Ticks);
        private Vector2 location;
        public bool Delete { get; set; }
        static Texture2D DRAW_particle;
        private List<Particle_Variables> particles = new List<Particle_Variables>(1000);

        public void Particle_Content(ContentManager content, Vector2 location)
        {

        //    bloom = content.Load<Effect>("bloom");
            this.location = location;
            Delete = false;
            particles.Add(new Particle_Variables());
            if (DRAW_particle == null)
            {
                DRAW_particle = content.Load<Texture2D>("Particle");
            }

            for (int i = 0; i < 200; i++)
            {
                float variable = (float)rand.NextDouble();
                particles.Add(new Particle_Variables());
                particles[i].Location = location;
                particles[i].x = location.X;
                particles[i].y = location.Y;
                particles[i].size = 2.5f;
                particles[i].C = new Color(255, 255, 255, 50);
                particles[i].rotation = (float)(rand.NextDouble()) ;
                particles[i].Speed_decrease = (float)(1.9 * rand.NextDouble());
                particles[i].Speed = (float)(10.0 * rand.NextDouble());
                particles[i].Angle = rand.Next(0, 360);
                particles[i].SpeedX = particles[i].Speed * (float)Math.Cos(particles[i].Angle * Math.PI / 180.0f);
                particles[i].SpeedY = particles[i].Speed * (float)Math.Sin(particles[i].Angle * Math.PI / 180.0f);
            }
        }

        public void Spawn_Points(GameTime gametime,int Size_Limit)
        {
     
            for (int i = 0; i < particles.Count; i++)
            {
            
                particles[i].x += particles[i].SpeedX/3;
                particles[i].y += particles[i].SpeedY/3;
                particles[i].Location = new Vector2(particles[i].x, particles[i].y);
                particles[i].Speed -= particles[i].Speed_decrease;
                if (particles[i].Speed < 0 ||
                   Math.Abs(location.X - particles[i].x) > 100 ||
                   Math.Abs(location.Y - particles[i].y) > 100)
                {
                    byte alpha = (byte)Math.Max(0, particles[i].C.A - 1);
                    particles[i].C = new Color(20, 20, 20, alpha);
                    particles[i].SpeedX = 0;
                    particles[i].SpeedY = 0;
                    particles[i].Speed = 0;
                    if (alpha <=0)
                    {
                        particles.Remove(particles[i]);
                    }
                }
            }
            if (particles.Count <= 2) 
            {
               Delete = true;
            }
        }

        public void Draw(SpriteBatch sprites)
        {
        //    bloom.CurrentTechnique = bloom.Techniques["BloomCombine"];
      //      bloom.Parameters["ColorMap"].SetValue(DRAW_particle);
      //      bloom.Begin();
            for (int i = 0; i < particles.Count; i++)
            {
                sprites.Draw(DRAW_particle, particles[i].Location, null, particles[i].C,
                             particles[i].rotation, new Vector2(20, 20), particles[i].size, SpriteEffects.None, 1);
               
            }
       //     bloom.End();

        }

    }
}
