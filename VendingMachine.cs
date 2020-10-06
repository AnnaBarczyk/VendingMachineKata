using System;
using System.Collections.Generic;
using Vending_Machine_Kata.Coins;
using Vending_Machine_Kata.Products;

namespace Vending_Machine_Kata
{
    public class VendingMachine
    {
        public List<Apple> apples { get; set; }
        public List<Kombucha> kombuchas { get; set; }
        public List<Salad> salads { get; set; }
        public List<Coin> _dimes { get; set; }
        public List<Coin> _quarters { get; set; }
        public List<Coin> _nickels { get; set; }
        public int _moneyValueInMachine { get; set; }
        public int _clientMoney { get; set; }

        public VendingMachine(int howManyDimes, int howManyQuarters, int howManyNickels)
        {
            _dimes = new List<Coin>();
            _quarters = new List<Coin>();
            _nickels = new List<Coin>();
            _moneyValueInMachine = 0;
            _clientMoney = 0;

            for (int i = 1; i <= howManyDimes; i++)
            {
                _dimes.Add(new Coin(CoinsEnum.CoinsNames.Dime));
                _moneyValueInMachine += 50;
            }

            for (int i = 1; i <= howManyQuarters; i++)
            {
                _quarters.Add(new Coin(CoinsEnum.CoinsNames.Quarter));
                _moneyValueInMachine += 65;
            }

            for (int i = 1; i <= howManyNickels; i++)
            {
                _nickels.Add(new Coin(CoinsEnum.CoinsNames.Nickel));
                _moneyValueInMachine += 100;
            }
        }

        public void Start()
        {
            ShowProducts();
            TakeAndCountMoneyFromClient();
            var order = TakeOrder();

            while (CheckIfProductExist(order) == false)
            {
                ShowProducts();
                order = TakeOrder();
            }
            Console.WriteLine("WOHOOOO");
        }
        public void ShowProducts()
        {
            string credits = (_clientMoney/100).ToString();

            Console.WriteLine(
                "You have " + credits + " credits" + "\n" + "\n" +
                "Hello, you can buy here:\n" +
                "1. Apples (0,5$):  " + apples.Count + "\n" +
                "2. Kombuchas (0,65$):  " + kombuchas.Count + "\n" +
                "3. Salads (1$):  " + salads.Count + "\n"
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
            Console.WriteLine("Please, choose one product, by entering a number."); 
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

        public List<Coin> TakeAndCountMoneyFromClient()
        {
            Console.WriteLine("Please, insert money :) ");
            
            Console.WriteLine("Pennies:");
            var pennies = int.Parse(Console.ReadLine());
            Console.WriteLine("Dimes:");
            var dimes = int.Parse(Console.ReadLine());
            Console.WriteLine("Quarters:");
            var quarters = int.Parse(Console.ReadLine());
            Console.WriteLine("Nickels:");
            var nickels = int.Parse(Console.ReadLine());
            Console.Clear();
            ShowProducts();

            var insertedCoins = new List<Coin> { };
            var penniesToReturn = new List<Coin> { };

            for(int i=1; i<=dimes; i++)
            {
                _dimes.Add(new Coin(CoinsEnum.CoinsNames.Dime));
                _clientMoney += 50;
                _moneyValueInMachine += 50;
            }

            for (int i = 1; i <= quarters; i++)
            {
                _quarters.Add(new Coin(CoinsEnum.CoinsNames.Quarter));
                _clientMoney += 65;
                _moneyValueInMachine += 65;
            }

            for (int i = 1; i <= nickels; i++)
            {
                _nickels.Add(new Coin(CoinsEnum.CoinsNames.Nickel));
                _clientMoney += 100;
                _moneyValueInMachine += 100;
            }

            for (int i = 1; i <= pennies; i++)
            {
                
                penniesToReturn.Add(new Coin(CoinsEnum.CoinsNames.Pennie));
            }

            if(penniesToReturn.Count > 0)
            {
                Console.WriteLine(
                    "Sorry, you can only use Dimes, Quarters and Nickels." +"\n"+
                    "Pennies returned: " + penniesToReturn.Count + "\n" +
                    "Please, take your money from machine and continue by pressing any key."
                    );
                Console.ReadLine();
                Console.Clear();
                ShowProducts();

            }
            return penniesToReturn;
        }
    }
}
