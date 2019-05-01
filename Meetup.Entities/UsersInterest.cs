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
        /// <summary>
        /// The interest's id
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// The id of the <see cref="Entities.User"/> who has the <see cref="Entities.Interest"/>
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// The id of the <see cref="Entities.Interest"/> the <see cref="Entities.User"/> has
        /// </summary>
        public int InterestId { get; set; }

        /// <summary>
        /// The <see cref="Entities.Interest"/> the <see cref="Entities.User"/> has
        /// </summary>
        public virtual Interest Interest { get; set; }

        /// <summary>
        /// The <see cref="Entities.User"/> who has the <see cref="Entities.Interest"/>
        /// </summary>
        public virtual User User { get; set; }

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
