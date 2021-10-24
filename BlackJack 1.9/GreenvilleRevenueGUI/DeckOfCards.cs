using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;

namespace GreenvilleRevenueGUI
{
    class DeckOfCards
    {
        Card[] AllCards = new Card[52];
        Card ACardBack = null;
        int currentcardnumber = 0;

        public DeckOfCards()
        {
            LoadCards();
            ShuffleDeck();
        }
        private void LoadCards()
        {
            Card ACard;
            string[] list = Directory.GetFiles(@"cards", "*.gif"); //populate an array of strings
                                                                   //containing the filenames of all "*.gif" found in the local "cards" directory.
                                                                   // The * essentially means (any/wildcard).
                                                                   // So all file names ending in .gif will be considered by the GetFiles() method

            for (int index = 0; index < 52; index++) //iterate through all 52 items(cards)
            {
                int value = GetNextCardValue(index); //captures the card value. GetNextCard returns card value.
                Image image = Image.FromFile(list[index]); //captures card image

                ACard = new Card(image, value); //instantiates a card object and passes the captured values to that object.
                if (index > 32 && index < 36)
                {
                    ACard.SetCardToAce();         //Sets IsAce boolean to True if card is an Ace.
                                                  //The Aces will be between index 32 and 36 due to the order in which the array was populated

                }
                AllCards[index] = ACard; //Assigns the properties of Acard to a Card object in the AllCards[] array
                                         //at the current index(which is the incrementing forloop control variable).
            }


            string[] list2 = Directory.GetFiles(@"cards", "Wfswbackcard*.gif"); //populates array of strings
                                                                                //which are filenames containing "Wfswbackcard*.gif" in the local "cards" directory.
            Image Backimage = Image.FromFile(list2[0]); //Instantiates an image object referenced from the array of image filenames (list2[0]).
            ACardBack = new Card(Backimage, 0); //instantiates a Card object. Passing the image, and a card value of 0.
        }

        private int GetNextCardValue(int currentcardnumber)
        {
            int cardvalue = 0;
            if (currentcardnumber < 33)
                cardvalue = (currentcardnumber / 4) + 2;  //fun math, and integers dont return decimals.
            else
            {
                cardvalue = 10;
            }
            if (currentcardnumber > 31 && currentcardnumber < 36)
                cardvalue = 11;//aces
            return cardvalue;
        }
        public Card GetNextCard()
        {
            return (AllCards[currentcardnumber++]);
        }
        public void ShuffleDeck()
        {
            Random rand = new Random();
            for (int i = 0; i < AllCards.Length - 1; i++)
            {
                int j = rand.Next(i + 1, AllCards.Length);
                Card temp = AllCards[i];
                AllCards[i] = AllCards[j];
                AllCards[j] = temp;
            }
            currentcardnumber = 0;
        }
        //assumes you deal card to dealer, player, dealer, player in Purple
        // no shuffle in purple button
        public void DealerlowcardsFirst()
        {
            int aceindex = 0;
            int card9index = 0;
            int card5index = 0;
            int card6index = 0;

            Boolean keepgoing = true;
            ShuffleDeck();

            // find a 9
            card9index = aceindex = 0;
            while (keepgoing)
            {
                Card TempCard1 = AllCards[aceindex];

                if (TempCard1.GetValue() == 2)
                {
                    Card OriginalCard = AllCards[card9index];// original card spot to swap

                    AllCards[card9index] = TempCard1;//put the 9 in the 5th card spot
                    AllCards[aceindex] = OriginalCard;
                    keepgoing = false;
                }
                aceindex++;

            }

            // find a 3
            card5index = aceindex = 2;
            keepgoing = true;
            while (keepgoing)
            {
                Card TempCard1 = AllCards[aceindex];

                if (TempCard1.GetValue() == 3)
                {
                    Card OriginalCard = AllCards[card5index];// original card spot to swap

                    AllCards[card5index] = TempCard1;//put the 5 in the 6th card spot
                    AllCards[aceindex] = OriginalCard;
                    keepgoing = false;
                }
                aceindex++;

            }

            // find a 4 put it in the 4th card spot
            card6index = aceindex = 4;
            keepgoing = true;
            while (keepgoing)
            {
                Card TempCard1 = AllCards[aceindex];

                if (TempCard1.GetValue() == 4)
                {
                    Card OriginalCard = AllCards[card6index];// original card spot to swap

                    AllCards[card6index] = TempCard1;//put the 6 in the 7th card spot
                    AllCards[aceindex] = OriginalCard;
                    keepgoing = false;
                }
                aceindex++;

            }

            // find a 3 put it in the 5th card spot
            card6index = aceindex = 5;
            keepgoing = true;
            while (keepgoing)
            {
                Card TempCard1 = AllCards[aceindex];

                if (TempCard1.GetValue() == 3)
                {
                    Card OriginalCard = AllCards[card6index];// original card spot to swap

                    AllCards[card6index] = TempCard1;//put the 6 in the 7th card spot
                    AllCards[aceindex] = OriginalCard;
                    keepgoing = false;
                }
                aceindex++;

            }

            // find a 2 put it in the 6th card spot
            card6index = aceindex = 6;
            keepgoing = true;
            while (keepgoing)
            {
                Card TempCard1 = AllCards[aceindex];

                if (TempCard1.GetValue() == 2)
                {
                    Card OriginalCard = AllCards[card6index];// original card spot to swap

                    AllCards[card6index] = TempCard1;//put the 6 in the 7th card spot
                    AllCards[aceindex] = OriginalCard;
                    keepgoing = false;
                }
                aceindex++;

            }

            currentcardnumber = 0;//RWW
        }
    }
}
