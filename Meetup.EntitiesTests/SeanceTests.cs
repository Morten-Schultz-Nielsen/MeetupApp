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
    public class SeanceTests
    {
        [TestMethod()]
        public void SeanceTest()
        {
            Seance seance = new Seance(EventTests.GetSimpleEvent(),1, new DateTime(2000, 10, 10), new DateTime(2000, 10, 11));
            Assert.ThrowsException<ArgumentNullException>(() => { seance.Meetings = null; }, "Meetings cannot be null");
            Assert.ThrowsException<ArgumentNullException>(() => { seance.UserPauses = null; }, "UserPauses cannot be null");
        }
    }
}