using System;
using System.Collections.Generic;
using Vending_Machine_Kata.Products;
using static Vending_Machine_Kata.Products.ProductsEnum;

namespace Vending_Machine_Kata
{
    public class RandomDelivery
    {
        // Using List casting, just to exercise
        public List<Product> Deliver(ProductsNames productName)
        {
            
            Random _rnd = new Random();
            var quantity = _rnd.Next(3);
            List<Product> _goods = new List<Product>{};

            for (int i = 0; i <= quantity; i++)
            {
                switch (productName)
                {
                    case ProductsNames.Apple:
                        _goods.Add(new Apple());
                        break;
                    case ProductsNames.Kombucha:
                        _goods.Add(new Kombucha());
                        break;
                    case ProductsNames.Salad:
                        _goods.Add(new Salad());
                        break;
                }
            }
                return _goods;
        }
    }
}
