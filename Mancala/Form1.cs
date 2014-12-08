using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mancala
{
    public partial class Form1 : Form
    {
        private GameManager GM;
        private bool firstActivated = true;
        public Form1()
        {
            InitializeComponent();
            GM = new GameManager(Constants.AICOUNT);
        }

        public void DrawString(int pitCount, int pitSide, int pitIndex)
        {
           
            System.Drawing.Graphics formGraphics = this.CreateGraphics();
            string drawString = pitCount.ToString();
            System.Drawing.Font drawFont = new System.Drawing.Font("Arial", 16);
            System.Drawing.SolidBrush drawBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
            float x = Constants.tokenCountLoc[pitSide,pitIndex,0];
            float y = Constants.tokenCountLoc[pitSide, pitIndex, 1];
            System.Drawing.StringFormat drawFormat = new System.Drawing.StringFormat();
            formGraphics.DrawString(drawString, drawFont, drawBrush, x, y, drawFormat);
            drawFont.Dispose();
            drawBrush.Dispose();
            formGraphics.Dispose();
        }
        public void DrawGUI(string currentPlayer)
        {
            System.Drawing.Graphics formGraphics = this.CreateGraphics();
            System.Drawing.Font drawFont = new System.Drawing.Font("Arial", 16);
            System.Drawing.SolidBrush drawBrush = new System.Drawing.SolidBrush(System.Drawing.Color.White);
            float x = 450.0f;
            float y = 320.0f;
            System.Drawing.StringFormat drawFormat = new System.Drawing.StringFormat();
            formGraphics.DrawString(currentPlayer, drawFont, drawBrush, x, y, drawFormat);
            drawFont.Dispose();
            drawBrush.Dispose();
            formGraphics.Dispose();
        }
        public void UpdateBoard()
        {
            this.Refresh();
            Board b = GM.GetBoard();
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    DrawString(b.GetTokenCountAtPit(i, j), i, j);
                
                }
                DrawString(b.GetScore(i), i, 6);
            }
            string turn = "Current Player: ";
            if(GM.GetCurrentPlayer() == 0) {
                turn += "Player 1";
            }
            else {
                turn += "Player 2";
            }
            DrawGUI(turn);
            if (GM.CheckWin())
            {
                string msg = "Player 1 Score:" + b.GetScore(0) + "\nPlayer 2 Score: " + b.GetScore(1);
                if(b.GetScore(0) > b.GetScore(1)) {
                    msg += "\nPlayer 1 is the winner!";
                }
                else 
                {
                    msg += "\nPlayer 2 is the winner!";
                }
                MessageBox.Show(msg);
                Application.Exit();
            }
        }

        private void p1pit1_Click(object sender, EventArgs e)
        {
            //Remember that I pass slightly different values for pit and side than the object names because of index notation.
            //DON'T GET CONFUSED BY THIS LATER. 
            Constants.ClickEvent click = new Constants.ClickEvent();
            click.pitSide = 0;
            click.pitIndex = 0;
            click.player = GM.GetCurrentPlayer();
            GM.GetInput(click);
            UpdateBoard();
            

        }

        private void p1pit2_Click(object sender, EventArgs e)
        {
            Constants.ClickEvent click = new Constants.ClickEvent();
            click.pitSide = 0;
            click.pitIndex = 1;
            click.player = GM.GetCurrentPlayer();
            GM.GetInput(click); 
            UpdateBoard();
        }

        private void p1pit3_Click(object sender, EventArgs e)
        {
            Constants.ClickEvent click = new Constants.ClickEvent();
            click.pitSide = 0;
            click.pitIndex = 2;
            click.player = GM.GetCurrentPlayer();
            GM.GetInput(click);
            UpdateBoard();
        }

        private void p1pit4_Click(object sender, EventArgs e)
        {
            Constants.ClickEvent click = new Constants.ClickEvent();
            click.pitSide = 0;
            click.pitIndex = 3;
            click.player = GM.GetCurrentPlayer();
            GM.GetInput(click);
            UpdateBoard();
        }

        private void p1pit5_Click(object sender, EventArgs e)
        {
            Constants.ClickEvent click = new Constants.ClickEvent();
            click.pitSide = 0;
            click.pitIndex = 4;
            click.player = GM.GetCurrentPlayer();
            GM.GetInput(click);
            UpdateBoard();
        }

        private void p1pit6_Click(object sender, EventArgs e)
        {
            Constants.ClickEvent click = new Constants.ClickEvent();
            click.pitSide = 0;
            click.pitIndex = 5;
            click.player = GM.GetCurrentPlayer();
            GM.GetInput(click);
            UpdateBoard();
        }

        private void p2pit1_Click(object sender, EventArgs e)
        {
            Constants.ClickEvent click = new Constants.ClickEvent();
            click.pitSide = 1;
            click.pitIndex = 0;
            click.player = GM.GetCurrentPlayer();
            GM.GetInput(click);
            UpdateBoard();
        }

        private void p2pit2_Click(object sender, EventArgs e)
        {
            Constants.ClickEvent click = new Constants.ClickEvent();
            click.pitSide = 1;
            click.pitIndex = 1;
            click.player = GM.GetCurrentPlayer();
            GM.GetInput(click);
            UpdateBoard();
        }

        private void p2pit3_Click(object sender, EventArgs e)
        {
            Constants.ClickEvent click = new Constants.ClickEvent();
            click.pitSide = 1;
            click.pitIndex = 2;
            click.player = GM.GetCurrentPlayer();
            GM.GetInput(click);
            UpdateBoard();
        }

        private void p2pit4_Click(object sender, EventArgs e)
        {
            Constants.ClickEvent click = new Constants.ClickEvent();
            click.pitSide = 1;
            click.pitIndex = 3;
            click.player = GM.GetCurrentPlayer();
            GM.GetInput(click);
            UpdateBoard();
        }

        private void p2pit5_Click(object sender, EventArgs e)
        {
            Constants.ClickEvent click = new Constants.ClickEvent();
            click.pitSide = 1;
            click.pitIndex = 4;
            click.player = GM.GetCurrentPlayer();
            GM.GetInput(click);
            UpdateBoard();
        }

        private void p2pit6_Click(object sender, EventArgs e)
        {
            Constants.ClickEvent click = new Constants.ClickEvent();
            click.pitSide = 1;
            click.pitIndex = 5;
            click.player = GM.GetCurrentPlayer();
            GM.GetInput(click);
            UpdateBoard();
        }

        private void onLoad(object sender, EventArgs e)
        {
        }

        private void onActivated(object sender, EventArgs e)
        {
            if (firstActivated)
            {
                UpdateBoard();
                firstActivated = false;
            }
        }

    }
}
