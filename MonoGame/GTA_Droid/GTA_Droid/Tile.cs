using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GTA_Droid
{
    class Tile : GameObject
    {
        int col_scale = 128;
        public Tile(Vector2 pos)
            : this(pos, "Road/straight")
        {
        }
        public Tile(Vector2 pos, String tex)
            : this(pos, "Road/straight", 0)
        {
        }
        public Tile(Vector2 pos, String tex, float rotation)
        {



            int offset;
            setTex(Activity1.game.LoadContent(tex));
            setPos(pos);
            setRotation(rotation);
            gameObjects.Add(this);
            setDefaultCenter();
            if (getWidth() == 512 && getHeight() == 512)
            {
                offset = 256;
                new Waypoint(new Vector2(pos.X - offset, pos.Y - offset));
                new Waypoint(new Vector2(pos.X - offset + getWidth(), pos.Y - offset));
                new Waypoint(new Vector2(pos.X - offset, pos.Y - offset + getHeight()));
                new Waypoint(new Vector2(pos.X - offset + getWidth(), pos.Y - offset + getHeight()));

            }
            if (getWidth() > 512)
            {
                int i = getWidth() / 512;
            }
            if (getWidth() == 1024)
            {
                offset = 256;
                if (rotation == 0)
                {
                    setCenter(new Vector2(getCenter().X - offset, getCenter().Y));
                }
                else if (rotation == MathHelper.Pi)
                {
                    setCenter(new Vector2(getCenter().X + offset, getCenter().Y));
                }
                if (getHeight() == 1024)
                    new ColBox(new Vector2(getPos().X - 256 + col_scale, getPos().Y - 256 + col_scale), "1024");
                else if (getHeight() == 512)
                    new ColBox(new Vector2(getPos().X - 256 + col_scale, getPos().Y - 256 + col_scale), "1024-W");


            }
            else if (getWidth() == 2048)
            {
                new ColBox(new Vector2(getPos().X - 256 + col_scale, getPos().Y - 256 + col_scale), "2048-W");
                if (rotation == 0)
                    setCenter(new Vector2(getCenter().X - 768, getCenter().Y));
                else if (rotation == MathHelper.Pi)
                    setCenter(new Vector2(getCenter().X + 768, getCenter().Y));

            }
            if (getHeight() == 1024)
            {
                if (rotation == 0)
                    setCenter(new Vector2(getCenter().X, getCenter().Y - 256));
                else if (rotation == MathHelper.Pi)
                    setCenter(new Vector2(getCenter().X, getCenter().Y + 256));

            }
            else if (getHeight() == 2048)
            {
                if (rotation == 0)
                    setCenter(new Vector2(getCenter().X - 768, getCenter().Y));
                else if (rotation == MathHelper.Pi)
                    setCenter(new Vector2(getCenter().X + 768, getCenter().Y));
            }


        }
        public override void Update(GameTime gameTime)
        {

        }
    }
}
