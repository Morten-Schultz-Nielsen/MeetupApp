using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Meetup.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.IO;
using Meetup.Websites.Models;
using Meetup.Helper;

namespace Meetup.Websites.Controllers
{
    public class EventsController : Controller
    {
        /// <summary>
        /// An action returning a page showing all the user's events
        /// </summary>
        /// <returns>If user is logged in: Returns a page showing events</returns>
        public ActionResult Index()
        {
            MeetupModel model = new MeetupModel();
            int? infoID = this.UserId();
            if(infoID is null)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewEventsModel viewModel = new ViewEventsModel();
            User thisUser = model.Users.SingleOrDefault(userInfo => userInfo.Id == infoID);

            viewModel.OwnedEvents = model.Events.Where(e => e.HostUserId == thisUser.Id).ToList();
            viewModel.InvitedToEvents = thisUser.Invites.ToList();

            return View(viewModel);
        }

        /// <summary>
        /// An action returning a page for creating new events
        /// </summary>
        /// <returns>Returns a page for creating events</returns>
        public ActionResult Create()
        {
            return View("EditCreate", new EventEditCreationModel());
        }

        /// <summary>
        /// An action used to create a new event
        /// </summary>
        /// <param name="viewModel">The posted model with information about the created event</param>
        /// <returns>If success: the list over all the user's events</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EventEditCreationModel viewModel)
        {
            if(!ModelState.IsValid)
            {
                return View("EditCreate", viewModel);
            }
            else
            {
                //Get event owner and create event
                MeetupModel model = new MeetupModel();
                int? infoID = this.UserId();
                if(infoID is null)
                {
                    return RedirectToAction("Index", "Home");
                }

                //get event ID
                int randomNumber;
                Random random = new Random();
                do
                {
                    randomNumber = random.Next(0, int.MaxValue);
                }
                while(model.Events.Any(e => e.Id == randomNumber));

                //Create event
                Event newEvent = new Event(viewModel.Name, 
                    viewModel.Description, 
                    model.Users.SingleOrDefault(u => u.Id == infoID.Value), 
                    new Address(viewModel.Address.Country, viewModel.Address.City, Convert.ToInt32(viewModel.Address.CityZipCode), viewModel.Address.StreetName, viewModel.Address.StreetNumber),
                    randomNumber);
                newEvent.BeginningTime = viewModel.Time.Value;

                //Save event
                model.Events.Add(newEvent);
                model.SaveChanges();

                return RedirectToAction("Index");
            }
        }

        /// <summary>
        /// An action returning a page where the user can edit an event
        /// </summary>
        /// <param name="id">The id of the event to edit</param>
        /// <returns>If user owns the event: Returns a page to edit an event</returns>
        public ActionResult Edit(int id)
        {
            MeetupModel model = new MeetupModel();

            //Checks if user owns the event
            int? infoID = this.UserId();
            if(infoID is null)
            {
                return RedirectToAction("Index", "Home");
            }
            Event editEvent = model.Events.SingleOrDefault(e => e.Id == id && e.HostUserId == infoID);
            if(editEvent is null)
            {
                return RedirectToAction("Index");
            }

            //Fill in view model
            EventEditCreationModel viewModel = new EventEditCreationModel();
            viewModel.Address = new AddressModel();
            viewModel.Address.City = editEvent.Address.CityName;
            viewModel.Address.Country = editEvent.Address.Country;
            viewModel.Address.CityZipCode = editEvent.Address.ZipCode.ToString();
            viewModel.Address.StreetName = editEvent.Address.StreetName;
            viewModel.Address.StreetNumber = editEvent.Address.StreetNumber;

            viewModel.Description = editEvent.Description;
            viewModel.EditingId = editEvent.Id;
            viewModel.Name = editEvent.Name;
            viewModel.Time = editEvent.BeginningTime;
            viewModel.Editing = true;

            return View("EditCreate", viewModel);
        }

