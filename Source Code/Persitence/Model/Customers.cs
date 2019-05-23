using System;
namespace Persitence.Model
{
    public class Customers
    {
        public Customers(int cusID, string userName, string password, string cusName, string address, string phoneNumber)
        {
            CusID = cusID;
            UserName = userName;
            Password = password;
            CusName = cusName;
            Address = address;
            PhoneNumber = phoneNumber;
        }
        public Customers()
        {
            
        }

        public int CusID { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string CusName { get; set; }

        public string Address {get;set;}

        public string PhoneNumber{get;set;}
    }

}