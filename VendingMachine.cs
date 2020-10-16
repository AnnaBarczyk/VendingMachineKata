using System;
using System.Collections.Generic;
using Vending_Machine_Kata.Coins;
using Vending_Machine_Kata.Products;

namespace Vending_Machine_Kata
{
    public class VendingMachine
    {
        // Products on shelves, public because user sees them and delivery can change them ;) Can be made private.
        public List<Apple> apples { get; set; }
        public List<Kombucha> kombuchas { get; set; }
        public List<Salad> salads { get; set; }

        //Coins in machine safe
        private List<Coin> _dimes { get; set; }
        private List<Coin> _quarters { get; set; }
        private List<Coin> _nickels { get; set; }

        //Temp values for actual transaction
        private int _clientMoneyValue { get; set; }

        //Price values
        private int _applePrice { get; set; }
        private int _kombuchaPrice { get; set; }
        private int _saladPrice { get; set; }

        //Money values
        private int _dimeValue { get; set; }
        private int _nickelValue { get; set; }
        private int _quarterValue { get; set; }

        //Product quantity
        private int _appleNumber { get; set; }
        private int _kombuchaNumber { get; set; }
        private int _saladNumber { get; set; }
        private int _howManyProductTypes { get; set; }

        public VendingMachine(int howManyDimes, int howManyQuarters, int howManyNickels)
        {
            // Coins
            _quarters = new List<Coin>();
            _dimes = new List<Coin>(); 
            _nickels = new List<Coin>();
            // Prices
            _applePrice = 50;
            _kombuchaPrice = 65;
            _saladPrice = 100;
            // Coin values
            _dimeValue = 10;
            _nickelValue = 5;
            _quarterValue = 25;
            // Products numbers
            _appleNumber = 1;
            _kombuchaNumber = 2;
            _saladNumber = 3;
            // Product types count
            _howManyProductTypes = Enum.GetNames(typeof(ProductsEnum.ProductsNames)).Length;

            // Add starter money to machine
            for (int i = 1; i <= howManyDimes; i++)
            {
                _dimes.Add(new Coin(CoinsEnum.CoinsNames.Dime));
            }

            for (int i = 1; i <= howManyQuarters; i++)
            {
                _quarters.Add(new Coin(CoinsEnum.CoinsNames.Quarter));
            }

            for (int i = 1; i <= howManyNickels; i++)
            {
                _nickels.Add(new Coin(CoinsEnum.CoinsNames.Nickel));
            }

            // Total values
            _clientMoneyValue = 0;
        }

        public void Start()
        {
            ShowProducts();
            TakeAndCountMoneyFromClient();
            var order = TakeOrder();

            while (CheckIfProductExist(order) == false)
            {
                ShowProducts();
                CheckIfExactMoneyNeeded();
                order = TakeOrder();
            }
            if (CheckIsThereEnoughCredits(order))
            {
                var changeCoinsQuantity = CountChange(_clientMoneyValue - CheckOrderValue(order));
                SellProduct(order);
                GiveChange(changeCoinsQuantity);
                Start();
            }
            else
            {
                GiveChange(CountChange(_clientMoneyValue));
                Start();
            }
        }

