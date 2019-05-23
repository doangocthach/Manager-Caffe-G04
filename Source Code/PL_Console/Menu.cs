using System;
using System.Text;
using System.Text.RegularExpressions;
using BL;
using Persitence.Model;
using System.Security;
using System.Collections.Generic;
using ConsoleTables;
using System.IO;
using Newtonsoft.Json;
using System.Threading;
namespace PL_Console
{
    public class Menu
    {
        private Items it = new Items();
        ItemsBL itBl = new ItemsBL();
        private Customers cusAll = new Customers();
        private List<Items> items = new List<Items>();
        private OrderBL ordersbl = new OrderBL();
        Orders or = new Orders();
        public void MainMenu()
        {
            while (true)
            {
                string choice;

                Console.WriteLine("1. Login ");
                Console.WriteLine("2. Exit ");
                Console.Write("Enter your selection : ");
                choice = Console.ReadLine();
                switch (choice)
                {

                    case "1":
                        LoginMenu();
                        break;
                    case "2":
                        Console.WriteLine("See you again ! ");
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("input invalid !");
                        continue;
                }

            }
        }
        public string checkYN()
        {

            string choice = Console.ReadLine().ToUpper();
            while (true)
            {
                if (choice != "Y" && choice != "N")
                {
                    Console.Write("You can only enter (y / s): ");
                    choice = Console.ReadLine().ToUpper();
                    continue;
                }
                break;
            }
            switch (choice)
            {
                case "Y":
                    break;
                case "N":
                    break;

            }
            return choice;
        }
        public void AddToCart(Items item)
        {
            Customers cust = new Customers();
            CustomersBL cusBl = new CustomersBL();
            items.Add(item);
            string sJSONResponse = JsonConvert.SerializeObject(items);
            // Console.WriteLine(sJSONResponse);
            BinaryWriter bw;
            string fileName = "shoppingcart" + cusAll.UserName + ".dat";

            try
            {
                FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                bw = new BinaryWriter(fs);
                bw.Write((string)(object)sJSONResponse);
                fs.Close();

            }
            catch (System.Exception)
            {

                Console.WriteLine("Disconnect from database !");
            }
            Console.WriteLine("Add to cart success !");


        }

