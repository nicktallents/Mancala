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
        private Form1 form;
        private Board gameBoard;
        private Player player1;
        private Player player2;
        private int currentPlayer;
        public GameManager()
        {
            player1 = CreatePlayer(0);
            player2 = CreatePlayer(1);
            gameBoard = new Board();
        }
        public GameManager(int numAI, Form1 form) 
        {
            if (numAI == 0)
            {
                player1 = CreatePlayer(0);
                player2 = CreatePlayer(1);
            }
            else if(numAI == 1) {
                player1 = CreatePlayer(0);
                player2 = GenerateAIPlayer(1);
            }
            else if (numAI == 2)
            {
                player1 = GenerateAIPlayer(0);
                player2 = GenerateAIPlayer(1);
            }
            else
            {
                Console.Write("Invalid AI number");
                Environment.Exit(0);
            }
            gameBoard = new Board();
            Random r = new Random();
            this.form = form;
            
        }
        public int GetCurrentPlayer()
        {
            return currentPlayer;
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
                    gameBoard.CheckCapture(currentPlayer);
                    form.addHistory(click);
                    if(!gameBoard.CheckExtraTurn(currentPlayer)) {
                        currentPlayer = (currentPlayer + 1) % 2;
                    }
                    if (Constants.AICOUNT == 1)
                    {
                        RunAI();
                    }
                    
                }
            }
            else
            {
                MessageBox.Show("Invalid Move! Please select a pit on your side.");
            }
        }
        public void RunAI()
        {
            while (currentPlayer == 1)
            {
                player2.UpdateKB(gameBoard);
                player2.GenerateMinimax();
                Constants.ClickEvent c = player2.Decision();
                form.addHistory(c);
                gameBoard.MoveTokens(c);
                gameBoard.CheckCapture(currentPlayer);
                if (!gameBoard.CheckExtraTurn(currentPlayer))
                {
                    currentPlayer = (currentPlayer + 1) % 2;
                }
                
                form.UpdateBoard();
            }
           
        }
        private Player GenerateAIPlayer(int player)
        {
            return new Player(true, player);
        }
        private Player CreatePlayer(int player)
        {
            return new Player(false, player);
        }
        public bool CheckWin()
        {
            bool p1win = gameBoard.SideEmpty(0);
            bool p2win = gameBoard.SideEmpty(1);
            if (p1win || p2win)
            {
                //Handle game win
                if (p1win)
                {
                    gameBoard.SideCleared(0);
                }
                else
                {
                    gameBoard.SideCleared(1);
                }
                return true;
            }
            return false;
        }
    }
}
