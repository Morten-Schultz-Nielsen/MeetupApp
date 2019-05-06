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
            new MeetingScore(0, new User(), new User());
            Assert.ThrowsException<ArgumentNullException>(() => { new MeetingScore(0, null, new User()); });
            Assert.ThrowsException<ArgumentNullException>(() => { new MeetingScore(0, new User(), null); });

            //test score
            MeetingScore meetingScore = new MeetingScore(0, new User()
            {
                Id = 0,
                Wishes = new List<Wish>()
                {
                    new Wish() { EventId = 0, WishUserId = 1 }
                }
            },
            new User()
            {
                Id = 1,
                Wishes = new List<Wish>()
                {
                    new Wish() { EventId = 10, WishUserId = 0 }, //ignored wish (wrong event id)
                    new Wish() { EventId = 0, WishInterests = new List<WishInterests>()
                    {
                        new WishInterests() { Interest = new Interest("a") { Id = 1 } }
                    }}
                }
            });
            Assert.AreEqual(1000, meetingScore.Score, "Wrong score calculated");
        }

        [TestMethod()]
        public void SortTest()
        {
            //test meetings
            MeetingScore.Sort(new MeetingScore(0, new User(), new User()), new MeetingScore(0, new User(), new User()));
            Assert.ThrowsException<ArgumentNullException>(() => { MeetingScore.Sort(null, new MeetingScore(0, new User(), new User())); });
            Assert.ThrowsException<ArgumentNullException>(() => { MeetingScore.Sort(new MeetingScore(0, new User(), new User()), null); });

            //Test sorting
            //Create test users
            User testUser1 = new User()
            {
                Id = 1,
                Wishes = new List<Wish>()
                {
                    new Wish() { WishUserId = 2 }
                },
                UsersInterests = new List<UsersInterest>()
                {
                    new UsersInterest() { Interest = new Interest("a") { Id = 0 } }
                }
            };
            User testUser2 = new User()
            {
                Id = 2,
                Wishes = new List<Wish>()
                {
                    new Wish() { WishUserId = 1 }
                }
            };
            User testUser3 = new User()
            {
                Id = 3,
                Wishes = new List<Wish>()
                {
                    new Wish() {WishInterests = new List<WishInterests>()
                    {
                        new WishInterests() { InterestId = 0 }
                    }}
                }
            };

            //Create test meeting scores
            MeetingScore testMeeting1 = new MeetingScore(0, testUser1, testUser2);
            MeetingScore testMeeting2 = new MeetingScore(0, testUser2, testUser1);
            MeetingScore testMeeting3 = new MeetingScore(0, testUser3, testUser1);

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
                new User() { Id = 1 },
                new User() { Id = 2 },
                new User() { Id = 3 },
                new User() { Id = 4 }
            };
            List<MeetingScore> meetings = MeetingScore.GetPossibleMeetings(users, 1);
            //3+2+1 = 6
            Assert.AreEqual(6,meetings.Count, "The correct amount of meetings wasnt created");
            Assert.AreEqual(3, meetings.Count(m => m.Person1.Id == 3 || m.Person2.Id == 3), "the users arent in the correct amount of meetings");
        }
    }
}