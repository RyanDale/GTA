using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GTA_Droid
{
    class Pagani : Vehicle
    {
        public Pagani(Vector2 pos):base(pos,"pagani",true)
        {
            setSpeed(0.5f);
        }
    }
}
