using System;
using DAL;
using Persitence.Model;
using System.Text.RegularExpressions;
namespace BL
{
    public class CustomersBL
    {
        private CustomersDAL cusDAL = new CustomersDAL();
        public Customers Login(string userName, string password)
        {
            if (userName == null || password == null)
            {
                return null;
            }
            Regex regex = new Regex("[a-zA-Z0-9_]");
            MatchCollection matchCollectionUsername = regex.Matches(userName);
            MatchCollection matchCollectionPassword = regex.Matches(password);

            if (matchCollectionUsername.Count < userName.Length || matchCollectionPassword.Count < password.Length)
            {
                return null;
            }
            return cusDAL.Login(userName, password);



        }
    }
}
