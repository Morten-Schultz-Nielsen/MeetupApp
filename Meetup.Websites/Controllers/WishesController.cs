using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Meetup.Entities;
using Meetup.Helper;
using Meetup.Websites.Models;

namespace Meetup.Websites.Controllers
{
    public class WishesController : Controller
    {
        /// <summary>
        /// An action returning a page with all the wishes the user has for an event
        /// </summary>
        /// <param name="id">the id of the event to show wishes for</param>
        /// <returns>if user is in event: A page with all the user's wishes for the event</returns>
        public ActionResult List(int id)
        {
            //Checks if user is logged in and is in event
            int? infoID = this.UserId();
            if(infoID is null)
            {
                return RedirectToAction("Index", "Home");
            }

            MeetupModel model = new MeetupModel();
            WishesViewModel viewModel = new WishesViewModel();
            viewModel.TheEvent = model.Events.SingleOrDefault(e => e.Id == id);
            if(viewModel.TheEvent is null)
            {
                return RedirectToAction("Index", "Home");
            }

            //Gets wish list
            viewModel.WishList = model.Wishes.Where(w => w.UserId == infoID && w.EventId == id).ToList();

            return View(viewModel);
        }

        /// <summary>
        /// An action returning a page where the user can edit a wish
        /// </summary>
        /// <param name="id">The wish to edit</param>
        /// <returns>if wish exists and is owned by the user: The edit page for the wish</returns>
        public ActionResult Edit(int id)
        {
            //Checks if user is logged in, is in the event and owns the wish
            int? infoID = this.UserId();
            if(infoID is null)
            {
                return RedirectToAction("Index", "Home");
            }

            MeetupModel model = new MeetupModel();
            Wish editingWish = model.Wishes.SingleOrDefault(w => w.UserId == infoID && w.Id == id);
            if(editingWish is null || !editingWish.Event.Invites.Any(u => u.UserId == infoID))
            {
                return RedirectToAction("Index", "Home");
            }

            WishEditCreateModel viewModel = GetFilledWishCreateEditModel(editingWish.Event);
            viewModel.WishInformation = editingWish;
            viewModel.WishId = editingWish.Id;

            //Creates list of chosen and unchosen interests
            viewModel.ChosenInterestsList = viewModel.WishInformation.GetInterests().ToList();
            viewModel.UnchosenInterests = viewModel.UnchosenInterests.Except(viewModel.ChosenInterestsList).ToList();

            //Creates list of chosen and unchosen businesses
            viewModel.ChosenBusinessesList = viewModel.WishInformation.GetBusinesses().ToList();
            viewModel.UnchosenBusinesses = viewModel.UnchosenBusinesses.Except(viewModel.ChosenBusinessesList).ToList();

            //Checks if the wish was for another user
            if(!(viewModel.WishInformation.WishUser is null))
            {
                viewModel.ChosenName = viewModel.WishInformation.WishUser.FullName;
            }

            //Finds the organization the wish was wishing for
            if(!(viewModel.WishInformation.WishOrganizationId is null))
            {
                viewModel.SelectedOrganizationIndex = viewModel.Organizations.IndexOf(viewModel.Organizations.SingleOrDefault(o => o.Id == viewModel.WishInformation.WishOrganizationId));
            }
            else
            {
                viewModel.SelectedOrganizationIndex = -1;
            }
            viewModel.OrganizationWish = new WishOrganizationModel();
            viewModel.OrganizationWish.WorkYears = viewModel.WishInformation.WishOrganizationTime;

            //Sets editing flag and returns edit page
            viewModel.EditingWish = true;
            return View("EditCreate", viewModel);
        }

        /// <summary>
        /// An action returning a page where the user can create a new wish
        /// </summary>
        /// <param name="id">The id of the event to add the wish to</param>
        /// <returns>if user is in event: The creation page for the wish</returns>
        public ActionResult Create(int id)
        {
            //Checks if user is in event
            MeetupModel model = new MeetupModel();
            Event theEvent = model.Events.SingleOrDefault(e => e.Id == id);
            if(theEvent is null)
            {
                return RedirectToAction("Index", "Home");
            }

            List<User> usersInEvent = theEvent.GetUsers().ToList();
            int? infoID = this.UserId();
            if(infoID is null)
            {
                return RedirectToAction("Index", "Home");
            }
            if(!usersInEvent.Any(u => u.Id == infoID))
            {
                return RedirectToAction("Index", "Home");
            }

            //Setup view model
            WishEditCreateModel viewModel = GetFilledWishCreateEditModel(theEvent);
            viewModel.ChosenInterestsList = new List<Interest>();
            viewModel.ChosenBusinessesList = new List<Business>();
            viewModel.SelectedOrganizationIndex = -1;

            return View("EditCreate", viewModel);
        }

