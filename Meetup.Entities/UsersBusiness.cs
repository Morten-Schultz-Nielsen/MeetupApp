namespace Meetup.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// An <see cref="object"/> connecting <see cref="Entities.User"/>s to businesses
    /// </summary>
    public partial class UsersBusiness
    {
        private Business business;
        private User user;

        /// <summary>
        /// The id of this connection
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id
        {
            get; set;
        }

        /// <summary>
        /// The id of the <see cref="Entities.User"/> who has the <see cref="Entities.Business"/>
        /// </summary>
        public int UserId
        {
            get; set;
        }

        /// <summary>
        /// The id of the <see cref="Entities.Business"/> the <see cref="Entities.User"/> has
        /// </summary>
        public int BusinessId
        {
            get; set;
        }

        /// <summary>
        /// The <see cref="Entities.Business"/> the <see cref="Entities.User"/> has
        /// </summary>
        public virtual Business Business
        {
            get
            {
                return business;
            }
            set
            {
                if(value is null)
                {
                    throw new ArgumentNullException(nameof(Business), "value may not be null");
                }
                business = value;
            }
        }

        /// <summary>
        /// The <see cref="Entities.User"/> who has the <see cref="Entities.Business"/>
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
                    throw new ArgumentNullException(nameof(User), "value may not be null");
                }
                user = value;
            }
        }

        /// <summary>
        /// Converts a list of <see cref="Business"/> objects into a list of <see cref="UsersBusiness"/> objects
        /// </summary>
        /// <param name="businesses">the list of <see cref="Business"/> objects</param>
        /// <param name="userId">the user id to insert into all the <see cref="UsersBusiness"/> objects</param>
        /// <returns>A list of <see cref="UsersBusiness"/> objects made from the parameters</returns>
        public static IEnumerable<UsersBusiness> Convert(IEnumerable<Business> businesses, int userId = 0)
        {
            foreach(Business business in businesses)
            {
                yield return new UsersBusiness { Business = business, UserId = userId };
            }
            yield break;
        }
    }
}
