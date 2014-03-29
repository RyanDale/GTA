using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GTA_Droid
{
    class AI : Human
    {
        private Waypoint target;
        private Waypoint waypoint;
        private bool done;
        private int tChk;
        private String previous;
        private static int spacing = 512;
        private Waypoint p;
        private static Random r = new Random(Waypoint.waypoints.Count);
        private static int numOfAis = 0;
        public AI()
        {
        
            

            numOfAis++;
            setTex(Activity1.game.LoadContent("Player Animations/idle"));
            setPos(generatePoint().getPos());
            setPos(new Vector2(getPos().X - 3, getPos().Y - 3));
            target = generatePoint();
            setDefaultCenter();
            gameObjects.Add(this);
            waypoint = findDistance(findPoints());
            setRotation(findRotation(waypoint));

            done = false;
            tChk = 50;
         
            walking = new Microsoft.Xna.Framework.Graphics.Texture2D[] { Activity1.game.LoadContent("Player Animations/idle"), Activity1.game.LoadContent("Player Animations/anim1") ,
                                            Activity1.game.LoadContent("Player Animations/anim2"),Activity1.game.LoadContent("Player Animations/anim3"),Activity1.game.LoadContent("Player Animations/anim4")};
            alive = true;

        }
        public AI(int aiNum)
        {
            this.aiNum = aiNum;

            setTex(Activity1.game.LoadContent("Player Animations/idle"));
            setPos(generatePoint().getPos());
            setPos(new Vector2(getPos().X - 3, getPos().Y - 3));
            target = generatePoint();
            setDefaultCenter();

            waypoint = findDistance(findPoints());
            setRotation(findRotation(waypoint));

            done = false;
            tChk = 50;
            alive = true;

        }
        public void respawn()
        {
            setTex(Activity1.game.LoadContent("Player Animations/idle"));
            setPos(generatePoint().getPos());
            setPos(new Vector2(getPos().X - 3, getPos().Y - 3));
            target = generatePoint();
            setDefaultCenter();
            waypoint = findDistance(findPoints());
            setRotation(findRotation(waypoint));

            done = false;
            tChk = 50;
            alive = true;


        }
        public override void Update(GameTime gameTime)
        {
            float SCALE = 20.0f;
            float speed = gameTime.ElapsedGameTime.Milliseconds / 150.0f;
            tChk++;
            if (getRect().Intersects(new Rectangle(target.getRect().X - 4, target.getRect().Y - 4, 8, 8)))
            {

                setPos(new Vector2(p.getPos().X - 3, p.getPos().Y - 3));
                target = generatePoint();
                waypoint = findDistance(findPoints());
                setRotation(findRotation(waypoint));

            }
            if (new Rectangle(getRect().X + (int)getCenter().X - 1, getRect().Y + (int)getCenter().Y - 1, 2, 2).Intersects(new Rectangle(waypoint.getRect().X + (int)waypoint.getCenter().X - 1, waypoint.getRect().Y + (int)waypoint.getCenter().Y - 1, 2, 2)))
            {
                    waypoint = findDistance(findPoints());
                    setRotation(findRotation(waypoint));

        
            }
            if (alive)
                for (int i = 0; i < gameObjects.Count; i++)
                    if (gameObjects[i].GetType() == typeof(Bullet) && getRect().Intersects(gameObjects[i].getRect()))
                    {
                        Die();
                        done = true;
                        gameObjects.Remove(gameObjects[i]);

                        break;
                    }
            if (!done)
            {
                walkCycle(true);
                setPos(new Vector2((float)(Math.Sin(getRotation()) * speed * SCALE) + getPos().X, (getPos().Y - (float)Math.Cos(getRotation()) * speed * SCALE)));
            }
            else
            {


                deathCycle();


            }
            time++;
        }
        public Waypoint generatePoint()
        {
            int proplem = 0;
            bool x = true;
            while (x)
            {
                proplem = (int)r.Next(Waypoint.waypoints.Count);
                p = Waypoint.waypoints[proplem];
                if (p.getPos().Y <= 5210 + 1)
                    x = false;
            }
            return new Waypoint(Waypoint.waypoints[proplem].getPos(), "hello");
        }
        public List<Waypoint> findPoints()
        {
            List<Waypoint> wp = new List<Waypoint>();
            for (int i = 0; i < Waypoint.waypoints.Count; i++)
                 if (previous != "left" && new Rectangle((int)getPos().X + spacing, (int)getPos().Y, getWidth(), getHeight()).Intersects(Waypoint.waypoints[i].getRect()))
                 {

                     wp.Add(Waypoint.waypoints[i]);
                 }
                 else if (previous != "right" && new Rectangle((int)getPos().X - spacing, (int)getPos().Y, getWidth(), getHeight()).Intersects(Waypoint.waypoints[i].getRect()))
                 {
                     wp.Add(Waypoint.waypoints[i]);
                 }
                 else if (previous != "up" && new Rectangle((int)getPos().X, (int)getPos().Y + spacing, getWidth(), getHeight()).Intersects(Waypoint.waypoints[i].getRect()))
                 {
                     wp.Add(Waypoint.waypoints[i]);
                 }
                 else if (previous != "down" && new Rectangle((int)getPos().X, (int)getPos().Y - spacing, getWidth(), getHeight()).Intersects(Waypoint.waypoints[i].getRect()))
                 {
                     wp.Add(Waypoint.waypoints[i]);
                 }
                
            if (wp.Count == 0&&false)
            {
                for (int i = 0; i < Waypoint.waypoints.Count; i++)
                    if (new Rectangle((int)getPos().X + spacing, (int)getPos().Y, getWidth(), getHeight()).Intersects(Waypoint.waypoints[i].getRect()))
                        wp.Add(Waypoint.waypoints[i]);
                    else if (new Rectangle((int)getPos().X - spacing, (int)getPos().Y, getWidth(), getHeight()).Intersects(Waypoint.waypoints[i].getRect()))
                        wp.Add(Waypoint.waypoints[i]);
                    else if (new Rectangle((int)getPos().X, (int)getPos().Y + spacing, getWidth(), getHeight()).Intersects(Waypoint.waypoints[i].getRect()))
                        wp.Add(Waypoint.waypoints[i]);
                    else if (new Rectangle((int)getPos().X, (int)getPos().Y - spacing, getWidth(), getHeight()).Intersects(Waypoint.waypoints[i].getRect()))
                        wp.Add(Waypoint.waypoints[i]);

            }
            return wp;
        }
        public Waypoint findDistance(List<Waypoint> wp)
        {
            Waypoint x;
            if (wp.Count <= 1)
            {
                return wp[0];

            }

            float f = Waypoint.Distance(wp[0].getPos(), target.getPos());
            x = wp[0];
            for (int i = 0; i < wp.Count; i++)
                if (Waypoint.Distance(wp[i].getPos(), target.getPos()) < f)
                {
                    f = Waypoint.Distance(wp[i].getPos(), target.getPos());
                    x = wp[i];
                }
            return x;
        }
        public float findRotation(Waypoint wp)
        {
            if (wp.getPos().X - getPos().X > 100)
            {
                previous = "right";
                return MathHelper.Pi / 2;
            }
            else if (getPos().X - wp.getPos().X > 100)
            {
                previous = "left";
                return MathHelper.Pi * 3 / 2;
            }
            else if (wp.getPos().Y - getPos().Y > 100)
            {
                previous = "down";
                return MathHelper.Pi;
            }
            else
            { 
                previous = "up";
                
                return MathHelper.Pi * 2;
                
            }
        }
        public int returnValue(int i)
        {
            if (i >= 0 && i <= 7)
                return i;
            else if (i < 0)
                return 0;
            else if (i > 7)
                return 7;
            return 0;
        }
        public void Die()
        {
            time = 0;
            done = true;
            alive = false;
            
        }
        public bool isAlive()
        {
            return alive;
        }

    }
}
