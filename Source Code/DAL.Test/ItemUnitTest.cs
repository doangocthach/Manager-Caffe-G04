using Persitence.Model;
using DAL;
using Xunit;
namespace DAL.Test
{

    class ItemUnitTest
    {
        private ItemsDAL idal = new ItemsDAL();
   
           [Theory]
           [InlineData("1")]
           [InlineData("2")]
        public void GetItemByID1(int itemID)
        {
            Items it = idal.GetItemByID(itemID);
            Assert.NotNull(it);
            Assert.Equal(itemID, it.ItemID);
        }
    }
}