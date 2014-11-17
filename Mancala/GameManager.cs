using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mancala
{
    class GameManager
    {
        private Player player1;
        private Player player2;
        public GameManager()
        {
            player1 = CreatePlayer();
            player2 = CreatePlayer();
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
        }
        public void Run()
        {
        }
        private Player GenerateAIPlayer()
        {
            return new Player;
        }
        private Player CreatePlayer()
        {
            return new Player;
        }
    }
}
