using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace Gamescreen
{
    class Building_Variables
    {
        public Texture2D building { get; set; }
        public Vector2 location { get; set; }
        public bool destruction { get; set; }
        public float X { get; set; }
        public float y{ get; set; }
        public float speed { get; set; }
        public bool Smoke_Removed { get; set; }

    }
    public class Buildings_Randomiser
    {
        private Rectangle Destruction_rect;
        public int building_count { get; set; }
        private Random rand = new Random((int)DateTime.Now.Ticks);
        private List<Smoke> Smoke = new List<Smoke>();
        List<Building_Variables> buildings = new List<Building_Variables>();
        List<Building_Variables> Back_buildings = new List<Building_Variables>();
        ContentManager contents;
        public Buildings_Randomiser()
        {
        }

        public void Building_Type(ContentManager content)
        {
            building_count = 20;
            contents = content;
            Smoke.Add(new Smoke(Vector2.Zero,10));
            Smoke[0].Content(content);
            for (int i = 0; i < 20; i++)
            {
                
                buildings.Add(new Building_Variables());
               buildings[i].X = (i * 40) +20;
                buildings[i].y = 600;
                buildings[i].Smoke_Removed = false;
                buildings[i].building = content.Load<Texture2D>("Buildings//b" + rand.Next(0, 50));
                buildings[i].speed = ((float)(2*rand.NextDouble()));
                if (buildings[i].speed < 0.5f)
                {
                    buildings[i].speed = 0.6f;
                }

            }
            for (int b = 0; b < 20; b++)
            {
                buildings[b].Smoke_Removed = false;
                Back_buildings.Add(new Building_Variables());
                Back_buildings[b].X = (b * 40)+20;
                 Back_buildings[b].y = 600;
                Back_buildings[b].building = content.Load<Texture2D>("Buildings//b" + rand.Next(0, 50));
                Back_buildings[b].speed = ((float)(2 * rand.NextDouble()));

            }
         
        }



        Vector2 Destruciton_old;
        public void update(GameTime gametime, Vector2 destruction)
        {
            for (int i = 0; i < buildings.Count; i++)
            {
                for (int x = 0; x < Smoke.Count; x++)
                {
                    Smoke[x].SpawnPoint();
                    if (Smoke[x].particle_Count <= 0)
                    {
                       
                        Smoke.RemoveAt(x);
                    }
                }
              
                Back_buildings[i].location = new Vector2(Back_buildings[i].X, Back_buildings[i].y);
                buildings[i].location = new Vector2(buildings[i].X, buildings[i].y);
                if (buildings[i].destruction == true)
                {
                    buildings[i].Smoke_Removed = true;
                    buildings[i].y = Back_buildings[i].y+= buildings[i].speed;
                    buildings[i].X =Back_buildings[i].X += (float)rand.NextDouble();
                    buildings[i].X = Back_buildings[i].X -= (float)rand.NextDouble();


                }

            }


                 
            if (destruction != Destruciton_old)
            {
                Destruction_rect = new Rectangle((int)( destruction.X-50), (int)destruction.Y, 100, 600);
               for (int i = 0; i < buildings.Count; i++)
               {

                       if (Destruction_rect.Intersects(new Rectangle(((int)buildings[i].location.X), (int)buildings[i].location.Y, 40, 500)))
                       {
                         
                           if (buildings[i].Smoke_Removed == false)
                           {
                               building_count -= 1;
                               Smoke.Add(new Smoke(new Vector2(buildings[i].X, 40 + buildings[i].y), 20));
                               Smoke[Smoke.Count - 1].loadParticle(buildings[i].speed);
            
                               buildings[i].destruction = true;
                           }
                               
                       }
                   
               }
               for (int i = 0; i < Back_buildings.Count; i++)
               {
                   if (Destruction_rect.Intersects(new Rectangle((int)Back_buildings[i].location.X, (int)Back_buildings[i].location.Y, 40, 500)))
                   {
                       Back_buildings[i].destruction = true;
                   }
               }
            }
            Destruciton_old = destruction;

        }
        public void Back_Draw(SpriteBatch sprites)
        {
            for (int i = 0; i < buildings.Count; i++)
            {
                sprites.Draw(Back_buildings[i].building, Back_buildings[i].location, null, Color.Gray, 0.0f, new Vector2(20, 200), 1.0f, SpriteEffects.None, 1);

            }
        }
        public void Draw(SpriteBatch sprites)
        {

            for (int i = 0; i < buildings.Count; i++)
            {
                sprites.Draw(buildings[i].building, buildings[i].location, null, Color.White, 0.0f, new Vector2(20, 200), 1.0f, SpriteEffects.None, 0);
              

            }
            for (int i = 0; i < Smoke.Count; i++)
            {
                Smoke[i].Draw(sprites);
            }


        }

        public void Kill_building()
        {
            for (int i = 0; i < buildings.Count; i++)
            {
                buildings[i].destruction = true;
              //  Smoke.Add(new Smoke(new Vector2(buildings[i].X, 40 + buildings[i].y), 40));
              //  Smoke[Smoke.Count - 1].loadParticle(buildings[i].speed);
            }
        }
        public void reset()
        {
            buildings.Clear();
            Back_buildings.Clear();
            for (int i = 0; i < 20; i++)
            {

                buildings.Add(new Building_Variables());
                buildings[i].X = (i * 40) + 20;
                buildings[i].y = 600;
                buildings[i].Smoke_Removed = false;
                buildings[i].building = contents.Load<Texture2D>("Buildings//b" + rand.Next(0, 20));
                buildings[i].speed = ((float)(2 * rand.NextDouble()));
                if (buildings[i].speed < 0.5f)
                {
                    buildings[i].speed = 0.6f;
                }

            }
            for (int b = 0; b < 20; b++)
            {
                buildings[b].Smoke_Removed = false;
                Back_buildings.Add(new Building_Variables());
                Back_buildings[b].X = (b * 40) + 20;
                Back_buildings[b].y = 600;
                Back_buildings[b].building = contents.Load<Texture2D>("Buildings//b" + rand.Next(0, 50));
                Back_buildings[b].speed = ((float)(2 * rand.NextDouble()));

            }
        }
    }
}
