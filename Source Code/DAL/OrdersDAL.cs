using System.Collections.Generic;
using Persitence.Model;
using MySql.Data.MySqlClient;
using System.Transactions;

namespace DAL
{
    public class OrderDAL
    {
        private MySqlConnection connection;
        private MySqlDataReader reader;

        private string query;
        // OrderDetailDAL orderDetailDAL = new OrderDetailDAL();

        public OrderDAL()
        {
            connection = DbHelper.OpenConnection();
        }

        public bool CreateOrder(Orders order)
        {
            bool result = false;
            if (order == null || order.Items.Count == 0)
            {
                return result; ;
            }
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }

            MySqlCommand command = new MySqlCommand();
            command.CommandText = @"lock tables Items write, Orders write;";
            command.ExecuteNonQuery();
            MySqlTransaction transactions = connection.BeginTransaction();

            command.Transaction = transactions;

            try
            {

                if (order.OrderID == null)
                {   
                    int itemId = order.ItemID;
                    int orderID = order.ItemID;
                    int customerID = order.CustomerID;
                    string orderDate = order.OrderDate.ToString("yyyy/MM/dd HH:mm:ss");
                    string note = order.Note;
                    string status = order.Status;

                    


                    query = @"insert into Order(CusID,ItemID,OrderDate,Note,OrderStatus) values" + 
                    "(" + customerID + ", " + itemId + ", " + orderDate + ", '" + note + "', '" + status + "');";
                    command.CommandText = query;
                    command.ExecuteNonQuery();

                    command.CommandText = "select LAST_INSERT_ID() as OrderID";
                    using (reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            
                        }
                    }
                }

            }
            catch (System.Exception)
            {

                throw;
            }

            return result;


        }




    }
}