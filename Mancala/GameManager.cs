using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
                    MessageBox.Show("Invalid Move! Please select a pit that contains tokens.");
                }
                else
                {
                    gameBoard.MoveTokens(click);
                }
            }
            else
            {
                MessageBox.Show("Invalid Move! Please select a pit on your side.");
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
            if (gameBoard.SideEmpty(0) || gameBoard.SideEmpty(1))
            {
                //Handle game win
                return true;
            }
            return false;
        }
    }
}
