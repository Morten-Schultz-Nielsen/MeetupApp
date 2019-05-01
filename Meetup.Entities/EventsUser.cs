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
    public partial class EventsUser
    {
        /// <summary>
        /// The id of this connection
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// The id of the <see cref="Entities.User"/> invited to the <see cref="Entities.Event"/>
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// the id of the <see cref="Entities.Event"/> the <see cref="Entities.User"/> is invited to
        /// </summary>
        public int EventId { get; set; }

        /// <summary>
        /// the <see cref="Entities.Event"/> the <see cref="Entities.User"/> is invited to
        /// </summary>
        public virtual Event Event { get; set; }

        /// <summary>
        /// The <see cref="Entities.User"/> invited to the <see cref="Entities.Event"/>
        /// </summary>
        public virtual User User { get; set; }
    }
}
