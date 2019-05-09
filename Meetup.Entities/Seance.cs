namespace Meetup.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// An <see cref="object"/> containing the information for a seance
    /// </summary>
    public partial class Seance
    {
        private DateTime beginningTime;
        private DateTime endTime;
        private Event @event;
        private ICollection<Meeting> meetings;
        private ICollection<UserPause> userPauses;

        protected Seance()
        {
            Meetings = new HashSet<Meeting>();
            UserPauses = new HashSet<UserPause>();
        }

        /// <summary>
        /// Creates a new <see cref="Seance"/> object
        /// </summary>
        /// <param name="event">The <see cref="Event"/> the seance is for</param>
        /// <param name="number">The number this seance has (used for sorting)</param>
        /// <param name="beginningTime">The time the seance begins</param>
        /// <param name="endTime">The time the seance ends</param>
        public Seance(Event @event, int number ,DateTime beginningTime, DateTime endTime) : this()
        {
            Event = @event;
            BeginningTime = beginningTime;
            EndTime = endTime;
            MeetingNumber = number;
        }

        /// <summary>
        /// The id of this seance
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id
        {
            get; set;
        }

        /// <summary>
        /// The id of the <see cref="Entities.Event"/> this seance is for
        /// </summary>
        public int EventId
        {
            get;
            private set;
        }

        /// <summary>
        /// A number specifiyng which number seance this is for the event
        /// </summary>
        public int MeetingNumber
        {
            get; set;
        }

        /// <summary>
        /// The time this seance is beginning
        /// </summary>
        public DateTime BeginningTime
        {
            get
            {
                return beginningTime;
            }
            set
            {
                beginningTime = value;
            }
        }

        /// <summary>
        /// The time this seance is ending
        /// </summary>
        public DateTime EndTime
        {
            get
            {
                return endTime;
            }
            set
            {
                endTime = value;
            }
        }

        /// <summary>
        /// The <see cref="Entities.Event"/> this seance is for
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
        /// A list of all the <see cref="Meeting"/>s in this seance
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Meeting> Meetings
        {
            get
            {
                return meetings;
            }
            set
            {
                if(value is null)
                {
                    throw new ArgumentNullException(nameof(Meetings), "value may not be null");
                }
                meetings = value;
            }
        }

        /// <summary>
        /// A list of users who arent in this seance
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserPause> UserPauses
        {
            get
            {
                return userPauses;
            }
            set
            {
                if(value is null)
                {
                    throw new ArgumentNullException(nameof(UserPauses), "value may not be null");
                }
                userPauses = value;
            }
        }
    }
}
