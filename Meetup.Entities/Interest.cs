using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using System.Text.RegularExpressions;

namespace Meetup.Entities
{
    /// <summary>
    /// An <see cref="object"/> for an interest a <see cref="User"/> can have
    /// </summary>
    public partial class Interest
    {
        /// <summary>
        /// Pattern to check is a interest is valid
        /// </summary>
        public const string InterestPattern = @"^([\S])+([\S\s])*$";
        private string name;
        private ICollection<UsersInterest> usersInterests;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Interest()
        {
            UsersInterests = new HashSet<UsersInterest>();
        }

        /// <summary>
        /// the interest's id
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id
        {
            get; set;
        }

        /// <summary>
        /// The name of the interest
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
                if(value.Length > 30)
                {
                    throw new ArgumentException("Value may not be longer than 30 letters", nameof(Name));
                }
                if(!Regex.IsMatch(value, InterestPattern))
                {
                    throw new ArgumentException($"Value is invalid and should match: {InterestPattern}", nameof(Name));
                }

                name = value;
            }
        }

        /// <summary>
        /// A list over all connections to <see cref="User"/>s with this interest
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UsersInterest> UsersInterests
        {
            get
            {
                return usersInterests;
            }
            set
            {
                if(value is null)
                {
                    throw new ArgumentNullException(nameof(UsersInterests), "value may not be null");
                }
                usersInterests = value;
            }
        }
    }
}
