using System;
using Xunit;
using DAL;
using Persitence.Model;
namespace DAL.Test
{
    public class CustomerDALTest
    {
        private CustomersDAL customersDAL = new CustomersDAL();

        [Theory]
        [InlineData("Thach123", "123456789")]
        [InlineData("Toan123", "123456789")]

        public void loginTest1(string userName, string password)
        {
            Customers Cus = customersDAL.Login(userName, password);

            Assert.NotNull(Cus);
            Assert.Equal(userName, Cus.UserName);
        }

        [Theory]
        [InlineData("customer_01", "12345688889")]
        [InlineData("'?^%'", "'.:=='")]
        [InlineData("'?^%'", null)]
        [InlineData(null, "'.:=='")]

        public void LoginTest2(string userName, string password)
        {
            Assert.Null(customersDAL.Login(userName, password));
        }


    }
}
