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
            private int tokenCount;
            public void addToken() { tokenCount++; empty = false; }
            public int getTokenCount() { return tokenCount; }
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

        private Constants.PITNAMES lastPit;
        bool capture = false;

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
            int tokensToMove = Pits[currentSide][currentIndex-1].getTokenCount();
            Pits[currentSide][currentIndex-1].emptyPit();
            while (tokensToMove > 0)
            {
                if (currentIndex > 5)
                {
                    if (currentSide == click.player)
                    {
                        Banks[currentSide]++;
                        tokensToMove--;
                        lastPit = (Constants.PITNAMES)(currentSide * 10 + 6);
                    }

                    currentIndex = 0;
                    currentSide = (currentSide + 1) % 2;
                }
                else
                {
                    if (tokensToMove == 1)
                    {
                        if (Pits[currentSide][currentIndex].isEmpty())
                        {
                            capture = true;
                        }
                    }
                    Pits[currentSide][currentIndex].addToken();
                    currentIndex++;
                    tokensToMove--;
                    lastPit = (Constants.PITNAMES)(currentSide * 10 + currentIndex - 1);
                }
            }
        }
        public bool CheckExtraTurn(int currentPlayer)
        {
            if ((currentPlayer * 10 + 6) == (int)lastPit)
            {
                return true;
            }
            return false;
        }
        public void CheckCapture(int currentPlayer)
        {
            if (capture)
            {
                if(currentPlayer == 0) {
                    switch (lastPit)
                    {
                        case Constants.PITNAMES.PIT00:
                            if (!Pits[1][5].isEmpty())
                            {
                                CapturePit(currentPlayer, 1, 5);
                                CapturePit(currentPlayer, 0, 0);
                            }
                            break;
                        case Constants.PITNAMES.PIT01:
                            if (!Pits[1][4].isEmpty())
                            {
                                CapturePit(currentPlayer, 1, 4);
                                CapturePit(currentPlayer, 0, 1);
                            }
                            break;
                        case Constants.PITNAMES.PIT02:
                            if (!Pits[1][3].isEmpty())
                            {
                                CapturePit(currentPlayer, 1, 3);
                                CapturePit(currentPlayer, 0, 2);
                            }
                            break;
                        case Constants.PITNAMES.PIT03:
                            if (!Pits[1][2].isEmpty())
                            {
                                CapturePit(currentPlayer, 1, 2);
                                CapturePit(currentPlayer, 0, 3);
                            }
                            break;
                        case Constants.PITNAMES.PIT04:
                            if (!Pits[1][1].isEmpty())
                            {
                                CapturePit(currentPlayer, 1, 1);
                                CapturePit(currentPlayer, 0, 4);
                            }
                            break;
                        case Constants.PITNAMES.PIT05:
                            if (!Pits[1][0].isEmpty())
                            {
                                CapturePit(currentPlayer, 1, 0);
                                CapturePit(currentPlayer, 0, 5);
                            }
                            break;
                        default: break;
                    }
                }
                else {
                    switch(lastPit)
                    {
                        case Constants.PITNAMES.PIT10:
                            if (!Pits[0][5].isEmpty())
                            {
                                CapturePit(currentPlayer, 0, 5);
                                CapturePit(currentPlayer, 1, 0);
                            }
                            break;
                        case Constants.PITNAMES.PIT11:
                            if (!Pits[0][4].isEmpty())
                            {
                                CapturePit(currentPlayer, 0, 4);
                                CapturePit(currentPlayer, 1, 1);
                            }
                            break;
                        case Constants.PITNAMES.PIT12:
                            if (!Pits[0][3].isEmpty())
                            {
                                CapturePit(currentPlayer, 0, 3);
                                CapturePit(currentPlayer, 1, 2);
                            }
                            break;
                        case Constants.PITNAMES.PIT13:
                            if (!Pits[0][2].isEmpty())
                            {
                                CapturePit(currentPlayer, 0, 2);
                                CapturePit(currentPlayer, 1, 3);
                            }
                            break;
                        case Constants.PITNAMES.PIT14:
                            if (!Pits[0][1].isEmpty())
                            {
                                CapturePit(currentPlayer, 0, 1);
                                CapturePit(currentPlayer, 1, 4);
                            }
                            break;
                        case Constants.PITNAMES.PIT15:
                            if (!Pits[0][0].isEmpty())
                            {
                                CapturePit(currentPlayer, 0, 0);
                                CapturePit(currentPlayer, 1, 5);
                            }
                            break;
                        default: break;
                    }
                }
                capture = false;
            }
        }
        public void CapturePit(int player, int pitSide, int pitIndex)
        {
            Banks[player] += Pits[pitSide][pitIndex].getTokenCount();
            Pits[pitSide][pitIndex].emptyPit();
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
            return Pits[side][index].getTokenCount();
        }
        public void SideCleared(int playerCleared)
        {
            foreach (Pit p in Pits[playerCleared])
            {
                Banks[playerCleared] += p.getTokenCount();
                p.emptyPit();
            }
        }

    }
}
