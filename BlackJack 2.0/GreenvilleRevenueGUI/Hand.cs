using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenvilleRevenueGUI
{
    class Hand
    {
        Card[] thehand = new Card[5];
        int total = 0;
        int numberofcards = 0;
        String name;

        public Hand(String thename)
        {
            name = thename;
        }
        public void DealCard(Card ACard)
        {
            thehand[numberofcards++] = ACard;
            total = total + ACard.GetValue();
        }
        public int GetTotal()
        {
            return total;
        }
        public void SetTotal()
        {
            int newtotal = 0;
            for (int i = 0; i < this.GetNumberofCards(); i++)
            {
                int cardvalue = GetCard(i).GetValue();                
                newtotal = newtotal + cardvalue;
            }
            total = newtotal;
        }
        public String GetTotalString()
        {
            return Convert.ToString(total);
        }
        public void ResetHand()
        {
            total = 0;
            numberofcards = 0;
            for (int i = 0; i < 5; i++)
            {
                if (GetCard(i) != null && GetCard(i).GetAce())
                {
                    if (GetCard(i).GetValue() == 1)
                    {
                        GetCard(i).ToggleAce();
                    }
                }
                thehand[i] = null;
            }
        }
        public int GetNumberofCards()
        {
            return numberofcards;
        }
        public Card GetCard(int index)
        {
            return thehand[index];
        }
        public bool HasAces()
        {
            for (int i = 0; i < thehand.Length; i++)
            {
                {
                    if (GetCard(i) != null && GetCard(i).GetAce())
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }

}
