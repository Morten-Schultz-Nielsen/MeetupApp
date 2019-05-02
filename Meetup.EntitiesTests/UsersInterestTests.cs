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
    public class UsersInterestTests
    {

        [TestMethod()]
        public void ConvertTest()
        {
            List<UsersInterest> convertResult = UsersInterest.Convert(new List<Interest>()
            {
                new Interest() { Id = 1 },
                new Interest() { Id = 2 },
                new Interest() { Id = 3 },
                new Interest() { Id = 4 }
            }, 10).ToList();
            Assert.AreEqual(4, convertResult.Count, "failed to convert the list");
            Assert.AreEqual(1, convertResult[0].Interest.Id, "failed to convert interest to the list");
            Assert.AreEqual(10, convertResult[0].UserId, "failed to convert id to the list");
        }
    }
}