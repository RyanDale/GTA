
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GTA_Droid
{
    class Explosion : GameObject
    {
        private int time;
        private string type;
        Random r = new Random();
        private float interval;
        public Explosion(Vector2 pos)
        {
            setTex(Activity1.game.LoadContent("Explosion/Explosion1"));
            setPos(pos);
            setDefaultCenter();
            setScale((float)(1.0+r.NextDouble()));
            gameObjects.Add(this);
            interval = 3;
        }
        public Explosion(Vector2 pos, float scale, String type)
        {
            this.type = type;
            setTex(Activity1.game.LoadContent("Explosion/Explosion1"));
            setPos(pos);
            setDefaultCenter();
            setScale(scale);
            gameObjects.Add(this);
            interval = 3;
        }
        public Explosion(Vector2 pos,float scale, float interval)
        {
            setTex(Activity1.game.LoadContent("Explosion/Explosion1"));
            setPos(pos);
            setDefaultCenter();
            setScale(scale);
            gameObjects.Add(this);
            this.interval = interval;
        }
        public override void Update(GameTime gameTime)
        {
            

            if (time < interval)
                setTex(Activity1.game.LoadContent("Explosion/Explosion1"));
            else if (time < interval * 2)
                setTex(Activity1.game.LoadContent("Explosion/Explosion2"));
            else if (time < interval * 3)
                setTex(Activity1.game.LoadContent("Explosion/Explosion3"));
            else if (time < interval * 4)
                setTex(Activity1.game.LoadContent("Explosion/Explosion4"));
            else if (time < interval * 5)
                setTex(Activity1.game.LoadContent("Explosion/Explosion5"));
            else if (time < interval * 6)
                setTex(Activity1.game.LoadContent("Explosion/Explosion6"));
            else if (time < interval * 7)
                setTex(Activity1.game.LoadContent("Explosion/Explosion7"));
            else if (time < interval * 8)
                setTex(Activity1.game.LoadContent("Explosion/Explosion8"));
            else if (time < interval * 9)
                setTex(Activity1.game.LoadContent("Explosion/Explosion9"));
            else if (time < interval * 10)
                setTex(Activity1.game.LoadContent("Explosion/Explosion10"));
            else if (time < interval * 11)
                setTex(Activity1.game.LoadContent("Explosion/Explosion11"));
            else if (time < interval * 12)
                setTex(Activity1.game.LoadContent("Explosion/Explosion12"));
            else if (time < interval * 13)
                setTex(Activity1.game.LoadContent("Explosion/Explosion13"));
            else if (time < interval * 14)
                setTex(Activity1.game.LoadContent("Explosion/Explosion14"));
            else if (time < interval * 15)
                setTex(Activity1.game.LoadContent("Explosion/Explosion15"));
            else if (time < interval * 16)
                setTex(Activity1.game.LoadContent("Explosion/Explosion16"));
            else if (time < interval * 17)
                setTex(Activity1.game.LoadContent("Explosion/Explosion17"));
            else if (time < interval * 18)
                setTex(Activity1.game.LoadContent("Explosion/Explosion18"));
            else if (time < interval * 19)
                setTex(Activity1.game.LoadContent("Explosion/Explosion19"));
            else if (time < interval * 20)
                setTex(Activity1.game.LoadContent("Explosion/Explosion20"));
            else if (time < interval * 21)
                setTex(Activity1.game.LoadContent("Explosion/Explosion21"));
            else if (time < interval * 22)
                setTex(Activity1.game.LoadContent("Explosion/Explosion22"));
            else if (time < interval * 23)
                setTex(Activity1.game.LoadContent("Explosion/Explosion23"));
            else if (time < interval * 24)
                setTex(Activity1.game.LoadContent("Explosion/Explosion24"));
            else if (time < interval * 25)
                setTex(Activity1.game.LoadContent("Explosion/Explosion25"));
            else if (time < interval * 26)
                setTex(Activity1.game.LoadContent("Explosion/Explosion26"));
            else if (time < interval * 27)
                setTex(Activity1.game.LoadContent("Explosion/Explosion27"));
            else if (time < interval * 28)
                setTex(Activity1.game.LoadContent("Explosion/Explosion28"));
            else if (time < interval * 29)
                setTex(Activity1.game.LoadContent("Explosion/Explosion29"));
            else if (time < interval * 30)
                setTex(Activity1.game.LoadContent("Explosion/Explosion30"));
            else if (time < interval * 31)
                setTex(Activity1.game.LoadContent("Explosion/Explosion31"));
            else if (time < interval * 32)
                setTex(Activity1.game.LoadContent("Explosion/Explosion32"));
            else if (time < interval * 33)
                setTex(Activity1.game.LoadContent("Explosion/Explosion33"));
            else if (time < interval * 34)
                setTex(Activity1.game.LoadContent("Explosion/Explosion34"));
            else if (time < interval * 35)
                setTex(Activity1.game.LoadContent("Explosion/Explosion35"));
            else if (time < interval * 36)
                setTex(Activity1.game.LoadContent("Explosion/Explosion36"));
            else if (time >= interval * 36)
            {
                gameObjects.Remove(this);
                if (type == "Player")
                    new Player();
            }
            time++;
        }
    }
}
