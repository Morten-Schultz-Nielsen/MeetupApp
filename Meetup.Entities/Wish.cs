﻿namespace Meetup.Entities
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
        private User wishUser;
        private Organization wishOrganization;
        private ICollection<WishBusinesses> wishBusinesses;
        private ICollection<WishInterests> wishInterests;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        protected Wish()
        {
            WishInterests = new HashSet<WishInterests>();
            WishBusinesses = new HashSet<WishBusinesses>();
        }

        /// <summary>
        /// Creates a new <see cref="Wish"/> object
        /// </summary>
        /// <param name="user">The user who has the wish</param>
        /// <param name="event">The event the wish is for</param>
        /// <param name="id">The wish's id</param>
        public Wish(User user, Event @event, int id) : this()
        {
            User = user;
            Event = @event;
            Id = id;
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
            get;
            private set;
        }

        /// <summary>
        /// The id of the <see cref="Entities.Event"/> this wish is for
        /// </summary>
        public int EventId
        {
            get;
            private set;
        }

        /// <summary>
        /// The id of the <see cref="Entities.User"/> the wish is wishing for
        /// </summary>
        public int? WishUserId
        {
            get;
            private set;
        }

        /// <summary>
        /// The id of the <see cref="Organization"/> the wish is wishing for
        /// </summary>
        public int? WishOrganizationId
        {
            get;
            private set;
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
                    UserId = 0;
                }
                else
                {
                    UserId = value.Id;
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
                    EventId = 0;
                }
                else
                {
                    EventId = value.Id;
                }
                @event = value;
            }
        }

        /// <summary>
        /// The <see cref="Entities.User"/> the wish is wishing for
        /// </summary>
        public virtual User WishUser
        {
            get
            {
                return wishUser;
            }
            set
            {
                WishUserId = value?.Id;
                wishUser = value;
            }
        }

        /// <summary>
        /// The <see cref="Organization"/> the wish is wishing for
        /// </summary>
        public virtual Organization WishOrganization
        {
            get
            {
                return wishOrganization;
            }
            set
            {
                WishOrganizationId = value?.Id;
                wishOrganization = value;
            }
        }

        /// <summary>
        /// A list over all connections for <see cref="Business"/>es this wish wishes for
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WishBusinesses> WishBusinesses
        {
            get
            {
                return wishBusinesses;
            }
            set
            {
                if(value is null)
                {
                    throw new ArgumentNullException(nameof(WishBusinesses), "value may not be null");
                }
                wishBusinesses = value;
            }
        }

        /// <summary>
        /// A list over all connections for <see cref="Interest"/>s this wish wishes for
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WishInterests> WishInterests
        {
            get
            {
                return wishInterests;
            }
            set
            {
                if(value is null)
                {
                    throw new ArgumentNullException(nameof(WishInterests), "value may not be null");
                }
                wishInterests = value;
            }
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
                string returnString = "Ønsker at snakke med en person som";
                List<string> parts = new List<string>();
                if(WishInterests.Count != 0)
                {
                    parts.Add(" har " + WishInterests.Count + (WishInterests.Count == 1 ? " interesse" : " interesser"));
                }
                if(WishBusinesses.Count != 0)
                {
                    parts.Add(" arbejder i " + WishBusinesses.Count + " erhverv");
                }

                if(WishOrganizationTime is null)
                {
                    if(!(WishOrganizationId is null))
                    {
                        parts.Add(" har arbejdet i organisationen \"" + WishOrganization.Name + "\"");
                    }
                }
                else
                {
                    if(!(WishOrganizationId is null))
                    {
                        parts.Add(" har arbejdet i organisationen \"" + WishOrganization.Name + "\"");
                    }
                    else
                    {
                        parts.Add(" har arbejdet i en organisation");
                    }
                    parts[parts.Count - 1] += " i " + WishOrganizationTime + " år";
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
                if(parts.Count > 1)
                {
                    returnString += " og" + parts[parts.Count - 1];
                }
                return returnString + ".";
            }
            else
            {
                //If the wish is for a specific user
                return "Ønsker at snakke med " + WishUser.FullName + ".";
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
