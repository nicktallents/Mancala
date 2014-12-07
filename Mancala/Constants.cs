using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mancala
{
    public static class Constants
    {
        public static float[,,] tokenCountLoc = 
        { 
            { 
                {210, 210}, 
                {340, 210}, 
                {470, 210},
                {600, 210},
                {730, 210},
                {860, 210},
                {990, 145}
            },
            {
                {860, 80},
                {730, 80},
                {600, 80},
                {470, 80},
                {340, 80},
                {210, 80},
                {80, 145}
            } 
        };
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
