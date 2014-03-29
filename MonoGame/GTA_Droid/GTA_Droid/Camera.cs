using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;



/*
 * This is highly modified code from an XNA tutorial 
 * 
 * */

namespace GTA_Droid
{
    public class Camera
    {

        private Matrix transform;
        private Viewport viewport;
        public Vector2 center;
        public float rotation;
        GameObject o;
        private bool zoomFlag;
        private float elapsedTime;
        private bool timeFlag;
        public Camera(Viewport viewport, GameObject o)
        {
            this.viewport = viewport;
            this.o = o;
            elapsedTime = 0;

        }
        public void Update(GameTime gameTime)
        {
            for (int j = 0; j < GameObject.gameObjects.Count; j++)
                if (GameObject.gameObjects[j].GetType() == typeof(Player))
                    o = GameObject.gameObjects[j];
            float f = 1f;
            elapsedTime += gameTime.ElapsedGameTime.Milliseconds;
            rotation = 0;
            center = new Vector2(o.getPos().X + o.getWidth() / 2 - (Activity1.game.Window.ClientBounds.Width / 2), o.getPos().Y + o.getHeight() / 2 - (Activity1.game.Window.ClientBounds.Height / 2));
            transform = Matrix.CreateTranslation(new Vector3(-o.getPos().X, -o.getPos().Y, 0)) * Matrix.CreateScale(f, f, f) * Matrix.CreateRotationZ(-rotation) * 
                Matrix.CreateTranslation(new Vector3(Activity1.game.Window.ClientBounds.Width / 2, Activity1.game.Window.ClientBounds.Height / 2, 0));
            if (elapsedTime > 1000)
            {
                timeFlag = false;
            }

            timeFlag = true;
            elapsedTime = 0;
        }
        public Matrix getTransform()
        {
            return transform;
        }
        public float getZoom()
        {
            return 0;
        }
        //NOT USED IN MOBILE
        public void setZoom(float zoom) { }
    }
}
