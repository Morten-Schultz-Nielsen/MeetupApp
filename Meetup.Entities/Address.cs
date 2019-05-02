using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using System.Text.RegularExpressions;

namespace Meetup.Entities
{
    /// <summary>
    /// An <see cref="object"/> containing the things in an address
    /// </summary>
    public partial class Address
    {
        /// <summary>
        /// Pattern for checking if a country name is correct
        /// </summary>
        public const string CountryPattern = @"^([a-zA-ZÊ¯Â∆ÿ≈])+$";

        /// <summary>
        /// Pattern to check if a street name is correct
        /// </summary>
        public const string StreetPattern = @"^([a-zA-ZÊ¯Â∆ÿ≈])+[\sa-zA-ZÊ¯Â∆ÿ≈]*$";

        /// <summary>
        /// Pattern to check if a city name is correct
        /// </summary>
        public const string CityPattern = @"^([a-zA-ZÊ¯Â∆ÿ≈])+$";

        /// <summary>
        /// Pattern to check if a street number is correct
        /// </summary>
        public const string StreetNumberPattern = @"^[0-9]+[0-9a-zA-ZÊ¯Â∆ÿ≈]*$";

        private string country;
        private string streetName;
        private string streetNumber;
        private string cityName;
        private int zipCode;
        private ICollection<Event> events;
        private ICollection<User> users;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Address()
        {
            Events = new HashSet<Event>();
            Users = new HashSet<User>();
        }

        /// <summary>
        /// The address' id
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id
        {
            get; set;
        }

        /// <summary>
        /// The country the address is specifying
        /// </summary>
        public string Country
        {
            get
            {
                return country;
            }
            set
            {
                if(string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Value may not be null or empty", nameof(Country));
                }
                value = value.Trim();
                if(!Regex.IsMatch(value, CountryPattern))
                {
                    throw new ArgumentException($"Name is invalid and should match: {CountryPattern}", nameof(Country));
                }
                country = value;
            }
        }

        /// <summary>
        /// The street name the address is specifying
        /// </summary>
        public string StreetName
        {
            get
            {
                return streetName;
            }
            set
            {
                if(string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Value may not be null or empty", nameof(StreetName));
                }
                value = value.Trim();
                if(!Regex.IsMatch(value, StreetPattern))
                {
                    throw new ArgumentException($"Name is invalid and should match: {StreetPattern}", nameof(StreetName));
                }
                streetName = value;
            }
        }

        /// <summary>
        /// The street number the address is specifying
        /// </summary>
        public string StreetNumber
        {
            get
            {
                return streetNumber;
            }
            set
            {
                if(string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Value may not be null or empty", nameof(StreetNumber));
                }
                value = value.Trim();
                if(!Regex.IsMatch(value, StreetNumberPattern))
                {
                    throw new ArgumentException($"Number is invalid and should match: {StreetNumberPattern}", nameof(StreetNumber));
                }
                streetNumber = value;
            }
        }

        /// <summary>
        /// The name of the city the address is specifying
        /// </summary>
        public string CityName
        {
            get
            {
                return cityName;
            }
            set
            {
                if(string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Value may not be null or empty", nameof(CityName));
                }
                value = value.Trim();
                if(!Regex.IsMatch(value, CityPattern))
                {
                    throw new ArgumentException($"Name is invalid and should match: {CityPattern}", nameof(CityName));
                }
                cityName = value;
            }
        }

        /// <summary>
        /// The zipcode of the city the address is specifying
        /// </summary>
        public int ZipCode
        {
            get
            {
                return zipCode;
            }
            set
            {
                if(value < 1000 || value > 9999)
                {
                    throw new ArgumentException("Number has to be between 1000 and 9999", nameof(ZipCode));
                }
                zipCode = value;
            }
        }

        /// <summary>
        /// A list of <see cref="Event"/>s using this address
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Event> Events
        {
            get
            {
                return events;
            }
            set
            {
                if(value is null)
                {
                    throw new ArgumentNullException(nameof(Events), "value may not be null");
                }
                events = value;
            }
        }

        /// <summary>
        /// A list of <see cref="User"/>s using this address
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<User> Users
        {
            get
            {
                return users;
            }
            set
            {
                if(value is null)
                {
                    throw new ArgumentNullException(nameof(Users), "value may not be null");
                }
                users = value;
            }
        }

        /// <summary>
        /// Returns the address formated as a string
        /// </summary>
        /// <returns>a string containing the address</returns>
        public override string ToString()
        {
            return StreetName + " " + StreetNumber + " " + CityName + " " + ZipCode + " " + Country;
        }
    }
}
