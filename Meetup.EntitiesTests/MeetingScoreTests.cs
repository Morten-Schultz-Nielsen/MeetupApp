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
    public class MeetingScoreTests
    {
        [TestMethod()]
        public void MeetingScoreTest()
        {
            //test users
            new MeetingScore(0, UserTests.GetSimpleUser(), UserTests.GetSimpleUser());
            Assert.ThrowsException<ArgumentNullException>(() => { new MeetingScore(0, null, UserTests.GetSimpleUser()); });
            Assert.ThrowsException<ArgumentNullException>(() => { new MeetingScore(0, UserTests.GetSimpleUser(), null); });

            //test score
            User user1 = UserTests.GetSimpleUser(0);
            User user2 = UserTests.GetSimpleUser(1);
            user1.Wishes = new List<Wish>()
            {
                new Wish(UserTests.GetSimpleUser(1), EventTests.GetSimpleEvent(0)) { WishUser = user2 }
            };
            user2.Wishes = new List<Wish>()
            {
                new Wish(UserTests.GetSimpleUser(1), EventTests.GetSimpleEvent(10)) {  WishUser = UserTests.GetSimpleUser(0) }, //ignored wish (wrong event id)
                new Wish(UserTests.GetSimpleUser(1), EventTests.GetSimpleEvent(0)) {WishInterests = new List<WishInterests>()
                {
                    new WishInterests(new Interest("a") { Id = 1 }, WishTests.GetSimpleWish())
                }}
            };
            MeetingScore meetingScore = new MeetingScore(0, user1, user2);
            Assert.AreEqual(1000, meetingScore.Score, "Wrong score calculated");
        }

        [TestMethod()]
        public void SortTest()
        {
            //test meetings
            MeetingScore.Sort(new MeetingScore(0, UserTests.GetSimpleUser(), UserTests.GetSimpleUser()), new MeetingScore(0, UserTests.GetSimpleUser(), UserTests.GetSimpleUser()));
            Assert.ThrowsException<ArgumentNullException>(() => { MeetingScore.Sort(null, new MeetingScore(0, UserTests.GetSimpleUser(), UserTests.GetSimpleUser())); });
            Assert.ThrowsException<ArgumentNullException>(() => { MeetingScore.Sort(new MeetingScore(0, UserTests.GetSimpleUser(), UserTests.GetSimpleUser()), null); });

            //Test sorting
            Event @event = EventTests.GetSimpleEvent(0);
            //Create test users
            User testUser1 = UserTests.GetSimpleUser(1);
            User testUser2 = UserTests.GetSimpleUser(2);
            User testUser3 = UserTests.GetSimpleUser(3);
            testUser1.Wishes = new List<Wish>()
            {
                new Wish(testUser1, @event) { WishUser = testUser2 }
            };
            testUser1.UsersInterests = new List<UsersInterest>()
            {
                new UsersInterest(InterestTests.GetSimpleInterest(0), testUser1)
            };
            testUser2.Wishes = new List<Wish>()
            {
                new Wish(testUser2, @event) { WishUser = testUser1 }
            };
            testUser3.Wishes = new List<Wish>()
            {
                new Wish(testUser3, @event) {WishInterests = new List<WishInterests>()
                {
                    new WishInterests(InterestTests.GetSimpleInterest(), WishTests.GetSimpleWish())
                }}
            };

            //Create test meeting scores
            MeetingScore testMeeting1 = new MeetingScore(@event.Id, testUser1, testUser2);
            MeetingScore testMeeting2 = new MeetingScore(@event.Id, testUser2, testUser1);
            MeetingScore testMeeting3 = new MeetingScore(@event.Id, testUser3, testUser1);

            //do tests
            Assert.AreEqual(0, MeetingScore.Sort(testMeeting1, testMeeting2));
            Assert.AreEqual(-1, MeetingScore.Sort(testMeeting1, testMeeting3));
            Assert.AreEqual(1, MeetingScore.Sort(testMeeting3, testMeeting1));
        }

        [TestMethod()]
        public void GetPossibleMeetingsTest()
        {
            //Test parameter null
            Assert.ThrowsException<ArgumentNullException>(() => { MeetingScore.GetPossibleMeetings(null, 0); });

            //Test creation
            List<User> users = new List<User>()
            {
                UserTests.GetSimpleUser(1),
                UserTests.GetSimpleUser(2),
                UserTests.GetSimpleUser(3),
                UserTests.GetSimpleUser(4)
            };
            List<MeetingScore> meetings = MeetingScore.GetPossibleMeetings(users, 1);
            //3+2+1 = 6
            Assert.AreEqual(6,meetings.Count, "The correct amount of meetings wasnt created");
            Assert.AreEqual(3, meetings.Count(m => m.Person1.Id == 3 || m.Person2.Id == 3), "the users arent in the correct amount of meetings");
        }
    }
}