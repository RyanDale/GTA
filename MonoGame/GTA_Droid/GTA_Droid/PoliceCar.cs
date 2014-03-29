using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GTA_Droid
{
    class PoliceCar : Vehicle
    {
        private int count = 0;
        private bool lights;
        public PoliceCar(Vector2 pos):base(pos,"Police Car",true)
        {
            lights = false;
            setSpeed(0.4f);
        }
        public new void Update(GameTime gameTime) 
        {
            if (lights)
                flashLights();
        }
        public void flashLights()
        {
            count++;
            if (count < 20)
                setTex(Activity1.game.LoadContent("Police Car"));
            else if (count < 40)
                setTex(Activity1.game.LoadContent("Police Car_2"));
            else if (count >= 40)
                count = 0;
        }
    }
}
