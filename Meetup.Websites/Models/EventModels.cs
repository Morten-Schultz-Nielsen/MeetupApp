using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Meetup.Entities;

namespace Meetup.Websites.Models
{
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

        [Required(ErrorMessage = "The field \"{0}\" is required.")]
        [Display(Name = "Event Name")]
        [StringLength(30, ErrorMessage = "The field \"{0}\" must be {2}-{1} characters long.", MinimumLength = 2)]
        public string Name { get; set; }

        [Required(ErrorMessage = "The field \"{0}\" is required.")]
        [Display(Name = "Description")]
        [StringLength(int.MaxValue, ErrorMessage = "The field \"{0}\" must be atleast {2} characters long.", MinimumLength = 10)]
        public string Description { get; set; }

        [Required(ErrorMessage = "The field \"{0}\" is required.")]
        [Display(Name = "Event Start Time")]
        public DateTime? Time { get; set; }

        public AddressModel Address { get; set; }
    }

    public class EventListCreationModel
    {
        public Event Event { get; set; }


        [Required(ErrorMessage = "The field \"{0}\" is required.")]
        [Display(Name = "Meeting time")]
        [RegularExpression("^[1-9]{1}[0-9]*$", ErrorMessage = "The number has to be higher than 0")]
        public int MinuteInterval { get; set; }

        [Required(ErrorMessage = "The field \"{0}\" is required.")]
        [Display(Name = "Amount of meetings")]
        [RegularExpression("^[1-9]{1}[0-9]*$", ErrorMessage = "The number has to be higher than 0")]
        public int AmountOfMeetings { get; set; }
    }
}