        /// <summary>
        /// An action used to edit an event
        /// </summary>
        /// <param name="viewModel">The posted model containing data about the edits</param>
        /// <returns>if user owns the event: The edit event page</returns>
        [HttpPost]
        public ActionResult Edit(EventEditCreationModel viewModel)
        {
            if(!ModelState.IsValid)
            {
                viewModel.Editing = true;
                return View("EditCreate", viewModel);
            }
            else
            {
                MeetupModel model = new MeetupModel();

                //Check if user owns the event
                int? infoID = this.UserId();
                if(infoID is null)
                {
                    return RedirectToAction("Index", "Home");
                }
                Event editEvent = model.Events.SingleOrDefault(e => e.Id == viewModel.EditingId && e.HostUserId == infoID);
                if(editEvent is null)
                {
                    return RedirectToAction("Index");
                }

                //Fill in the event with the view model
                editEvent.Name = viewModel.Name;
                editEvent.Description = viewModel.Description;
                editEvent.BeginningTime = viewModel.Time.Value;
                editEvent.Address.CityName = viewModel.Address.City;
                editEvent.Address.Country = viewModel.Address.Country;
                editEvent.Address.StreetName = viewModel.Address.StreetName;
                editEvent.Address.StreetNumber = viewModel.Address.StreetNumber;
                editEvent.Address.ZipCode = Convert.ToInt32(viewModel.Address.CityZipCode);

                model.SaveChanges();
                return RedirectToAction("Page", new {eventId = viewModel.EditingId });
            }
        }

        /// <summary>
        /// An action returning a page where a user can (un)invite another user to an event
        /// </summary>
        /// <param name="id">The id of the user to (un)invite</param>
        /// <returns>if user exists: the (un)invite page</returns>
        public ActionResult Invite(int id)
        {
            int? infoID = this.UserId();
            if(infoID is null)
            {
                return RedirectToAction("Index", "Home");
            }

            //Finds the user and the events the user is in and isnt in
            MeetupModel model = new MeetupModel();
            InviteToEventModel viewModel = new InviteToEventModel();
            viewModel.IsInvitedTo = model.Events.Where(e => e.HostUserId == infoID && e.Invites.Any(u => u.UserId == id)).ToList();
            viewModel.CanInviteTo = model.Events.Where(e => e.HostUserId == infoID && !e.Invites.Any(u => u.UserId == id)).ToList();
            viewModel.Inviting = model.Users.SingleOrDefault(u => u.Id == id);
            if(viewModel.Inviting is null)
            {
                return RedirectToAction("List", "User");
            }

            viewModel.CameFrom = Request.UrlReferrer.ToString();

            return View(viewModel);
        }

        /// <summary>
        /// An action for inviting another user to an event
        /// </summary>
        /// <param name="user">the id of the user to invite</param>
        /// <param name="theEvent">the id of the event to invite to</param>
        /// <param name="returnTo">A link to the page to load after te user is invited</param>
        /// <returns>if success: Returns to the <paramref name="returnTo"/> page</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Invite(int user, int theEvent, string returnTo)
        {
            //Check if user owns the event and add the other user to it
            MeetupModel model = new MeetupModel();
            int? infoID = this.UserId();
            if(infoID is null)
            {
                return RedirectToAction("Index", "Home");
            }

            User inviting = model.Users.SingleOrDefault(u => u.Id == user);
            Event inviteToEvent = model.Events.SingleOrDefault(e => e.Id == theEvent && e.HostUserId == infoID);
            if(inviting is null || inviteToEvent is null)
            {
                return RedirectToAction("Index", "Home");
            }

            //Make sure user isnt invited already
            if(!inviteToEvent.GetUsers().Any(u => u.Id == user))
            {
                inviteToEvent.Invites.Add(new Invite(inviteToEvent, inviting, DateTime.Now));
                model.SaveChanges();
            }

            //Redirect back to last page if possible
            if(string.IsNullOrWhiteSpace(returnTo))
            {
                return RedirectToAction("Page", "Events", new { Id = theEvent });
            }
            else
            {
                return Redirect(returnTo);
            }
        }

