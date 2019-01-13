using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace Gamescreen
{
    public class Smoke_Variables
    {
        public Vector2 Location { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float YSpeed  { get; set; }
        public float XSpeed  { get; set; }
        public float Alpha { get; set; }
        public float rotation { get; set; }
        public float Rotation_Speed { get; set; }
        public bool YSwitch { get; set; }

    }
    class Smoke
    {
        private Random rand = new Random((int)DateTime.Now.Ticks);

        static Texture2D Smoke_Particles;
        public List<Smoke_Variables> particles = new List<Smoke_Variables>();
         public int particle_Count{get;set;}
        
        Vector2 location ;
        public Smoke(Vector2 loaction,int particle_Count)
        {
            
            this.location = loaction;
            this.particle_Count = particle_Count;
        }


        public void Content(ContentManager content)
        {
            Smoke_Particles = content.Load<Texture2D>("smoke-1");

        }
        public void loadParticle(float buidlingSpeed)
        {
            for (int i = 0; i < particle_Count; i++)
            {
                int Aswitchs = rand.Next(0, 100);
                int Bswitchs = rand.Next(0, 70);
                particles.Add(new Smoke_Variables());
                particles[i].Location = new Vector2(-40, -40);
                particles[i].X = location.X;
                particles[i].Y = (location.Y + (float)rand.Next(0, 50));
                particles[i].Alpha = rand.Next(0,200);
                particles[i].YSwitch = false;
                particles[i].rotation = (float)rand.NextDouble();
                particles[i].Rotation_Speed = (float)(0.001*rand.NextDouble());
                particles[i].YSpeed = (float)(-0.02*((rand.NextDouble())+ buidlingSpeed));
                particles[i].XSpeed = (float)(0.02*rand.NextDouble());
                if (Aswitchs >= 50)
                {
                    particles[i].Rotation_Speed *= -1;
                }
                 if (Bswitchs >= 50)
                {
                    particles[i].XSpeed *= -1;
                }
              
            }
        }
        public void SpawnPoint()
        {
            for (int i = 0; i < particles.Count; i++)
            {
                particles[i].Location = new Vector2(particles[i].X, particles[i].Y);
                particles[i].rotation += particles[i].Rotation_Speed;
                particles[i].X += particles[i].XSpeed;
                 if (particles[i].Y < 570)
                 {
                     particles[i].YSwitch = true;
                 }
                 if (particles[i].YSwitch == true)
                 {
                     particles[i].X += 0.02f;
                     particles[i].Y += particles[i].YSpeed;
                     particles[i].Alpha -= 0.05f;
                     if (particles[i].Alpha <= 0)
                     {
                         particle_Count -= 1;
                         particles.RemoveAt(i);
                     }
                
                 }
                 else
                 {
                     particles[i].Y += particles[i].YSpeed;
                 }
               
            } 
        }
        public void Draw(SpriteBatch sprites)
        {
            for (int i = 0; i < particles.Count; i++)
            {

                sprites.Draw(Smoke_Particles, particles[i].Location, null, new Color(100, 100, 100, (byte)particles[i].Alpha), particles[i].rotation, new Vector2(184 / 2, 186 / 2), 0.5f, SpriteEffects.None, 0);
            }
        }
    }
}
