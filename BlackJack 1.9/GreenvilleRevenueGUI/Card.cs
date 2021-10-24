using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GreenvilleRevenueGUI
{
    class Card
    {
        int value;
        bool IsAce;
        Image image;

        public Card(Image myimage, int myvalue)
        {
            image = myimage;
            value = myvalue;
            IsAce = false;
            if (this.GetValue() == 1 || this.GetValue() == 11)
            {
                this.SetCardToAce();
            }
        }
        public void SetCardToAce()
        {
            IsAce = true;
        }
        public int GetValue()
        {
            return value;
        }
        public String GetValueString()
        {
            return Convert.ToString(value);
        }
        public void ToggleAce()
        {
            if (IsAce)
            {
                if (value == 1)
                {
                    value = 11;
                }
                else
                {
                    value = 1;
                }
            }
        }
        public Image GetImage()
        {
            return image;
        }
        public bool GetAce()
        {
            return IsAce;
        }
    }
}
