namespace Meetup.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// An <see cref="object"/> containing the information for how long a <see cref="Entities.User"/> has been in an <see cref="Entities.Organization"/>
    /// </summary>
    public partial class UsersOrganizations
    {
        private DateTime startDate;
        private DateTime? endDate;
        private Organization organization;
        private User user;

        protected UsersOrganizations()
        {

        }

        public UsersOrganizations(Organization organization, User user, DateTime startDate)
        {
            Organization = organization;
            User = user;
            StartDate = startDate;
        }

        /// <summary>
        /// The id of this <see cref="object"/>
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id
        {
            get; set;
        }

        /// <summary>
        /// The id of the <see cref="Entities.User"/> who has been in the <see cref="Entities.Organization"/>
        /// </summary>
        public int UserId
        {
            get; set;
        }

        /// <summary>
        /// the id of the <see cref="Entities.Organization"/> the <see cref="Entities.User"/> has been in
        /// </summary>
        public int OrganizationId
        {
            get; set;
        }

        /// <summary>
        /// The date the user started to be in the <see cref="Entities.Organization"/>
        /// </summary>
        public DateTime StartDate
        {
            get
            {
                return startDate;
            }
            set
            {
                if(!(endDate is null) && value >= EndDate)
                {
                    throw new ArgumentException("Value cannot be after " + nameof(EndDate), nameof(StartDate));
                }

                startDate = value;
            }
        }

        /// <summary>
        /// The date the user stopped being in the <see cref="Entities.Organization"/>
        /// </summary>
        public DateTime? EndDate
        {
            get
            {
                return endDate;
            }
            set
            {
                if(value <= StartDate)
                {
                    throw new ArgumentException("Value cannot be before " + nameof(StartDate), nameof(EndDate));
                }
                endDate = value;
            }
        }

        /// <summary>
        /// the <see cref="Entities.Organization"/> the <see cref="Entities.User"/> has been in
        /// </summary>
        public virtual Organization Organization
        {
            get
            {
                return organization;
            }
            set
            {
                organization = value;
            }
        }

        /// <summary>
        /// The <see cref="Entities.User"/> who has been in the <see cref="Entities.Organization"/>
        /// </summary>
        public virtual User User
        {
            get
            {
                return user;
            }
            set
            {
                user = value;
            }
        }

        /// <summary>
        /// A string showing the information in this <see cref="object"/>
        /// </summary>
        /// <returns>A string with information</returns>
        public override string ToString()
        {
            if(EndDate is null)
            {
                return Organization.Name + ": Ansættelsesdato " + StartDate.ToString("dd-MM-yyyy");
            }
            else
            {
                return Organization.Name + ": " + StartDate.ToString("dd-MM-yyyy") + " - " + EndDate.Value.ToString("dd-MM-yyyy");
            }
        }

        /// <summary>
        /// Outputs the (rounded) number of years the <see cref="Entities.User"/> has been in the <see cref="Entities.Organization"/>
        /// </summary>
        [NotMapped]
        public int WorkYears
        {
            get
            {
                DateTime endTime;
                if(EndDate is null)
                {
                    endTime = DateTime.Now;
                }
                else
                {
                    endTime = EndDate.Value;
                }

                return ((endTime - StartDate).Days + 182) / 365;
            }
        }
    }
}
