namespace Meetup.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// An <see cref="object"/> containing the information for a meeting with two <see cref="User"/>s
    /// </summary>
    public partial class Meeting
    {
        private Seance seance;
        private User userTwo;
        private User userOne;

        protected Meeting()
        {

        }

        public Meeting(User userOne, User userTwo)
        {
            UserOne = userOne;
            UserTwo = userTwo;
        }

        /// <summary>
        /// The id of this meeting
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id
        {
            get; set;
        }

        /// <summary>
        /// The id of the <see cref="Entities.Seance"/> this meeting is a part of
        /// </summary>
        public int SeanceId
        {
            get; set;
        }

        /// <summary>
        /// The id of one of the <see cref="User"/>s in this meeting
        /// </summary>
        public int UserOneId
        {
            get;
            private set;
        }

        /// <summary>
        /// The id of one of the <see cref="User"/>s in this meeting
        /// </summary>
        public int UserTwoId
        {
            get;
            private set;
        }

        /// <summary>
        /// The <see cref="Entities.Seance"/> this meeting is a part of
        /// </summary>
        public virtual Seance Seance
        {
            get
            {
                return seance;
            }
            set
            {
                seance = value;
            }
        }

        /// <summary>
        /// One of the <see cref="User"/>s in this meeting
        /// </summary>
        public virtual User UserOne
        {
            get
            {
                return userOne;
            }
            set
            {
                UserOneId = value.Id;
                userOne = value;
            }
        }

        /// <summary>
        /// One of the <see cref="User"/>s in this meeting
        /// </summary>
        public virtual User UserTwo
        {
            get
            {
                return userTwo;
            }
            set
            {
                UserTwoId = value.Id;
                userTwo = value;
            }
        }

        /// <summary>
        /// Checks if a user is in this meeting
        /// </summary>
        /// <param name="userId">the id of the user to check</param>
        /// <returns>True if the user is in this meeting</returns>
        public bool MeetingContainsUser(int userId)
        {
            return UserOneId == userId || UserTwoId == userId;
        }
    }
}
