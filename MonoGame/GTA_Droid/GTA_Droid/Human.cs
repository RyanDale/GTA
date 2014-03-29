using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GTA_Droid
{
    public abstract class Human : GameObject
    {
        protected int time;
        protected bool alive;
        protected bool punching;
        public Texture2D[] walking;
        public  Texture2D[] punch_anim;
        protected int aiNum;
        bool blood = false;
        public void walkCycle(bool forward)
        {
            setDefaultCenter();
            setDefaultCenter();
            float interval = 10;
            if (!forward)
            {
                if (time < interval)
                {
                    setTex(walking[0]);
                }
                else if (time < interval * 2)
                    setTex(walking[1]);
                else if (time < interval * 3)
                    setTex(walking[2]);
                else if (time < interval * 4)
                    setTex(walking[3]);
                else if (time < interval * 5)
                    setTex(walking[4]);
                else if (time >= interval * 5)
                    time = 0;
            }
            else
                if (time < interval)
                    setTex(walking[0]);
                else if (time < interval * 2)
                    setTex(walking[1]);
                else if (time < interval * 3)
                    setTex(walking[2]);
                else if (time < interval * 4)
                    setTex(walking[3]);
                else if (time < interval * 5)
                    setTex(walking[4]);
                else if (time >= interval * 5)
                    time = 0;
            setDefaultCenter();
        }
        public void punch()
        {
            float interval = 10;
            setCenter(new Vector2(punch_anim[0].Width / 2, punch_anim[0].Height / 2));
            if (time < interval)
            {
                setTex(punch_anim[0]);

            }
            else if (time < interval * 2)
            {
                setTex(punch_anim[1]);

            }
            else if (time < interval * 3)
            {
                setTex(punch_anim[2]);


            }
            else if (time < interval * 4)
            {
                setTex(punch_anim[3]);


            }
            else if (time >= interval * 4)
            {
                setTex(walking[0]);


                time = 0;
                punching = false;
                setDefaultCenter();
            }
            if (time < interval * 3)
                for (int i = 0; i < gameObjects.Count; i++)
                    if (getRect().Intersects(gameObjects[i].getRect()) && gameObjects[i].GetType() == typeof(AI))
                    {
                        AI ai = (AI)gameObjects[i];
                        ai.Die();
                    }
        }
        public void deathCycle()
        {
            if (!blood)
            {
                new BloodSplatter(new Vector2(getPos().X + getCenter().X, getPos().Y + getCenter().Y));
                blood = true;
                if (GetType() == typeof(AI))
                    Game1.mission.setScore(100);
            }

            float interval = 10;
            if (time < interval)
            {
                setTex(Activity1.game.LoadContent("Player Animations/Death Animations/anim1"));

            }
            else if (time < interval * 10)
            {
                setTex(Activity1.game.LoadContent("Player Animations/Death Animations/anim2"));

            }
            else if (time >= interval * 30)
            {
                {

                    setTex(walking[0]);
                    time = 0;
                    if (GetType() == typeof(AI))
                    {
                        gameObjects.Remove(this);
                        new AI();
                    }
                    else
                        alive = true;
                }
            }
        }
    }
}
