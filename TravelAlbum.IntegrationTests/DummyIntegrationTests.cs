using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TravelAlbum.Data;

namespace TravelAlbum.IntegrationTests
{
    [TestClass]
    public class DummyTests
    {
        [TestMethod]
        public void DummyMethod()
        {
            TravelAlbumEfDbContext dbContext = new TravelAlbumEfDbContext();
            string cnn = dbContext.Database.Connection.ConnectionString;

            //var extrtravels = dbContext.Travels.ToList();

            var check = 0;
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void DummyMethod2()
        {
            Assert.IsTrue(true);
        }
    }
}
