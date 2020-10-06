using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Vending_Machine_Kata.Products;

namespace Vending_Machine_Kata
{
    public class VendingMachine
    {
        public List<Apple> apples { get; set; }
        public List<Kombucha> kombuchas { get; set; }
        public List<Salad> salads { get; set; }

        public VendingMachine()
        {
            
        }

        public void Start()
        {
            ShowProducts();
            var order = TakeOrder();

            while (CheckIfProductExist(order)==false)
            {
                ShowProducts();
                order = TakeOrder();
            }
            Console.WriteLine("WOHOOOO");
        }
        public void ShowProducts()
        {
            Console.WriteLine(
                "Hello, you can buy here:\n" +
                "1. Apples (0,5$):  " + apples.Count + "\n" +
                "2. Kombuchas (0,65$):  " + kombuchas.Count + "\n" +
                "3. Salads (1$):  " + salads.Count + "\n" +
                "Please, choose one, by entering a number."
                );
        }

        public bool CheckIfProductExist(int numberChosenByUser)
        {
            var productsMemberCount = Enum.GetNames(typeof(ProductsEnum.ProductsNames)).Length;

            if (numberChosenByUser >= 1 && numberChosenByUser <= productsMemberCount) return true;
            else return false;
        }
        public int TakeOrder()
            {
                var chosenProductPosition = Console.ReadLine();
                    try
                    {
                        var productNumber = int.Parse(chosenProductPosition);
                        return productNumber;
                    }
                    catch (Exception)
                    {
                        Console.Clear();
                        Console.WriteLine("Sorry, wrong input, choose a number");
                        return 0;
                    }
        }
    }
}
