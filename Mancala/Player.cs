using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
namespace Mancala
{
    public class Player
    {
        public struct PitState
        {
            public PitState(int index, int side, int count) { this.pitIndex = index; this.pitSide = side; this.tokenCount = count; }
            public int pitIndex;
            public int pitSide;
            public int tokenCount;
        }

        public class BoardState
        {
            public BoardState(int turn)
            {
                pits = new PitState[2][];
                for (int i = 0; i < 2; i++)
                {
                    pits[i] = new PitState[6];
                    for (int j = 0; j < 6; j++)
                    {
                        pits[i][j] = new PitState(i, j, 4);
                    }
                }
                banks = new int[2];
                for (int i = 0; i < 2; i++)
                {
                    banks[i] = 0;
                }
                playerTurn = turn;
            }
            public PitState[][] pits;
            public int[] banks;

            //Player who's current turn it is
            public int playerTurn;
            public List<BoardState> childNodes; 
        }

        private bool AIplayer;
        private int playerID;
        private BoardState kb;

        private BoardState MinimaxTree; 
        //Minimax structure
        //AI decision making
        //AI Knowledge Base

        public Player(int player)
        {
            AIplayer = false;
        }
        public Player(bool AI, int player)
        {
            if (AI)
            {
                AIplayer = true;
                kb = new BoardState(0);
                playerID = player;
            }
            else
            {
                AIplayer = false;
            }
        }
        public  Constants.ClickEvent GetPlayerInput()
        {
            //Handle player clicking
            Constants.ClickEvent click = new Constants.ClickEvent();
            return click;
        }
        public void UpdateKB(Board boardinfo) 
        {
            if(AIplayer) 
            {
                for (int i = 0; i < 2; i++)
                {
                    kb.banks[i] = boardinfo.GetScore(i);
                    for (int j = 0; j < 6; j++)
                    {
                        kb.pits[i][j].tokenCount = boardinfo.GetTokenCountAtPit(i, j);
                    }
                }
            //Update KB if AI
            }
        }
        public void GenerateMinimax()
        {
            MinimaxTree = new BoardState(playerID);
            for (int i = 0; i < 6; i++)
            {
                MinimaxTree.childNodes.Add(SimulateTurn(1,MinimaxTree));
            }
            
        }
        public BoardState SimulateTurn(int depth, BoardState root)
        {
            BoardState b = root;
            if (depth <= Constants.MAXDEPTH)
            {
                for (int i = 0; i < 6; i++)
                {
                    b.childNodes.Add(MoveTokens(b.playerTurn, i, b));
                    SimulateTurn(depth + 1, b);
                }
            }
            return b;
        }
        public BoardState MoveTokens(int player, int index, BoardState root)
        {
            BoardState b = root;
            Constants.PITNAMES lastPit = Constants.PITNAMES.PIT00;
            bool capture = false;
            int currentSide = player;
            int currentIndex = index;
            int tokensToMove = b.pits[currentSide][currentIndex - 1].tokenCount;
            b.pits[currentSide][currentIndex - 1].tokenCount = 0;
            while (tokensToMove > 0)
            {
                if (currentIndex > 5)
                {
                    if (currentSide == player)
                    {
                        b.banks[currentSide]++;
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
                        if (b.pits[currentSide][currentIndex].tokenCount == 0)
                        {
                            capture = true;
                        }
                    }
                    b.pits[currentSide][currentIndex].tokenCount++;
                    currentIndex++;
                    tokensToMove--;
                    lastPit = (Constants.PITNAMES)(currentSide * 10 + currentIndex - 1);
                }
            }
            if ((player * 10 + 6) == (int)lastPit)
            {
                b.playerTurn = player;
            }
            else
            {
                b.playerTurn = (player + 1) % 2;
            }
            if (capture)
            {
                if (player == 0)
                {
                    switch (lastPit)
                    {
                        case Constants.PITNAMES.PIT00:
                            b.banks[player] += b.pits[1][5].tokenCount;
                            b.pits[1][5].tokenCount = 0;
                            break;
                        case Constants.PITNAMES.PIT01:
                            b.banks[player] += b.pits[1][4].tokenCount;
                            b.pits[1][4].tokenCount = 0;
                            break;
                        case Constants.PITNAMES.PIT02:
                            b.banks[player] += b.pits[1][3].tokenCount;
                            b.pits[1][3].tokenCount = 0;
                            break;
                        case Constants.PITNAMES.PIT03:
                            b.banks[player] += b.pits[1][2].tokenCount;
                            b.pits[1][2].tokenCount = 0;
                            break;
                        case Constants.PITNAMES.PIT04:
                            b.banks[player] += b.pits[1][1].tokenCount;
                            b.pits[1][1].tokenCount = 0;
                            break;
                        case Constants.PITNAMES.PIT05:
                            b.banks[player] += b.pits[1][0].tokenCount;
                            b.pits[1][0].tokenCount = 0;
                            break;
                        default: break;
                    }
                }
                else
                {
                    switch (lastPit)
                    {
                        case Constants.PITNAMES.PIT10:
                            b.banks[player] += b.pits[0][5].tokenCount;
                            b.pits[1][5].tokenCount = 0;
                            break;
                        case Constants.PITNAMES.PIT11:
                            b.banks[player] += b.pits[0][4].tokenCount;
                            b.pits[1][5].tokenCount = 0;
                            break;
                        case Constants.PITNAMES.PIT12:
                            b.banks[player] += b.pits[0][3].tokenCount;
                            b.pits[1][5].tokenCount = 0;
                            break;
                        case Constants.PITNAMES.PIT13:
                            b.banks[player] += b.pits[0][2].tokenCount;
                            b.pits[1][5].tokenCount = 0;
                            break;
                        case Constants.PITNAMES.PIT14:
                            b.banks[player] += b.pits[0][1].tokenCount;
                            b.pits[1][5].tokenCount = 0;
                            break;
                        case Constants.PITNAMES.PIT15:
                            b.banks[player] += b.pits[0][0].tokenCount;
                            b.pits[1][5].tokenCount = 0;
                            break;
                        default: break;
                    }
                }
                capture = false;
            }
            return b;
        }
    }

}
