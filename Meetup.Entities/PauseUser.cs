namespace Meetup.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// An <see cref="object"/> telling what <see cref="Entities.User"/> is not in a meeting in the given <see cref="Entities.Seance"/>
    /// </summary>
    public partial class UserPause
    {
        private Seance seance;
        private User user;

        protected UserPause()
        {

        }

        /// <summary>
        /// Creates a new <see cref="UserPause"/> object
        /// </summary>
        /// <param name="user">The <see cref="Entities.User"/> who isnt in the <see cref="Entities.Seance"/></param>
        /// <param name="seance">The <see cref="Entities.Seance"/> the <see cref="Entities.User"/> isnt in</param>
        public UserPause(User user, Seance seance)
        {
            User = user;
            Seance = seance;
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
        /// The id of the <see cref="Entities.Seance"/> the <see cref="Entities.User"/> isnt in
        /// </summary>
        public int SeanceId
        {
            get;
            private set;
        }

        /// <summary>
        /// The id of the <see cref="Entities.User"/> who isnt in the <see cref="Entities.Seance"/>
        /// </summary>
        public int UserId
        {
            get;
            private set;
        }

        /// <summary>
        /// The <see cref="Entities.Seance"/> the <see cref="Entities.User"/> isnt in
        /// </summary>
        public virtual Seance Seance
        {
            get
            {
                return seance;
            }
            set
            {
                if(value is null)
                {
                    SeanceId = 0;
                }
                else
                {
                    SeanceId = value.Id;
                }
                seance = value;
            }
        }

        /// <summary>
        /// The <see cref="Entities.User"/> who isnt in the <see cref="Entities.Seance"/>
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
    }
}
