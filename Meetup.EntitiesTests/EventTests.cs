﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
        /// <summary>
        /// Creates a simple <see cref="Event"/> object
        /// </summary>
        /// <param name="eventId">The id the event should have</param>
        /// <param name="ownerId">The id the event owner should have</param>
        /// <returns>A simple <see cref="Event"/></returns>
        public static Event GetSimpleEvent(int eventId = 0, int ownerId = 0)
        {
            return new Event("My 1st Event", "My 783945uisdfsf{[]{£@$@$ event description", UserTests.GetSimpleUser(ownerId), AddressTests.GetSimpleAddress(), eventId);
        }

        [TestMethod()]
        public void GetSimpleTest()
        {
            GetSimpleEvent();
        }

        [TestMethod()]
        public void EventTest()
        {
            Event testEvent = GetSimpleEvent();

            //Test event name
            Assert.ThrowsException<ArgumentException>(() => { testEvent.Name = "[My Event] ~Cool~"; }, "Invalid event name");
            Assert.ThrowsException<ArgumentException>(() => { testEvent.Name = string.Empty; }, "Event name cannot be empty");

            //Test event description
            Assert.ThrowsException<ArgumentException>(() => { testEvent.Description = string.Empty; }, "Invalid event description");

            //Test address
            Assert.ThrowsException<ArgumentNullException>(() => { testEvent.Address = null; }, "Address cannot be null");

            //Test event owner
            Assert.ThrowsException<ArgumentNullException>(() => { testEvent.User = null; }, "User cannot be null");

            //Test user list
            Assert.ThrowsException<ArgumentNullException>(() => { testEvent.Invites = null; }, "EventsUsers cannot be null");
        }

        [TestMethod()]
        public void GetUsersTest()
        {
            Event testEvent = GetSimpleEvent();
            testEvent.Invites = new List<Invite>()
            {
                new Invite(testEvent, UserTests.GetSimpleUser(), DateTime.Now),
                new Invite(testEvent, UserTests.GetSimpleUser(), DateTime.Now)
            };

            List<User> eventUsers = testEvent.GetUsers().ToList();
            Assert.AreEqual(2, eventUsers.Count, "The list doesnt contain all the users in the event");
            Assert.AreEqual("Bob", eventUsers[1].FirstName, "The users arent correct");
        }

        [TestMethod()]
        public void SortTest()
        {
            Event event1 = GetSimpleEvent();
            event1.BeginningTime = new DateTime(2001, 10, 13);

            Event event2 = GetSimpleEvent();
            event2.BeginningTime = new DateTime(2001, 10, 13);


            Event event3 = GetSimpleEvent();
            event3.BeginningTime = new DateTime(2003, 10, 13);

            Assert.AreEqual(0, Event.Sort(event1, event2));
            Assert.AreEqual(-1, Event.Sort(event1, event3));
            Assert.AreEqual(1, Event.Sort(event3, event1));
        }
    }
}