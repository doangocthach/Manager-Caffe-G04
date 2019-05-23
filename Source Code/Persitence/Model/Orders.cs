using System;
using System.Collections.Generic;
namespace Persitence.Model
{
    public class Orders
    {


        public Orders()
        {

        }

        public Orders(int orderID, DateTime orderDate, string note, string status, int customerID, List<Items> items,int itemID)
        {
            OrderID = orderID;
            OrderDate = orderDate;
            Note = note;
            Status = status;
            CustomerID = customerID;
            Items = items;
            ItemID = itemID;    
        }

        public int? OrderID { get; set; }
        public int ItemID {get;set;}
        public DateTime OrderDate { get; set; }

        public string Note { get; set; }

        public string Status { get; set; }

        public int CustomerID { get; set; }
        public List<Items> Items { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Orders orders &&
                   OrderID == orders.OrderID;
        }

        public override int GetHashCode()
        {
            return (OrderID + Status + Note + CustomerID + OrderDate + Items ).GetHashCode();
        }


    }
}