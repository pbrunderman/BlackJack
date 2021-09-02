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
        Boolean IsAce;
        Image image;
        
        public Card(Image myimage, int myvalue)
        {
            image = myimage;
            value = myvalue;
            IsAce = false;
        }
        public void SetCardToAce()
        {
            IsAce = true;
        }
        public int GetValue()
        {
            return value;
        }
        public Image GetImage()
        {
            return image;
        }
    }
}
