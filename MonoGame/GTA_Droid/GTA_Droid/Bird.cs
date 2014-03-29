using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GTA_Droid
{
    class Bird : GameObject
    {
        private static Bird[] birds = new Bird[20];
        private static int birdCount = 0;
        private int birdNum;
        private int time;
        private String[] birdTex;
        Random r = new Random();
        static int width = Activity1.game.Window.ClientBounds.Width;
        static int height = Activity1.game.Window.ClientBounds.Height;
        private static int offset;
        int space = 5120;
        public Bird(String[] birdTex)
        {

            this.birdTex = birdTex;
            int b = (int)(r.NextDouble() * 2);
            if (b == 0)
                birdTex = new String[3] { "Birds/Bird1/bird0", "Birds/Bird1/bird1", "Birds/Bird1/bird2", };
            else if (b == 1)
                birdTex = new String[3] { "Birds/Bird2/bird1", "Birds/Bird2/bird2", "Birds/Bird2/bird3", };

            this.birdTex = birdTex;
            setTex(Activity1.game.LoadContent(birdTex[0]));

            setRotation(MathHelper.ToRadians((float)(r.NextDouble() * 90) + 270));
            setPos(new Vector2(-10000, -10000));
            setScale(2.0f);
            birds[birdNum = birdCount] = this;
            birdCount++;

            setRotation(MathHelper.ToRadians((float)(r.NextDouble() * 360)));

            gameObjects.Add(this);

        }
        public Bird(String[] birdTex, int i)
        {
            r = new Random();
            int b = (int)(r.NextDouble() *3);
            if (b == 0)
                birdTex = new String[3] { "Birds/Bird1/bird0", "Birds/Bird1/bird1", "Birds/Bird1/bird2", };
            else if (b == 1)
                birdTex = new String[3] { "Birds/Bird2/bird1", "Birds/Bird2/bird2", "Birds/Bird2/bird3", };
            else if (b == 2)
                birdTex = new String[3] { "Birds/Bird2/bird1", "Birds/Bird2/bird2", "Birds/Bird2/bird3", };

            this.birdTex = birdTex;
            setTex(Activity1.game.LoadContent(birdTex[0]));
            r = new Random();
            int m = (int)(r.NextDouble() * 4);
            if (m == 0)
            {
                setPos(new Vector2(-offset, -offset));
                setRotation(MathHelper.ToRadians((float)(r.NextDouble() * 90) + 90));
            }
            else if (m == 1)
            {
                setPos(new Vector2(space + offset, -offset));
                setRotation(MathHelper.ToRadians((float)(r.NextDouble() * 90) + 180));
            }
            else if (m == 2)
            {
                setPos(new Vector2(offset, space + offset));
                setRotation(MathHelper.ToRadians((float)(r.NextDouble() * 90) + 0));
            }
            else if (m == 3)
            {
                setPos(new Vector2(space + offset, space + offset));
                setRotation(MathHelper.ToRadians((float)(r.NextDouble() * 90) + 270));
            }
            gameObjects.Add(this);
            setScale((float)(1.8f+r.NextDouble()));
            birdNum = i;
        }
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            float SCALE = 20.0f;
            float speed = gameTime.ElapsedGameTime.Milliseconds / 150.0f;
            setPos(new Vector2((float)(Math.Sin(getRotation()) * speed * SCALE) + getPos().X, (getPos().Y - (float)Math.Cos(getRotation()) * speed * SCALE)));
            int interval = 5;
            time++;
            if (time < interval)
                setTex(Activity1.game.LoadContent(birdTex[0]));
            else if (time < interval * 2)
                setTex(Activity1.game.LoadContent(birdTex[1]));
            else if (time < interval * 3)
                setTex(Activity1.game.LoadContent(birdTex[2]));
            else if (time < interval * 4)
                setTex(Activity1.game.LoadContent(birdTex[1]));
            else if (time >= interval * 4)
                time = 0;
            if (!onScreen())
            {
                gameObjects.Remove(this);
                birds[birdNum] = new Bird(new String[3] { "Birds/Bird1/bird0", "Birds/Bird1/bird1", "Birds/Bird1/bird2", }, birdNum);

            }
        }
        public Boolean onScreen()
        {

            if (getPos().X < -width || getPos().Y < -height || getPos().X > 5120 + width || getPos().Y > 5120 + height)
                return false;
            return true;
        }
    }
}
