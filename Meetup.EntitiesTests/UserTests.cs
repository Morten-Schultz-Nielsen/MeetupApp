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
            User user = new User() { FirstName = "Bob", LastName = "Bob" };
            Assert.ThrowsException<ArgumentException>(() => { new User() { FirstName = "Bob1", LastName = "Bob1" }; }, "Names were supposed to throw an argument exception");

            //Check if full name property works correct
            Assert.AreEqual("Bob Bob", user.FullName, "FullName property failed to output correct name");

            //Test descritption
            new User() { Description = "Hello world." };
            Assert.ThrowsException<ArgumentException>(() => { new User() { Description = "   " }; }, "Description is not supposed to be able to be empty");

            //Test descritption
            new User() { PictureUri = @"data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAssAAALLCAYAAAAPCM/bAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsM%E2%80%A6" };
            Assert.ThrowsException<ArgumentException>(() => { new User() { PictureUri = @"data:image;base64,aaa" }; }, "Picture URI should not be able to contain invalid pictures");

            new User() { Address = new Address() };
            Assert.ThrowsException<ArgumentNullException>(() => { new User() { Address = null }; }, "Users should be unable to have null adress");
        }

        [TestMethod()]
        public void CalculateHappinessScoreTest()
        {
            Assert.Fail();
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
                    new UsersInterest() { Interest = new Interest() { Name = "a", Id = 0 } },
                    new UsersInterest() { Interest = new Interest() { Name = "b", Id = 1 } },
                    new UsersInterest() { Interest = new Interest() { Name = "c", Id = 2 } },
                    new UsersInterest() { Interest = new Interest() { Name = "d", Id = 3 } },
                    new UsersInterest() { Interest = new Interest() { Name = "e", Id = 4 } },
                    new UsersInterest() { Interest = new Interest() { Name = "f", Id = 5 } }
                },
                UsersBusinesses = new List<UsersBusiness>()
                {
                    new UsersBusiness() { Business = new Business() { Name = "a", Id = 0 } },
                    new UsersBusiness() { Business = new Business() { Name = "b", Id = 1 } },
                    new UsersBusiness() { Business = new Business() { Name = "c", Id = 2 } },
                    new UsersBusiness() { Business = new Business() { Name = "d", Id = 3 } },
                    new UsersBusiness() { Business = new Business() { Name = "e", Id = 4 } },
                    new UsersBusiness() { Business = new Business() { Name = "f", Id = 5 } }
                }
            };

            //Test if 100% interest wish gives correct score
            Wish wish = new Wish()
            {
                WishInterests = new List<WishInterests>()
                {
                    new WishInterests() { Interest = new Interest() { Name = "a", Id = 0 } },
                    new WishInterests() { Interest = new Interest() { Name = "b", Id = 1 } },
                    new WishInterests() { Interest = new Interest() { Name = "c", Id = 2 } },
                    new WishInterests() { Interest = new Interest() { Name = "d", Id = 3 } },
                    new WishInterests() { Interest = new Interest() { Name = "e", Id = 4 } },
                    new WishInterests() { Interest = new Interest() { Name = "f", Id = 5 } }
                }
            };
            Assert.AreEqual(700, user.CalculateHappinessScore(wish), "Happiness score got the wrong result");

            //Test if 50% interest/business gives correct score
            wish = new Wish()
            {
                WishInterests = new List<WishInterests>()
                {
                    new WishInterests() { Interest = new Interest() { Name = "a" } },
                    new WishInterests() { Interest = new Interest() { Name = "b" } },
                    new WishInterests() { Interest = new Interest() { Name = "c" } }
                },
                WishBusinesses = new List<WishBusinesses>()
                {
                    new WishBusinesses() { Business = new Business() { Name = "a" } },
                    new WishBusinesses() { Business = new Business() { Name = "b" } },
                    new WishBusinesses() { Business = new Business() { Name = "c" } },
                    new WishBusinesses() { Business = new Business() { Name = "q" } }
                }
            };
            //business score = 600/725
            //interest score = 725/725
            Assert.AreEqual(639, user.CalculateHappinessScore(wish), "Happiness score got the wrong result");
        }

        [TestMethod()]
        public void GetInterestsTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetBusinessesTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetOrganizationsTest()
        {
            Assert.Fail();
        }
    }
}