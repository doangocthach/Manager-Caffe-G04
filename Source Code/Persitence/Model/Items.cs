using System;

namespace Persitence.Model
{
    public class Items
    {
        public Items(decimal itemPrice, int itemID, string itemName, string itemDescription, string size)
        {
            ItemPrice = itemPrice;
            ItemID = itemID;
            ItemName = itemName;
            ItemDescription = itemDescription;
            Size = size;
        }
        public Items()
        {
            
        }
        public decimal  ItemPrice{get;set;}

        public int ItemID {get;set;}

        public string ItemName{get;set;}

        public string ItemDescription{get;set;}

        public string Size {get;set;}
    }
    
}