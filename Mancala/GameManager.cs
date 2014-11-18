using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mancala
{
    public class GameManager
    {
        private Board gameBoard;
        private Player player1;
        private Player player2;
        private int currentPlayer;
        public GameManager()
        {
            player1 = CreatePlayer();
            player2 = CreatePlayer();
            gameBoard = new Board();
        }
        public GameManager(int numAI) 
        {
            if (numAI == 0)
            {
                player1 = CreatePlayer();
                player2 = CreatePlayer();
            }
            else if(numAI == 1) {
                player1 = CreatePlayer();
                player2 = GenerateAIPlayer();
            }
            else if (numAI == 2)
            {
                player1 = GenerateAIPlayer();
                player2 = GenerateAIPlayer();
            }
            else
            {
                Console.Write("Invalid AI number");
                Environment.Exit(0);
            }
            gameBoard = new Board();
            currentPlayer = 1;
        }
        public Board GetBoard() { return gameBoard; }
        public void GetInput(Constants.ClickEvent click)
        {
            if (click.pitSide == currentPlayer)
            {
                if (gameBoard.EmptyPit(click))
                {
                    //Invalid move
                }
                else
                {
                    gameBoard.MoveTokens(click);
                }
            }
            else
            {
                //Invalid side
            }
        }
        public int GetCurrentPlayer()
        {
            return currentPlayer;
        }
        public void Run()
        {
        }
        private Player GenerateAIPlayer()
        {
            return new Player();
        }
        private Player CreatePlayer()
        {
            return new Player();
        }
        private bool CheckWin()
        {
            if (gameBoard.SideEmpty(1) || gameBoard.SideEmpty(2))
            {
                //Handle game win
                return true;
            }
            return false;
        }
    }
}
