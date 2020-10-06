using System;
using System.Dynamic;

namespace Vending_Machine_Kata
{
    class Program
    {
        static void Main(string[] args)
        {
            var machine = new VendingMachine();
            var day = new Day();

            day.StartNewDay(machine);
        }
    }
}
