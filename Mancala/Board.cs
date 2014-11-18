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
            public Pit(int t) { this.tokenCount = t; empty = false; }
            public int tokenCount;
            public bool empty;
        }
        private int Player1Bank;
        private int Player2Bank;
        private List<Pit> Player1Side = new List<Pit>();
        private List<Pit> Player2Side = new List<Pit>();

        public Board()
        {
            Player1Bank = 0;
            Player2Bank = 0;
            for (int i = 0; i < 6; i++)
            {
                Player1Side.Insert(i, new Pit(4));
                Player2Side.Insert(i, new Pit(4));
            }
        }
        public void MoveTokens(Constants.ClickEvent click)
        {
            //Logic for handling moving tokens from a pit
        }
        public bool EmptyPit(Constants.ClickEvent click)
        {
            if (click.pitSide == 1)
            {
                if (Player1Side[click.pitIndex - 1].empty) return true;
            }
            else if (click.pitSide == 2)
            {
                if (Player2Side[click.pitIndex - 1].empty) return true;
            }
            return false;
        }
        public bool SideEmpty(int player)
        {
            if (player == 1)
            {
               foreach (Pit p in Player1Side) 
               {
                   if (!p.empty)
                   {
                       return false;
                   }
               }
            }
            else if (player == 2)
            {
                foreach (Pit p in Player2Side)
                {
                    if (!p.empty)
                    {
                        return false;
                    }
                }
            }
            else
            {
                //Invalid player number
                return false;
            }
            return true;
        }
        public int GetScore(int player)
        {
            if (player == 1)
            {
                return Player1Bank;
            }
            else if (player == 2)
            {
                return Player2Bank;
            }
            else
            {
                //Invalid player number
                return -1;
            }
        }

    }
}
