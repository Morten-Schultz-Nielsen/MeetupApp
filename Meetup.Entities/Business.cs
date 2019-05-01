using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using System.Text.RegularExpressions;

namespace Meetup.Entities
{
    /// <summary>
    /// An <see cref="object"/> for a business a <see cref="User"/> can have
    /// </summary>
    public partial class Business
    {
        /// <summary>
        /// Pattern to check if a business name is correct
        /// </summary>
        public const string BusinessPattern = @"^([a-øA-Ø0-9])+([a-øA-Ø0-9\s])*$";

        private string name;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Business()
        {
            UsersBusinesses = new HashSet<UsersBusiness>();
        }

        /// <summary>
        /// The business' id
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id
        {
            get; set;
        }

        /// <summary>
        /// What the business is called
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
                    throw new ArgumentException("Value may not be null or empty", nameof(Name));
                }
                value = value.Trim();
                if(!Regex.IsMatch(value, BusinessPattern))
                {
                    throw new ArgumentException($"Value is invalid and should match: {BusinessPattern}", nameof(Name));
                }

                name = value;
            }
        }

        /// <summary>
        /// A list over all connections to <see cref="User"/>s with this business
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UsersBusiness> UsersBusinesses
        {
            get; set;
        }

    }
}
