using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;

namespace GTA_Droid
{
    public class AnalogStick
    {
        Vector2 stick_pos, def_pos,left_pos,right_pos,btn_two;
        private Texture2D analog_tex, bg_tex,btn;
        private bool isMoving;
        public bool button_pressed, button_2_pressed;
        public AnalogStick(Vector2 stick_pos)
        {
            this.stick_pos = stick_pos;
            def_pos=stick_pos;
            analog_tex = Activity1.game.LoadContent("stick");
            bg_tex = Activity1.game.LoadContent("analog_back");
            btn= Activity1.game.LoadContent("a_btn");
            left_pos = stick_pos;
           
            right_pos = new Vector2(1920*Activity1.game.menuScale - stick_pos.X, stick_pos.Y);
            btn_two = right_pos + new Vector2(128 * Activity1.game.menuScale, 128 * Activity1.game.menuScale);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(bg_tex, new Vector2(def_pos.X, def_pos.Y), null, Color.White, 0, new Vector2(bg_tex.Width / 2, bg_tex.Height / 2), Activity1.game.menuScale, SpriteEffects.None, 0);
            spriteBatch.Draw(analog_tex, stick_pos, null, Color.White, 0, new Vector2(analog_tex.Width / 2, analog_tex.Height / 2), Activity1.game.menuScale, SpriteEffects.None, 0);
            spriteBatch.Draw(btn, right_pos, null, Color.White, 0, new Vector2(btn.Width / 2, btn.Height / 2), Activity1.game.menuScale, SpriteEffects.None, 0);
            spriteBatch.Draw(btn, btn_two, null, Color.White, 0, new Vector2(btn.Width / 2, btn.Height / 2), Activity1.game.menuScale, SpriteEffects.None, 0);
        }
        public bool moved()
        {
            return isMoving;
        }
        public void Update(GameTime gameTime)
        {
            button_pressed = false;
            button_2_pressed = false;
            TouchCollection touchCollection = TouchPanel.GetState();
            
            isMoving = false;
            foreach (TouchLocation tl in touchCollection)
            {
                if (tl.State == TouchLocationState.Pressed || tl.State == TouchLocationState.Moved)
                {
                    Vector2 touch_pos = tl.Position;
                    Rectangle touch_rect=new Rectangle((int)touch_pos.X, (int)touch_pos.Y, 1, 1);
                    if (
                        new Rectangle((int)(stick_pos.X - Activity1.game.menuScale * analog_tex.Width / 2), (int)(stick_pos.Y - Activity1.game.menuScale * analog_tex.Height / 2), (int)(analog_tex.Width * Activity1.game.menuScale), (int)(analog_tex.Height * Activity1.game.menuScale))
                         .Intersects(new Rectangle((int)(def_pos.X - Activity1.game.menuScale * (bg_tex.Width) / 2), (int)(def_pos.Y - Activity1.game.menuScale * (bg_tex.Height) / 2), (int)((bg_tex.Width) * Activity1.game.menuScale), (int)((bg_tex.Height) * Activity1.game.menuScale))) &&
                              touch_rect.Intersects(
                              new Rectangle((int)(stick_pos.X - Activity1.game.menuScale * analog_tex.Width / 2), (int)(stick_pos.Y - Activity1.game.menuScale * analog_tex.Height / 2), (int)(analog_tex.Width * Activity1.game.menuScale), (int)(analog_tex.Height * Activity1.game.menuScale))))
                    {
                        isMoving = true;
                        stick_pos = new Vector2(touch_pos.X, touch_pos.Y);

                    }
                    if (touch_rect.Intersects(new Rectangle((int)(right_pos.X - Activity1.game.menuScale * btn.Width / 2), (int)(right_pos.Y - Activity1.game.menuScale * btn.Height / 2), (int)(btn.Width * Activity1.game.menuScale), (int)(btn.Height * Activity1.game.menuScale))))
                    {
                        button_pressed = true;
                    }
                    if (touch_rect.Intersects(new Rectangle((int)(btn_two.X - Activity1.game.menuScale * btn.Width / 2), (int)(btn_two.Y - Activity1.game.menuScale * btn.Height / 2), (int)(btn.Width * Activity1.game.menuScale), (int)(btn.Height * Activity1.game.menuScale))))
                    {
                        button_2_pressed = true;
                    }
                }
            }
             if(!isMoving)
                 stick_pos = def_pos;
        }
        public float getRotation()
        {
            return (float)Math.Atan2(stick_pos.X - def_pos.X, def_pos.Y - stick_pos.Y);
        }
    }
}