using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GTA_Droid
{
    class Ring : GameObject
    {
        private int time;
        public Ring()
        {
            setTex(Activity1.game.LoadContent("Ring/Ring1"));
            Vector2 pos=Waypoint.Random().getPos();
            setPos(pos);
            setDefaultCenter();
            gameObjects.Add(this);
        }
        public override void Update(GameTime gameTime)
        {
            for (int i = 0; i < gameObjects.Count; i++)
                if (gameObjects[i].GetType() == typeof(Player) && getRect().Intersects(gameObjects[i].getRect()))
                {
                    Player p = (Player)gameObjects[i];
                    if (!p.isInVec())
                    {
                        Game1.mission.setScore(500);
                        gameObjects.Remove(this);
                        new Ring();
                    }
                }
            float interval = 3;
            time++;
            if (time < interval)
                setTex(Activity1.game.LoadContent("Ring/Ring1"));
            else if (time < interval * 2)
                setTex(Activity1.game.LoadContent("Ring/Ring2"));
            else if (time < interval * 3)
                setTex(Activity1.game.LoadContent("Ring/Ring3"));
            else if (time < interval * 4)
                setTex(Activity1.game.LoadContent("Ring/Ring4"));
            else if (time < interval * 5)
                setTex(Activity1.game.LoadContent("Ring/Ring5"));
            else if (time < interval * 6)
                setTex(Activity1.game.LoadContent("Ring/Ring6"));
            else if (time < interval * 7)
                setTex(Activity1.game.LoadContent("Ring/Ring7"));
            else if (time < interval * 8)
                setTex(Activity1.game.LoadContent("Ring/Ring8"));
            else if (time >= interval * 8)
                time = 0;
        }

    }
}
