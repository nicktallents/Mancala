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
        public enum MAXMIN { MAX = 0, MIN = 1 };
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
                        pits[i][j] = new PitState(j, i, 4);
                    }
                }
                banks = new int[2];
                for (int i = 0; i < 2; i++)
                {
                    banks[i] = 0;
                }
                playerTurn = turn;
                childNodes = new List<BoardState>();
                end = false;
            }
            public BoardState(BoardState b)
            {
                if (b == null)
                {
                    invalidMove = true;

                }
                else
                {
                    this.pits = new PitState[2][];
                    for (int i = 0; i < 2; i++)
                    {
                        this.pits[i] = new PitState[6];
                        for (int j = 0; j < 6; j++)
                        {
                            this.pits[i][j] = new PitState(j, i, b.pits[i][j].tokenCount);
                        }
                    }
                    this.banks = new int[2];
                    for (int i = 0; i < 2; i++)
                    {
                        this.banks[i] = b.banks[i];
                    }
                    this.playerTurn = b.playerTurn;
                    this.childNodes = new List<BoardState>();
                    foreach (BoardState children in b.childNodes)
                    {
                        this.childNodes.Add(children);
                    }
                    this.end = b.end;
                }
            }

            public bool invalidMove;
            public PitState[][] pits;
            public int[] banks;

            public bool end;

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
        public Constants.ClickEvent GetPlayerInput()
        {
            //Handle player clicking
            Constants.ClickEvent click = new Constants.ClickEvent();
            return click;
        }
        public void UpdateKB(Board boardinfo)
        {
            if (AIplayer)
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
            MinimaxTree = new BoardState(SimulateTurn(1, kb, playerID));

        }
        public BoardState SimulateTurn(int depth, BoardState root, int currentTurn)
        {
            BoardState b = new BoardState(root);
            b.playerTurn = currentTurn;
            if (depth <= Constants.MAXDEPTH)
            {
                if (depth == Constants.MAXDEPTH)
                {
                    b.end = true;
                    return b;
                }
                b.childNodes = new List<BoardState>();
                for (int i = 0; i < 6; i++)
                {
                    BoardState temp = new BoardState(MoveTokens(b.playerTurn, i, b));
                    if (!temp.invalidMove)
                    {
                        b.childNodes.Add(temp);
                    }
                }

                for (int i = 0; i < b.childNodes.Count; i++)
                {
                    if (b.childNodes[i] != null)
                    {
                        b.childNodes[i] = new BoardState(SimulateTurn(depth + 1, b.childNodes[i], (currentTurn + 1) % 2));
                    }
                }
                return b;
            }
            else return null;
        }
        public BoardState MoveTokens(int player, int index, BoardState root)
        {
            BoardState b = new BoardState(root);
            Constants.PITNAMES lastPit = Constants.PITNAMES.PIT00;
            bool capture = false;
            int currentSide = player;
            int currentIndex = index;
            int tokensToMove = b.pits[currentSide][currentIndex].tokenCount;
            b.pits[currentSide][currentIndex].tokenCount = 0;
            currentIndex++;
            if (tokensToMove == 0)
            {
                return null;
            }
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
                            if (!(b.pits[1][5].tokenCount == 0))
                            {
                                b.banks[player] += b.pits[1][5].tokenCount;
                                b.pits[1][5].tokenCount = 0;
                                b.banks[player] += b.pits[0][0].tokenCount;
                                b.pits[0][0].tokenCount = 0;
                            }
                            break;
                        case Constants.PITNAMES.PIT01:
                            if (!(b.pits[1][4].tokenCount == 0))
                            {
                                b.banks[player] += b.pits[1][4].tokenCount;
                                b.pits[1][4].tokenCount = 0;
                                b.banks[player] += b.pits[0][1].tokenCount;
                                b.pits[0][1].tokenCount = 0;
                            }
                            break;
                        case Constants.PITNAMES.PIT02:
                            if (!(b.pits[1][3].tokenCount == 0))
                            {
                                b.banks[player] += b.pits[1][3].tokenCount;
                                b.pits[1][3].tokenCount = 0;
                                b.banks[player] += b.pits[0][2].tokenCount;
                                b.pits[0][2].tokenCount = 0;
                            }
                            break;
                        case Constants.PITNAMES.PIT03:
                            if (!(b.pits[1][2].tokenCount == 0))
                            {
                                b.banks[player] += b.pits[1][2].tokenCount;
                                b.pits[1][2].tokenCount = 0;
                                b.banks[player] += b.pits[0][3].tokenCount;
                                b.pits[0][3].tokenCount = 0;
                            }
                            break;
                        case Constants.PITNAMES.PIT04:
                            if (!(b.pits[1][1].tokenCount == 0))
                            {
                                b.banks[player] += b.pits[1][1].tokenCount;
                                b.pits[1][1].tokenCount = 0;
                                b.banks[player] += b.pits[0][4].tokenCount;
                                b.pits[0][4].tokenCount = 0;
                            }
                            break;
                        case Constants.PITNAMES.PIT05:
                            if (!(b.pits[1][0].tokenCount == 0))
                            {
                                b.banks[player] += b.pits[1][0].tokenCount;
                                b.pits[1][0].tokenCount = 0;
                                b.banks[player] += b.pits[0][5].tokenCount;
                                b.pits[0][5].tokenCount = 0;
                            }
                            break;
                        default: break;
                    }
                }
                else
                {
                    switch (lastPit)
                    {
                        case Constants.PITNAMES.PIT10:
                            if (!(b.pits[0][5].tokenCount == 0))
                            {
                                b.banks[player] += b.pits[0][5].tokenCount;
                                b.pits[0][5].tokenCount = 0;
                                b.banks[player] += b.pits[1][0].tokenCount;
                                b.pits[1][0].tokenCount = 0;
                            }
                            break;
                        case Constants.PITNAMES.PIT11:
                            if (!(b.pits[0][4].tokenCount == 0))
                            {
                                b.banks[player] += b.pits[0][4].tokenCount;
                                b.pits[0][4].tokenCount = 0;
                                b.banks[player] += b.pits[1][1].tokenCount;
                                b.pits[1][1].tokenCount = 0;
                            }
                            break;
                        case Constants.PITNAMES.PIT12:
                            if (!(b.pits[0][3].tokenCount == 0))
                            {
                                b.banks[player] += b.pits[0][3].tokenCount;
                                b.pits[0][3].tokenCount = 0;
                                b.banks[player] += b.pits[1][2].tokenCount;
                                b.pits[1][2].tokenCount = 0;
                            }
                            break;
                        case Constants.PITNAMES.PIT13:
                            if (!(b.pits[0][2].tokenCount == 0))
                            {
                                b.banks[player] += b.pits[0][2].tokenCount;
                                b.pits[0][2].tokenCount = 0;
                                b.banks[player] += b.pits[1][3].tokenCount;
                                b.pits[1][3].tokenCount = 0;
                            }
                            break;
                        case Constants.PITNAMES.PIT14:
                            if (!(b.pits[0][1].tokenCount == 0))
                            {
                                b.banks[player] += b.pits[0][1].tokenCount;
                                b.pits[0][1].tokenCount = 0;
                                b.banks[player] += b.pits[1][4].tokenCount;
                                b.pits[1][4].tokenCount = 0;
                            }
                            break;
                        case Constants.PITNAMES.PIT15:
                            if (!(b.pits[0][0].tokenCount == 0))
                            {
                                b.banks[player] += b.pits[0][0].tokenCount;
                                b.pits[0][0].tokenCount = 0;
                                b.banks[player] += b.pits[1][5].tokenCount;
                                b.pits[1][5].tokenCount = 0;
                            }
                            break;
                        default: break;
                    }
                }
                capture = false;
            }
            return b;
        }
        public Constants.ClickEvent Decision()
        {
            int index = ParseDecisionTree();
            Constants.ClickEvent click = new Constants.ClickEvent();
            click.pitIndex = index;
            click.pitSide = playerID;
            click.player = playerID;
            return click;
        }
        public int ParseDecisionTree()
        {
            
            int max = Constants.LOWMAX;
            int index = 0;
            int pitIndex = 0;
            for (int i = 0; i < MinimaxTree.childNodes.Count; i++)
            {
                int temp = ParseDecisionTree(MinimaxTree.childNodes[i], (int)MAXMIN.MAX);
                
                if (temp > max && kb.pits[playerID][i].tokenCount>0)
                {
                    max = temp;
                    pitIndex = kb.pits[playerID][i].pitIndex;
                    index = i;
                }
            }

            //If best decision is on a pit with no tokens, select the next valid pit
            if(kb.pits[playerID][index].tokenCount == 0) {
                index = 0;
                while (kb.pits[playerID][index].tokenCount == 0)
                {
                    index++;
                    pitIndex = kb.pits[playerID][index].pitIndex;
                }
            }


            return pitIndex;
        }
        public int ParseDecisionTree(BoardState b, int turn)
        {
            if (b.end)
            {
                return b.banks[playerID] - b.banks[(playerID + 1) % 2];
            }
            if (turn == (int)MAXMIN.MAX)
            {
                int max = Constants.LOWMAX;
                foreach (BoardState choice in b.childNodes)
                {
                    if (choice == null) continue;
                    int temp = ParseDecisionTree(choice, (int)MAXMIN.MIN);
                    if (temp > max)
                    {
                        max = temp;
                    }
                }
                return max;
            }
            else
            {
                int min = Constants.HIGHMIN;
                foreach (BoardState choice in b.childNodes)
                {
                    if (choice == null) continue;
                    int temp = ParseDecisionTree(choice, (int)MAXMIN.MAX);
                    if (temp < min)
                    {
                        min = temp;
                    }
                }
                return min;
            }
        }

    }
}
