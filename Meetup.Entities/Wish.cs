namespace Meetup.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    /// <summary>
    /// An <see cref="object"/> containing the information for a wish a <see cref="Entities.User"/> can have
    /// </summary>
    public partial class Wish
    {
        private User user;
        private Event @event;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Wish()
        {
            WishInterests = new HashSet<WishInterests>();
            WishBusinesses = new HashSet<WishBusinesses>();
        }

        /// <summary>
        /// The wish's id
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id
        {
            get; set;
        }

        /// <summary>
        /// The id of the <see cref="Entities.User"/> who has the wish
        /// </summary>
        public int UserId
        {
            get; set;
        }

        /// <summary>
        /// The id of the <see cref="Entities.Event"/> this wish is for
        /// </summary>
        public int EventId
        {
            get; set;
        }

        /// <summary>
        /// The id of the <see cref="Entities.User"/> the wish is wishing for
        /// </summary>
        public int? WishUserId
        {
            get; set;
        }

        /// <summary>
        /// The id of the <see cref="Organization"/> the wish is wishing for
        /// </summary>
        public int? WishOrganizationId
        {
            get; set;
        }

        /// <summary>
        /// The amount of years of knowlegde the wish is wishing for
        /// </summary>
        public int? WishOrganizationTime
        {
            get; set;
        }

        /// <summary>
        /// The <see cref="Entities.User"/> wishing
        /// </summary>
        public virtual User User
        {
            get
            {
                return user;
            }
            set
            {
                if(value is null)
                {
                    throw new ArgumentNullException("Value may not be null", nameof(User));
                }
                user = value;
            }
        }

        /// <summary>
        /// The <see cref="Entities.Event"/> this wish is for
        /// </summary>
        public virtual Event Event
        {
            get
            {
                return @event;
            }
            set
            {
                if(value is null)
                {
                    throw new ArgumentNullException("Value may not be null", nameof(Event));
                }
                @event = value;
            }
        }

        /// <summary>
        /// The <see cref="Entities.User"/> the wish is wishing for
        /// </summary>
        public virtual User WishUser
        {
            get; set;
        }

        /// <summary>
        /// The <see cref="Organization"/> the wish is wishing for
        /// </summary>
        public virtual Organization WishOrganization
        {
            get; set;
        }

        /// <summary>
        /// A list over all connections for <see cref="Business"/>es this wish wishes for
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WishBusinesses> WishBusinesses
        {
            get; set;
        }

        /// <summary>
        /// A list over all connections for <see cref="Interest"/>s this wish wishes for
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WishInterests> WishInterests
        {
            get; set;
        }

        /// <summary>
        /// Creates a string explaining what the wish is
        /// </summary>
        /// <returns>A string with information about the wish</returns>
        public override string ToString()
        {
            if(WishUserId is null)
            {
                //If the wish isnt for a specific user
                string returnString = "Wishing to talk with a person who";
                List<string> parts = new List<string>();
                if(WishInterests.Count != 0)
                {
                    parts.Add(" has " + WishInterests.Count + (WishInterests.Count == 1 ? " interest" : " interests"));
                }
                if(WishBusinesses.Count != 0)
                {
                    parts.Add(" works in " + WishBusinesses.Count + (WishBusinesses.Count == 1 ? " business" : " businesses"));
                }

                if(WishOrganizationTime is null)
                {
                    if(!(WishOrganizationId is null))
                    {
                        parts.Add(" worked in the organization " + WishOrganization.Name);
                    }
                }
                else
                {
                    if(!(WishOrganizationId is null))
                    {
                        parts.Add(" worked in the organization \"" + WishOrganization.Name + "\"");
                    }
                    else
                    {
                        parts.Add(" worked in an organization");
                    }
                    parts[parts.Count - 1] += " for " + WishOrganizationTime + (WishOrganizationTime == 1 ? " year" : " years");
                }

                //add wish parts together into one single string
                if(parts.Count != 0)
                {
                    returnString += parts[0];
                }
                for(int i = 1; i < parts.Count - 1; i++)
                {
                    returnString += "," + parts[i];
                }
                if(parts.Count != 0)
                {
                    returnString += " and" + parts[parts.Count - 1];
                }
                return returnString + ".";
            }
            else
            {
                //If the wish is for a specific user
                return "Wishing to talk with " + WishUser.FullName;
            }
        }

        /// <summary>
        /// Returns a list of the interests this wish is wishing for
        /// </summary>
        /// <returns>A list of interests</returns>
        public IEnumerable<Interest> GetInterests()
        {
            return from WishInterests interests in WishInterests select interests.Interest;
        }

        /// <summary>
        /// Returns a list of the interests this wish is wishing for
        /// </summary>
        /// <returns>A list of interests</returns>
        public IEnumerable<Business> GetBusinesses()
        {
            return from WishBusinesses business in WishBusinesses select business.Business;
        }
    }
}
