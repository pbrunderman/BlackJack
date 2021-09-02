using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace GreenvilleRevenueGUI
{
    public partial class Form1 : Form
    {
        BlackJack Game = new BlackJack();

        public Form1()
        {            
            InitializeComponent();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            Game.DealStartingHand();
            playerCardValue1.Text = Convert.ToString(Game.GetPlayer().GetPlayerHand(0).GetValue());
            playerCardValue2.Text = Convert.ToString(Game.GetPlayer().GetPlayerHand(1).GetValue());
            playerCard1.BackgroundImage = Game.GetPlayer().GetPlayerHand(0).GetImage();
            playerCard2.BackgroundImage = Game.GetPlayer().GetPlayerHand(1).GetImage();
            playerTotal.Text = Convert.ToString(Game.GetPlayer().GetTotal());

            dealerCardValue1.Text = Convert.ToString(Game.GetDealer().GetDealerHand(0).GetValue());
            dealerCard1.BackgroundImage = Game.GetDealer().GetDealerHand(0).GetImage();
            dealerTotal.Text = Convert.ToString(Game.GetDealer().GetDealerHand(0).GetValue());
            startButton.Enabled = false;
        }

        private void hitButton_Click(object sender, EventArgs e)
        {

        }
    }
}

