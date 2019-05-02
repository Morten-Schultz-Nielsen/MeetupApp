namespace Meetup.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// An <see cref="object"/> connecting <see cref="Entities.Wish"/>es to <see cref="Entities.Business"/>es
    /// </summary>
    public partial class WishBusinesses
    {
        private Business business;
        private Wish wish;

        /// <summary>
        /// The id of this connection
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id
        {
            get; set;
        }

        /// <summary>
        /// The id of the <see cref="Entities.Wish"/> who wished the <see cref="Entities.Business"/>
        /// </summary>
        public int WishId
        {
            get; set;
        }

        /// <summary>
        /// The id of the <see cref="Entities.Business"/> the <see cref="Entities.Wish"/> wished for
        /// </summary>
        public int BusinessId
        {
            get; set;
        }

        /// <summary>
        /// The <see cref="Entities.Business"/> the <see cref="Entities.Wish"/> wished for
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
        /// The <see cref="Entities.Wish"/> who wished the <see cref="Entities.Business"/>
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
                    throw new ArgumentNullException(nameof(Wish), "value may not be null");
                }
                wish = value;
            }
        }

        /// <summary>
        /// Converts a list of <see cref="Business"/> objects into a list of <see cref="WishBusinesses"/> objects
        /// </summary>
        /// <param name="businesses">the list of <see cref="Business"/> objects</param>
        /// <param name="wishId">the wish id to insert into all the <see cref="WishBusinesses"/> objects</param>
        /// <returns>A list of <see cref="WishBusinesses"/> objects made from the parameters</returns>
        public static IEnumerable<WishBusinesses> Convert(IEnumerable<Business> businesses, int wishId = 0)
        {
            foreach(Business business in businesses)
            {
                yield return new WishBusinesses { Business = business, WishId = wishId };
            }
            yield break;
        }
    }
}
