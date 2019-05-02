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
    public class UserTests
    {
        [TestMethod()]
        public void UserTest()
        {
            //Test if naming works correct
            new User() { FirstName = "Bob", LastName = "Bob" };
            Assert.ThrowsException<ArgumentException>(() => { new User() { FirstName = "Bob1", LastName = "Bob1" }; }, "Names were supposed to throw an argument exception");
            Assert.ThrowsException<ArgumentException>(() => { new User() { FirstName = "" }; }, "FirstName cannot be empty");
            Assert.ThrowsException<ArgumentException>(() => { new User() { LastName = "" }; }, "LastName cannot be empty");

            //Test descritption
            new User() { Description = "Hello world." };
            Assert.ThrowsException<ArgumentException>(() => { new User() { Description = "   " }; }, "Description is not supposed to be able to be empty");
            Assert.ThrowsException<ArgumentException>(() => { new User() { Description = "" }; }, "Description cannot be empty");

            //Test picture uri
            new User() { PictureUri = @"data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAssAAALLCAYAAAAPCM/bAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsM%E2%80%A6" };
            Assert.ThrowsException<ArgumentException>(() => { new User() { PictureUri = @"data:image;base64,aaa" }; }, "Picture URI should not be able to contain invalid pictures");
            Assert.ThrowsException<ArgumentException>(() => { new User() { PictureUri = "" }; }, "PictureUri cannot be empty");

            //Test null address
            new User() { Address = new Address() };
            Assert.ThrowsException<ArgumentNullException>(() => { new User() { Address = null }; }, "Events cannot be null");

            //Test if lists can be null
            Assert.ThrowsException<ArgumentNullException>(() => { new User() { Events = null }; }, "Events cannot be null");
            Assert.ThrowsException<ArgumentNullException>(() => { new User() { Invites = null }; }, "EventsUsers cannot be null");
            Assert.ThrowsException<ArgumentNullException>(() => { new User() { Meetings = null }; }, "Meetings cannot be null");
            Assert.ThrowsException<ArgumentNullException>(() => { new User() { Meetings1 = null }; }, "Meetings1 cannot be null");
            Assert.ThrowsException<ArgumentNullException>(() => { new User() { UsersBusinesses = null }; }, "UsersBusinesses cannot be null");
            Assert.ThrowsException<ArgumentNullException>(() => { new User() { UsersInterests = null }; }, "UsersInterests cannot be null");
            Assert.ThrowsException<ArgumentNullException>(() => { new User() { UsersOrganizations = null }; }, "UsersOrganizations cannot be null");
            Assert.ThrowsException<ArgumentNullException>(() => { new User() { Wishes = null }; }, "Wishes cannot be null");
        }

        [TestMethod()]
        public void FullNameTest()
        {
            User user = new User() { FirstName = "Bob", LastName = "Bob" };
            Assert.AreEqual("Bob Bob", user.FullName, "FullName property failed to output correct name");
        }

        [TestMethod()]
        public void CalculateHappinessScoreTest()
        {
            //Create test user
            User user = new User()
            {
                Id = 1,
                UsersInterests = new List<UsersInterest>()
                {
                    new UsersInterest() { InterestId = 0 },
                    new UsersInterest() { InterestId = 1 },
                    new UsersInterest() { InterestId = 2 },
                    new UsersInterest() { InterestId = 3 },
                    new UsersInterest() { InterestId = 4 },
                    new UsersInterest() { InterestId = 5 }
                },
                UsersBusinesses = new List<UsersBusiness>()
                {
                    new UsersBusiness() { BusinessId = 0 },
                    new UsersBusiness() { BusinessId = 1 },
                    new UsersBusiness() { BusinessId = 2 },
                    new UsersBusiness() { BusinessId = 3 },
                    new UsersBusiness() { BusinessId = 4 },
                    new UsersBusiness() { BusinessId = 5 }
                }
            };

            //Create wish list
            List<Wish> wishList = new List<Wish>()
            {
                new Wish()
                {
                    WishInterests = new List<WishInterests>()
                    {
                        new WishInterests() { InterestId = 0 },
                        new WishInterests() { InterestId = 1 },
                        new WishInterests() { InterestId = 2 },
                        new WishInterests() { InterestId = 3 },
                        new WishInterests() { InterestId = 4 },
                        new WishInterests() { InterestId = 5 }
                    }
                },
                new Wish()
                {
                    WishUserId = 1
                }
            };
            Assert.AreEqual(1000, user.CalculateHappinessScore(wishList), "Happiness score was supposed to be the highest score of all the wishes.");
        }

        [TestMethod()]
        public void CalculateHappinessScoreTest1()
        {
            //Create test user
            User user = new User()
            {
                Id = 1,
                UsersInterests = new List<UsersInterest>()
                {
                    new UsersInterest() { InterestId = 0 },
                    new UsersInterest() { InterestId = 1 },
                    new UsersInterest() { InterestId = 2 },
                    new UsersInterest() { InterestId = 3 },
                    new UsersInterest() { InterestId = 4 },
                    new UsersInterest() { InterestId = 5 }
                },
                UsersBusinesses = new List<UsersBusiness>()
                {
                    new UsersBusiness() { BusinessId = 0 },
                    new UsersBusiness() { BusinessId = 1 },
                    new UsersBusiness() { BusinessId = 2 },
                    new UsersBusiness() { BusinessId = 3 },
                    new UsersBusiness() { BusinessId = 4 },
                    new UsersBusiness() { BusinessId = 5 }
                }
            };

            //Test if 100% interest wish gives correct score
            Wish wish = new Wish()
            {
                WishInterests = new List<WishInterests>()
                {
                    new WishInterests() { InterestId = 0 },
                    new WishInterests() { InterestId = 1 },
                    new WishInterests() { InterestId = 2 },
                    new WishInterests() { InterestId = 3 },
                    new WishInterests() { InterestId = 4 },
                    new WishInterests() { InterestId = 5 }
                }
            };
            Assert.AreEqual(700, user.CalculateHappinessScore(wish), "Happiness score got the wrong result");

            //Test if 50% interest/business gives correct score
            wish = new Wish()
            {
                WishInterests = new List<WishInterests>()
                {
                    new WishInterests() { InterestId = 0 },
                    new WishInterests() { InterestId = 1 },
                    new WishInterests() { InterestId = 2 }
                },
                WishBusinesses = new List<WishBusinesses>()
                {
                    new WishBusinesses() { BusinessId = 0 },
                    new WishBusinesses() { BusinessId = 1 },
                    new WishBusinesses() { BusinessId = 2 },
                    new WishBusinesses() { BusinessId = 101 }
                }
            };
            //business score = 600/725
            //interest score = 725/725
            Assert.AreEqual(639, user.CalculateHappinessScore(wish), "Happiness score was not supposed to be 700");

            //Test if user wishes outputs correct score
            wish = new Wish()
            {
                WishUserId = 1
            };
            Assert.AreEqual(1000, user.CalculateHappinessScore(wish), "Happiness score was not supposed to be 1000 because it was a user wish");
        }

        [TestMethod()]
        public void GetInterestsTest()
        {
            //Create test user
            User user = new User()
            {
                Id = 1,
                UsersInterests = new List<UsersInterest>()
                {
                    new UsersInterest() { Interest = new Interest() {Name = "a"} },
                    new UsersInterest() { Interest = new Interest() {Name = "b"} },
                    new UsersInterest() { Interest = new Interest() {Name = "c"} },
                    new UsersInterest() { Interest = new Interest() {Name = "d"} },
                    new UsersInterest() { Interest = new Interest() {Name = "e"} },
                    new UsersInterest() { Interest = new Interest() {Name = "f"} }
                }
            };

            List<Interest> interestList = user.GetInterests().ToList();
            Assert.AreEqual(6, interestList.Count, "GetInterests is supposed to output all the user's interests");
            Assert.AreEqual("a", interestList[0].Name, "The first interest is supposed to be the \"a\" interest");
        }

        [TestMethod()]
        public void GetBusinessesTest()
        {
            User user = new User()
            {
                Id = 1,
                UsersBusinesses = new List<UsersBusiness>()
                {
                    new UsersBusiness() { Business = new Business() {Name = "a"} },
                    new UsersBusiness() { Business = new Business() {Name = "b"} },
                    new UsersBusiness() { Business = new Business() {Name = "c"} },
                    new UsersBusiness() { Business = new Business() {Name = "d"} }
                }
            };

            List<Business> businessList = user.GetBusinesses().ToList();
            Assert.AreEqual(4, businessList.Count, "GetBusinesses is supposed to output all the user's businesses");
            Assert.AreEqual("d", businessList[3].Name, "The last business is supposed to be the \"d\" business");
        }

        [TestMethod()]
        public void GetOrganizationsTest()
        {
            User user = new User()
            {
                Id = 1,
                UsersOrganizations = new List<UsersOrganizations>()
                {
                    new UsersOrganizations() { StartDate = new DateTime(2010,10,30), Organization = new Organization() { Name = "TestOrganization" } },
                    new UsersOrganizations() { StartDate = new DateTime(2005,5,4), Organization = new Organization() { Name = "MyOrganization" } },
                    new UsersOrganizations() { StartDate = new DateTime(2008,6,19), Organization = new Organization() { Name = "TheirOrganization" } }
                }
            };

            List<Organization> organizationList = user.GetOrganizations().ToList();
            Assert.AreEqual(3, organizationList.Count, "GetOrganizations is supposed to output all the user's organization");
            Assert.AreEqual("MyOrganization", organizationList[1].Name, "the 2nd organization in the last is supposed to be \"MyOrganization\"");
        }
    }
}