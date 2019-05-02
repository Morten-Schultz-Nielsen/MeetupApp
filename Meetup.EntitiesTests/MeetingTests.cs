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
    public class MeetingTests
    {
        [TestMethod()]
        public void MeetingTest()
        {
            //Test seance
            new Meeting() { Seance = new Seance() };
            Assert.ThrowsException<ArgumentNullException>(() => { new Meeting() { Seance = null }; }, "Seance cannot be null");

            //Test user one
            new Meeting() { UserOne = new User() };
            Assert.ThrowsException<ArgumentNullException>(() => { new Meeting() { UserOne = null }; }, "UserOne cannot be null");

            //Test user two
            new Meeting() { UserTwo = new User() };
            Assert.ThrowsException<ArgumentNullException>(() => { new Meeting() { UserTwo = null }; }, "UserTwo cannot be null");
        }

        [TestMethod()]
        public void MeetingContainsUserTest()
        {
            User userOne = new User() { Id = 1 };
            User userTwo = new User() { Id = 2 };
            User userThree = new User() { Id = 3 };

            Meeting meeting = new Meeting() { UserOneId = userOne.Id, UserTwoId  = userTwo.Id };
            Assert.IsTrue(meeting.MeetingContainsUser(userOne.Id));
            Assert.IsTrue(meeting.MeetingContainsUser(userTwo.Id));
            Assert.IsFalse(meeting.MeetingContainsUser(userThree.Id));
        }
    }
}