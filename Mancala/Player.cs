using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mancala
{
    public class Player
    {
        private bool AIplayer; 
        //Minimax structure
        //AI decision making
        //AI Knowledge Base

        public Player()
        {
            AIplayer = false;
        }
        public Player(bool AI)
        {
            if (AI)
            {
                AIplayer = true;
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
            //Update KB if AI
            }
        }
        

    }
}
