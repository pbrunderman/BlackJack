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
        int currentcardnumber;

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
                                                                   // The * essentially means (any).
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
                cardvalue = (currentcardnumber / 4) + 2;
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
            Randomizer.Randomize(AllCards);
        }
        public class Randomizer
        {
            public static void Randomize<Card>(Card[] items)
            {
                Random rand = new Random();

                // For each spot in the array, pick
                // a random item to swap into that spot.
                for (int i = 0; i < items.Length - 1; i++)
                {
                    int j = rand.Next(i, items.Length); //select random position in the array.
                    Card temp = items[i]; //temporarily hold value of the card we are swapping out (starts with [0] per for loop control variable.)
                    items[i] = items[j]; //replace current card with the card selected by the randomizer.
                    items[j] = temp; //replace randomly selected card with current card.
                }
            }
        }
    }
}
