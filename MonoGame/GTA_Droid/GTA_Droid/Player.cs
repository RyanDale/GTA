using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace GTA_Droid
{
    public class Player : Human
    {

        private bool turnAround;
        private bool parenting;
        private Vehicle v;
        private bool inVec;
        private int minCarTime;
        private int resetCount;
        private Vector2 previousPos;
        private float previousRot;
        private Vector2 exitPos;
        private int weaponState;
        private int weaponStateDelay;
        private float wps_vel;
        private String wps_spr;
        private float wpn_to;
        private float wps_delay;
        private float wps_life;
        private float bomb_cnt;
        private bool bomb_created;
        private Bomb b;
        private static Texture2D[] normal;
        private static Texture2D[] rocket = new Texture2D[] { Activity1.game.LoadContent("Player Animations/Rocket Launcher/Rocket1"), Activity1.game.LoadContent("Player Animations/Rocket Launcher/Rocket3"),
            Activity1.game.LoadContent("Player Animations/Rocket Launcher/Rocket2"), Activity1.game.LoadContent("Player Animations/Rocket Launcher/Rocket4"), Activity1.game.LoadContent("Player Animations/Rocket Launcher/Rocket5") };
        private static Texture2D[] uzi = new Texture2D[] { Activity1.game.LoadContent("Player Animations/Uzi/UZI_3"), Activity1.game.LoadContent("Player Animations/Uzi/UZI_1"),
            Activity1.game.LoadContent("Player Animations/Uzi/UZI_2"), Activity1.game.LoadContent("Player Animations/Uzi/UZI_5"), Activity1.game.LoadContent("Player Animations/Uzi/UZI_4") };
        private static Texture2D[] m4 = new Texture2D[] { Activity1.game.LoadContent("Player Animations/M4/M4_4"), Activity1.game.LoadContent("Player Animations/M4/M4_3"), 
            Activity1.game.LoadContent("Player Animations/M4/M4_2"), Activity1.game.LoadContent("Player Animations/M4/M4_6"), Activity1.game.LoadContent("Player Animations/M4/M4_5") };
        private static Texture2D[] flame_thrower = new Texture2D[] {Activity1.game.LoadContent( "Player Animations/Flame/flmr_4"), Activity1.game.LoadContent("Player Animations/Flame/flmr_3"), 
            Activity1.game.LoadContent("Player Animations/Flame/flmr_2"), Activity1.game.LoadContent("Player Animations/Flame/flmr_6"), Activity1.game.LoadContent("Player Animations/Flame/flmr_5" )};
        public Player()
        {
            walking = new Texture2D[] { Activity1.game.LoadContent("Player Animations/idle"), Activity1.game.LoadContent("Player Animations/anim1") ,
                                            Activity1.game.LoadContent("Player Animations/anim2"),Activity1.game.LoadContent("Player Animations/anim3"),Activity1.game.LoadContent("Player Animations/anim4")};
            punch_anim = new Texture2D[] { Activity1.game.LoadContent("Player Animations/Punching/pun1") ,
                                            Activity1.game.LoadContent("Player Animations/Punching/pun2"),
                                            Activity1.game.LoadContent("Player Animations/Punching/pun3"),
                                            Activity1.game.LoadContent("Player Animations/Punching/pun4")
            };
            setTex(walking[0]);
            
            setPos(new Vector2(512 + 1024, 512 + 1024));
            
            setDefaultCenter();
            setRotation(0);
            alive = true;
            parenting = false;
            gameObjects.Add(this);
            minCarTime = 0;
            resetCount = 11;
            normal = walking;
            setTex(walking[0]);
            setDefaultCenter();
            bomb_cnt = 10;
            bomb_created = false;

        }

        public override void Update(GameTime gameTime)
        {
            float SCALE = 20.0f;
            float speed = gameTime.ElapsedGameTime.Milliseconds / 150.0f;
            for (int i = 0; i < gameObjects.Count; i++)
                if (gameObjects[i].GetType() == typeof(ColBox))
                {
                    if (CheckCollision(this, gameObjects[i]))
                    {
                        turnAround = true;
                        setPos(previousPos);
                        setRotation(previousRot);
                        Console.WriteLine("colx");
                    }

                }
            if (getPos().Y < 5120)
            {
                if (!inVec && (getPos().X < -32 - 256 || getPos().Y < -32 - 256 || getPos().X > 5120 - 256 + 32 || getPos().Y > 5120 - 256 + 32))
                {
                    setPos(previousPos);
                    
                }
            }
            else
                if (!inVec && (getPos().Y < -256 + 2048 || getPos().X < -256 + 512 * 4 || getPos().Y > 5120 - 256 + 32 + 512 * 14 || getPos().X > 5120 - 256))
                {
                    setPos(previousPos);
                    Game1.mission.setMessage("Turn Around");

                }

            previousPos = getPos();
            previousRot = getRotation();

            if (inVec)
            {
                previousPos = v.getPos();
                previousRot = v.getRotation();

            }
            time++;
            resetCount++;
            bool idle = true;
            if (weaponState == 0)
            {
                walking = normal;
            }
            else if (weaponState == 1)
            {
                walking = rocket;
                wps_spr = "Bullets/Missile";
                wps_vel = 4.0f;
                wps_delay = 100;
                wps_life = 100;

            }
            else if (weaponState == 2)
            {
                walking = uzi;
                wps_spr = "Bullets/uzi_bullet";
                wps_vel = 6.0f;
                wps_delay = 50;
                wps_life = 75;

            }
            else if (weaponState == 3)
            {
                walking = m4;
                wps_spr = "Bullets/m4_ammo";
                wps_vel = 7.5f;
                wps_delay = 200;
                wps_life = 175;

            }
            else if (weaponState == 4)
            {
                walking = flame_thrower;
                wps_spr = "Bullets/flame";
                wps_vel = 6.0f;
                wps_delay = 80;
                wps_life = 60;

            }
            setDefaultCenter();
            weaponStateDelay++;
            wpn_to++;
            if (Activity1.game.analog_stick.button_2_pressed&& weaponStateDelay > 10)
            {
                weaponState++;
                if (weaponState >= 5)
                    weaponState = 0;
                weaponStateDelay = 0;
            }
            if (Activity1.game.analog_stick.button_pressed && weaponState > 0 && wpn_to > wps_delay && !inVec)
            {
                new Bullet(new Vector2((float)(Math.Sin(getRotation()) * 5 * SCALE) + getPos().X, (getPos().Y - (float)Math.Cos(getRotation()) * 5 * SCALE))
                    , getRotation(), wps_vel, wps_life, wps_spr);
                wpn_to = 0;
            }
            bomb_cnt++;
            if ((Keyboard.GetState().IsKeyDown(Keys.Q) || GamePad.GetState(PlayerIndex.One).Buttons.X == ButtonState.Pressed) && bomb_cnt > 10)
            {
                if (bomb_created)
                {
                    bomb_created = false;
                    b.detonate();
                }
                else if (!bomb_created && !inVec)
                {
                    b = new Bomb(getPos());
                    bomb_created = true;
                }

                bomb_cnt = 0;
            }
            if (Activity1.game.analog_stick.button_pressed&& !punching && !inVec && walking == normal)
            {
                punching = true;

            }
            if ((Keyboard.GetState().IsKeyDown(Keys.E) || GamePad.GetState(PlayerIndex.One).Buttons.Y == ButtonState.Pressed) && v != null && minCarTime > 100)
            {
                bool x = true;
                if (v.GetType() == typeof(Helicopter) && Activity1.game.camera.getZoom() != 0.5f)
                {
                    x = false;
                }

                if (x)
                {
                    parenting = false;
                    v.setHasDriver(false);
                    inVec = false;
                    minCarTime = 0;
                    setTex(walking[0]);
                    resetCount = 0;
                    bool inCar = true;
                    int posOff = 40;
                    Vector2[] posLoc = new Vector2[8] { new Vector2(v.getPos().X + v.getWidth(), v.getPos().Y + v.getWidth()), new Vector2(v.getPos().X - v.getWidth(), v.getPos().Y - v.getWidth()),
                       new Vector2(v.getPos().X+v.getWidth(),v.getPos().Y-v.getWidth()), new Vector2(v.getPos().X-v.getWidth(),v.getPos().Y+v.getWidth()), 
                       new Vector2(v.getPos().X + v.getWidth()+posOff, v.getPos().Y + v.getWidth()+posOff), new Vector2(v.getPos().X - v.getWidth()-posOff, v.getPos().Y - v.getWidth()-posOff),
                       new Vector2(v.getPos().X+v.getWidth()+posOff,v.getPos().Y-v.getWidth()-posOff), new Vector2(v.getPos().X-v.getWidth()-posOff,v.getPos().Y+v.getWidth()+posOff)};

                    for (int i = 0; i < posLoc.Length; i++)
                    {
                        setPos(posLoc[i]);
                        if (!CheckCollision(this, v))
                        {
                            inCar = false;
                            for (int k = 0; k < gameObjects.Count; k++)
                                if (gameObjects[k].GetType() == typeof(ColBox) && getRect().Intersects(gameObjects[k].getRect()))
                                    inCar = true;
                                else if (gameObjects[k].GetType().IsSubclassOf(typeof(Vehicle)))
                                    if (CheckCollision(gameObjects[k], this))
                                        inCar = true;
                            if (getPos().Y < 5120)
                            {
                                if ((getPos().X < -32 - 256 || getPos().Y < -32 - 256 || getPos().X > 5120 - 256 + 32 || getPos().Y > 5120 - 256 + 32))
                                    inCar = true;
                            }
                            else
                                if ((getPos().Y < -256 + 2048 || getPos().X < -256 + 512 * 4 || getPos().Y > 5120 - 256 + 32 + 512 * 14 || getPos().X > 5120 - 256))
                                    inCar = true;
                        }
                        if (!inCar)
                            break;

                    }
                    v = null;
                }
            }
            if (!parenting)
            {
                if (Activity1.game.analog_stick.moved())
                {
                    setRotation(Activity1.game.analog_stick.getRotation());
                    setPos(new Vector2((float)(Math.Sin(getRotation())* 3) + getPos().X,
                        (getPos().Y - (float)Math.Cos(getRotation()) * 3)));
                    Walk(true);
                    idle = false;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Up) || GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y > 0.1)
                {
                    setPos(new Vector2((float)(Math.Sin(getRotation()) * speed * SCALE) + getPos().X, (getPos().Y - (float)Math.Cos(getRotation()) * speed * SCALE)));
                    Walk(true);
                    idle = false;
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Down) || GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y < -0.1)
                {
                    setPos(new Vector2(getPos().X - (float)(Math.Sin(getRotation()) * speed * SCALE), ((float)Math.Cos(getRotation()) * speed * SCALE) + getPos().Y));
                    Walk(false);
                    idle = false;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Left) || GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X < -0.1)
                {
                    setRotation(getRotation() - (float)(Math.PI / 180));
                    if (idle)
                        Walk(true);
                    idle = false;
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Right) || GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X > 0.1)
                {
                    setRotation(getRotation() + (float)(Math.PI / 180));
                    if (idle)
                        Walk(true);
                    idle = false;
                }
            }
            if (v == null)
            {
                Vehicle nearest = null;
                for (int i = 0; i < gameObjects.Count; i++)
                    if (gameObjects[i].GetType().IsSubclassOf(typeof(Vehicle)))
                        if (nearest == null)
                        {
                            nearest = (Vehicle)gameObjects[i];
                        }
                        else if (Vector2.Distance(nearest.getPos(), getPos()) > Vector2.Distance(gameObjects[i].getPos(), getPos()))
                            nearest = (Vehicle)gameObjects[i];
                if (nearest != null && Vector2.Distance(getPos(), nearest.getPos()) < 500)
                {
                    if ((Keyboard.GetState().IsKeyDown(Keys.E) || GamePad.GetState(PlayerIndex.One).Buttons.Y == ButtonState.Pressed) && resetCount > 10)
                    {

                        exitPos = nearest.getPos() - getPos();
                        v = nearest;
                        inVec = true;
                        if (v.isHidePlayer())
                        {
                            setTex(Activity1.game.LoadContent("Blank"));
                        }
                    }
                }
            }
            if (punching)
                punch();
            if (inVec)
                setVec();
            if (!alive)
                deathCycle();
            else if (idle && !punching)
                if (v == null)
                    setTex(walking[0]);
                else if (v != null && !v.isHidePlayer())
                    setTex(walking[0]);



        }
        public void Walk(Boolean b)
        {
            walkCycle(b);
        }
        public void setVec()
        {
            parent(v);
            parenting = true;
            minCarTime++;
            v.setHasDriver(true);
        }
        public bool isInVec()
        {
            return inVec;
        }
    }
}
