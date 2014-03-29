using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GTA_Droid
{
    class SUV : Vehicle
    {
        public SUV(Vector2 pos):base(pos,"car",true)
        {
            setSpeed(0.4f);
        }
    }
}
