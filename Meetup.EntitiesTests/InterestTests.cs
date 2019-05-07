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
        /// <summary>
        /// Creates a simple <see cref="Interest"/> object
        /// </summary>
        /// <param name="id">the id the interest should have</param>
        /// <returns>A simple <see cref="Interest"/></returns>
        public static Interest GetSimpleInterest(int id = 0)
        {
            return new Interest("C#") { Id = id };
        }

        [TestMethod()]
        public void GetSimpleTest()
        {
            GetSimpleInterest();
        }

        [TestMethod()]
        public void InterestTest()
        {
            GetSimpleInterest();

            //Test name
            Assert.ThrowsException<ArgumentException>(() => { new Interest(null); }, "Name cannot be empty");

            //Test if list can be null
            Assert.ThrowsException<ArgumentNullException>(() => { new Interest("a") { UsersInterests = null }; }, "UsersInterests cannot be null");
        }
    }
}