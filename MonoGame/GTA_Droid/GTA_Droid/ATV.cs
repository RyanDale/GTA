
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GTA_Droid
{
    class ATV :Vehicle
    {
        public ATV(Vector2 pos ):base(pos,"ATV",false)
        {
            setSpeed(0.275f);
        }
    }
}
