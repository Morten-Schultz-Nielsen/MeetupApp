namespace Meetup.Entities
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class MeetupModel: DbContext
    {
        public MeetupModel()
            : base("name=MeetupModel")
        {
        }

        public virtual DbSet<Seance> Seances
        {
            get; set;
        }

        public virtual DbSet<Address> Addresses
        {
            get; set;
        }
        public virtual DbSet<Business> Businesses
        {
            get; set;
        }
        public virtual DbSet<Event> Events
        {
            get; set;
        }
        public virtual DbSet<Invite> Invites
        {
            get; set;
        }
        public virtual DbSet<Interest> Interests
        {
            get; set;
        }
        public virtual DbSet<Meeting> Meetings
        {
            get; set;
        }
        public virtual DbSet<Organization> Organizations
        {
            get; set;
        }
        public virtual DbSet<User> Users
        {
            get; set;
        }
        public virtual DbSet<UsersBusiness> UsersBusinesses
        {
            get; set;
        }
        public virtual DbSet<UsersInterest> UsersInterests
        {
            get; set;
        }
        public virtual DbSet<UsersOrganizations> UsersOrganizations
        {
            get; set;
        }

        public virtual DbSet<Wish> Wishes
        {
            get; set;
        }

        public virtual DbSet<WishInterests> WishInterests
        {
            get; set;
        }

        public virtual DbSet<WishBusinesses> WishBusinesses
        {
            get; set;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>()
                .HasMany(e => e.Events)
                .WithRequired(e => e.Address)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Address>()
                .HasMany(e => e.Users)
                .WithRequired(e => e.Address)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Business>()
                .HasMany(e => e.UsersBusinesses)
                .WithRequired(e => e.Business)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Event>()
                .HasMany(e => e.Invites)
                .WithRequired(e => e.Event)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Event>()
                .HasMany(e => e.Seances)
                .WithRequired(e => e.Event)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Seance>()
                .HasMany(e => e.Meetings)
                .WithRequired(e => e.Seance)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Interest>()
                .HasMany(e => e.UsersInterests)
                .WithRequired(e => e.Interest)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Organization>()
                .HasMany(e => e.UsersOrganizations)
                .WithRequired(e => e.Organization)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Events)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.HostUserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Wishes)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Invites)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Meetings)
                .WithRequired(e => e.UserOne)
                .HasForeignKey(e => e.UserOneId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Meetings1)
                .WithRequired(e => e.UserTwo)
                .HasForeignKey(e => e.UserTwoId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.UsersBusinesses)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.UsersInterests)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.UsersOrganizations)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);
        }
    }
}
