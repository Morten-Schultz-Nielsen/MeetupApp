using Microsoft.VisualStudio.TestTools.UnitTesting;
using Meetup.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meetup.Entities.Tests
{
    [TestClass()]
    public class UsersBusinessTests
    {
        [TestMethod()]
        public void UsersBusinessTest()
        {
            //Test business
            new UsersBusiness() { Business = new Business() };
            Assert.ThrowsException<ArgumentNullException>(() => { new UsersBusiness() { Business = null }; }, "Business cannot be empty");

            //Test user
            new UsersBusiness() { User = new User() };
            Assert.ThrowsException<ArgumentNullException>(() => { new UsersBusiness() { User = null }; }, "User cannot be empty");
        }

        [TestMethod()]
        public void ConvertTest()
        {
            List<UsersBusiness> convertResult = UsersBusiness.Convert(new List<Business>()
            {
                new Business() { Id = 1 },
                new Business() { Id = 2 },
                new Business() { Id = 3 },
                new Business() { Id = 4 }
            }, 10).ToList();
            Assert.AreEqual(4, convertResult.Count, "failed to convert the list");
            Assert.AreEqual(1, convertResult[0].Business.Id, "failed to convert business to the list");
            Assert.AreEqual(10, convertResult[0].UserId, "failed to convert id to the list");
        }
    }
}