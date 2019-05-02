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
    public class InviteTests
    {
        [TestMethod]
        public void EventsUserTest()
        {
            //Test Event
            new Invite() { Event = new Event() };
            Assert.ThrowsException<ArgumentNullException>(() => { new Invite() { Event = null }; }, "Event cannot be null");

            //Test User
            new Invite() { User = new User() };
            Assert.ThrowsException<ArgumentNullException>(() => { new Invite() { User = null }; }, "User cannot be null");

            //Test Time
            new Invite() { Time = new DateTime(2000, 10, 13) };
            Assert.ThrowsException<ArgumentException>(() => { new Invite() { Time = new DateTime(2300, 10, 13) }; }, "Time cannot be in the future");
        }
    }
}