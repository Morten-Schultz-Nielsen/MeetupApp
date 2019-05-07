namespace Meetup.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// An <see cref="object"/> connecting <see cref="Entities.Wish"/>es to <see cref="Entities.Interest"/>s
    /// </summary>
    public partial class WishInterests
    {
        private Interest interest;
        private Wish wish;

        protected WishInterests()
        {

        }

        /// <summary>
        /// Creates a new <see cref="WishInterests"/> object
        /// </summary>
        /// <param name="interest">the interest in the link</param>
        /// <param name="wish">the wish in the link</param>
        public WishInterests(Interest interest, Wish wish)
        {
            Interest = interest;
            Wish = wish;
        }

        /// <summary>
        /// The id of this connection
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id
        {
            get; set;
        }

        /// <summary>
        /// The id of the <see cref="Entities.Wish"/> who wished the <see cref="Entities.Interest"/>
        /// </summary>
        public int WishId
        {
            get;
            private set;
        }

        /// <summary>
        /// The id of the <see cref="Entities.Interest"/> the <see cref="Entities.Wish"/> wished for
        /// </summary>
        public int InterestId
        {
            get;
            private set;
        }

        /// <summary>
        /// The <see cref="Entities.Interest"/> the <see cref="Entities.Wish"/> wished for
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
        /// The <see cref="Entities.Wish"/> who wished the <see cref="Entities.Interest"/>
        /// </summary>
        public virtual Wish Wish
        {
            get
            {
                return wish;
            }
            set
            {
                if(value is null)
                {
                    WishId = 0;
                }
                else
                {
                    WishId = value.Id;
                }
                wish = value;
            }
        }

        /// <summary>
        /// Converts a list of <see cref="Interest"/> objects into a list of <see cref="WishInterests"/> objects
        /// </summary>
        /// <param name="interests">the list of <see cref="Interest"/> objects</param>
        /// <param name="wishId">the wish id to insert into all the <see cref="WishInterests"/> objects</param>
        /// <returns>A list of <see cref="WishInterests"/> objects made from the parameters</returns>
        public static IEnumerable<WishInterests> Convert(IEnumerable<Interest> interests, int wishId = 0)
        {
            foreach(Interest interest in interests)
            {
                yield return new WishInterests { Interest = interest, WishId = wishId };
            }
            yield break;
        }
    }
}
