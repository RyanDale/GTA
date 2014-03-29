using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GTA_Droid
{
   public abstract class GameObject
    {
        public static List<GameObject> gameObjects = new List<GameObject>();
        private Vector2 pos;
        private Texture2D tex;
        private Rectangle rect;
        private float rotation;
        private Vector2 center;
        private float scale;
        protected Color[] pixelColor;

        public Vector2 getPos()
        {
            return pos;
        }
        public void setPos(Vector2 pos)
        {
            this.pos = pos;
            rect = new Rectangle((int)pos.X, (int)pos.Y, tex.Width, tex.Height);
        }
        public Texture2D getTex()
        {
            return tex;
        }
        public void setTex(Texture2D tex)
        {
            this.tex = tex;
        }
       public int getWidth()
       {
           return tex.Width;
       }
       public int getHeight()
       {
           return tex.Height;
       }
       public void setRotation(float rotation)
       {
           this.rotation = rotation;
       }
       public float getRotation()
       {
           return rotation;
       }
       public Vector2 getCenter()
       {
           return center;
       }
       public void setDefaultCenter()
       {
           center = new Vector2(tex.Width / 2, tex.Height / 2); 
       }
       public void setCenter(Vector2 center)
       {
           this.center = center;
       }
       public void parent(GameObject o)
       {
           setPos(o.getPos());
           setRotation(o.getRotation());
       }
       public abstract void Update(GameTime gameTime);

       public Rectangle getRect()
       {
           return rect;
       }
       public float getScale()
       {
           if(scale==0)
           return 1.0f;
           else
               return scale;
       }
       public void setScale(float scale)
       {
           this.scale = scale;
       }

      
       public static Rectangle TransformRectangle(Matrix transform, int width, int height)
       {

           //Get each corner of texture
           Vector2 leftTop = new Vector2(0.0f, 0.0f);
           Vector2 rightTop = new Vector2(width, 0.0f);
           Vector2 leftBottom = new Vector2(0.0f, height);
           Vector2 rightBottom = new Vector2(width, height);

           //Transform each corner
           Vector2.Transform(ref leftTop, ref transform, out leftTop);
           Vector2.Transform(ref rightTop, ref transform, out rightTop);
           Vector2.Transform(ref leftBottom, ref transform, out leftBottom);
           Vector2.Transform(ref rightBottom, ref transform, out rightBottom);

           //Find the minimum and maximum corners
           Vector2 min = Vector2.Min(Vector2.Min(leftTop, rightTop), Vector2.Min(leftBottom, rightBottom));
           Vector2 max = Vector2.Max(Vector2.Max(leftTop, rightTop), Vector2.Min(leftBottom, rightBottom));

           //Return the transformed rectangle
           return new Rectangle((int)min.X, (int)min.Y, (int)(max.X - min.X), (int)(max.Y - min.Y));
       }//End Transform Rectangle

       public Matrix Transform(Vector2 center, float rotation, Vector2 position)
       {
           // move to origin, scale (if desired), rotate, translate
           return Matrix.CreateTranslation(new Vector3(-center, 0.0f)) *
               // Add scaling here if you want
                         Matrix.CreateRotationZ(rotation) *
                         Matrix.CreateTranslation(new Vector3(position, 0.0f));
       }

       //deprecated (hopefully)
       public bool CheckCollision(GameObject A, GameObject B)
       {
           return A.getRect().Intersects(B.getRect());
       }
    }
}