        private List<int> CountChange(int changeToGive)
        {
            int quarters = 0;
            int dimes = 0;
            int nickels = 0;
            int change = changeToGive;


            while (change > 0)
            {
                if (change >= _quarterValue && CountCoinsQuantity(CoinsEnum.CoinsNames.Quarter) > 0)
                {
                    quarters++;
                    change -= _quarterValue;
                }
                else if (change >= _dimeValue && CountCoinsQuantity(CoinsEnum.CoinsNames.Dime) > 0)
                {
                    dimes++;
                    change -= _dimeValue;
                }
                else if (change >= _nickelValue && CountCoinsQuantity(CoinsEnum.CoinsNames.Nickel) > 0)
                {
                    nickels++;
                    change -= _nickelValue; 
                }
            }

            var result = new List<int> { quarters, dimes, nickels };


            return result;
        }
        public int CountMoneyValueInMachine()
        {
            var moneyValueInMachine = (CountCoinsQuantity(CoinsEnum.CoinsNames.Quarter) * _quarterValue) + (CountCoinsQuantity(CoinsEnum.CoinsNames.Nickel) * _nickelValue) + (CountCoinsQuantity(CoinsEnum.CoinsNames.Dime) * _dimeValue);
            return moneyValueInMachine;
        }
        public int CheckOrderValue(int orderNumber)
        {
           if (orderNumber == _appleNumber) return _applePrice;
           else if (orderNumber == _kombuchaNumber) return _kombuchaPrice;
           else if (orderNumber == _saladNumber) return _saladPrice;
           else return 0;
        }
        public bool CheckIsThereEnoughCredits(int orderNumber)
        {
            if (orderNumber == _appleNumber && _clientMoneyValue >= _applePrice) return true;
            else if (orderNumber == _kombuchaNumber && _clientMoneyValue >= _kombuchaPrice) return true;
            else if (orderNumber == _saladNumber && _clientMoneyValue >= _saladPrice) return true;
            else return false;
        }

        private List<Coin> GiveChange(List<int> changeQuantity)
        {
            var quartersToGive = changeQuantity[0];
            var dimesToGive = changeQuantity[1];
            var nickelsToGive = changeQuantity[2];

            List<Coin> changeToGive = new List<Coin> { };

            for (var i=0; i <= quartersToGive; i++)
            {
                changeToGive.Add(new Coin(CoinsEnum.CoinsNames.Quarter));
                _quarters.RemoveAt(0);
            }
            for (var i = 0; i <= dimesToGive; i++)
            {
                changeToGive.Add(new Coin(CoinsEnum.CoinsNames.Dime));
                _dimes.RemoveAt(0);
            }
            for (var i = 0; i <= nickelsToGive; i++)
            {
                changeToGive.Add(new Coin(CoinsEnum.CoinsNames.Nickel));
                _nickels.RemoveAt(0);
            }

            Console.WriteLine("Please take your change and hit enter: \n" + "\nQuarters:" + quartersToGive + "\nDimes:" + dimesToGive + "\nNickels:" + nickelsToGive);
            Console.ReadLine();
            Console.Clear();


            return changeToGive;
        }

        private Product SellProduct(int order)
        {
            var firstInList = 0;
            if (order == _appleNumber)
            {
                var appleToSell = apples[firstInList];
                apples.RemoveAt(firstInList);
                return appleToSell;
            }
            else if (order == _kombuchaNumber)
            {
                var kombuchaToSell = kombuchas[firstInList];
                kombuchas.RemoveAt(firstInList);
                return kombuchaToSell;
            }
            else if (order == _saladNumber)
            {
                var saladToSell = salads[firstInList];
                salads.RemoveAt(firstInList);
                return saladToSell;
            }
            return null;
        }

        private void ShowProducts()
        {
            float temp = _clientMoneyValue / 100;
            var credits = temp.ToString("C", System.Globalization.CultureInfo.CreateSpecificCulture("en-US"));
            string appleMessage = "SOLD OUT";
            string kombuchaMessage = "SOLD OUT";
            string saladMessage = "SOLD OUT";

            if (apples.Count > 0)
            {
                appleMessage = apples.Count.ToString();
            }
            if (kombuchas.Count > 0)
            {
                kombuchaMessage = kombuchas.Count.ToString();
            }
            if (salads.Count > 0)
            {
                saladMessage = salads.Count.ToString();
            }


            Console.WriteLine(
                "You have: " + credits + " credits" + "\n" + "\n" +
                "Hello, you can buy here:\n" +
                "1. Apples (0,5$):  " + appleMessage + "\n" +
                "2. Kombuchas (0,65$):  " + kombuchaMessage + "\n" +
                "3. Salads (1$):  " + saladMessage + "\n"
                );
        }

