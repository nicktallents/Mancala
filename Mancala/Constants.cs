using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mancala
{
    public static class Constants
    {
        public struct ClickEvent
        {
            ClickEvent(int side, int index) { this.pitSide = side; this.pitIndex = index; }
            public int pitSide;
            public int pitIndex;
        }
        public const int TOKENCOUNT = 48;

        //Need some kind of helper function to find board locations for click events
    }
}
