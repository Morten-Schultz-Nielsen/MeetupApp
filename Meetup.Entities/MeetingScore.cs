using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meetup.Entities
{
    /// <summary>
    /// An <see cref="object"/> holding the happiness score for a meeting
    /// </summary>
    public class MeetingScore
    {
        private User person1;
        private User person2;
        private readonly int eventId;

        /// <summary>
        /// Creates a new meeting score from 2 <see cref="User"/>s
        /// </summary>
        /// <param name="person1">one of the <see cref="User"/>s in this meeting</param>
        /// <param name="person2">one of the <see cref="User"/>s in this meeting</param>
        /// <param name="eventId">The Id of the <see cref="Event"/> with this meeting</param>
        public MeetingScore(int eventId, User person1, User person2)
        {
            this.eventId = eventId;

            Person1 = person1;
            Person2 = person2;
            List<Wish> person1Wishes = person1.Wishes.Where(w => w.EventId == eventId).ToList();
            List<Wish> person2Wishes = person2.Wishes.Where(w => w.EventId == eventId).ToList();
            Score = Person1.CalculateHappinessScore(person2Wishes);
            Score += Person2.CalculateHappinessScore(person1Wishes);
        }

        /// <summary>
        /// One of the users in this meeting
        /// </summary>
        public User Person1
        {
            get
            {
                return person1;
            }
            private set
            {
                if(value is null)
                {
                    throw new ArgumentNullException(nameof(Person1), "Value may not be null.");
                }
                if(!value.Invites.Any(i => i.EventId == eventId))
                {
                    throw new ArgumentException("User must have an invite to the event", nameof(Person1));
                }
                person1 = value;
            }
        }

        /// <summary>
        /// One of the users in this meeting
        /// </summary>
        public User Person2
        {
            get
            {
                return person2;
            }
            private set
            {
                if(value is null)
                {
                    throw new ArgumentNullException(nameof(Person2), "Value may not be null.");
                }
                if(!value.Invites.Any(i => i.EventId == eventId))
                {
                    throw new ArgumentException("User must have an invite to the event", nameof(Person2));
                }
                person2 = value;
            }
        }

        /// <summary>
        /// The meeting's happiness score
        /// </summary>
        public int Score
        {
            get; private set;
        }

        /// <summary>
        /// A static method outputing which <see cref="MeetingScore"/> has the highest score
        /// </summary>
        /// <param name="meeting1">the <see cref="MeetingScore"/> to check against</param>
        /// <param name="meeting2">the <see cref="MeetingScore"/> to check with</param>
        /// <returns>-1 if <paramref name="meeting1"/> is higher than <paramref name="meeting2"/>. 0 if they are equel. 1 if <paramref name="meeting1"/> is the smallest</returns>
        public static int Sort(MeetingScore meeting1, MeetingScore meeting2)
        {
            if(meeting1 is null)
            {
                throw new ArgumentNullException(nameof(meeting1), "paramter may not be null.");
            }
            if(meeting2 is null)
            {
                throw new ArgumentNullException(nameof(meeting2), "paramter may not be null.");
            }

            if(meeting1.Score > meeting2.Score)
            {
                return -1;
            }
            else if(meeting1.Score == meeting2.Score)
            {
                //If scores are equal output -1 if meeting1 has the first invited person
                long meeting1InviteScore = meeting1.Person1.Invites.SingleOrDefault(i => i.EventId == meeting1.eventId).Time.Ticks / 2 + meeting1.Person2.Invites.SingleOrDefault(i => i.EventId == meeting1.eventId).Time.Ticks / 2;
                long meeting2InviteScore = meeting2.Person1.Invites.SingleOrDefault(i => i.EventId == meeting2.eventId).Time.Ticks / 2 + meeting2.Person2.Invites.SingleOrDefault(i => i.EventId == meeting2.eventId).Time.Ticks / 2;

                if(meeting1InviteScore < meeting2InviteScore)
                {
                    return -1;
                }
                if(meeting1InviteScore == meeting2InviteScore)
                {
                    return 0;
                }
                return 1;
            }
            return 1;
        }

        /// <summary>
        /// Creates a sorted (highest score at index 0) list of all possible meetings there can be with the given users
        /// </summary>
        /// <param name="users">the list of users to create the list from</param>
        /// <param name="eventId">The id of the event the meetings are for</param>
        /// <returns>a list of sorted <see cref="MeetingScore"/> objects showing all possible meetings</returns>
        public static List<MeetingScore> GetPossibleMeetings(List<User> users, int eventId)
        {
            if(users is null)
            {
                throw new ArgumentNullException(nameof(users), "parameter may not be null");
            }

            List<MeetingScore> possibleMeetings = new List<MeetingScore>();
            for(int i = 0; i < users.Count; i++)
            {
                for(int j = i + 1; j < users.Count; j++)
                {
                    possibleMeetings.Add(new MeetingScore(eventId, users[i], users[j]));
                }
            }
            possibleMeetings.Sort(Sort);
            return possibleMeetings;
        }
    }
}
