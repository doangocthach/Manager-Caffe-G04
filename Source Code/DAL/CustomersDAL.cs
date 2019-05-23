using Persitence.Model;
using MySql.Data.MySqlClient;
using System;
using  System.Text.RegularExpressions;
namespace DAL
{
    public class CustomersDAL
    {
        private MySqlConnection connection;

        private MySqlDataReader reader ;


        private string query;

        public CustomersDAL()
        {
            connection = DbHelper.OpenConnection();
        }

       public Customers Login(string userName, string password)
        {
            if (userName == null || password == null)
            {
                return null;
            }
            Regex regex = new Regex("[a-zA-Z0-9_]");
            MatchCollection matchCollectionCustomersname = regex.Matches(userName);
            MatchCollection matchCollectionPassword = regex.Matches(password);

            if (matchCollectionCustomersname.Count < userName.Length || matchCollectionPassword.Count < password.Length)
            {
                return null;
            }
            try
            {
               if (connection == null)
               {
                   connection = DbHelper.OpenConnection();
               } 
               else if (connection.State == System.Data.ConnectionState.Closed)
               {
                   connection.Open();
               }
            }
            catch (System.Exception)
            {
                
               return null;
            }

            query = @"select * from Customers where  userName = '" + userName + "' and userPassword = '" + password + "';";
          
            MySqlCommand command = new MySqlCommand(query, connection);
           
            Customers customer = null;
            
            using (reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    customer = GetCustomer(reader);
                }
               
            }
            
            connection.Close();

            // if (user != null)
            // {
            //     CinemaDAL cinemaDAL = new CinemaDAL();
            //     Cinema cine = cinemaDAL.GetCinemaByCineId(user.Cine.CineId);
            // }

            return customer;

        }

         private Customers GetCustomer(MySqlDataReader reader)
        {
            Customers customer = new Customers();
            customer.UserName = reader.GetString("userName");
            customer.Password = reader.GetString("userPassword");
            customer.Address = reader.GetString("Address");
            customer.CusID = reader.GetInt32("CusID");
            customer.PhoneNumber = reader.GetString("PhoneNumber");
            customer.CusName = reader.GetString("CusName");
            
            
            return customer;
        }

        // public Customers Login(string userName, string password)
        // {
        //     throw new NotImplementedException();
        // }
    }
    
}