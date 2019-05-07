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
            Assert.AreEqual("Wishing to talk with Bob Bobsen.", wish.ToString(), "ToString not correct for user wish");

            //test organization
            wish.WishUser = null;
            wish.WishOrganization = new Organization("Org") { Id = 1 };
            Assert.AreEqual("Wishing to talk with a person who worked in the organization \"Org\".", wish.ToString(), "ToString not correct for organization.");
            wish.WishOrganizationTime = 1;
            Assert.AreEqual("Wishing to talk with a person who worked in the organization \"Org\" for 1 year.", wish.ToString(), "ToString not correct for organization (single year).");
            wish.WishOrganizationTime = 10;
            Assert.AreEqual("Wishing to talk with a person who worked in the organization \"Org\" for 10 years.", wish.ToString(), "ToString not correct for organization (multiple years).");
            wish.WishOrganization = null;
            Assert.AreEqual("Wishing to talk with a person who worked in an organization for 10 years.", wish.ToString(), "ToString not correct for organization time (multiple years).");
            wish.WishOrganizationTime = 1;
            Assert.AreEqual("Wishing to talk with a person who worked in an organization for 1 year.", wish.ToString(), "ToString not correct for organization time (single year).");

            //Test interests
            wish = GetSimpleWish();
            wish.WishInterests = new List<WishInterests>()
            {
                new WishInterests(InterestTests.GetSimpleInterest(), wish)
            };
            Assert.AreEqual("Wishing to talk with a person who has 1 interest.", wish.ToString(), "ToString not correct for single interest");
            wish.WishInterests.Add(new WishInterests(InterestTests.GetSimpleInterest(), wish));
            Assert.AreEqual("Wishing to talk with a person who has 2 interests.", wish.ToString(), "ToString not correct for multiple interests");

            //Test businesses
            wish = GetSimpleWish();
            wish.WishBusinesses = new List<WishBusinesses>()
            {
                new WishBusinesses(BusinessTests.GetSimpleBusiness(), wish)
            };
            Assert.AreEqual("Wishing to talk with a person who works in 1 business.", wish.ToString(), "ToString not correct for single business");
            wish.WishBusinesses.Add(new WishBusinesses(BusinessTests.GetSimpleBusiness(), wish));
            Assert.AreEqual("Wishing to talk with a person who works in 2 businesses.", wish.ToString(), "ToString not correct for multiple businesses");

            //Test wish with 2 parts
            wish.WishInterests = new List<WishInterests>()
            {
                new WishInterests(InterestTests.GetSimpleInterest(), wish)
            };
            Assert.AreEqual("Wishing to talk with a person who has 1 interest and works in 2 businesses.", wish.ToString(), "ToString not correct with 2 wish parts");

            //Test wish with 3 parts
            wish.WishOrganization = new Organization("Org") { Id = 1 };
            Assert.AreEqual("Wishing to talk with a person who has 1 interest, works in 2 businesses and worked in the organization \"Org\".", wish.ToString(), "ToString not correct with 3 wish parts");
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