        /// <summary>
        /// An action getting post request when users creates new wishes or edits wishes
        /// </summary>
        /// <param name="viewModel">the model containing the data for the wish</param>
        /// <returns>if creation/editting is successfull: Returns wish list of the event the wish is in</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCreate(WishEditCreateModel viewModel)
        {
            //Check if user is logged in
            MeetupModel model = new MeetupModel();
            int? infoID = this.UserId();
            if(infoID is null)
            {
                return RedirectToAction("Index", "Home");
            }
            User editUser = model.Users.SingleOrDefault(userInfo => userInfo.Id == infoID);

            //Check if user is in the event
            viewModel.EventInformation = model.Events.SingleOrDefault(e => e.Id == viewModel.EventId && e.Invites.Any(u => u.UserId == infoID));
            if(viewModel.EventInformation is null)
            {
                return RedirectToAction("Index", "Home");
            }

            //Create list of chosen and unchosen businesses
            if(string.IsNullOrEmpty(viewModel.ChosenBusinesses))
            {
                viewModel.ChosenBusinesses = "";
            }
            string[] businessesStringList = viewModel.ChosenBusinesses.Split(',');
            viewModel.ChosenBusinessesList = model.Businesses.Where(b => businessesStringList.Any(bl => bl.Contains(b.Name))).ToList();
            viewModel.UnchosenBusinesses = model.Businesses.ToList();
            viewModel.UnchosenBusinesses = viewModel.UnchosenBusinesses.Except(viewModel.ChosenBusinessesList).ToList();

            //Create list of chosen and unchosen interests
            if(string.IsNullOrEmpty(viewModel.ChosenInterests))
            {
                viewModel.ChosenInterests = "";
            }
            string[] interestsStringList = viewModel.ChosenInterests.Split(',');
            viewModel.ChosenInterestsList = model.Interests.Where(i => interestsStringList.Any(il => il.Contains(i.Name))).ToList();
            viewModel.UnchosenInterests = model.Interests.ToList();
            viewModel.UnchosenInterests = viewModel.UnchosenInterests.Except(viewModel.ChosenInterestsList).ToList();

            if(!ModelState.IsValid)
            {
                return RedirectBackToEditCreate(viewModel);
            }
            else
            {
                Wish theWish;

                if(viewModel.EditingWish)
                {
                    //If editing a wish, get wish
                    theWish = model.Wishes.SingleOrDefault(w => w.Id == viewModel.WishId && w.UserId == infoID);
                    if(theWish is null)
                    {
                        return RedirectToAction("List", new { viewModel.EventId });
                    }
                }
                else
                {
                    //Get an id for the wish
                    Random random = new Random();
                    int newId;
                    do
                    {
                        newId = random.Next(0, int.MaxValue);
                    }
                    while(model.Wishes.Any(w => w.Id == newId));

                    //If creating a wish, create new object
                    theWish = new Wish(editUser, viewModel.EventInformation, newId);
                    model.Wishes.Add(theWish);
                }

                if(!string.IsNullOrWhiteSpace(viewModel.ChosenName))
                {
                    //If the wish is a wish for another user
                    if(viewModel.EditingWish)
                    {
                        model.WishBusinesses.RemoveRange(theWish.WishBusinesses);
                        model.WishInterests.RemoveRange(theWish.WishInterests);
                    }

                    //Checks if wished user actually exists
                    User wishUser = model.Users.SingleOrDefault(u => u.FirstName + " " + u.LastName == viewModel.ChosenName);
                    if(wishUser is null)
                    {
                        ModelState.AddModelError("ChosenName", "Brugeren \"" + viewModel.ChosenName + "\" er ikke i denne event.");
                        return RedirectBackToEditCreate(viewModel);
                    }
                    else
                    {
                        theWish.WishUser = wishUser;
                    }
                }
                else
                {
                    //If the wish is a wish for a user with interests, businesses and such
                    theWish.WishUser = null;

                    //Removes and adds businesses to the wish
                    List<Business> selectedBusinesses = viewModel.ChosenBusinessesList.Where(cb => !theWish.WishBusinesses.Any(w => w.Business.Name == cb.Name)).ToList();
                    List<WishBusinesses> removedBusinesses = theWish.WishBusinesses.Where(w => !viewModel.ChosenBusinesses.Contains(w.Business.Name)).ToList();
                    model.WishBusinesses.RemoveRange(removedBusinesses);
                    theWish.WishBusinesses = theWish.WishBusinesses.Except(removedBusinesses).ToList();

                    foreach(Business business in selectedBusinesses)
                    {
                        theWish.WishBusinesses.Add(new WishBusinesses(business, theWish));
                    }

                    //Removes and adds interests to the wish
                    List<Interest> selectedInterests = viewModel.ChosenInterestsList.Where(ib => !theWish.WishInterests.Any(w => w.Interest.Name == ib.Name)).ToList();
                    List<WishInterests> removedInterests = theWish.WishInterests.Where(w => !viewModel.ChosenInterests.Contains(w.Interest.Name)).ToList();
                    model.WishInterests.RemoveRange(removedInterests);
                    theWish.WishInterests = theWish.WishInterests.Except(removedInterests).ToList();

                    foreach(Interest interest in selectedInterests)
                    {
                        theWish.WishInterests.Add(new WishInterests(interest, theWish ));
                    }

                    //Adds organization (and work time) to the wish if specified
                    Organization wishedOrganization = model.Organizations.SingleOrDefault(o => o.Name == viewModel.OrganizationWish.Name);
                    theWish.WishOrganization = wishedOrganization;
                    theWish.WishOrganizationTime = viewModel.OrganizationWish.WorkYears;

                    //Check if wish actually is wishing for something
                    if(theWish.WishBusinesses.Count <= 0 && theWish.WishInterests.Count <= 0 && theWish.WishOrganizationId is null)
                    {
                        ModelState.AddModelError("OrganizationWish.Name", "Du skal vælge mindst et erhverv, interesse or en organisation.");
                        return RedirectBackToEditCreate(viewModel);
                    }
                }

                //Save wish
                model.SaveChanges();

                return RedirectToAction("List", new { Id = viewModel.EventId });
            }
        }

