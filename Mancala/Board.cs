using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mancala
{
    public class Board
    {
        private struct Pit
        {
            public Pit(int t) { this.tokenCount = t; empty = (t == 0) ? true : false; }
            public int tokenCount;
            public bool isEmpty() {return empty;}
            private bool empty;
            public void emptyPit()
            {
                tokenCount = 0;
                empty = true;
            }
        }
        private int[] Banks;
        private Pit[][] Pits;

        public Board()
        {
            Banks = new int[2];
            for (int i = 0; i < Banks.Length; i++)
            {
                Banks[i] = 0;
            }
            Pits = new Pit[2][];
            for (int i = 0; i < Pits.Length; i++)
            {
                Pits[i] = new Pit[6];
                for (int j = 0; j < Pits[i].Length; j++)
                {
                    Pits[i][j] = new Pit(4);
                }
            }
        }

        public void MoveTokens(Constants.ClickEvent click)
        {
            int currentSide = click.pitSide;
            int currentIndex = click.pitIndex + 1;
            int tokensToMove = Pits[currentSide][currentIndex-1].tokenCount;
            Pits[currentSide][currentIndex-1].emptyPit();

            while (tokensToMove > 0)
            {
                if (currentIndex > 5)
                {
                    Banks[currentSide]++;
                    currentIndex = 0;
                    currentSide = (currentSide + 1) % 2;
                }
                else
                {
                    Pits[currentSide][currentIndex].tokenCount++;
                    currentIndex++;
                }
                tokensToMove--;
            }
        }
        public bool EmptyPit(Constants.ClickEvent click)
        {
            if (click.pitSide == 1)
            {
                if (Pits[click.pitSide][click.pitIndex].isEmpty()) return true;
            }
            else if (click.pitSide == 2)
            {
                if (Pits[click.pitSide][click.pitIndex].isEmpty()) return true;
            }
            return false;
        }
        
        public bool SideEmpty(int player)
        {
            if(player>1) {
                //Invalid player number
                return false;
            }
            foreach (Pit p in Pits[player]) 
            {
                if (!p.isEmpty())
                {
                    return false;
                }
            }
            return true;
        }
        public int GetScore(int player)
        {
            if (player > 1)
            {
                //Invalid player number
                return -1;
            }
            return Banks[player];
        }
        public int GetTokenCountAtPit(int side, int index)
        {
            return Pits[side][index].tokenCount;
        }

    }
}
