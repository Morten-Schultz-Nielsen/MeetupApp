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
        /// <summary>
        /// Creates an simple <see cref="User"/> object
        /// </summary>
        /// <param name="userId">The id the user should have</param>
        /// <returns>An simple <see cref="User"/></returns>
        public static User GetSimpleUser(int userId = 0)
        {
            return new User("Bob","Bobsen","Hello, I'm bob","data:image/png;base64,aaa","a@a.a", AddressTests.GetSimpleAddress()) { Id = userId };
        }

        [TestMethod()]
        public void GetSimpleTest()
        {
            GetSimpleUser();
        }

        [TestMethod()]
        public void UserTest()
        {
            GetSimpleUser();

            //Test if naming works correct
            Assert.ThrowsException<ArgumentException>(() => { new User("Bob1", "Bobsen1", "Hello, I'm bob", "data:image/png;base64,aaa", "a@a.a", AddressTests.GetSimpleAddress()); }, "Names were supposed to throw an argument exception");
            Assert.ThrowsException<ArgumentException>(() => { new User("", "Bobsen", "Hello, I'm bob", "data:image/png;base64,aaa", "a@a.a", AddressTests.GetSimpleAddress()); }, "FirstName cannot be empty");
            Assert.ThrowsException<ArgumentException>(() => { new User("Bob", "", "Hello, I'm bob", "data:image/png;base64,aaa", "a@a.a", AddressTests.GetSimpleAddress()); }, "LastName cannot be empty");

            //Test descritption
            Assert.ThrowsException<ArgumentException>(() => { new User("Bob", "Bobsen", "", "data:image/png;base64,aaa", "a@a.a", AddressTests.GetSimpleAddress()); }, "Description is not supposed to be able to be empty");

            //Test picture uri
            Assert.ThrowsException<ArgumentException>(() => { new User("Bob", "Bobsen", "Hello, I'm bob", "data:image/pn;base63,aaa", "a@a.a", AddressTests.GetSimpleAddress()); }, "Picture URI should not be able to contain invalid pictures");
            Assert.ThrowsException<ArgumentException>(() => { new User("Bob", "Bobsen", "Hello, I'm bob", "", "a@a.a", AddressTests.GetSimpleAddress()); }, "PictureUri cannot be empty");

            //Test null address
            User testUser = GetSimpleUser();
            testUser.Address = AddressTests.GetSimpleAddress();
            Assert.ThrowsException<ArgumentNullException>(() => { testUser.Address = null; }, "Events cannot be null");

            //Test if lists can be null
            Assert.ThrowsException<ArgumentNullException>(() => { testUser.Events = null; }, "Events cannot be null");
            Assert.ThrowsException<ArgumentNullException>(() => { testUser.Invites = null; }, "EventsUsers cannot be null");
            Assert.ThrowsException<ArgumentNullException>(() => { testUser.Meetings = null; }, "Meetings cannot be null");
            Assert.ThrowsException<ArgumentNullException>(() => { testUser.Meetings1 = null; }, "Meetings1 cannot be null");
            Assert.ThrowsException<ArgumentNullException>(() => { testUser.UsersBusinesses = null; }, "UsersBusinesses cannot be null");
            Assert.ThrowsException<ArgumentNullException>(() => { testUser.UsersInterests = null; }, "UsersInterests cannot be null");
            Assert.ThrowsException<ArgumentNullException>(() => { testUser.UsersOrganizations = null; }, "UsersOrganizations cannot be null");
            Assert.ThrowsException<ArgumentNullException>(() => { testUser.Wishes = null; }, "Wishes cannot be null");
            Assert.ThrowsException<ArgumentNullException>(() => { testUser.UserPauses = null; }, "UserPauses cannot be null");
        }

        [TestMethod()]
        public void FullNameTest()
        {
            Assert.AreEqual("Bob Bobsen", GetSimpleUser().FullName, "FullName property failed to output correct name");
        }

        [TestMethod()]
        public void CalculateHappinessScoreTest()
        {
            //Create test user
            User user = GetSimpleUser(1);
            user.UsersInterests = new List<UsersInterest>()
            {
                new UsersInterest(InterestTests.GetSimpleInterest(0), user),
                new UsersInterest(InterestTests.GetSimpleInterest(1), user),
                new UsersInterest(InterestTests.GetSimpleInterest(2), user),
                new UsersInterest(InterestTests.GetSimpleInterest(3), user),
                new UsersInterest(InterestTests.GetSimpleInterest(4), user),
                new UsersInterest(InterestTests.GetSimpleInterest(5), user)
            };
            user.UsersBusinesses = new List<UsersBusiness>()
            {
                new UsersBusiness(BusinessTests.GetSimpleBusiness(0),user),
                new UsersBusiness(BusinessTests.GetSimpleBusiness(1),user),
                new UsersBusiness(BusinessTests.GetSimpleBusiness(2),user),
                new UsersBusiness(BusinessTests.GetSimpleBusiness(3),user),
                new UsersBusiness(BusinessTests.GetSimpleBusiness(4),user),
                new UsersBusiness(BusinessTests.GetSimpleBusiness(5),user)
            };

            //Create wish list
            List<Wish> wishList = new List<Wish>()
            {
                new Wish(GetSimpleUser(), EventTests.GetSimpleEvent())
                {
                    WishUser = GetSimpleUser(1)
                }
            };
            wishList.Add(WishTests.GetSimpleWish());
            wishList[1].WishInterests = new List<WishInterests>()
            {
                new WishInterests(InterestTests.GetSimpleInterest(0), wishList[1]),
                new WishInterests(InterestTests.GetSimpleInterest(1), wishList[1]),
                new WishInterests(InterestTests.GetSimpleInterest(2), wishList[1]),
                new WishInterests(InterestTests.GetSimpleInterest(3), wishList[1]),
                new WishInterests(InterestTests.GetSimpleInterest(4), wishList[1]),
                new WishInterests(InterestTests.GetSimpleInterest(5), wishList[1])
            };
            Assert.AreEqual(1000, user.CalculateHappinessScore(wishList), "Happiness score was supposed to be the highest score of all the wishes.");
        }

        [TestMethod()]
        public void CalculateHappinessScoreTest1()
        {
            //Create test user
            User user = GetSimpleUser(1);
            user.UsersInterests = new List<UsersInterest>()
            {
                new UsersInterest(InterestTests.GetSimpleInterest(0), user),
                new UsersInterest(InterestTests.GetSimpleInterest(1), user),
                new UsersInterest(InterestTests.GetSimpleInterest(2), user),
                new UsersInterest(InterestTests.GetSimpleInterest(3), user),
                new UsersInterest(InterestTests.GetSimpleInterest(4), user),
                new UsersInterest(InterestTests.GetSimpleInterest(5), user)
            };
            user.UsersBusinesses = new List<UsersBusiness>()
            {
                new UsersBusiness(BusinessTests.GetSimpleBusiness(0), user),
                new UsersBusiness(BusinessTests.GetSimpleBusiness(1), user),
                new UsersBusiness(BusinessTests.GetSimpleBusiness(2), user),
                new UsersBusiness(BusinessTests.GetSimpleBusiness(3), user),
                new UsersBusiness(BusinessTests.GetSimpleBusiness(4), user),
                new UsersBusiness(BusinessTests.GetSimpleBusiness(5), user)
            };

            //Test if 100% interest wish gives correct score
            Wish wish = WishTests.GetSimpleWish();
            wish.WishInterests = new List<WishInterests>()
            {
                new WishInterests(InterestTests.GetSimpleInterest(0), wish),
                new WishInterests(InterestTests.GetSimpleInterest(1), wish),
                new WishInterests(InterestTests.GetSimpleInterest(2), wish),
                new WishInterests(InterestTests.GetSimpleInterest(3), wish),
                new WishInterests(InterestTests.GetSimpleInterest(4), wish),
                new WishInterests(InterestTests.GetSimpleInterest(5), wish)
            };
            Assert.AreEqual(700, user.CalculateHappinessScore(wish), "Happiness score got the wrong result");

            //Test if 50% interest/business gives correct score
            wish = new Wish(GetSimpleUser(), EventTests.GetSimpleEvent())
            {
                WishInterests = new List<WishInterests>()
                {
                    new WishInterests(InterestTests.GetSimpleInterest(0), wish),
                    new WishInterests(InterestTests.GetSimpleInterest(1), wish),
                    new WishInterests(InterestTests.GetSimpleInterest(2), wish)
                },
                WishBusinesses = new List<WishBusinesses>()
                {
                    new WishBusinesses(BusinessTests.GetSimpleBusiness(0), wish),
                    new WishBusinesses(BusinessTests.GetSimpleBusiness(1), wish),
                    new WishBusinesses(BusinessTests.GetSimpleBusiness(2), wish),
                    new WishBusinesses(BusinessTests.GetSimpleBusiness(101), wish)
                }
            };
            //business score = 600/725
            //interest score = 725/725
            Assert.AreEqual(639, user.CalculateHappinessScore(wish), "Happiness score was not supposed to be 700");

            //Test if user wishes outputs correct score
            wish = new Wish(GetSimpleUser(), EventTests.GetSimpleEvent())
            {
                WishUser = GetSimpleUser(1)
            };
            Assert.AreEqual(1000, user.CalculateHappinessScore(wish), "Happiness score was not supposed to be 1000 because it was a user wish");
        }

        [TestMethod()]
        public void GetInterestsTest()
        {
            //Create test user
            User user = GetSimpleUser(1);
            user.UsersInterests = new List<UsersInterest>()
            {
                new UsersInterest(new Interest("a"), user),
                new UsersInterest(new Interest("b"), user),
                new UsersInterest(new Interest("c"), user),
                new UsersInterest(new Interest("d"), user),
                new UsersInterest(new Interest("e"), user),
                new UsersInterest(new Interest("f"), user)
            };

            List<Interest> interestList = user.GetInterests().ToList();
            Assert.AreEqual(6, interestList.Count, "GetInterests is supposed to output all the user's interests");
            Assert.AreEqual("a", interestList[0].Name, "The first interest is supposed to be the \"a\" interest");
        }

        [TestMethod()]
        public void GetBusinessesTest()
        {
            User user = GetSimpleUser(1);
            user.UsersBusinesses = new List<UsersBusiness>()
            {
                new UsersBusiness(new Business("a"), user),
                new UsersBusiness(new Business("b"), user),
                new UsersBusiness(new Business("c"), user),
                new UsersBusiness(new Business("d"), user)
            };

            List<Business> businessList = user.GetBusinesses().ToList();
            Assert.AreEqual(4, businessList.Count, "GetBusinesses is supposed to output all the user's businesses");
            Assert.AreEqual("d", businessList[3].Name, "The last business is supposed to be the \"d\" business");
        }

        [TestMethod()]
        public void GetOrganizationsTest()
        {
            User user = GetSimpleUser(1);
            user.UsersOrganizations = new List<UsersOrganizations>()
            {
                new UsersOrganizations(new Organization("TestOrganization"), GetSimpleUser(), new DateTime(2010,10,30)),
                new UsersOrganizations(new Organization("MyOrganization"), GetSimpleUser(), new DateTime(2005,5,4)),
                new UsersOrganizations(new Organization("TheirOrganization"), GetSimpleUser(), new DateTime(2008,6,19))
            };

            List<Organization> organizationList = user.GetOrganizations().ToList();
            Assert.AreEqual(3, organizationList.Count, "GetOrganizations is supposed to output all the user's organization");
            Assert.AreEqual("MyOrganization", organizationList[1].Name, "the 2nd organization in the last is supposed to be \"MyOrganization\"");
        }
    }
}