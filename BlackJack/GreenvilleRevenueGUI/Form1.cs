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
        Label[] PlayerLabelList = new Label[5];
        Label[] DealerLabelList = new Label[5];
        Button[] PlayerButtonList = new Button[5];
        Button[] DealerButtonList = new Button[5];
        Hand PlayerHand = new Hand("Paul");
        Hand DealerHand = new Hand("Dealer");
        DeckOfCards Deck = new DeckOfCards();
        Image HitMeGreyedout;
        Image HitMeOk;
        Image StayButtonGreyedOut;
        Image StayButtonOk;
        Image BackofCard;
        bool firsthand = false;
        bool lowcardstoggle = false;


        public Form1()
        {
            InitializeComponent();
            LoadLists();
            HitMeGreyedout = Image.FromFile("ButtonImages/hitmegreyedout.png");
            HitMeOk = Image.FromFile("ButtonImages/hitme200x175.png");
            StayButtonGreyedOut = Image.FromFile("ButtonImages/staygreyedout.png");
            StayButtonOk = Image.FromFile("ButtonImages/stay200x157.png");
            BackofCard = Image.FromFile("cards/Wfswbackcard.gif");
            hitButton.BackgroundImage = HitMeGreyedout;
            stayButton.BackgroundImage = StayButtonGreyedOut;
        }

        public void LoadLists()
        {
            PlayerButtonList[0] = playerCard1;
            PlayerButtonList[1] = playerCard2;
            PlayerButtonList[2] = playerCard3;
            PlayerButtonList[3] = playerCard4;
            PlayerButtonList[4] = playerCard5;
            DealerButtonList[0] = dealerCard1;
            DealerButtonList[1] = dealerCard2;
            DealerButtonList[2] = dealerCard3;
            DealerButtonList[3] = dealerCard4;
            DealerButtonList[4] = dealerCard5;
            PlayerLabelList[0] = playerCardValue1;
            PlayerLabelList[1] = playerCardValue2;
            PlayerLabelList[2] = playerCardValue3;
            PlayerLabelList[3] = playerCardValue4;
            PlayerLabelList[4] = playerCardValue5;
            DealerLabelList[0] = dealerCardValue1;
            DealerLabelList[1] = dealerCardValue2;
            DealerLabelList[2] = dealerCardValue3;
            DealerLabelList[3] = dealerCardValue4;
            DealerLabelList[4] = dealerCardValue5;
        }
        public void DisplayDealerGraphics()
        {
            if (firsthand)
            {
                for (int i = 0; i < 5; i++)
                {
                    DealerButtonList[i].Visible = false;
                    DealerLabelList[i].Text = "";
                }
                DealerButtonList[0].BackgroundImage = DealerHand.GetCard(0).GetImage();
                DealerButtonList[0].Visible = true;
                DealerButtonList[1].BackgroundImage = BackofCard;
                DealerButtonList[1].Visible = true;
                DealerLabelList[0].Text = DealerHand.GetCard(0).GetValueString();
                dealerTotal.Text = DealerHand.GetCard(0).GetValueString();
            }
            else
            {
                for (int i = 0; i < DealerHand.GetNumberofCards(); i++)
                {
                    Card acard = DealerHand.GetCard(i);
                    DealerButtonList[i].BackgroundImage = acard.GetImage();
                    DealerLabelList[i].Text = acard.GetValueString();
                    DealerButtonList[i].Visible = true;
                    dealerTotal.Text = DealerHand.GetTotalString();
                }
            }
        }
        public void DisplayPlayerGraphics()
        {
            if (firsthand)
            {
                for (int i = 0; i < 5; i++)
                {
                    PlayerLabelList[i].Text = "";
                    PlayerButtonList[i].Visible = false;
                    playerTotal.Text = "";
                }
                for (int i = 0; i < PlayerHand.GetNumberofCards(); i++)
                {
                    Card acard = PlayerHand.GetCard(i);
                    {
                        PlayerButtonList[i].BackgroundImage = acard.GetImage();
                        PlayerLabelList[i].Text = acard.GetValueString();
                        PlayerButtonList[i].Visible = true;
                        playerTotal.Text = PlayerHand.GetTotalString();
                    }
                }
            }
            else
            {
                for (int i = 0; i < PlayerHand.GetNumberofCards(); i++)
                {
                    Card acard = PlayerHand.GetCard(i);
                    PlayerButtonList[i].BackgroundImage = acard.GetImage();
                    PlayerLabelList[i].Text = acard.GetValueString();
                    PlayerButtonList[i].Visible = true;
                    playerTotal.Text = PlayerHand.GetTotalString();
                }
            }
        }
        private void startButton_Click(object sender, EventArgs e)
        {
            PlayerHand.ResetHand();
            DealerHand.ResetHand();
            hitButton.Enabled = true;
            stayButton.Enabled = true;
            hitButton.BackgroundImage = HitMeOk;
            stayButton.BackgroundImage = StayButtonOk;

            firsthand = true;
            if (lowcardstoggle)
            {
                Deck.DealerlowcardsFirst();
            }
            else
            {
                Deck.ShuffleDeck();
            }

            Card somecard = Deck.GetNextCard();
            DealerHand.DealCard(somecard);

            somecard = Deck.GetNextCard();
            PlayerHand.DealCard(somecard);

            somecard = Deck.GetNextCard();
            DealerHand.DealCard(somecard);

            somecard = Deck.GetNextCard();
            PlayerHand.DealCard(somecard);

            DisplayDealerGraphics();
            DisplayPlayerGraphics();
            firsthand = false;

            if (PlayerHand.GetTotal() == 21 && DealerHand.GetTotal() == 21)
            {
                PlayerHas21();
                DisplayDealerGraphics();
                MessageBox.Show("Tie. Nothing happens.");
            }
            else if (PlayerHand.GetTotal() == 21)
            {
                PlayerHas21();
                DisplayDealerGraphics();
                MessageBox.Show("Blackjack!! You win!!!");
            }
            else if (DealerHand.GetTotal() == 21)
            {
                PlayerHas21();
                DisplayDealerGraphics();
                MessageBox.Show("Dealer has Blackjack. You lose.");
            }
        }
        private void hitButton_Click(object sender, EventArgs e)
        {
            if (DealACardToPlayer())
            {
                if (PlayerHand.GetTotal() == 21)
                {                   
                    hitButton.Enabled = false;
                    hitButton.BackgroundImage = HitMeGreyedout;
                    stayButton.Enabled = false;
                    stayButton.BackgroundImage = StayButtonGreyedOut;
                    DealersTurn();
                    CheckWinner();
                    DisplayDealerGraphics();

                }
                else if (PlayerHand.GetTotal() > 21)
                {
                    PlayerHas21();
                    DisplayDealerGraphics();
                    MessageBox.Show("You busted! Dealer wins.");
                }
            }
            else
            {
                hitButton.Enabled = false;
                hitButton.BackgroundImage = HitMeGreyedout;
                startButton.Enabled = true;
            }
        }
        private void PlayerHas21()
        {
            hitButton.Enabled = false;
            stayButton.Enabled = false;
            hitButton.BackgroundImage = HitMeGreyedout;
            stayButton.BackgroundImage = StayButtonGreyedOut;
            startButton.Enabled = true;
        }
        private Boolean DealACardToDealer()
        {
            int numberofcards = DealerHand.GetNumberofCards();
            if (numberofcards < 5)
            {
                Card somecard = Deck.GetNextCard();
                DealerHand.DealCard(somecard);
                DisplayDealerGraphics();
                return true;
            }
            return false;
        }
        private Boolean DealACardToPlayer()
        {
            int numberofcards = PlayerHand.GetNumberofCards();
            if (numberofcards < 5)
            {
                Card somecard = Deck.GetNextCard();
                PlayerHand.DealCard(somecard);
                DisplayPlayerGraphics();
                return true;
            }
            return false;
        }
        private void DealersTurn()
        {
            bool dealing = true;
            while (dealing)
            {
                if (DealerHand.GetTotal() >= 21)
                {
                    dealing = false;
                }
                else
                {
                    if (DealerHand.GetTotal() < 17 || DealerHand.GetTotal() < PlayerHand.GetTotal())
                    {
                        if (!DealACardToDealer())
                        {
                            dealing = false;
                        }
                    }
                    if (DealerHand.GetTotal() >= 17 && DealerHand.GetTotal() >= PlayerHand.GetTotal())
                    {
                        dealing = false;
                    }
                }
            }
        }
        private void stayButton_Click(object sender, EventArgs e)
        {
            DisplayDealerGraphics();

            if (PlayerHand.GetTotal() > 21)
            {
                hitButton.Enabled = false;
                stayButton.Enabled = false;
                hitButton.BackgroundImage = HitMeGreyedout;
                stayButton.BackgroundImage = StayButtonGreyedOut;
            }
            else
            {
                bool dealing = true;
                while (dealing)
                {
                    if (DealerHand.GetTotal() >= 21)
                    {
                        dealing = false;
                    }
                    else
                    {
                        if (DealerHand.GetTotal() < 17 || DealerHand.GetTotal() < PlayerHand.GetTotal())
                        {
                            if (!DealACardToDealer())
                            {
                                dealing = false;
                            }
                        }
                        if (DealerHand.GetTotal() >= 17 && DealerHand.GetTotal() >= PlayerHand.GetTotal())
                        {
                            dealing = false;
                        }
                    }
                }
            }
            CheckWinner();
        }
        private void CheckWinner()
        {
            if (PlayerHand.GetTotal() > DealerHand.GetTotal())
            {
                if(DealerHand.GetTotal() > 21)
                {
                    MessageBox.Show("Dealer Busts. Player wins!");
                }
                else
                {
                    MessageBox.Show("Player has the better hand. Player wins!");
                }
                
            }
            else if (PlayerHand.GetTotal() < DealerHand.GetTotal())
            {
                if (DealerHand.GetTotal() <= 21)
                {
                    MessageBox.Show("Dealer has the better hand. Dealer wins.");
                }
                else
                {
                    MessageBox.Show("Dealer Busts. Player wins!");
                }
            }
            else
            {
                MessageBox.Show("Tie. Nothing happens.");
            }
            hitButton.Enabled = false;
            stayButton.Enabled = false;
            hitButton.BackgroundImage = HitMeGreyedout;
            stayButton.BackgroundImage = StayButtonGreyedOut;
        }

        private void LowCardsButton_Click(object sender, EventArgs e)
        {
            if (lowcardstoggle)
            {
                lowcardstoggle = false;
                LowCardsButton.BackColor = Color.White;
            }
            else
            {
                lowcardstoggle = true;
                LowCardsButton.BackColor = Color.Lime;
            }
        }
    }
}

