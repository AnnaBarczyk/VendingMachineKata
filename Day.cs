using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using Vending_Machine_Kata.Products;
using static Vending_Machine_Kata.Products.ProductsEnum;

namespace Vending_Machine_Kata
{
    public class Day
    {
        public RandomDelivery _delivery { get; set; }

        public Day()
        {
            _delivery = new RandomDelivery();
        }
        public void StartNewDay(VendingMachine machine)
        {
            // Deliver goods to vending machine, casting used to exercise and remember
            // TypeOf gets rid of unwanted items like Kombucha in apples pack.
            var packedGoods = _delivery.Deliver(ProductsNames.Apple);
            var apples = packedGoods.OfType<Apple>().ToList();
            machine.apples = apples;

            packedGoods = _delivery.Deliver(ProductsNames.Kombucha);
            var kombuchas = packedGoods.OfType<Kombucha>().ToList();
            machine.kombuchas = kombuchas;

            packedGoods = _delivery.Deliver(ProductsNames.Salad);
            var salads = packedGoods.OfType<Salad>().ToList();
            machine.salads = salads;

            machine.Start();
        }    
    }
}