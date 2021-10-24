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
        bool acestoggle = false;
        bool dealeracestoggle = false;
        Funds playerfunds = new Funds(5000);


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
        private void startButton_Click(object sender, EventArgs e)
        {
            if (playerfunds.GetBetAmount() > playerfunds.GetTotalFunds())
            {
                numericUpDown1.Value = playerfunds.GetTotalFunds();
                playerfunds.SetBetAmount(playerfunds.GetBetAmount());
            }
            PlayerHand.ResetHand();
            DealerHand.ResetHand();
            EnableCardButtons();
            hitButton.Enabled = true;
            stayButton.Enabled = true;
            hitButton.BackgroundImage = HitMeOk;
            stayButton.BackgroundImage = StayButtonOk;
            numericUpDown1.Enabled = false;
            LowCardsButton.Enabled = false;
            AcesButton.Enabled = false;
            DealerAcesButton.Enabled = false;

            firsthand = true;
            if (lowcardstoggle)
            {
                Deck.DealerlowcardsFirst();
            }
            else if (acestoggle)
            {
                Deck.PlayerDealerAces();
            }
            else if (dealeracestoggle)
            {
                Deck.DealerAces();
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
                PlayerStopHitAndStay();
                DisplayDealerGraphics();
                MessageBox.Show("Tie. Nothing happens.");
            }
            else if (PlayerHand.GetTotal() == 21)
            {
                playerfunds.WonBet();
                numericUpDown1.Maximum = playerfunds.GetTotalFunds();
                PlayerStopHitAndStay();
                DisplayDealerGraphics();
                DisplayPlayerGraphics();
                MessageBox.Show("Blackjack!! You win!!!");
            }
            else if (DealerHand.GetTotal() == 21)
            {
                playerfunds.LostBet();
                numericUpDown1.Maximum = playerfunds.GetTotalFunds();
                PlayerStopHitAndStay();
                DisplayDealerGraphics();
                DisplayPlayerGraphics();
                MessageBox.Show("Dealer has Blackjack. You lose.");
                CheckBankrupt();
            }
        }
        private void hitButton_Click(object sender, EventArgs e)
        {
            if (DealACardToPlayer())
            {
                if (PlayerHand.GetTotal() == 21)
                {
                    PlayerStopHitAndStay();
                    DealersTurn();
                    CheckWinner();
                    DisplayDealerGraphics();

                }
                else if (PlayerHand.GetTotal() > 21 && !PlayerHand.HasAces())
                {
                    playerfunds.LostBet();
                    PlayerStopHitAndStay();
                    DisplayDealerGraphics();
                    DisplayPlayerGraphics();
                    MessageBox.Show("You busted! Dealer wins.");
                    CheckBankrupt();
                }
            }
            else
            {
                hitButton.Enabled = false;
                hitButton.BackgroundImage = HitMeGreyedout;
            }
        }
        private void stayButton_Click(object sender, EventArgs e)
        {
            DisplayDealerGraphics();

            if (PlayerHand.GetTotal() > 21)
            {
                playerfunds.LostBet();
                PlayerStopHitAndStay();
                DealerCheckAce();
                DisplayDealerGraphics();
                DisplayPlayerGraphics();
                MessageBox.Show("You busted! Dealer wins.");
                CheckBankrupt();
            }
            else
            {
                bool dealing = true;
                while (dealing)
                {
                    if (DealerHand.GetTotal() >= 21)
                    {
                        if (!DealerCheckAce())
                        {
                            dealing = false;
                        }
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
                            if (!DealerCheckAce())
                            {
                                dealing = false;
                            }
                        }
                    }
                }
                CheckWinner();
                numericUpDown1.Enabled = true;
            }
        }
        private bool DealerCheckAce()
        {
            if (DealerHand.GetTotal() > 21 && DealerHand.HasAces())
            {
                for (int i = 0; i < DealerHand.GetNumberofCards(); i++)
                {
                    if (DealerHand.GetCard(i).GetAce() && DealerHand.GetCard(i).GetValue() == 11)
                    {
                        DealerHand.GetCard(i).ToggleAce();
                        DealerHand.SetTotal();
                        DisplayDealerGraphics();
                        return true;
                    }
                }
            }
            return false;
        }
        private void LowCardsButton_Click(object sender, EventArgs e)
        {
            if (lowcardstoggle)
            {
                lowcardstoggle = false;
                LowCardsButton.BackColor = Color.White;
                AcesButton.Enabled = true;
                DealerAcesButton.Enabled = true;
            }
            else
            {
                lowcardstoggle = true;
                LowCardsButton.BackColor = Color.Lime;
                AcesButton.Enabled = false;
                DealerAcesButton.Enabled = false;
            }
        }
        private void Aces_Click(object sender, EventArgs e)
        {
            if (acestoggle)
            {
                acestoggle = false;
                AcesButton.BackColor = Color.White;
                LowCardsButton.Enabled = true;
                DealerAcesButton.Enabled = true;
            }
            else
            {
                acestoggle = true;
                AcesButton.BackColor = Color.Lime;
                LowCardsButton.Enabled = false;
                DealerAcesButton.Enabled = false;
            }
        }
        private void DealerAcesButton_Click(object sender, EventArgs e)
        {
            if (dealeracestoggle)
            {
                dealeracestoggle = false;
                DealerAcesButton.BackColor = Color.White;
                AcesButton.Enabled = true;
                LowCardsButton.Enabled = true;
            }
            else
            {
                dealeracestoggle = true;
                DealerAcesButton.BackColor = Color.Lime;
                AcesButton.Enabled = false;
                LowCardsButton.Enabled = false;
            }
        }

        private void playerCard1_Click(object sender, EventArgs e)
        {
            PlayerHand.GetCard(0).ToggleAce();
            PlayerHand.SetTotal();
            DisplayPlayerGraphics();
        }

        private void playerCard2_Click(object sender, EventArgs e)
        {
            PlayerHand.GetCard(1).ToggleAce();
            PlayerHand.SetTotal();
            DisplayPlayerGraphics();
        }

        private void playerCard3_Click(object sender, EventArgs e)
        {
            PlayerHand.GetCard(2).ToggleAce();
            PlayerHand.SetTotal();
            DisplayPlayerGraphics();
        }

        private void playerCard4_Click(object sender, EventArgs e)
        {
            PlayerHand.GetCard(3).ToggleAce();
            PlayerHand.SetTotal();
            DisplayPlayerGraphics();
        }

        private void playerCard5_Click(object sender, EventArgs e)
        {
            PlayerHand.GetCard(4).ToggleAce();
            PlayerHand.SetTotal();
            DisplayPlayerGraphics();
        }
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            playerfunds.SetBetAmount((int)numericUpDown1.Value);
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
                    if (!DealerCheckAce())
                    {
                        dealing = false;
                    }
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
                        if (!DealerCheckAce())
                        {
                            dealing = false;
                        }
                    }
                }
            }
        }
        
        private void CheckWinner()
        {
            if (PlayerHand.GetTotal() > DealerHand.GetTotal())
            {
                if (DealerHand.GetTotal() > 21)
                {
                    playerfunds.WonBet();
                    numericUpDown1.Maximum = playerfunds.GetTotalFunds();
                    DisplayPlayerGraphics();
                    MessageBox.Show("Dealer's Hand:"+DealerHand.GetTotalString()+"\nPlayer's Hand:"+PlayerHand.GetTotalString()+ "\n\nDealer Busts. You win!");
                }
                else
                {

                    playerfunds.WonBet();
                    numericUpDown1.Maximum = playerfunds.GetTotalFunds();
                    DisplayPlayerGraphics();
                    MessageBox.Show("Dealer's Hand:" + DealerHand.GetTotalString() + "\nPlayer's Hand:" + PlayerHand.GetTotalString() + "\n\nYou have the better hand. You win!");
                }

            }
            else if (PlayerHand.GetTotal() < DealerHand.GetTotal())
            {
                if (DealerHand.GetTotal() <= 21)
                {
                    playerfunds.LostBet();
                    numericUpDown1.Maximum = playerfunds.GetTotalFunds();
                    DisplayPlayerGraphics();
                    MessageBox.Show("Dealer's Hand:" + DealerHand.GetTotalString() + "\nPlayer's Hand:" + PlayerHand.GetTotalString() + "\n\nDealer has the better hand. You lose.");
                    CheckBankrupt();
                }
                else
                {
                    playerfunds.WonBet();
                    numericUpDown1.Maximum = playerfunds.GetTotalFunds();
                    DisplayPlayerGraphics();
                    MessageBox.Show("Dealer's Hand:" + DealerHand.GetTotalString() + "\nPlayer's Hand:" + PlayerHand.GetTotalString() + "\n\nDealer Busts. You win!");
                }
            }
            else
            {
                MessageBox.Show("Dealer's Hand:" + DealerHand.GetTotalString() + "\nPlayer's Hand:" + PlayerHand.GetTotalString() + "\n\nTie. Nothing happens.");
            }
            PlayerStopHitAndStay();
            DiableCardButtons();
            numericUpDown1.Enabled = true;

        }
        private void CheckBankrupt()
        {
            if (playerfunds.GetTotalFunds() <= 0)
            {
                MessageBox.Show("You ran out of money. Game over!");
                Application.Exit();
            }
        }
        private void DiableCardButtons()
        {
            playerCard1.Enabled = false;
            playerCard2.Enabled = false;
            playerCard3.Enabled = false;
            playerCard4.Enabled = false;
            playerCard5.Enabled = false;
        }
        private void EnableCardButtons()
        {
            playerCard1.Enabled = true;
            playerCard2.Enabled = true;
            playerCard3.Enabled = true;
            playerCard4.Enabled = true;
            playerCard5.Enabled = true;
        }
        private void PlayerStopHitAndStay()
        {
            hitButton.Enabled = false;
            stayButton.Enabled = false;
            hitButton.BackgroundImage = HitMeGreyedout;
            stayButton.BackgroundImage = StayButtonGreyedOut;                                    
            numericUpDown1.Enabled = true;
            if (dealeracestoggle)
            {
                DealerAcesButton.Enabled = true;
            }
            else if (acestoggle)
            {
                AcesButton.Enabled = true;
            }
            else if (lowcardstoggle)
            {
                LowCardsButton.Enabled = true;
            }
            else
            {
                DealerAcesButton.Enabled = true;
                AcesButton.Enabled = true;
                LowCardsButton.Enabled = true;
            }
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
            fundsValue.Text = playerfunds.GetTotalFundsString();
        }
    }
}

