using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Text.RegularExpressions;

namespace Meetup.Entities
{
    /// <summary>
    /// An <see cref="object"/> containing the basic information about a user.
    /// </summary>
    public partial class User
    {
        /// <summary>
        /// A pattern to check if a name is valid
        /// </summary>
        public const string NamePattern = @"^([a-zA-ZÊ¯Â∆ÿ≈])+([a-zA-ZÊ¯Â∆ÿ≈\s])*$";

        /// <summary>
        /// A pattern to check if a description is valid
        /// </summary>
        public const string DescriptionPattern = @"^[\S]+[\s\S]*$";

        /// <summary>
        /// A pattern to check if the picture uri is correct
        /// </summary>
        public const string PictureUriPattern = @"^(data:image/png;base64,)[\S]*$";

        /// <summary>
        /// A pattern to check is an email is valid
        /// </summary>
        public const string EmailPattern = @"^[\w\-]+@([\w\-]+[.]{0,1})+.[\w]+$";

        private Address address;
        private string firstName;
        private string lastName;
        private string description;
        private string pictureUri;
        private string email;
        private ICollection<Event> events;
        private ICollection<Invite> invite;
        private ICollection<Meeting> meetings;
        private ICollection<Meeting> meetings1;
        private ICollection<UsersBusiness> usersBusinesses;
        private ICollection<UsersInterest> usersInterests;
        private ICollection<UsersOrganizations> usersOrganizations;
        private ICollection<Wish> wishes;
        private ICollection<UserPause> userPauses;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        protected User()
        {
            Events = new HashSet<Event>();
            Invites = new HashSet<Invite>();
            Meetings = new HashSet<Meeting>();
            Meetings1 = new HashSet<Meeting>();
            UsersBusinesses = new HashSet<UsersBusiness>();
            UsersInterests = new HashSet<UsersInterest>();
            UsersOrganizations = new HashSet<UsersOrganizations>();
            Wishes = new HashSet<Wish>();
            UserPauses = new HashSet<UserPause>();
        }

        /// <summary>
        /// Creates a new <see cref="User"/> object
        /// </summary>
        /// <param name="firstName">The user's first name</param>
        /// <param name="lastName">The user's last name</param>
        /// <param name="description">A description about the user</param>
        /// <param name="picture">A picture of the user</param>
        /// <param name="email">The user's email</param>
        /// <param name="address">The user's address</param>
        /// <param name="id">The user's id</param>
        public User(string firstName, string lastName, string description, string picture, string email, Address address, int id) : this()
        {
            FirstName = firstName;
            LastName = lastName;
            Description = description;
            PictureUri = picture;
            Email = email;
            Address = address;
            Id = id;
        }

        /// <summary>
        /// The user's id
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id
        {
            get; set;
        }

        /// <summary>
        /// The user's first name
        /// </summary>
        public string FirstName
        {
            get
            {
                return firstName;
            }
            set
            {
                if(string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Value may not be null or empty", nameof(FirstName));
                }
                value = value.Trim();
                if(value.Length > 30)
                {
                    throw new ArgumentException("Value may not be longer than 30 letters", nameof(FirstName));
                }
                if(!Regex.IsMatch(value, NamePattern))
                {
                    throw new ArgumentException($"Value is invalid and should match: {NamePattern}", nameof(FirstName));
                }

                firstName = value;
            }
        }

        /// <summary>
        /// The user's last name
        /// </summary>
        public string LastName
        {
            get
            {
                return lastName;
            }
            set
            {
                if(string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Value may not be null or empty", nameof(LastName));
                }
                value = value.Trim();
                if(value.Length > 30)
                {
                    throw new ArgumentException("Value may not be longer than 30 letters", nameof(LastName));
                }
                if(!Regex.IsMatch(value, NamePattern))
                {
                    throw new ArgumentException($"Value is invalid and should match: {NamePattern}", nameof(LastName));
                }

                lastName = value;
            }
        }

        /// <summary>
        /// A description of the user
        /// </summary>
        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                if(string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Value may not be null or empty", nameof(Description));
                }
                value = value.Trim();
                if(!Regex.IsMatch(value, DescriptionPattern))
                {
                    throw new ArgumentException($"Value is invalid and should match: {DescriptionPattern}", nameof(Description));
                }

