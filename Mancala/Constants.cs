using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mancala
{
    public static class Constants
    {
        public enum PITNAMES 
        { 
            PIT00 = 0, 
            PIT01 = 1, 
            PIT02 = 2, 
            PIT03 = 3, 
            PIT04 = 4, 
            PIT05 = 5, 
            BANK0 = 6, 
            PIT10 = 10, 
            PIT11 = 11, 
            PIT12 = 12, 
            PIT13 = 13, 
            PIT14 = 14, 
            PIT15 = 15,
            BANK1 = 16 };
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
            ClickEvent(int side, int index, int player) { this.pitSide = side; this.pitIndex = index; this.player = player; }
            public int pitSide;
            public int pitIndex;
            public int player;
        }
        public const int TOKENCOUNT = 48;

        public const int MAXDEPTH = 2;

        public const int AICOUNT = 1;

        public const int LOWMAX = -48;
        public const int HIGHMIN = 48;

    }
}
