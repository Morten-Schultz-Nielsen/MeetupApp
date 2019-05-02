using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using Newtonsoft.Json;
using System.Net;
using System.Linq;

namespace Meetup.Entities
{
    /// <summary>
    /// An <see cref="object"/> for an organization a <see cref="User"/> can have
    /// </summary>
    public partial class Organization
    {
        private string name;
        private ICollection<UsersOrganizations> usersOrganizations;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Organization()
        {
            UsersOrganizations = new HashSet<UsersOrganizations>();
        }

        /// <summary>
        /// The organization's id
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id
        {
            get; set;
        }

        /// <summary>
        /// The name of this organization
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if(string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("value may not be null or empty", nameof(Name));
                }
                name = value;
            }
        }

        /// <summary>
        /// A list over all connections to <see cref="User"/>s with this organization
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UsersOrganizations> UsersOrganizations
        {
            get
            {
                return usersOrganizations;
            }
            set
            {
                if(value is null)
                {
                    throw new ArgumentNullException(nameof(UsersOrganizations), "value may not be null");
                }
                usersOrganizations = value;
            }
        }

        /// <summary>
        /// Checks if an organization name exists or not
        /// </summary>
        /// <param name="name">The name of the organization to check</param>
        /// <returns>true if the organization exists</returns>
        public static bool NameExists(string name)
        {
            if(string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("parameter may not be null or empty", nameof(name));
            }

            using(WebClient client = new WebClient())
            {
                string nameList = client.DownloadString("https://autocomplete.clearbit.com/v1/companies/suggest?query=" + name);
                OrganizationNamesFromJSON[] names = JsonConvert.DeserializeObject<OrganizationNamesFromJSON[]>(nameList);
                return names.Any(n => n.Name == name);
            }
        }

        /// <summary>
        /// Class used for deserializing json from api
        /// </summary>
        private class OrganizationNamesFromJSON
        {
            /// <summary>
            /// The name
            /// </summary>
            public string Name
            {
                get; set;
            }
        }
    }
}
