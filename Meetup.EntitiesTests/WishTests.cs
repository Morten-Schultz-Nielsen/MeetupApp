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
    public class WishTests
    {
        /// <summary>
        /// Creates a simple <see cref="Wish"/> object
        /// </summary>
        /// <param name="userId">the id of the user who is wishing</param>
        /// <param name="eventId">the id of the event the wish is for</param>
        /// <returns>A simple <see cref="Wish"/></returns>
        public static Wish GetSimpleWish(int userId = 0, int eventId = 0)
        {
            return new Wish(UserTests.GetSimpleUser(userId), EventTests.GetSimpleEvent(eventId));
        }

        [TestMethod()]
        public void GetSimpleTest()
        {
            GetSimpleWish();
        }

        [TestMethod()]
        public void WishTest()
        {
            Wish testWish = GetSimpleWish();

            //test wishUser
            new Wish(UserTests.GetSimpleUser(0), EventTests.GetSimpleEvent(0)) { WishUser = null };

            //Test wishorganization
            new Wish(UserTests.GetSimpleUser(0), EventTests.GetSimpleEvent(0)) { WishOrganization = null };
            new Wish(UserTests.GetSimpleUser(0), EventTests.GetSimpleEvent(0)) { WishOrganizationTime = null };

            //Test lists
            Assert.ThrowsException<ArgumentNullException>(() => { testWish.WishBusinesses = null; }, "WishBusinesses cannot be empty");
            Assert.ThrowsException<ArgumentNullException>(() => { testWish.WishInterests = null; }, "WishInterests cannot be empty");
        }

        [TestMethod()]
        public void ToStringTest()
        {
            //test wish for user tostring
            Wish wish = GetSimpleWish();
            wish.WishUser = UserTests.GetSimpleUser(1);
            Assert.AreEqual("Ønsker at snakke med Bob Bobsen.", wish.ToString(), "ToString not correct for user wish");

            //test organization
            wish.WishUser = null;
            wish.WishOrganization = new Organization("Org") { Id = 1 };
            Assert.AreEqual("Ønsker at snakke med en person som har arbejdet i organisationen \"Org\".", wish.ToString(), "ToString not correct for organization.");
            wish.WishOrganizationTime = 1;
            Assert.AreEqual("Ønsker at snakke med en person som har arbejdet i organisationen \"Org\" i 1 år.", wish.ToString(), "ToString not correct for organization (years).");
            wish.WishOrganization = null;
            Assert.AreEqual("Ønsker at snakke med en person som har arbejdet i en organisation i 1 år.", wish.ToString(), "ToString not correct for organization time.");

            //Test interests
            wish = GetSimpleWish();
            wish.WishInterests = new List<WishInterests>()
            {
                new WishInterests(InterestTests.GetSimpleInterest(), wish)
            };
            Assert.AreEqual("Ønsker at snakke med en person som har 1 interesse.", wish.ToString(), "ToString not correct for single interest");
            wish.WishInterests.Add(new WishInterests(InterestTests.GetSimpleInterest(), wish));
            Assert.AreEqual("Ønsker at snakke med en person som har 2 interesser.", wish.ToString(), "ToString not correct for multiple interests");

            //Test businesses
            wish = GetSimpleWish();
            wish.WishBusinesses = new List<WishBusinesses>()
            {
                new WishBusinesses(BusinessTests.GetSimpleBusiness(), wish)
            };
            Assert.AreEqual("Ønsker at snakke med en person som arbejder i 1 erhverv.", wish.ToString(), "ToString not correct for single business");

            //Test wish with 2 parts
            wish.WishInterests = new List<WishInterests>()
            {
                new WishInterests(InterestTests.GetSimpleInterest(), wish)
            };
            Assert.AreEqual("Ønsker at snakke med en person som har 1 interesse og arbejder i 1 erhverv.", wish.ToString(), "ToString not correct with 2 wish parts");

            //Test wish with 3 parts
            wish.WishOrganization = new Organization("Org") { Id = 1 };
            Assert.AreEqual("Ønsker at snakke med en person som har 1 interesse, arbejder i 1 erhverv og har arbejdet i organisationen \"Org\".", wish.ToString(), "ToString not correct with 3 wish parts");
        }

        [TestMethod()]
        public void GetInterestsTest()
        {
            Wish wish = GetSimpleWish();
            wish.WishInterests = new List<WishInterests>()
            {
                new WishInterests(new Interest("a") { Id = 0 }, wish),
                new WishInterests(new Interest("a") { Id = 1 }, wish),
                new WishInterests(new Interest("a") { Id = 2 }, wish),
                new WishInterests(new Interest("a") { Id = 3 }, wish)
            };

            List<Interest> interestList = wish.GetInterests().ToList();
            Assert.AreEqual(4, interestList.Count, "GetInterest returned wrong amount of interests");
            Assert.AreEqual(2, interestList[2].Id, "GetInterest returned wrong interests");
        }

        [TestMethod()]
        public void GetBusinessesTest()
        {
            Wish wish = GetSimpleWish();
            wish.WishBusinesses = new List<WishBusinesses>()
            {
                new WishBusinesses(new Business("a") { Id = 0 }, wish),
                new WishBusinesses(new Business("a") { Id = 1 }, wish),
                new WishBusinesses(new Business("a") { Id = 2 }, wish),
                new WishBusinesses(new Business("a") { Id = 3 }, wish)
            };

            List<Business> businessList = wish.GetBusinesses().ToList();
            Assert.AreEqual(4, businessList.Count, "GetBusinesses returned wrong amount of businesses");
            Assert.AreEqual(2, businessList[2].Id, "GetBusinesses returned wrong businesses");
        }
    }
}