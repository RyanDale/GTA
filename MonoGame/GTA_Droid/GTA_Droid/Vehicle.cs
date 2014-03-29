using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GTA_Droid
{
    abstract class Vehicle : GameObject
    {
        private float speed;
        private float damage;
        private Boolean hasDriver;
        private bool hidePlayer;
        private bool moving;
        protected Vector2 previousPos;
        protected float previousRot;
        private bool reverse;
        private float reverse_tme;
        public Vehicle(Vector2 pos, String s, bool hidePlayer)
        {
            setTex(Activity1.game.LoadContent(s));
            setPos(pos);
            setDefaultCenter();
            this.hidePlayer = hidePlayer;
            pixelColor = new Color[getTex().Width * getTex().Height];
            gameObjects.Add(this);
            reverse = false;
        }

        public override void Update(GameTime gameTime)
        {
            float SCALE = 20.0f;
            moving = false;
            if (hasDriver)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Up))
                {
                    setPos(new Vector2((float)(Math.Sin(getRotation()) * speed * SCALE) + getPos().X, (getPos().Y - (float)Math.Cos(getRotation()) * speed * SCALE)));
                    moving = true;
                    if (Keyboard.GetState().IsKeyDown(Keys.Left))
                    {
                        setRotation(getRotation() - (float)(Math.PI * 2 / 180));

                    }
                    else if (Keyboard.GetState().IsKeyDown(Keys.Right))
                    {
                        setRotation(getRotation() + (float)(Math.PI * 2 / 180));

                    }
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Down))
                {
                    setPos(new Vector2(getPos().X - (float)(Math.Sin(getRotation()) * speed * SCALE), ((float)Math.Cos(getRotation()) * speed * SCALE) + getPos().Y));
                    moving = true;
                    if (Keyboard.GetState().IsKeyDown(Keys.Left))
                    {
                        setRotation(getRotation() - (float)(Math.PI * 2 / 180));

                    }
                    else if (Keyboard.GetState().IsKeyDown(Keys.Right))
                    {
                        setRotation(getRotation() + (float)(Math.PI * 2 / 180));

                    }

                }
                reverse_tme++;
                if (GamePad.GetState(PlayerIndex.One).Buttons.LeftShoulder == ButtonState.Pressed && reverse_tme > 5)
                {
                    if (reverse)
                        reverse = false;
                    else if (!reverse)
                        reverse = true;
                    reverse_tme = 0;
                }

                if (GamePad.GetState(PlayerIndex.One).Triggers.Left > 0.1f)
                {
                    if (!reverse)
                        setPos(new Vector2((float)(Math.Sin(getRotation()) * speed * SCALE) + getPos().X, (getPos().Y - (float)Math.Cos(getRotation()) * speed * SCALE)));
                    else
                        setPos(new Vector2(getPos().X - (float)(Math.Sin(getRotation()) * speed * SCALE), ((float)Math.Cos(getRotation()) * speed * SCALE) + getPos().Y));
                    if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X > 0.2)
                    {
                        setRotation(getRotation() + (float)(Math.PI * 2 / 180));
                    }
                    else if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X < -0.2)
                    {
                        setRotation(getRotation() - (float)(Math.PI * 2 / 180));
                    }

                }

            }
            if (GetType() == typeof(PoliceCar))
            {
                PoliceCar p = (PoliceCar)this;
                p.flashLights();

            }
            if (hasDriver)
            {
                if (getPos().Y < 5120)
                {
                    if (getPos().X < -32 - 256 || getPos().Y < -32 - 256 || getPos().X > 5120 - 256 + 32 || getPos().Y > 5120 - 256 + 32)
                        setPos(previousPos);
                }
                else
                    if (getPos().Y < -256 + 2048 || getPos().X < -256 + 512 * 4 || getPos().Y > 5120 - 256 + 32 + 512 * 14 || getPos().X > 5120 - 256)
                        setPos(previousPos);
                for (int i = 0; i < gameObjects.Count; i++)
                {
                    if (gameObjects[i].GetType() == typeof(AI))
                        if (CheckCollision(this, gameObjects[i]))
                        {
                            AI ai = (AI)gameObjects[i];


                            if (ai.isAlive())
                            {

                                ai.Die();
                                break;
                            }

                        }
                    if (gameObjects[i].GetType().IsSubclassOf(typeof(Vehicle)) && !gameObjects[i].Equals(this))
                    {
                        if (CheckCollision(this, gameObjects[i]))
                        {
                            setPos(previousPos);
                            setRotation(previousRot);
                        }
                    }
                    else if (gameObjects[i].GetType() == typeof(ColBox))
                    {
                        if (CheckCollision(this, gameObjects[i]))
                        {
                            setPos(previousPos);
                            setRotation(previousRot);
                        }

                    }
                }
            }
            previousPos = getPos();
            previousRot = getRotation();

        }
        public void setHasDriver(Boolean hasDriver)
        {
            this.hasDriver = hasDriver;
        }
        public float getSpeed()
        {
            return speed;
        }
        public Boolean isHasDriver()
        {
            return hasDriver;
        }
        public void setSpeed(float speed)
        {
            this.speed = speed;
        }
        public bool isHidePlayer()
        {
            return hidePlayer;
        }

    }
}
