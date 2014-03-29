using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GTA_Droid
{
    class Bullet : GameObject
    {
        private float vel;
        private float life;
        private int total_lifetime;
        private String name;
        public Bullet(Vector2 pos, float rotation, float vel, float life, String sprite)
        {
            setTex(Activity1.game.LoadContent(sprite));
            setRotation(rotation);
            setPos(pos);
            this.vel = vel;
            this.life = life;
            setDefaultCenter();
            total_lifetime = 0;
            GameObject.gameObjects.Add(this);
            name = sprite;
            if (name == "Bullets/flame")
                new Explosion(pos, .2f, 1);
        }
        public override void Update(GameTime gameTime)
        {
            total_lifetime++;
            float SCALE = 20.0f;
            float speed = gameTime.ElapsedGameTime.Milliseconds / 100.0f;
            if (total_lifetime < life)
                setPos(new Vector2((float)(Math.Sin(getRotation()) * speed * vel * SCALE) + getPos().X, (getPos().Y - (float)Math.Cos(getRotation()) * vel * speed * SCALE)));
            else
            {
                if ("Bullets/Missile" == name || "Bullets/flame" == name)
                    new Explosion(getPos());
                gameObjects.Remove(this);
            }
            for (int i = 0; i < gameObjects.Count; i++)
                if (gameObjects[i].GetType().IsSubclassOf(typeof(Vehicle)) && CheckCollision(this, gameObjects[i]))
                {
                    if ("Bullets/Missile" == name || "Bullets/flame" == name)
                        new Explosion(getPos());
                    gameObjects.Remove(this);

                }
                else if (gameObjects[i].GetType() == typeof(AI) && getRect().Intersects(gameObjects[i].getRect()))
                {
                    AI ai = (AI)gameObjects[i];
                    if (ai.isAlive())
                        if ("Bullets/Missile" == name || "Bullets/flame" == name)
                            new Explosion(new Vector2(ai.getPos().X, ai.getPos().Y));

                }
        }

    }
}
