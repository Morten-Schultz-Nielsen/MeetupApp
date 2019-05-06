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
    public class InterestTests
    {
        [TestMethod()]
        public void InterestTest()
        {
            //Test name
            new Interest("c#");
            Assert.ThrowsException<ArgumentException>(() => { new Interest(null); }, "Name cannot be empty");

            //Test if list can be null
            Assert.ThrowsException<ArgumentNullException>(() => { new Interest("a") { UsersInterests = null }; }, "UsersInterests cannot be null");
        }
    }
}