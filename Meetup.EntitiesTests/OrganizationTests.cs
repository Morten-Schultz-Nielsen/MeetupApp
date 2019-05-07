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
    public class OrganizationTests
    {
        public static Organization GetSimpleOrganization(int id = 0)
        {
            return new Organization("My Organization") { Id = 0 };
        }

        [TestMethod()]
        public void OrganizationTest()
        {
            GetSimpleOrganization();

            //Test name
            Assert.ThrowsException<ArgumentException>(() => { new Organization(null); }, "Name cannot be empty");

            //Test if list can be null
            Assert.ThrowsException<ArgumentNullException>(() => { new Organization("Bla") { UsersOrganizations = null }; }, "UsersOrganizations cannot be null");
        }

        [TestMethod()]
        public void NameExistsTest()
        {
            //Test API
            Assert.IsTrue(Organization.NameExists("LEGO Group"));
            Assert.IsFalse(Organization.NameExists("hsjdkfhsjdkfdshjkfhsk"));

            //Test API null parameter
            Assert.ThrowsException<ArgumentException>(() => { Organization.NameExists(null); });
        }
    }
}