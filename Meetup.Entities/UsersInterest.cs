namespace Meetup.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// An <see cref="object"/> connecting <see cref="Entities.User"/>s to <see cref="Entities.Interest"/>s
    /// </summary>
    public partial class UsersInterest
    {
        private Interest interest;
        private User user;

        protected UsersInterest()
        {

        }

        /// <summary>
        /// Creates a new <see cref="UsersInterest"/> object
        /// </summary>
        /// <param name="interest">The interest in the link</param>
        /// <param name="user">The user in the link</param>
        public UsersInterest(Interest interest, User user)
        {
            Interest = interest;
            User = user;
        }

        /// <summary>
        /// The interest's id
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id
        {
            get; set;
        }

        /// <summary>
        /// The id of the <see cref="Entities.User"/> who has the <see cref="Entities.Interest"/>
        /// </summary>
        public int UserId
        {
            get;
            private set;
        }

        /// <summary>
        /// The id of the <see cref="Entities.Interest"/> the <see cref="Entities.User"/> has
        /// </summary>
        public int InterestId
        {
            get;
            private set;
        }

        /// <summary>
        /// The <see cref="Entities.Interest"/> the <see cref="Entities.User"/> has
        /// </summary>
        public virtual Interest Interest
        {
            get
            {
                return interest;
            }
            set
            {
                if(value is null)
                {
                    InterestId = 0;
                }
                else
                {
                    InterestId = value.Id;
                }
                interest = value;
            }
        }

        /// <summary>
        /// The <see cref="Entities.User"/> who has the <see cref="Entities.Interest"/>
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
        /// Converts a list of <see cref="Interest"/> objects into a list of <see cref="UsersInterest"/> objects
        /// </summary>
        /// <param name="interests">the list of <see cref="Interest"/> objects</param>
        /// <param name="userId">the user id to insert into all the <see cref="UsersInterest"/> objects</param>
        /// <returns>A list of <see cref="UsersInterest"/> objects made from the parameters</returns>
        public static IEnumerable<UsersInterest> Convert(IEnumerable<Interest> interests, int userId = 0)
        {
            foreach(Interest interest in interests)
            {
                yield return new UsersInterest { Interest = interest, UserId = userId };
            }
            yield break;
        }
    }
}