        /// <summary>
        /// Returns an action result which returns the user back to the creation/editing page (used in case wish state is invalid)
        /// </summary>
        /// <param name="viewModel">information about the wish</param>
        /// <returns>Returns an action result which returns the edit/creation page</returns>
        private ActionResult RedirectBackToEditCreate(WishEditCreateModel viewModel)
        {
            //Get list of users in the event
            List<User> usersInEvent = viewModel.EventInformation.GetUsers().ToList();
            viewModel.UsersInEvent = new List<SelectListItem>();
            viewModel = FillModelWithUsersAndOrganizations(viewModel, usersInEvent);

            return View("EditCreate", viewModel);
        }

        /// <summary>
        /// An action returning a confirmation page to ask if the user wants to delete the wish
        /// </summary>
        /// <param name="id">the wish to delete</param>
        /// <returns>If the user is in the event and owns the wish: A page to delete the wish</returns>
        public ActionResult Delete(int id)
        {
            //Checks if the user owns the wish and is in the event
            MeetupModel model = new MeetupModel();
            int? infoID = this.UserId();
            if(infoID is null)
            {
                return RedirectToAction("Index", "Home");
            }

            WishDeleteModel viewModel = new WishDeleteModel();
            viewModel.WishInformation = model.Wishes.SingleOrDefault(w => w.Id == id && w.UserId == infoID);
            if(viewModel.WishInformation is null)
            {
                return RedirectToAction("Index", "Home");
            }
            viewModel.WishId = viewModel.WishInformation.Id;

            viewModel.EventInformation = viewModel.WishInformation.Event;
            if(!viewModel.EventInformation.Invites.Any(u => u.UserId == infoID))
            {
                return RedirectToAction("Index", "Home");
            }
            viewModel.EventId = viewModel.EventInformation.Id;

            return View(viewModel);
        }

