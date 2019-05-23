using System;
using System.Collections.Generic;
using Persitence.Model;
using MySql.Data.MySqlClient;

namespace DAL
{
    public class ItemsDAL
    {
        private string query;
        private MySqlConnection connection;
        private MySqlDataReader reader;

        public ItemsDAL()
        {
            connection = DbHelper.OpenConnection();
        }

        public Items GetItemByID(int? ITemID)
        {
            if (ITemID == null)
            {
                return null;
            }
            if (connection == null)
            {
                return null;

            }
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }

            query = $"select * from Items where ItemID = " + ITemID + ";";
            MySqlCommand command = new MySqlCommand(query, connection);
            Items item = null;
            using (reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    item = GetItem(reader);
                }
            }
            connection.Close();
            return item;

        }
        public List<Items> GetItemsByID(int? ItemID)
        {
            if (ItemID == null)
            {
                return null;
            }
            if (connection == null)
            {
                connection = DbHelper.OpenConnection();
            }
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }

            query = $"select * from Items;";
            MySqlCommand command = new MySqlCommand(query, connection);
            List<Items> items = null;
            using (reader = command.ExecuteReader())
            {
                items = new List<Items>();
                while (reader.Read())
                {
                    items.Add(GetItem(reader));
                }
            }
            connection.Close();
            return items;
        }
        private Items GetItem(MySqlDataReader reader)
        {
            Items it = new Items();
            it.ItemID = reader.GetInt32("ItemID");
            it.ItemName = reader.GetString("ItemName");
            it.ItemPrice = reader.GetDecimal("ItemPrice");
            it.ItemDescription = reader.GetString("ItemDescription");
            it.Size = reader.GetString("Size");
            return it;
        }

    }

}