        /// <summary>
        /// An action for uninviting another user to an event
        /// </summary>
        /// <param name="user">the id of the user to uninvite</param>
        /// <param name="theEvent">the id of the event to invite to</param>
        /// <param name="returnTo">A link to the page to load after te user is uninvited</param>
        /// <returns>if success: Returns to the <paramref name="returnTo"/> page</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Uninvite(int user, int theEvent, string returnTo)
        {
            //Check if user owns the event and remove the other user from it
            MeetupModel model = new MeetupModel();
            int? infoID = this.UserId();
            if(infoID is null)
            {
                return RedirectToAction("Index", "Home");
            }

            Event uninviteToEvent = model.Events.SingleOrDefault(e => e.Id == theEvent && e.HostUserId == infoID);
            Invite invite = model.Invites.SingleOrDefault(i => i.EventId == theEvent && i.UserId == user);
            if(uninviteToEvent is null)
            {
                return RedirectToAction("Index", "Home");
            }

            //Remove invite
            if(!(invite is null))
            {
                model.Invites.Remove(invite);
                model.SaveChanges();
            }

            return Redirect(returnTo);
        }

        /// <summary>
        /// An action returning a page showing an event
        /// </summary>
        /// <param name="id">The id of the event to show</param>
        /// <returns>If user is in the event: Returns a page containing information about the event</returns>
        public ActionResult Page(int id)
        {
            //Find the event and makes sure you are in it (dont show if the user isnt invited/owns the event)
            int? infoID = this.UserId();
            MeetupModel model = new MeetupModel();
            EventPageModel viewModel = new EventPageModel();
            viewModel.Event = model.Events.SingleOrDefault(e => e.Id == id && (e.Invites.Any(i => i.UserId == infoID) || e.HostUserId == infoID));
            if(viewModel.Event is null)
            {
                return RedirectToAction("Index", "Home");
            }

            //Check if the user owns the event
            viewModel.UserID = infoID.Value;
            if(viewModel.Event.HostUserId == infoID)
            {
                viewModel.EventOwner = true;
            }

            //Creates the list of all users invited to the event
            viewModel.Invited = viewModel.Event.Invites.Any(i => i.UserId == infoID);
            viewModel.InvitedUsers = viewModel.Event.GetUsers().ToList();

            //Creates the list of all seances/meetings with this user
            List<Seance> seances = viewModel.Event.Seances.ToList();
            viewModel.Meetings = new List<Meeting>();
            foreach(Seance seance in seances)
            {
                Meeting meeting = seance.Meetings.SingleOrDefault(m => m.MeetingContainsUser(infoID.Value));
                if(!(meeting is null))
                {
                    viewModel.Meetings.Add(meeting);
                }
            }

            //Gets a list of the user's wishes for the event
            viewModel.UserWishes = new List<Wish>();
            User user = model.Users.SingleOrDefault(u => u.Id == viewModel.UserID);
            viewModel.UserWishes = user.Wishes.Where(w => w.EventId == viewModel.Event.Id).ToList();

            return View(viewModel);
        }

        /// <summary>
        /// An action returning a page used to create a meeting list
        /// </summary>
        /// <param name="id">The event to create the list for</param>
        /// <returns>if the user owns the event: Returns a page where the user can create a meeting list</returns>
        public ActionResult CreateList(int id)
        {
            MeetupModel model = new MeetupModel();

            //Checks if the user owns the event or not
            int? infoID = this.UserId();
            Event theEvent = model.Events.SingleOrDefault(e => e.Id == id && e.HostUserId == infoID);
            if(theEvent is null)
            {
                return RedirectToAction("Index", "Home");
            }

            EventListCreationModel viewModel = new EventListCreationModel();
            viewModel.EventInformation = theEvent;
            viewModel.EventId = theEvent.Id;

            if(viewModel.EventInformation.Invites.Count < 2)
            {
                return View("NotEnoughPeople", viewModel);
            }

            return View(viewModel);
        }

