using System;
using System.Collections.Generic;
using System.Text;
using static Vending_Machine_Kata.Coins.CoinsEnum;

namespace Vending_Machine_Kata.Coins
{
    class Coin
    {
        public CoinsNames _coinName { get; set; }

        public Coin(CoinsNames coinName)
        {
            _coinName = coinName;
        }
    }
}
