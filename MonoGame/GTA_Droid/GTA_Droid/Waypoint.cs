
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GTA_Droid
{
    class Waypoint : GameObject
    {
        public static List<Waypoint> waypoints = new List<Waypoint>();
        private static Random r = new Random();
        public Waypoint(Vector2 pos)
        {
         
            setTex(Activity1.game.LoadContent("wp"));
            setPos(pos);
            setDefaultCenter();
            for (int i = 0; i < waypoints.Count; i++)
                if (getPos() == waypoints[i].getPos())
                {
                    return;
                }
            
            waypoints.Add(this);
        }
        public Waypoint(Vector2 pos, String s)
        {
            setTex(Activity1.game.LoadContent("wp"));
            setPos(pos);
            setDefaultCenter();
            
        }
        public override void Update(GameTime gameTime)
        {
         
        }
        public static float Distance(Vector2 v1, Vector2 v2)
        {
            return Vector2.Distance(v1, v2);
        }
        public static Waypoint Random()
        { 
            int i =(int)(r.NextDouble()*waypoints.Count);
            return waypoints[i];

        }
    }
}
