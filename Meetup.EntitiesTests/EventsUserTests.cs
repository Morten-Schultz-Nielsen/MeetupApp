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
    public class EventsUserTests
    {
        [TestMethod]
        public void EventsUserTest()
        {
            //Test Event
            new EventsUser() { Event = new Event() };
            Assert.ThrowsException<ArgumentNullException>(() => { new EventsUser() { Event = null }; }, "Event cannot be null");

            //Test User
            new EventsUser() { User = new User() };
            Assert.ThrowsException<ArgumentNullException>(() => { new EventsUser() { User = null }; }, "User cannot be null");
        }
    }
}