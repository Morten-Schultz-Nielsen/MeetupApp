namespace Meetup.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// An <see cref="object"/> connecting <see cref="Entities.User"/>s to <see cref="Entities.Event"/>s
    /// </summary>
    public partial class Invite
    {
        private Event @event;
        private User user;
        private DateTime time;

        /// <summary>
        /// The id of this connection
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id
        {
            get; set;
        }

        /// <summary>
        /// The id of the <see cref="Entities.User"/> invited to the <see cref="Entities.Event"/>
        /// </summary>
        public int UserId
        {
            get; set;
        }

        /// <summary>
        /// the id of the <see cref="Entities.Event"/> the <see cref="Entities.User"/> is invited to
        /// </summary>
        public int EventId
        {
            get; set;
        }

        /// <summary>
        /// the <see cref="Entities.Event"/> the <see cref="Entities.User"/> is invited to
        /// </summary>
        public virtual Event Event
        {
            get
            {
                return @event;
            }
            set
            {
                @event = value;
            }
        }

        /// <summary>
        /// The <see cref="Entities.User"/> invited to the <see cref="Entities.Event"/>
        /// </summary>
        public virtual User User
        {
            get
            {
                return user;
            }
            set
            {
                user = value;
            }
        }

        /// <summary>
        /// The time the invite was made
        /// </summary>
        public virtual DateTime Time
        {
            get
            {
                return time;
            }
            set
            {
                if(value > DateTime.Now)
                {
                    throw new ArgumentException("Invite cannot be from the future", nameof(Time));
                }
                time = value;
            }
        }

        /// <summary>
        /// A static method outputing which <see cref="Invite"/> time is highest
        /// </summary>
        /// <param name="invite1">the <see cref="Invite"/> to check against</param>
        /// <param name="invite2">the <see cref="Invite"/> to check with</param>
        /// <returns>-1 if <paramref name="invite1"/> is higher than <paramref name="invite2"/>. 0 if they are equel. 1 if <paramref name="invite1"/> is the smallest</returns>
        public static int Sort(Invite invite1, Invite invite2)
        {
            if(invite1 is null)
            {
                throw new ArgumentNullException(nameof(invite1), "Parameter may not be null");
            }
            if(invite2 is null)
            {
                throw new ArgumentNullException(nameof(invite2), "Parameter may not be null");
            }

            if(invite1.Time == invite2.time)
            {
                return 0;
            }
            if(invite1.Time > invite2.time)
            {
                return -1;
            }
            return 1;
        }
    }
}
