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
    public class UserPauseTests
    {
        [TestMethod()]
        public void UserPauseTest()
        {
            new UserPause(UserTests.GetSimpleUser(), new Seance(EventTests.GetSimpleEvent(), 0, new DateTime(2000,10,10), new DateTime(2000,10,11)));
        }
    }
}