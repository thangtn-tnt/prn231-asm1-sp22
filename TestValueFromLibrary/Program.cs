using DataAccess;
using System;

namespace TestValueFromLibrary
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(SD.DefaultAccount.Password);
        }
    }
}
