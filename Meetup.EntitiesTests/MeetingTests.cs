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
        public void MeetingContainsUserTest()
        {
            User userOne = UserTests.GetSimpleUser(1);
            User userTwo = UserTests.GetSimpleUser(2);
            User userThree = UserTests.GetSimpleUser(3);

            Meeting meeting = new Meeting(userOne, userTwo, new Seance(EventTests.GetSimpleEvent(),1, new DateTime(2000,10,10), new DateTime(2000, 10, 11)));
            Assert.IsTrue(meeting.MeetingContainsUser(userOne.Id));
            Assert.IsTrue(meeting.MeetingContainsUser(userTwo.Id));
            Assert.IsFalse(meeting.MeetingContainsUser(userThree.Id));
        }
    }
}