        public void ShowCart()
        {
            while (true)
            {
                if (File.Exists("shoppingcart" + cusAll.UserName + ".dat"))
                {

                    List<Items> itemsa = null;
                    Orders or = new Orders();
                    decimal amount = 0;
                    try
                    {
                        FileStream fs = new FileStream("shoppingcart" + cusAll.UserName + ".dat", FileMode.OpenOrCreate, FileAccess.ReadWrite);
                        BinaryReader br = new BinaryReader(fs);
                        string a = br.ReadString();

                        itemsa = JsonConvert.DeserializeObject<List<Items>>(a);

                        br.Close();
                        fs.Close();
                    }
                    catch (System.Exception)
                    {
                        throw;
                    }
                    var table = new ConsoleTable("ID", "ITEM NAME");
                    foreach (var itema in itemsa)
                    {
                        table.AddRow(itema.ItemID, itema.ItemName);
                        amount += itema.ItemPrice;
                    }
                    table.Write();
                    string orde;
                    Console.Write("Do you want create order ? (Y/N):");
                    orde = checkYN();
                    if (orde == "Y")
                    {

                        Console.Write("Enter your note: ");
                        string note = Console.ReadLine();
                        DateTime date = DateTime.Now;
                        or.OrderDate = date;
                        or.Status = "Not yet";
                        or.Amount = amount;
                        or.CustomerID = cusAll;
                        foreach (var item in itemsa)
                        {
                            or.Items = new List<Items>();
                            or.Items.Add(itBl.GetItemByItemID(or.ItemID));
                            // ordersbl.CreateOrder(or);

                        }
                        bool a = true;
                        try
                        {
                            a = ordersbl.CreateOrder(or);
                        }
                        catch (System.Exception)
                        {
                            a = false;
                            Console.WriteLine("\n ☹  Create order faild , press anykey to continue !\n");
                            Console.ReadKey();
                            break;

                        }
                        if (a == true)
                        {
                            Console.WriteLine("Create order success ! ");
                            try
                            {
                                // Check if file exists with its full path    
                                if (File.Exists(Path.Combine("shoppingcart" + cusAll.UserName + ".dat")))
                                {
                                    // If file found, delete it    
                                    File.Delete(Path.Combine("shoppingcart" + cusAll.UserName + ".dat"));
                                }
                                else Console.WriteLine("Cart not found");
                            }
                            catch (IOException ioExp)
                            {
                                Console.WriteLine(ioExp.Message);
                            }
                        }
                        else
                        {
                            Console.WriteLine("\n ☹  Create order faild , press anykey to continue !\n");
                            Console.ReadKey();
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }

                }
                else
                {
                    Console.WriteLine("\nNo shopping cart yet , press anykey to continue !\n");
                    Console.ReadKey();
                    break;
                }
            }

        }

        public void showItems()
        {
            // while (true)
            // {
            string row = ("==================================================================");
            Console.WriteLine(row);
            Console.WriteLine("MENU");
            Console.WriteLine(row);
            ItemsBL itemBL = new ItemsBL();


            Items it = new Items();

            List<Items> li = new List<Items>();
            try
            {
                li = itemBL.getItemsByItemID(1);
            }
            catch (System.Exception)
            {

                Console.WriteLine("Disconnect from datebase !");
                Environment.Exit(0);
            }


            var table = new ConsoleTable("ID", "ITEM NAME");

            foreach (var item in li)
            {
                table.AddRow(item.ItemID, item.ItemName);
            }
            table.Write();

            showItemDetail();

            // }

        }
        public void showItemDetail()
        {
            while (true)
            {

                string row = ("==================================================================");


                ItemsBL itBL = new ItemsBL();
                Items it = new Items();
                List<Items> li = new List<Items>();
                Customers custom = new Customers();
                CustomersBL cutomBL = new CustomersBL();

                string choice;
                int itID;
                try
                {
                    li = itBL.getItemsByItemID(1);
                    Console.Write("Enter item id: ");
                    itID = int.Parse(Console.ReadLine());
                }
                catch (System.Exception)
                {

                    Console.WriteLine("Item id must be integer and in the options !");
                    continue;
                }

                if (itID > li.Count || validateChoice(itID.ToString()) == false)
                {
                    Console.Write("You are only entered in the number of existing ids !");

                    while (true)
                    {
                        Console.Write("Do  you want re-enter ? (Y/N): ");
                        choice = Console.ReadLine().ToUpper();
                        if (choice != "Y" && choice != "N")
                        {
                            Console.Write("You can only enter  (Y/N): ");
                            choice = Console.ReadLine().ToUpper();
                            continue;
                        }
                        break;

                    }

                    switch (choice)
                    {
                        case "Y":
                            continue;

                        case "N":
                            showItems();
                            break;

                        default:
                            continue;
                    }
                }


                try
                {
                    it = itBL.GetItemByItemID(itID);
                }
                catch (System.Exception)
                {

                    Console.WriteLine("Disconnect from database !");

                }
                if (it == null)
                {
                    Console.WriteLine("The item does not exist !");
                }
                else
                {
                    Console.WriteLine(row);
                    Console.WriteLine("DETAIL OF ITEM");
                    Console.WriteLine(row);
                    var table = new ConsoleTable("ID", "ITEM NAME", "ITEM PRICE", "SIZE");

                    table.AddRow(it.ItemID, it.ItemName, it.ItemPrice, it.Size);

                    table.Write();
                    Console.WriteLine("DESCRIPTION : ");
                    Console.WriteLine(it.ItemDescription);
                }
                string select;
                Console.WriteLine("\n" + row + "\n");
                Console.WriteLine("1. Add to cart");
                Console.WriteLine("2. Back to Menu");
                Console.Write("Enter your selection: ");
                select = Console.ReadLine();

                switch (select)
                {
                    case "1":
                        AddToCart(it);
                        break;
                    case "2":
                        showItems();
                        break;
                    default:
                        Console.WriteLine("You are only entered in the number existing !");
                        continue;
                }
                string conti;
                Console.Write("Do you continue ? (Y/N): ");
                conti = checkYN();
                if (conti == "Y")
                {
                    showItems();
                }
                else
                {
                    break;
                }


            }
        }
        public string Password()
        {
            StringBuilder sb = new StringBuilder();
            while (true)
            {
                ConsoleKeyInfo cki = Console.ReadKey(true);
                if (cki.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine();
                    break;
                }
                if (cki.Key == ConsoleKey.Backspace)
                {
                    if (sb.Length > 0)
                    {
                        Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                        Console.Write(" ");
                        Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                        sb.Length--;
                    }
                    continue;
                }
                Console.Write('*');

                sb.Append(cki.KeyChar);
            }
            return sb.ToString();
        }


        public bool validate(string str)
        {
            Regex regex = new Regex("[a-zA-Z0-9_]");
            MatchCollection matchCollectionstr = regex.Matches(str);
            // Console.WriteLine(matchCollectionstr.Count);
            if (matchCollectionstr.Count < str.Length)
            {
                return false;
            }
            return true;

        }
        public bool validateChoice(string str)
        {
            Regex regex = new Regex("[0-9]");
            MatchCollection matchCollectionstr = regex.Matches(str);
            // Console.WriteLine(matchCollectionstr.Count);
            if (matchCollectionstr.Count < str.Length)
            {
                return false;
            }
            return true;

        }
        public void ViewInfo(string userName, string Password)
        {
            while (true)
            {

                string row = ("==================================================================");
                Customers cus = new Customers();
                CustomersBL cuBL = new CustomersBL();
                
                try
                {
                    cus = cuBL.Login(userName, Password);
                }
                catch (System.Exception)
                {

                    Console.WriteLine("Disconnect from datebase !");
                    break;
                }

                Console.WriteLine(row);
                Console.WriteLine("CUSTOMER INFOMATION");
                Console.WriteLine(row);
                Console.WriteLine("Customer Id : {0}", cus.CusID);
                Console.WriteLine("Customer Username : {0}", cus.UserName);
                Console.WriteLine("Customer Name : {0}", cus.CusName);
                Console.WriteLine("Customer Address : {0}", cus.Address);
                Console.WriteLine("Customer Phone Number: {0}", cus.PhoneNumber);
                string choice = null;
                Console.WriteLine("\n" + row + "\n");
                Console.WriteLine("1. Go to menu");
                Console.WriteLine("2. Back");
                Console.Write("Please enter your selection: ");
                choice = Console.ReadLine();
                if (validateChoice(choice) == false)
                {
                    Console.Write("You are only entered in the number existing!");

                }

                switch (choice)
                {
                    case "1":
                        showItems();
                        break;
                    case "2":


                        break;
                    default:
                        Console.WriteLine("You are only entered in the number of existing ids !");
                        continue;
                }
                if (choice == "2")
                {
                    break;
                }
            }
        }

        public void LoginMenu()
        {
            Console.Clear();
            CustomersBL cuBL = new CustomersBL();
            Customers cu = new Customers();
            Items it = new Items();
            string userName = null;
            string password = null;
            string choice;
            while (true)
            {
                string row = ("==================================================================");
                Console.WriteLine(row);
                Console.WriteLine();
                Console.WriteLine("LOGIN MENU \n");
                Console.WriteLine(row);
                Console.Write("Enter user name: ");
                userName = Console.ReadLine().Trim();
                // if (userName == null)
                // {
                //   Console.WriteLine("The username cannot be empty !");
                //   continue;    
                // }
                Console.Write("Enter password: ");
                password = Password().Trim();
                try
                {
                    cusAll = cu = cuBL.Login(userName, password);
                }
                catch (System.Exception)
                {
                    Console.WriteLine("Disconnect from database!");
                    LoginMenu();
                }
                if ((validate(userName) == false) || (validate(password) == false))
                {
                    Console.Write("User name / Password cannot contain special characters, do you want re-enter ? (Y/N)");
                    choice = checkYN();
                    switch (choice)
                    {
                        case "Y":
                            continue;
                        case "N":
                            showItems();
                            break;
                        default:
                            continue;
                    }
                }
                if (cu == null)
                {
                    Console.WriteLine("Username or password is incorrect!");
                }
                else
                {
                    while (true)
                    {


                        Console.WriteLine(row);
                        Console.WriteLine("WELCOME BACK ! {0}", cu.CusName.ToUpper());
                        string chose;
                        Console.WriteLine(row);
                        Console.WriteLine("1. View Infomation");
                        Console.WriteLine("2. Go to menu");
                        Console.WriteLine("3. Show Cart");
                        Console.WriteLine("4. log out");
                        Console.Write("Enter your selction: ");
                        chose = Console.ReadLine();


                        switch (chose)
                        {
                            case "1":
                                ViewInfo(userName, password);
                                break;
                            case "2":
                                showItems();
                                break;
                            case "3":

                                ShowCart();
                                break;

                            case "4":
                                LoginMenu();
                                break;

                            default:
                                Console.WriteLine("Input invalid !");
                                continue;
                        }
                    }
                }
            }
        }

        // private class JsonConvert
        // {
        //     internal static T DeserializeObject<T>(string sJSONResponse)
        //     {
        //         throw new NotImplementedException();
        //     }

        //     internal static string SerializeObject(List<Items> items)
        //     {
        //         throw new NotImplementedException();
        //     }
        // }
    }
}
