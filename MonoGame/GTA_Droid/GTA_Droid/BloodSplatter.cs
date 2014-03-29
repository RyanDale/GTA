using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GTA_Droid
{
    class BloodSplatter : GameObject
    {
        private int time;
        public BloodSplatter(Vector2 pos)
        {
            Random r = new Random();
            int x = (int)(r.NextDouble() * 3);
            if (x == 0)
                setTex(Activity1.game.LoadContent("Blood Splatter/blood1"));
            else if (x == 1)
                setTex(Activity1.game.LoadContent("Blood Splatter/blood2"));
            else if (x == 2)
                setTex(Activity1.game.LoadContent("Blood Splatter/blood3"));
            setPos(pos);
            setRotation((float)MathHelper.ToRadians((float)(r.NextDouble()*360)));
            setDefaultCenter();
            gameObjects.Add(this);
            time = 0;
        }
        public override void Update(GameTime gameTime)
        {
            time++;
            if (time > 500)
                gameObjects.Remove(this);
        }
    }
}
