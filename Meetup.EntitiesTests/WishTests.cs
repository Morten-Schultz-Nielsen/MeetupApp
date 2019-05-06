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
        [TestMethod()]
        public void WishTest()
        {
            //Test user
            new Wish() { User = new User() };
            Assert.ThrowsException<ArgumentNullException>(() => { new Wish() { User = null }; }, "User cannot be empty");

            //Test event
            new Wish() { Event = EventTests.GetSimpleEvent() };
            Assert.ThrowsException<ArgumentNullException>(() => { new Wish() { Event = null }; }, "Event cannot be empty");

            //test wishUser
            new Wish() { WishUserId = null };
            new Wish() { WishUser = null };

            //Test wishorganization
            new Wish() { WishOrganizationId = null };
            new Wish() { WishOrganization = null };
            new Wish() { WishOrganizationTime = null };

            //Test lists
            Assert.ThrowsException<ArgumentNullException>(() => { new Wish() { WishBusinesses = null }; }, "WishBusinesses cannot be empty");
            Assert.ThrowsException<ArgumentNullException>(() => { new Wish() { WishInterests = null }; }, "WishInterests cannot be empty");
        }

        [TestMethod()]
        public void ToStringTest()
        {
            //test wish for user tostring
            Wish wish = new Wish() { WishUserId = 1, WishUser = new User() { Id = 1, FirstName = "Bla", LastName = "Bla" } };
            Assert.AreEqual("Wishing to talk with Bla Bla.", wish.ToString(), "ToString not correct for user wish");

            //test organization
            wish = new Wish() { WishOrganizationId = 1, WishOrganization = new Organization() { Id = 1, Name = "Org" } };
            Assert.AreEqual("Wishing to talk with a person who worked in the organization \"Org\".", wish.ToString(), "ToString not correct for organization.");
            wish.WishOrganizationTime = 1;
            Assert.AreEqual("Wishing to talk with a person who worked in the organization \"Org\" for 1 year.", wish.ToString(), "ToString not correct for organization (single year).");
            wish.WishOrganizationTime = 10;
            Assert.AreEqual("Wishing to talk with a person who worked in the organization \"Org\" for 10 years.", wish.ToString(), "ToString not correct for organization (multiple years).");
            wish.WishOrganizationId = null;
            wish.WishOrganization = null;
            Assert.AreEqual("Wishing to talk with a person who worked in an organization for 10 years.", wish.ToString(), "ToString not correct for organization time (multiple years).");
            wish.WishOrganizationTime = 1;
            Assert.AreEqual("Wishing to talk with a person who worked in an organization for 1 year.", wish.ToString(), "ToString not correct for organization time (single year).");

            //Test interests
            wish = new Wish() { WishInterests = new List<WishInterests>()
            {
                new WishInterests()
            }};
            Assert.AreEqual("Wishing to talk with a person who has 1 interest.", wish.ToString(), "ToString not correct for single interest");
            wish.WishInterests.Add(new WishInterests());
            Assert.AreEqual("Wishing to talk with a person who has 2 interests.", wish.ToString(), "ToString not correct for multiple interests");

            //Test businesses
            wish = new Wish() { WishBusinesses = new List<WishBusinesses>()
            {
                new WishBusinesses()
            }};
            Assert.AreEqual("Wishing to talk with a person who works in 1 business.", wish.ToString(), "ToString not correct for single business");
            wish.WishBusinesses.Add(new WishBusinesses());
            Assert.AreEqual("Wishing to talk with a person who works in 2 businesses.", wish.ToString(), "ToString not correct for multiple businesses");

            //Test wish with 2 parts
            wish.WishInterests = new List<WishInterests>()
            {
                new WishInterests()
            };
            Assert.AreEqual("Wishing to talk with a person who has 1 interest and works in 2 businesses.", wish.ToString(), "ToString not correct with 2 wish parts");

            //Test wish with 3 parts
            wish.WishOrganization = new Organization() { Id = 1, Name = "Org" };
            wish.WishOrganizationId = 1;
            Assert.AreEqual("Wishing to talk with a person who has 1 interest, works in 2 businesses and worked in the organization \"Org\".", wish.ToString(), "ToString not correct with 3 wish parts");
        }

        [TestMethod()]
        public void GetInterestsTest()
        {
            Wish wish = new Wish() { WishInterests = new List<WishInterests>()
            {
                new WishInterests() { Interest = new Interest("a") { Id = 0 } },
                new WishInterests() { Interest = new Interest("a") { Id = 1 } },
                new WishInterests() { Interest = new Interest("a") { Id = 2 } },
                new WishInterests() { Interest = new Interest("a") { Id = 3 } }
            }};

            List<Interest> interestList = wish.GetInterests().ToList();
            Assert.AreEqual(4, interestList.Count, "GetInterest returned wrong amount of interests");
            Assert.AreEqual(2, interestList[2].Id, "GetInterest returned wrong interests");
        }

        [TestMethod()]
        public void GetBusinessesTest()
        {
            Wish wish = new Wish() { WishBusinesses = new List<WishBusinesses>()
            {
                new WishBusinesses() { Business = new Business("a") { Id = 0 } },
                new WishBusinesses() { Business = new Business("a") { Id = 1 } },
                new WishBusinesses() { Business = new Business("a") { Id = 2 } },
                new WishBusinesses() { Business = new Business("a") { Id = 3 } }
            }};

            List<Business> businessList = wish.GetBusinesses().ToList();
            Assert.AreEqual(4, businessList.Count, "GetBusinesses returned wrong amount of businesses");
            Assert.AreEqual(2, businessList[2].Id, "GetBusinesses returned wrong businesses");
        }
    }
}