        /// <summary>
        /// An action creating the meeting list for an event
        /// </summary>
        /// <param name="viewModel">The model containing information about how the list should be created</param>
        /// <returns>If the user owns the event: The event page</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateList(EventListCreationModel viewModel)
        {
            MeetupModel model = new MeetupModel();

            //Makes sure the user owns the event
            int? infoID = this.UserId();
            viewModel.EventInformation = model.Events.SingleOrDefault(e => e.Id == viewModel.EventId && e.HostUserId == infoID);
            if(viewModel.EventInformation is null)
            {
                return RedirectToAction("Index", "Home");
            }
            if(!ModelState.IsValid)
            {
                return View(viewModel);
            }

            //Make sure there are enough people
            if(viewModel.EventInformation.Invites.Count < 2)
            {
                return View("NotEnoughPeople",viewModel);
            }

            //Creates a list of all users in the event
            List<User> UsersInEvent = viewModel.EventInformation.GetUsers().ToList();

            //Clear old meeting list
            foreach(Seance meetingRow in viewModel.EventInformation.Seances)
            {
                model.Meetings.RemoveRange(meetingRow.Meetings);
                model.UserPauses.RemoveRange(meetingRow.UserPauses);
            }
            model.Seances.RemoveRange(viewModel.EventInformation.Seances);

            //Get list of all possible meetings
            List<MeetingScore> possibleMeetings = MeetingScore.GetPossibleMeetings(UsersInEvent, viewModel.EventId);

            //Generate Seances/meetings list
            int meetingsPerSeance = UsersInEvent.Count / 2;
            for(int i = 0; i < viewModel.AmountOfMeetings && possibleMeetings.Count != 0; i++)
            {
                //Create a meeting row (seance)
                List<MeetingScore> possibleMeetingsThisRow = possibleMeetings.Where(m => true).ToList();
                List<User> usersNotInMeeting = UsersInEvent.Where(u => true).ToList();
                DateTime beginningTime = viewModel.EventInformation.BeginningTime.AddMinutes(viewModel.MinuteInterval * i);
                Seance eventMeetingsRow = new Seance(viewModel.EventInformation, i, beginningTime, beginningTime.AddMinutes(viewModel.MinuteInterval));

                for(int j = 0; j < meetingsPerSeance; j++)
                {
                    if(possibleMeetingsThisRow.Count == 0)
                    {
                        //fill with already used meetings if turned on
                        if(viewModel.ForceFillMeetings && usersNotInMeeting.Count >= 2)
                        {
                            possibleMeetingsThisRow = MeetingScore.GetPossibleMeetings(usersNotInMeeting, viewModel.EventId);
                        }
                        else
                        {
                            //Create list of users not in meetings in this seance
                            foreach(User userNotInMeeting in usersNotInMeeting)
                            {
                                eventMeetingsRow.UserPauses.Add(new UserPause(userNotInMeeting, eventMeetingsRow));
                            }
                            break;
                        }
                    }

                    //add highest scored meeting to the event
                    MeetingScore meetingToAdd = possibleMeetingsThisRow[0];

                    //remove impossible meetings (meetings containing the users who just have been added to the meeting list)
                    eventMeetingsRow.Meetings.Add(new Meeting(meetingToAdd.Person1, meetingToAdd.Person2, eventMeetingsRow));
                    possibleMeetingsThisRow = possibleMeetingsThisRow.Where(m => m.Person1.Id != meetingToAdd.Person1.Id && m.Person2.Id != meetingToAdd.Person2.Id && m.Person1.Id != meetingToAdd.Person2.Id && m.Person2.Id != meetingToAdd.Person1.Id).ToList();
                    usersNotInMeeting = usersNotInMeeting.Where(u => u.Id != meetingToAdd.Person1.Id && u.Id != meetingToAdd.Person2.Id).ToList();
                    possibleMeetings.Remove(meetingToAdd);
                }

                viewModel.EventInformation.Seances.Add(eventMeetingsRow);
            }

            model.SaveChanges();

            return RedirectToAction("Page", new { Id = viewModel.EventId });
        }

