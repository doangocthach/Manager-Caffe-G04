using System;
using Xunit;
using DAL;
using MySql.Data.MySqlClient;


namespace DAL.Test
{
    public class DBHelperTest
    {   
        [Fact]
      
        public void OpenConnectionTest()
        {
            Assert.NotNull(DbHelper.OpenConnection());
        }
    }
    
}