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
            //Test Time
            new Invite() { Time = new DateTime(2000, 10, 13) };
            Assert.ThrowsException<ArgumentException>(() => { new Invite() { Time = new DateTime(2300, 10, 13) }; }, "Time cannot be in the future");
        }

        [TestMethod()]
        public void SortTest()
        {
            Invite invite1 = new Invite() { Time = new DateTime(2001, 10, 13) };
            Invite invite2 = new Invite() { Time = new DateTime(2001, 10, 13) };
            Invite invite3 = new Invite() { Time = new DateTime(2003, 10, 13) };

            Assert.AreEqual(0, Invite.Sort(invite1, invite2));
            Assert.AreEqual(-1, Invite.Sort(invite1, invite3));
            Assert.AreEqual(1, Invite.Sort(invite3, invite1));
        }
    }
}