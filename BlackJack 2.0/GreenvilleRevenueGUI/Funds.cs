using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenvilleRevenueGUI
{
    class Funds
    {
        int totalfunds;
        int betamount = 100;

        public Funds(int initialfunds)
        {
            totalfunds = initialfunds;
        }
        public void SetBetAmount(int bet)
        {
            betamount = bet;
        }
        public int GetBetAmount()
        {
            return betamount;
        }
        public void WonBet()
        {
            totalfunds = totalfunds + betamount;
        }
        public void LostBet()
        {
            totalfunds = totalfunds - betamount;
        }
        public int GetTotalFunds()
        {
            return totalfunds;
        }
        public string GetTotalFundsString()
        {
            return "$" + Convert.ToString(totalfunds);
        }
    }
}
