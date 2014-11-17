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
            public int pitSide;
            public int pitIndex;
        }
        public const int MARKERCOUNT = 48;

        //Need some kind of helper function to find board locations for click events
    }
}
