using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Meetup.Entities;

namespace Meetup.Websites.Models
{
    public class LeaveEventModel
    {
        public Event EventInformation { get; set; }

        public int EventId { get; set; }
    }
    public class SeanceViewModel
    {
        public List<Seance> Seances { get; set; }

        public int UserId { get; set; }

        public Event Event { get; set; }
    }

    public class ViewEventsModel
    {
        public List<Event> OwnedEvents { get; set; }

        public List<Invite> InvitedToEvents { get; set; }
    }

    public class InviteToEventModel
    {
        public List<Event> CanInviteTo { get; set; }

        public List<Event> IsInvitedTo { get; set; }

        public User Inviting { get; set; }
    }

    public class EventPageModel
    {
        public Event Event { get; set; }

        public bool Invited { get; set; }

        public int UserID { get; set; }

        public List<User> InvitedUsers { get; set; }

        public List<Meeting> Meetings { get; set; }

        public bool EventOwner { get; set; }

        public List<Wish> UserWishes { get; set; }
    }

    public class EventEditCreationModel
    {
        public bool Editing { get; set; }

        public int? EditingId { get; set; }

        [Required(ErrorMessage = "Feltet \"{0}\" skal udfyldes.")]
        [Display(Name = "Event navn")]
        [StringLength(30, ErrorMessage = "Feltet \"{0}\" skal være {2}-{1} bogstaver langt.", MinimumLength = 2)]
        [RegularExpression(Event.NamePattern, ErrorMessage = "Feltet \"{0}\" indeholder et ugyldigt navn.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Feltet \"{0}\" skal udfyldes.")]
        [Display(Name = "Beskrivelse")]
        [StringLength(int.MaxValue, ErrorMessage = "Feltet \"{0}\" skal være {2}-{1} bogstaver langt.", MinimumLength = 10)]
        [RegularExpression(Event.DescriptionPattern, ErrorMessage = "Feltet \"{0}\" indeholder en ugyldig berskrivelse.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Feltet \"{0}\" skal udfyldes.")]
        [Display(Name = "Event startstidspunkt")]
        public DateTime? Time { get; set; }

        public AddressModel Address { get; set; }
    }

    public class EventListCreationModel
    {
        public Event EventInformation { get; set; }

        public int EventId { get; set; }

        [Required(ErrorMessage = "Feltet \"{0}\" skal udfyldes.")]
        [Display(Name = "Tid per møde")]
        [RegularExpression("^[1-9]{1}[0-9]*$", ErrorMessage = "Tallet skal være højere end 0")]
        public int MinuteInterval { get; set; }

        [Required(ErrorMessage = "Feltet \"{0}\" skal udfyldes.")]
        [Display(Name = "Antal seancer")]
        [RegularExpression("^[1-9]{1}[0-9]*$", ErrorMessage = "Tallet skal være højere end 0")]
        public int AmountOfMeetings { get; set; }

        [Required(ErrorMessage = "Feltet \"{0}\" skal udfyldes.")]
        [Display(Name = "Tillad ens møder")]
        public bool ForceFillMeetings { get; set; }
    }
}