        private bool CheckIfProductExist(int numberChosenByUser)
        {
            if (numberChosenByUser >= 1 && numberChosenByUser <= _howManyProductTypes) return true;
            else return false;
        }
        private int TakeOrder()
        {
            Console.WriteLine("Please, choose one product, by entering a number.");
            var chosenProductPosition = Console.ReadLine();
            var none = 0;
            try
            {
                var productNumber = short.Parse(chosenProductPosition);

                if (productNumber == _appleNumber && apples.Count > none)
                {
                    return productNumber;
                }
                else if (productNumber == _kombuchaNumber && kombuchas.Count > none)
                {
                    return productNumber;
                }
                else if (productNumber == _saladNumber && salads.Count > none)
                {
                    return productNumber;
                }
                else if (productNumber > _howManyProductTypes)
                {
                    Console.Clear();
                    Console.WriteLine("Sorry, we don't have that many products :) \n");
                    return none;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Sorry, we are out of stack, choose something else :) \n");
                    return none;
                }
            }
            catch (FormatException)
            {
                Console.Clear();
                Console.WriteLine("Sorry, wrong input, choose a number \n");
                return none;
            }
        }

        private List<Coin> TakeAndCountMoneyFromClient()
        {
            Console.WriteLine("Quarters:");
            var quarters = int.Parse(Console.ReadLine());
            Console.WriteLine("Dimes:");
            var dimes = int.Parse(Console.ReadLine());
            Console.WriteLine("Nickels:");
            var nickels = int.Parse(Console.ReadLine());
            Console.WriteLine("Pennies:");
            var pennies = int.Parse(Console.ReadLine());
            Console.Clear();
            ShowProducts();

            var penniesToReturn = new List<Coin> { };

            for (int i = 1; i <= dimes; i++)
            {
                _dimes.Add(new Coin(CoinsEnum.CoinsNames.Dime));
                _clientMoneyValue += _dimeValue;
            }

            for (int i = 1; i <= quarters; i++)
            {
                _quarters.Add(new Coin(CoinsEnum.CoinsNames.Quarter));
                _clientMoneyValue += _quarterValue;
            }

            for (int i = 1; i <= nickels; i++)
            {
                _nickels.Add(new Coin(CoinsEnum.CoinsNames.Nickel));
                _clientMoneyValue += _nickelValue;
            }

            for (int i = 1; i <= pennies; i++)
            {

                penniesToReturn.Add(new Coin(CoinsEnum.CoinsNames.Pennie));
            }

            if (penniesToReturn.Count > 0)
            {
                Console.WriteLine(
                    "Sorry, you can only use Dimes, Quarters and Nickels." + "\n" +
                    "Pennies returned: " + penniesToReturn.Count + "\n" +
                    "Please, take your money from machine and continue by pressing any key."
                    );
                Console.ReadLine();
                Console.Clear();
                ShowProducts();

            }
            return penniesToReturn;
        }

        private void CheckIfExactMoneyNeeded()
        {
            var moneyValueInMachine = CountMoneyValueInMachine();
            var cheapestProduct = _applePrice;
        
            var ifExactNeeded = moneyValueInMachine < cheapestProduct;

            if (ifExactNeeded)
            {
                Console.WriteLine("Exact change only");
                Console.WriteLine("Please, insert money :) ");
            }
            else
            {
                Console.WriteLine("Please, insert money :) ");
            }
        }

        private int CountCoinsQuantity(CoinsEnum.CoinsNames name)
        {
            if(name == CoinsEnum.CoinsNames.Dime) return _dimes.Count;
            else if(name == CoinsEnum.CoinsNames.Nickel) return _nickels.Count;
            else if (name == CoinsEnum.CoinsNames.Quarter) return _quarters.Count;
            else return 0;
        }
    }
}