        /// <summary>
        /// An action deleting a wish
        /// </summary>
        /// <param name="viewModel">The posted model containing information about the wish to delete</param>
        /// <returns>if the user is in the event: The list of all the wishes the user has for the event</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(WishDeleteModel viewModel)
        {
            MeetupModel model = new MeetupModel();

            //Check if user is in the event
            int? infoID = this.UserId();
            if(infoID is null)
            {
                return RedirectToAction("Index", "Home");
            }
            Event theEvent = model.Events.SingleOrDefault(e => e.Id == viewModel.EventId && e.Invites.Any(u => u.UserId == infoID));
            if(theEvent is null)
            {
                return RedirectToAction("Index", "Home");
            }

            //Check if the user owns the wish
            Wish theWish = model.Wishes.SingleOrDefault(w => w.Id == viewModel.WishId && w.UserId == infoID);
            if(theWish is null)
            {
                return RedirectToAction("List", new { viewModel.EventId });
            }

            //Delete the wish
            model.WishBusinesses.RemoveRange(theWish.WishBusinesses);
            model.WishInterests.RemoveRange(theWish.WishInterests);
            model.Wishes.Remove(theWish);
            model.SaveChanges();

            return RedirectToAction("List", new { Id = viewModel.EventId });
        }

        /// <summary>
        /// Outputs a <see cref="WishEditCreateModel"/> view model filled with possible values the user can wish for
        /// </summary>
        /// <param name="eventWithData">The event the <see cref="WishEditCreateModel"/> should get information from.</param>
        /// <returns>Returns a <see cref="WishEditCreateModel"/> with <see cref="WishEditCreateModel.UnchosenBusinesses"/>, <see cref="WishEditCreateModel.UnchosenInterests"/> and <see cref="WishEditCreateModel.Organizations"/></returns>
        [NonAction]
        private static WishEditCreateModel GetFilledWishCreateEditModel(Event eventWithData)
        {
            WishEditCreateModel returnModel = new WishEditCreateModel();
            returnModel.EventInformation = eventWithData;
            returnModel.EventId = eventWithData.Id;
            List<User> usersInEvent = eventWithData.GetUsers().ToList();

            //Create interest list
            returnModel.UnchosenInterests = new List<Interest>();
            foreach(User user in usersInEvent)
            {
                returnModel.UnchosenInterests.AddRange(user.GetInterests());
            }
            returnModel.UnchosenInterests = returnModel.UnchosenInterests.Distinct().ToList();

            //Create business list
            returnModel.UnchosenBusinesses = new List<Business>();
            foreach(User user in usersInEvent)
            {
                returnModel.UnchosenBusinesses.AddRange(user.GetBusinesses());
            }
            returnModel.UnchosenBusinesses = returnModel.UnchosenBusinesses.Distinct().ToList();

            FillModelWithUsersAndOrganizations(returnModel, usersInEvent);

            return returnModel;
        }

        /// <summary>
        /// Fills the given <see cref="WishEditCreateModel"/> with the possible organizations and users
        /// </summary>
        /// <param name="viewModel">the <see cref="WishEditCreateModel"/> to fill</param>
        /// <param name="usersInEvent">a list of users in the event</param>
        /// <returns>Returns the given <paramref name="viewModel"/> with the information</returns>
        [NonAction]
        private static WishEditCreateModel FillModelWithUsersAndOrganizations(WishEditCreateModel viewModel, List<User> usersInEvent)
        {
            //Create organization list
            viewModel.Organizations = new List<Organization>();
            foreach(User user in usersInEvent)
            {
                viewModel.Organizations.AddRange(user.GetOrganizations());
            }
            viewModel.Organizations = viewModel.Organizations.Distinct().ToList();

            //Create user SelectListItem list
            viewModel.UsersInEvent = new List<SelectListItem>();
            foreach(User user in usersInEvent)
            {
                viewModel.UsersInEvent.Add(new SelectListItem { Text = user.FullName, Value = user.FullName, Selected = false });
            }

            return viewModel;
        }
    }
}