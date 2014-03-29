using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GTA_Droid
{
    class Hyundai : Vehicle
    {
        public Hyundai(Vector2 pos) : base(pos,"Hyundai",true)
        {
            setSpeed(0.35f);
        }
    }
}
