using System;
using BL;
using PL_Console;
using Persitence.Model;
using System.Security;
using System.Collections.Generic;
namespace PL_Console
{
    class Program
    {   Items it = new Items();
        List<Items> li = new List<Items>();      
        static void Main(string[] args)
        {  Console.Clear();
           Menu menu = new Menu();
           Console.WriteLine("=================== WELCOME TO VTCA CAFFE !=======================");
           menu.MainMenu();
        }
    }
}
