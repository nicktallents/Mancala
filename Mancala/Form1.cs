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
        public Form1()
        {
            InitializeComponent();
            GM = new GameManager(0);
        }

        private void p1pit1_Click(object sender, EventArgs e)
        {
            Constants.ClickEvent click = new Constants.ClickEvent();
            click.pitSide = 1;
            click.pitIndex = 1;
            GM.GetInput(click);
        }

        private void p1pit2_Click(object sender, EventArgs e)
        {
            Constants.ClickEvent click = new Constants.ClickEvent();
            click.pitSide = 1;
            click.pitIndex = 2;
            GM.GetInput(click);
        }

        private void p1pit3_Click(object sender, EventArgs e)
        {
            Constants.ClickEvent click = new Constants.ClickEvent();
            click.pitSide = 1;
            click.pitIndex = 3;
            GM.GetInput(click);
        }

        private void p1pit4_Click(object sender, EventArgs e)
        {
            Constants.ClickEvent click = new Constants.ClickEvent();
            click.pitSide = 1;
            click.pitIndex = 4;
            GM.GetInput(click);
        }

        private void p1pit5_Click(object sender, EventArgs e)
        {
            Constants.ClickEvent click = new Constants.ClickEvent();
            click.pitSide = 1;
            click.pitIndex = 5;
            GM.GetInput(click);
        }

        private void p1pit6_Click(object sender, EventArgs e)
        {
            Constants.ClickEvent click = new Constants.ClickEvent();
            click.pitSide = 1;
            click.pitIndex = 6;
            GM.GetInput(click);
        }

        private void p2pit1_Click(object sender, EventArgs e)
        {
            Constants.ClickEvent click = new Constants.ClickEvent();
            click.pitSide = 2;
            click.pitIndex = 1;
            GM.GetInput(click);
        }

        private void p2pit2_Click(object sender, EventArgs e)
        {
            Constants.ClickEvent click = new Constants.ClickEvent();
            click.pitSide = 2;
            click.pitIndex = 2;
            GM.GetInput(click);
        }

        private void p2pit3_Click(object sender, EventArgs e)
        {
            Constants.ClickEvent click = new Constants.ClickEvent();
            click.pitSide = 2;
            click.pitIndex = 3;
            GM.GetInput(click);
        }

        private void p2pit4_Click(object sender, EventArgs e)
        {
            Constants.ClickEvent click = new Constants.ClickEvent();
            click.pitSide = 2;
            click.pitIndex = 4;
            GM.GetInput(click);
        }

        private void p2pit5_Click(object sender, EventArgs e)
        {
            Constants.ClickEvent click = new Constants.ClickEvent();
            click.pitSide = 2;
            click.pitIndex = 5;
            GM.GetInput(click);
        }

        private void p2pit6_Click(object sender, EventArgs e)
        {
            Constants.ClickEvent click = new Constants.ClickEvent();
            click.pitSide = 2;
            click.pitIndex = 6;
            GM.GetInput(click);
        }

    }
}
