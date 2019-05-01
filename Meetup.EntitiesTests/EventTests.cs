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
    public class EventTests
    {
        [TestMethod()]
        public void EventTest()
        {
            //Test event name
            new Event() { Name = "My 1st Event" };
            Assert.ThrowsException<ArgumentException>(() => { new Event() { Name = "[MY EVENT] ~~Cool Event~~" }; }, "Invalid event name");

            //Test event description
            new Event() { Description = "My 783945uisdfsf{[]{£@$@$ event description" };
            Assert.ThrowsException<ArgumentException>(() => { new Event() { Name = "     " }; }, "Invalid event description");
        }

        [TestMethod()]
        public void GetUsersTest()
        {
            Event testEvent = new Event()
            {
                EventsUsers = new List<EventsUser>()
                {
                    new EventsUser() { User = new User() { FirstName = "Name" } },
                    new EventsUser() { User = new User() { FirstName = "Person" } }
                }
            };

            List<User> eventUsers = testEvent.GetUsers().ToList();
            Assert.AreEqual(2, eventUsers.Count, "The list doesnt contain all the users in the event");
            Assert.AreEqual("Person", eventUsers[1].FirstName, "The users arent correct");
        }
    }
}