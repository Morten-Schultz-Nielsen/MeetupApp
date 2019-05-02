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

        /// <summary>
        /// Creates a new meeting score from 2 <see cref="User"/>s
        /// </summary>
        /// <param name="person1">one of the <see cref="User"/>s in this meeting</param>
        /// <param name="person2">one of the <see cref="User"/>s in this meeting</param>
        /// <param name="eventId">The Id of the <see cref="Event"/> with this meeting</param>
        public MeetingScore(int eventId, User person1, User person2)
        {
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
                return 0;
            }
            return 1;
        }
    }
}
