using System;
using DAL;
using Persitence.Model;
using System.Text.RegularExpressions;
using System.Collections.Generic;


namespace BL
{
    public class ItemsBL
    {
        private ItemsDAL itDal = new ItemsDAL();
        public Items GetItemByItemID(int? itemID)
        {
            Regex regex = new Regex("[0-9]");
            MatchCollection matchCollection = regex.Matches(itemID.ToString());
            if (itemID == null)
            {
                return null;
            }
            else if (matchCollection.Count < itemID.ToString().Length)
            {
                return null;
                
            }
            return itDal.GetItemByID(itemID);
        }
        
        public List<Items> getItemsByItemID(int? itemID)
        {
          if (itemID == null)
          {
              return null;
          }
          Regex regex = new Regex("[0-9]");
          MatchCollection matchCollection = regex.Matches(itemID.ToString());
          if (matchCollection.Count < itemID.ToString().Length)
          {
              return null;
              
          }
          return itDal.GetItemsByID(itemID);

        }
        
        
    }


}