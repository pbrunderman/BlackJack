using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenvilleRevenueGUI
{
    class BlackJack
    {
        DeckOfCards Deck = new DeckOfCards();
        Player Player = new Player();
        Dealer Dealer = new Dealer();        
        public void DealStartingHand()
        {
            DealCard("player", 0);
            DealCard("dealer", 0);
            DealCard("player", 1);
            DealCard("dealer", 1);
        }
        public void DealCard(string dealto,int cardnum)
        {
            Card thecard = Deck.GetNextCard();
            if (dealto == "player")
            {
                Player.SetCard(thecard, cardnum);
            }
            else
            {
                Dealer.SetCard(thecard, cardnum);
            }
        }
        public void Hit()
        {

        }
        public void Stay()
        {

        }
        public bool Check()
        {
            bool win = false;
            return win;
        }
        public Player GetPlayer()
        {
            return Player;
        }
        public Dealer GetDealer()
        {
            return Dealer;
        }
    }
}
