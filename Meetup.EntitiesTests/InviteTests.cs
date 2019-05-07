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
        /// <summary>
        /// Creates an simple <see cref="Invite"/> object
        /// </summary>
        /// <param name="eventId">the id of the event the invite is for</param>
        /// <param name="userId">the id of the user the invite is to</param>
        /// <returns>An simple <see cref="Invite"/></returns>
        public static Invite GetSimpleInvite(int eventId = 0, int userId = 0)
        {
            return new Invite(EventTests.GetSimpleEvent(eventId), UserTests.GetSimpleUser(userId), DateTime.Now);
        }

        [TestMethod]
        public void EventsUserTest()
        {
            //Test Time
            Invite invite = GetSimpleInvite();
            Assert.ThrowsException<ArgumentException>(() => { invite.Time = new DateTime(2300, 10, 13); }, "Time cannot be in the future");
        }

        [TestMethod()]
        public void SortTest()
        {
            Invite invite1 = GetSimpleInvite();
            invite1.Time = new DateTime(2001, 10, 13);

            Invite invite2 = GetSimpleInvite();
            invite2.Time = new DateTime(2001, 10, 13);

            Invite invite3 = GetSimpleInvite();
            invite3.Time = new DateTime(2003, 10, 13);

            Assert.AreEqual(0, Invite.Sort(invite1, invite2));
            Assert.AreEqual(1, Invite.Sort(invite1, invite3));
            Assert.AreEqual(-1, Invite.Sort(invite3, invite1));
        }
    }
}