                description = value;
            }
        }

        /// <summary>
        /// The id of the user's <see cref="Entities.Address"/>
        /// </summary>
        public int AddressId
        {
            get; set;
        }

        /// <summary>
        /// The user's picture in format of a link
        /// </summary>
        public string PictureUri
        {
            get
            {
                return pictureUri;
            }
            set
            {
                if(string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Value may not be null or empty", nameof(PictureUri));
                }
                value = value.Trim();
                if(!Regex.IsMatch(value, PictureUriPattern))
                {
                    throw new ArgumentException($"Value is invalid and should match: {PictureUriPattern}", nameof(PictureUri));
                }
                pictureUri = value;
            }
        }

        /// <summary>
        /// The user's email
        /// </summary>
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                if(string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Value may not be null or empty", nameof(Email));
                }
                value = value.Trim();
                if(value.Length > 40)
                {
                    throw new ArgumentException("Value may not be longer than 40 letters", nameof(Email));
                }
                if(!Regex.IsMatch(value, EmailPattern))
                {
                    throw new ArgumentException($"Value is invalid and should match: {EmailPattern}", nameof(Email));
                }

                email = value;
            }
        }

        /// <summary>
        /// The user's <see cref="Address"/>
        /// </summary>
        public virtual Address Address
        {
            get
            {
                return address;
            }
            set
            {
                if(value is null)
                {
                    throw new ArgumentNullException(nameof(Address), "value may not be null");
                }
                address = value;
            }
        }

        /// <summary>
        /// A list over all <see cref="Event"/>s this user is hosting
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
        /// A list over all the connections for the <see cref="Event"/>s this user is invited to
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Invite> Invites
        {
            get
            {
                return invite;
            }
            set
            {
                if(value is null)
                {
                    throw new ArgumentNullException(nameof(Invites), "value may not be null");
                }
                invite = value;
            }
        }

        /// <summary>
        /// A list over all <see cref="Meeting"/>s this user is the first user in
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Meeting> Meetings
        {
            get
            {
                return meetings;
            }
            set
            {
                if(value is null)
                {
                    throw new ArgumentNullException(nameof(Meetings), "value may not be null");
                }
                meetings = value;
            }
        }

        /// <summary>
        /// A list over all <see cref="Meeting"/>s this user is the second user in
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Meeting> Meetings1
        {
            get
            {
                return meetings1;
            }
            set
            {
                if(value is null)
                {
                    throw new ArgumentNullException(nameof(Meetings1), "value may not be null");
                }
                meetings1 = value;
            }
        }

        /// <summary>
        /// A list over all connections for <see cref="Business"/>es this user has
        /// </summary>

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UsersBusiness> UsersBusinesses
        {
            get
            {
                return usersBusinesses;
            }
            set
            {
                if(value is null)
                {
                    throw new ArgumentNullException(nameof(UsersBusinesses), "value may not be null");
                }
                usersBusinesses = value;
            }
        }

        /// <summary>
        /// A list over all connections for <see cref="Interest"/>s this user has
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

        /// <summary>
        /// A list over all the <see cref="Organization"/>s this user has been in
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UsersOrganizations> UsersOrganizations
        {
            get
            {
                return usersOrganizations;
            }
            set
            {
                if(value is null)
                {
                    throw new ArgumentNullException(nameof(UsersOrganizations), "value may not be null");
                }
                usersOrganizations = value;
            }
        }

        /// <summary>
        /// A list of all the <see cref="Wish"/>es this user has
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Wish> Wishes
        {
            get
            {
                return wishes;
            }
            set
            {
                if(value is null)
                {
                    throw new ArgumentNullException(nameof(Wishes), "value may not be null");
                }
                wishes = value;
            }
        }

        /// <summary>
        /// A list over all seance pauses this user has
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserPause> UserPauses
        {
            get
            {
                return userPauses;
            }
            set
            {
                if(value is null)
                {
                    throw new ArgumentNullException(nameof(UserPauses), "value may not be null");
                }
                userPauses = value;
            }
        }

        /// <summary>
        /// The user's full name
        /// </summary>
        [NotMapped]
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        /// <summary>
        /// Calculates how high a happiness score the wishes gives
        /// </summary>
        /// <param name="wishes">A list of wishes used to calculate the score</param>
        /// <returns>A number showing how well the wishes fit on this <see cref="User"/></returns>
        public int CalculateHappinessScore(List<Wish> wishes)
        {
            if(wishes is null)
            {
                throw new ArgumentNullException(nameof(wishes), "Parameter may not be null");
            }

            int highestScore = 0;
            foreach(Wish wish in wishes)
            {
                highestScore = Math.Max(highestScore, CalculateHappinessScore(wish));
            }

            return highestScore;
        }

        /// <summary>
        /// Calculates the happiness score for the given <see cref="Wish"/>
        /// </summary>
        /// <param name="wish">The <see cref="Wish"/> used to calculate the score</param>
        /// <returns>A number showing how well a <see cref="Wish"/> fits on this <see cref="User"/></returns>
        public int CalculateHappinessScore(Wish wish)
        {
            if(wish is null)
            {
                throw new ArgumentNullException(nameof(wish), "parameter may not be null");
            }

            if(wish.WishUserId == Id)
            {
                return 1000;
            }
            if(wish.WishUserId is null)
            {
                int maxScore = 0;

                //calculate score from organization wish
                int organizationScore = 0;
                if(!(wish.WishOrganizationId is null))
                {
                    maxScore += 950;
                    foreach(UsersOrganizations organizationWork in UsersOrganizations)
                    {
                        if(organizationWork.OrganizationId == wish.WishOrganizationId)
                        {
                            if(wish.WishOrganizationTime is null)
                            {
                                organizationScore = 900;
                            }
                            else
                            {
                                if(organizationWork.WorkYears < wish.WishOrganizationTime)
                                {
                                    organizationScore = 500;
                                }
                                else
                                {
                                    organizationScore = 950;
                                }
                            }
                        }
                    }
                }

                //calculate score from business wishes
                int businessScore = 0;
                if(wish.WishBusinesses.Count != 0)
                {
                    int ownedWishedBusinesses = 0;

                    foreach(WishBusinesses business in wish.WishBusinesses)
                    {
                        if(UsersBusinesses.Any(b => b.BusinessId == business.BusinessId))
                        {
                            ownedWishedBusinesses++;
                        }
                    }
                    businessScore = 500 * ownedWishedBusinesses / wish.WishBusinesses.Count + ownedWishedBusinesses * 75;
                    maxScore += 500 + ownedWishedBusinesses * 75;
                }

                //calculate score from interest wishes
                int interestScore = 0;
                if(wish.WishInterests.Count != 0)
                {
                    int ownedWishedInterests = 0;

                    foreach(WishInterests interest in wish.WishInterests)
                    {
                        if(UsersInterests.Any(i => i.InterestId == interest.InterestId))
                        {
                            ownedWishedInterests++;
                        }
                    }
                    interestScore = 500 * ownedWishedInterests / wish.WishInterests.Count + ownedWishedInterests * 75;
                    maxScore += 500 + ownedWishedInterests * 75;
                }

                //Calculate how good the scores are and return
                if(maxScore == 0)
                {
                    return 0;
                }
                return 700 * (organizationScore + businessScore + interestScore) / maxScore;
            }
            return 0;
        }

        /// <summary>
        /// Gets a list of all the interests the user have
        /// </summary>
        /// <returns>A list of all interests the user have</returns>
        public IEnumerable<Interest> GetInterests()
        {
            return from UsersInterest interest in UsersInterests select interest.Interest;
        }

        /// <summary>
        /// Gets a list of all the businesses the user have
        /// </summary>
        /// <returns>A list of all the businesses the user have</returns>
        public IEnumerable<Business> GetBusinesses()
        {
            return from UsersBusiness business in UsersBusinesses select business.Business;
        }

        /// <summary>
        /// Gets a list of all the organizations this user has been in
        /// </summary>
        /// <returns>A list of this user's organizations</returns>
        public IEnumerable<Organization> GetOrganizations()
        {
            return from UsersOrganizations organization in UsersOrganizations select organization.Organization;
        }
    }
}
