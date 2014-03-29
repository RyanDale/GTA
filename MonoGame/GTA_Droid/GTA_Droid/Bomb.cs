using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GTA_Droid
{
    class Bomb : GameObject
    {
        public Bomb(Vector2 pos)
        {
            setTex(Activity1.game.LoadContent("Bullets/bomb"));
            setPos(pos);
            gameObjects.Add(this);
        }
        public override void Update(GameTime gameTime)
        {
        }
        public void detonate()
        {
            bool x = false;
            Random r = new Random();
            
            for (int i = 0; i < gameObjects.Count; i++)
            {
                if (gameObjects[i].GetType() == typeof(AI) && gameObjects[i].getRect().Intersects(new Rectangle((int)getPos().X - 256, (int)getPos().Y - 256, 512, 512)))
                {
                    AI ai = (AI)gameObjects[i];
                    ai.Die();
                }
                else if (gameObjects[i].GetType() == typeof(Player) && gameObjects[i].getRect().Intersects(new Rectangle((int)getPos().X - 256, (int)getPos().Y - 256, 512, 512)))
                {
                    Player p = (Player)gameObjects[i];
                    if (!p.isInVec())
                    {
                        gameObjects.Remove(gameObjects[i]);
                        new Explosion(getPos(), (float)(8.0f + r.NextDouble()), "Player");
                        x = true;
                    }
                }
                if (gameObjects[i].GetType() == typeof(Helicopter) && gameObjects[i].getRect().Intersects(new Rectangle((int)getPos().X - 256, (int)getPos().Y - 256, 512, 512)))
                {
                    
                    Helicopter h = (Helicopter)gameObjects[i];
                    if (h.getThrottle() <= 3 && h.getThrottle() > 0)
                    {
                        gameObjects.Remove(gameObjects[i]);
                        for (int j = 0; j < gameObjects.Count; j++)
                            if (gameObjects[j].GetType() == typeof(Player))
                                gameObjects.Remove(gameObjects[j]);
                        new Explosion(getPos(), (float)(8.0f + r.NextDouble()), "Player");
                        new Helicopter(new Vector2(0, 512));
                        x = true;
                        gameObjects.Remove(this);
                        Activity1.game.camera.setZoom(0.5f);
                    }

                }

            }
            if (!x)
                new Explosion(getPos(), (float)(8.0f + r.NextDouble()), "null");
            gameObjects.Remove(this);

        }
    }
}
