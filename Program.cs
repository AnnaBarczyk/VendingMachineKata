using System;
using System.Dynamic;

namespace Vending_Machine_Kata
{
    class Program
    {
        static void Main(string[] args)
        {
            var machine = new VendingMachine(5,5,5);
            var day = new Day();

            day.StartNewDay(machine);
        }
    }
}