        /// <summary>
        /// An action returning a page where the user confirms they want to leave
        /// </summary>
        /// <param name="id">The event the user wants to leave</param>
        /// <returns>if the user is in the event: Returns a page where the user can leave the event</returns>
        public ActionResult Leave(int id)
        {
            //Get event and make sure the user is in it
            MeetupModel model = new MeetupModel();
            int? infoId = this.UserId();
            if(infoId is null)
            {
                return RedirectToAction("Index", "Home");
            }

            LeaveEventModel viewModel = new LeaveEventModel();
            viewModel.EventInformation = model.Events.SingleOrDefault(e => e.Id == id && e.Invites.Any(u => u.UserId == infoId));
            if(viewModel.EventInformation is null)
            {
                return RedirectToAction("Index", "Home");
            }
            viewModel.EventId = viewModel.EventInformation.Id;

            return View(viewModel);
        }

        /// <summary>
        /// An action making a user leave the given event
        /// </summary>
        /// <param name="theEvent">the event to leave</param>
        /// <returns>The event list page</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Leave(LeaveEventModel viewModel)
        {
            //Get all the event data and make sure the user is in the event
            MeetupModel model = new MeetupModel();
            int? infoId = this.UserId();
            if(infoId is null)
            {
                return RedirectToAction("Index", "Home");
            }
            viewModel.EventInformation = model.Events.SingleOrDefault(e => e.Id == viewModel.EventId && e.Invites.Any(u => u.UserId == infoId));
            if(viewModel.EventInformation is null)
            {
                return RedirectToAction("Index", "Home");
            }

            //Remove invite
            model.Invites.Remove(viewModel.EventInformation.Invites.SingleOrDefault(u => u.UserId == infoId));

            model.SaveChanges();
            return RedirectToAction("Index");
        }

        /// <summary>
        /// An action returning a page with a list of all seances in an event
        /// </summary>
        /// <param name="id">The id of the event to show seances for</param>
        /// <returns>If user is in the event: Returns a page to view seances for the event</returns>
        public ActionResult Seances(int id)
        {
            //Get event and make sure user is in it
            MeetupModel model = new MeetupModel();
            int? infoId = this.UserId();
            if(infoId is null)
            {
                return RedirectToAction("Index", "Home");
            }
            SeanceViewModel viewModel = new SeanceViewModel();
            viewModel.Event = model.Events.SingleOrDefault(e => e.Id == id && (e.HostUserId == infoId || e.Invites.Any(u => u.UserId == infoId)));
            if(viewModel.Event is null)
            {
                return RedirectToAction("Index", "Home");
            }

            //sort seance list
            List<Seance> seances = viewModel.Event.Seances.ToList();
            List<Seance> sortedMeetings = new List<Seance>();
            foreach(Seance meeting in seances)
            {
                bool sorted = false;
                for(int i = 0; i < sortedMeetings.Count; i++)
                {
                    if(meeting.MeetingNumber < sortedMeetings[i].MeetingNumber)
                    {
                        sortedMeetings.Insert(i, meeting);
                        sorted = true;
                        break;
                    }
                }
                if(!sorted)
                {
                    sortedMeetings.Add(meeting);
                }
            }

            viewModel.Seances = viewModel.Event.Seances.ToList();
            viewModel.UserId = infoId.Value;

            return View(viewModel);
        }
    }
}