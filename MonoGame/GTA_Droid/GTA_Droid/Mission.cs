using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GTA_Droid
{
    public class Mission : GameObject
    {
        private Player p;
        private float score;
        private string message;
        private Vector2 nv;
        private String prev;
        public Mission()
        {
            setTex(Activity1.game.LoadContent("money"));
            setScale(2.0f);
            new Ring();
            score = 0;
            message = "";
            prev = "";
        }
        public override void Update(GameTime gameTime)
        {
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);
            spriteBatch.Draw(getTex(), new Vector2(20, 20), Color.White);
            spriteBatch.DrawString(Game1.font1, "$" + score, new Vector2(130, 50), Color.White);
            spriteBatch.DrawString(Game1.font1, message, new Vector2(Activity1.game.Window.ClientBounds.Width / 2 - Game1.font1.MeasureString(message).X / 2, Activity1.game.Window.ClientBounds.Height / 4),//Activity1.game.Window.ClientBounds.Height/2-Game1.font1.MeasureString(message).Y/2),
                Color.White);
            Activity1.game.analog_stick.Draw(spriteBatch);
            spriteBatch.End();
        }
        public void setScore(float score)
        {
            this.score += score;
        }
        public void setMessage(String message)
        {
            prev=this.message;
            this.message = message;
        }
        public void revert(String s)
        {
            if (message.Equals(s))
                message = "";
        }

    }
}
