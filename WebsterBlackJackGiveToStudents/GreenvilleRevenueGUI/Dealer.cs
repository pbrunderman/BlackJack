using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenvilleRevenueGUI
{
    class Dealer
    {
        Card[] DealerHand = new Card[5];
        int dealertotal;
        public void SetCard(Card thecard, int cardnum)
        {
            DealerHand[cardnum] = thecard;
            dealertotal = DealerHand[cardnum].GetValue() + dealertotal;
        }
        public Card GetDealerHand(int cardnum)
        {
            return DealerHand[cardnum];
        }
        public int GetTotal()
        {
            return dealertotal;
        }
    }
}
