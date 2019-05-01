using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Text.RegularExpressions;

namespace Meetup.Entities
{
    /// <summary>
    /// An <see cref="object"/> containing the things for an event
    /// </summary>
    public partial class Event
    {
        /// <summary>
        /// Pattern to check if a event name is valid
        /// </summary>
        public const string NamePattern = @"^[\w\s]*$";

        /// <summary>
        /// Pattern to check if an description is valid
        /// </summary>
        public const string DescriptionPattern = @"^[\S]+[\s\S]*$";

        private string name;
        private string description;
        private Address address;
        private User user;
        private ICollection<EventsUser> eventsUsers;
        private ICollection<Seance> seances;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Event()
        {
            EventsUsers = new HashSet<EventsUser>();
            Seances = new HashSet<Seance>();
        }

        /// <summary>
        /// The event's id
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id
        {
            get; set;
        }

        /// <summary>
        /// The id of the <see cref="Entities.User"/> who hosts this event
        /// </summary>
        public int HostUserId
        {
            get; set;
        }

        /// <summary>
        /// The id of the <see cref="Entities.Address"/> this event will be at
        /// </summary>
        public int AddressId
        {
            get; set;
        }

        /// <summary>
        /// The time this event begins
        /// </summary>
        public DateTime BeginningTime
        {
            get; set;
        }

        /// <summary>
        /// The name of this event
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
                    throw new ArgumentException("Value may not be null or empty", nameof(Name));
                }
                value = value.Trim();
                if(!Regex.IsMatch(value, NamePattern))
                {
                    throw new ArgumentException($"Value is invalid and should match: {NamePattern}", nameof(Name));
                }
                name = value;
            }
        }

        /// <summary>
        /// A description of this event
        /// </summary>
        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                if(string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Value may not be null or empty", nameof(Description));
                }
                value = value.Trim();
                if(!Regex.IsMatch(value, DescriptionPattern))
                {
                    throw new ArgumentException($"Value is invalid and should match: {DescriptionPattern}", nameof(Description));
                }
                description = value;
            }
        }

        /// <summary>
        /// The <see cref="Entities.Address"/> this event will be at
        /// </summary>
        public virtual Address Address
        {
            get
            {
                return address;
            }
            set
            {
                if(value is null)
                {
                    throw new ArgumentNullException("value may not be null", nameof(Address));
                }
                address = value;
            }
        }

        /// <summary>
        /// The <see cref="Entities.User"/> hosting this event
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
                    throw new ArgumentNullException("value may not be null", nameof(User));
                }
                user = value;
            }
        }

        /// <summary>
        /// A list over all connections to <see cref="Entities.User"/>s who are invited to this event
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EventsUser> EventsUsers
        {
            get
            {
                return eventsUsers;
            }
            set
            {
                if(value is null)
                {
                    throw new ArgumentNullException("Value cannot be null");
                }
                eventsUsers = value;
            }
        }

        /// <summary>
        /// A list over all connections to <see cref="Seance"/>s for this event
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Seance> Seances
        {
            get
            {
                return seances;
            }
            set
            {
                if(value is null)
                {
                    throw new ArgumentNullException("Value cannot be null");
                }
                seances = value;
            }
        }

        /// <summary>
        /// Returns a list of all users invited to this event
        /// </summary>
        /// <returns>A list of all users in this event</returns>
        public IEnumerable<User> GetUsers()
        {
            return from EventsUser invite in EventsUsers select invite.User;
        }
    }
}
