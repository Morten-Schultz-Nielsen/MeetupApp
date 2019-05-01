namespace Meetup.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// An <see cref="object"/> for an organization a <see cref="User"/> can have
    /// </summary>
    public partial class Organization
    {
        private string name;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Organization()
        {
            UsersOrganizations = new HashSet<UsersOrganizations>();
        }

        /// <summary>
        /// The organization's id
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id
        {
            get; set;
        }

        /// <summary>
        /// The name of this organization
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if(string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("value may not be null or empty", nameof(Name));
                }
                name = value;
            }
        }

        /// <summary>
        /// A list over all connections to <see cref="User"/>s with this organization
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UsersOrganizations> UsersOrganizations
        {
            get; set;
        }
    }
}
