using DataAccess;
using DataAccess.DAO;
using Newtonsoft.Json;
using System;

namespace TestValueFromLibrary
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(OrderDetailDAO.GetOrderDetail(3));
        }
    }
}
