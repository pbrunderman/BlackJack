using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenvilleRevenueGUI
{
    class Player
    {
        Card[] PlayerHand = new Card[5];

        int playertotal;
        public void SetCard(Card thecard, int cardnum)
        {
            PlayerHand[cardnum] = thecard;
            playertotal = PlayerHand[cardnum].GetValue() + playertotal;
        }
        public Card GetPlayerHand(int cardnum)
        {
            return PlayerHand[cardnum];
        }
        public int GetTotal()
        {
            return playertotal;
        }
    }

}
