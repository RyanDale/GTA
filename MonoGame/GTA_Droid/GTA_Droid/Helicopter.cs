using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GTA_Droid
{
    class Helicopter : Vehicle
    {
        private int time;
        private bool flying;
        private int resetUp;
        private int resetDown;
        private float throttle;
        bool alreadyPlaying;
        public Helicopter(Vector2 pos)
            : base(pos, "Helicopter/heli1", true)
        {
            time = 0;
            setSpeed(0.4f);
            setScale(1.0f);
            resetUp = 5;
            resetDown = 5;
            throttle = 0;
            alreadyPlaying = false;
        }
        public override void Update(GameTime gameTime)
        {

            float SCALE = 20.0f;

            if (isHasDriver())
            {
                rotateProps();
                if (!alreadyPlaying)
                {
                    alreadyPlaying = true;
                }

            }
            else
            {
                alreadyPlaying = false;
            }
            if (isHasDriver())
            {
                if (throttle < 2)
                    for (int i = 0; i < gameObjects.Count; i++)
                    {
                        if (gameObjects[i].GetType() == typeof(ColBox) || gameObjects[i].GetType().IsSubclassOf(typeof(Vehicle)) && !gameObjects[i].Equals(this))
                            if (CheckCollision(this, gameObjects[i]))
                            {
                                for (int j = 0; j < gameObjects.Count; j++)
                                    if (gameObjects[j].GetType() == typeof(Player))
                                        gameObjects.Remove(gameObjects[j]);
                                new Explosion(getPos(), 6.0f, "Player");
                                Activity1.game.camera.setZoom(0.5f);
                                if (getScale() * 9 / 10 <= 1)
                                    setScale(1);

                                gameObjects.Remove(this);
                                new Helicopter(new Vector2(0, 512));
                            }
                    }
                if (throttle == 0)
                    if (getPos().Y < 5120)
                    {
                        if (getPos().X < -32 - 256 || getPos().Y < -32 - 256 || getPos().X > 5120 - 256 + 32 || getPos().Y > 5120 - 256 + 32)
                        {
                            for (int j = 0; j < gameObjects.Count; j++)
                                if (gameObjects[j].GetType() == typeof(Player))
                                    gameObjects.Remove(gameObjects[j]);
                            new Explosion(getPos(), 6.0f, "Player");
                            Activity1.game.camera.setZoom(0.5f);
                            if (getScale() * 9 / 10 <= 1)
                                setScale(1);

                            gameObjects.Remove(this);
                            new Helicopter(new Vector2(0, 512));
                        }
                    }
                    else
                        if (getPos().Y < -256 + 2048 || getPos().X < -256 + 512 * 4 || getPos().Y > 5120 - 256 + 32 + 512 * 14 || getPos().X > 5120 - 256)
                        {
                            for (int j = 0; j < gameObjects.Count; j++)
                                if (gameObjects[j].GetType() == typeof(Player))
                                    gameObjects.Remove(gameObjects[j]);
                            new Explosion(getPos(), 6.0f, "Player");
                            Activity1.game.camera.setZoom(0.5f);
                            if (getScale() * 9 / 10 <= 1)
                                setScale(1);

                            gameObjects.Remove(this);
                            new Helicopter(new Vector2(0, 512));
                        }
                resetUp++;
                resetDown++;
                previousPos = getPos();
                previousRot = getRotation();
                if ((Keyboard.GetState().IsKeyDown(Keys.W) || GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.Y > .2) && throttle <= 10 && resetUp > 5)
                {
                    setScale(getScale() * 10 / 9);
                    Activity1.game.camera.setZoom(Activity1.game.camera.getZoom() * 9 / 10);
                    throttle++;
                    resetUp = 0;
                    flying = true;
                }
                else if ((Keyboard.GetState().IsKeyDown(Keys.S) || GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.Y < -.2) && throttle >= 0 && resetDown > 5)
                {
                    if (getScale() * 9 / 10 >= 1)
                        setScale(getScale() * 9 / 10);
                    else
                        setScale(1);
                    if (throttle == 0)
                    {
                        Activity1.game.camera.setZoom(0.5f);
                        flying = false;
                    }
                    else
                        Activity1.game.camera.setZoom(Activity1.game.camera.getZoom() * 10 / 9);
                    throttle--;
                    resetDown = 0;
                }
                if (Activity1.game.camera.getZoom() < 0.5f)
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.Up))
                    {
                        setPos(new Vector2((float)(Math.Sin(getRotation()) * getSpeed() * SCALE) + getPos().X, (getPos().Y - (float)Math.Cos(getRotation()) * getSpeed() * SCALE)));
                    }
                    else if (Keyboard.GetState().IsKeyDown(Keys.Down))
                    {

                        setPos(new Vector2(getPos().X - (float)(Math.Sin(getRotation()) * getSpeed() * SCALE), ((float)Math.Cos(getRotation()) * getSpeed() * SCALE) + getPos().Y));
                    }

                    if (Keyboard.GetState().IsKeyDown(Keys.Left))
                    {
                        setRotation(getRotation() - (float)(Math.PI * 2 / 180));
                    }
                    else if (Keyboard.GetState().IsKeyDown(Keys.Right))
                    {
                        setRotation(getRotation() + (float)(Math.PI * 2 / 180));
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.Up) || GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y > 0.1)
                    {
                        setPos(new Vector2((float)(Math.Sin(getRotation()) * getSpeed() * SCALE) + getPos().X, (getPos().Y - (float)Math.Cos(getRotation()) * getSpeed() * SCALE)));

                    }
                    else if (Keyboard.GetState().IsKeyDown(Keys.Down) || GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y < -0.1)
                    {
                        setPos(new Vector2(getPos().X - (float)(Math.Sin(getRotation()) * getSpeed() * SCALE), ((float)Math.Cos(getRotation()) * getSpeed() * SCALE) + getPos().Y));
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.Left) || GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X < -0.1)
                    {
                        setRotation(getRotation() - (float)(Math.PI / 180));

                    }
                    else if (Keyboard.GetState().IsKeyDown(Keys.Right) || GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X > 0.1)
                    {
                        setRotation(getRotation() + (float)(Math.PI / 180));
                    }
                }
            }
            int camDis = 4096 + 1024;
            if (getPos().X > 5120 + camDis - 512 || getPos().Y > 5120 + camDis - 512 || getPos().X < -camDis + 512 || getPos().Y < -camDis + 512)
            {
                Game1.mission.setMessage("Turn Around");
            }
            if (getPos().X > 5120 + camDis || getPos().Y > 5120 + camDis || getPos().X < -camDis || getPos().Y < -camDis)
            {
                setPos(previousPos);
                setRotation(previousRot);

            }

        }
        public float getThrottle()
        {
            return throttle;
        }
        public void rotateProps()
        {
            float interval = 1.5f;
            time++;
            if (time < interval)
                setTex(Activity1.game.LoadContent("Helicopter/heli1"));
            else if (time < interval * 2)
                setTex(Activity1.game.LoadContent("Helicopter/heli2"));
            else if (time < interval * 3)
                setTex(Activity1.game.LoadContent("Helicopter/heli3"));
            else if (time < interval * 4)
                setTex(Activity1.game.LoadContent("Helicopter/heli4"));
            else if (time < interval * 5)
                setTex(Activity1.game.LoadContent("Helicopter/heli5"));
            else if (time < interval * 6)
                setTex(Activity1.game.LoadContent("Helicopter/heli6"));
            else if (time >= interval * 6)
                time = 0;
        }
        public bool isFlying()
        {
            return flying;
        }
    }
}
