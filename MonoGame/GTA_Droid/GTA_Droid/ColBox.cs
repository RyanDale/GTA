using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GTA_Droid
{
    class ColBox : GameObject
    {
        public ColBox(Vector2 pos, String s)
        {
            setTex(Activity1.game.LoadContent("ColBox/"+s));
            setPos(pos);
            gameObjects.Add(this);
        }
        public override void Update(GameTime gameTime)
        {
        }
    }
}
