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
            Assert.ThrowsException<ArgumentNullException>(() => { new Seance() { Meetings = null }; }, "Meetings cannot be null");
        }
    }
}