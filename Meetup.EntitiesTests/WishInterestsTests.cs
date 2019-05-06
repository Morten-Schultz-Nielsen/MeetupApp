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
    public class WishInterestsTests
    {
        [TestMethod()]
        public void ConvertTest()
        {
            List<WishInterests> convertResult = WishInterests.Convert(new List<Interest>()
            {
                new Interest("a") { Id = 1 },
                new Interest("a") { Id = 2 },
                new Interest("a") { Id = 3 },
                new Interest("a") { Id = 4 }
            }, 10).ToList();
            Assert.AreEqual(4, convertResult.Count, "failed to convert the list");
            Assert.AreEqual(1, convertResult[0].Interest.Id, "failed to convert interest to the list");
            Assert.AreEqual(10, convertResult[0].WishId, "failed to convert id to the list");
        }
    }
}