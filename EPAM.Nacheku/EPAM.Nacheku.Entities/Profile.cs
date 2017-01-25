using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPAM.Nacheku.Entities
{
    public class Profile
    {
        public int UserId { get; private set; }

        public string FirstName { get; private set; }

        public string MiddleName { get; private set; }

        public string LastName { get; private set; }

        public DateTime BirthDay { get; private set; }

        public string Location { get; private set; }

        public Guid? AvatarId { get; private set; }

        public string Skill { get; private set; }

        public AgeStruct Age
        {
            get
            {
                var now = DateTime.Now;

                var years = now.Year - this.BirthDay.Year;
                var months = now.Month - this.BirthDay.Month;
                var days = now.Day - this.BirthDay.Day;

                if (months <= 0)
                {
                    years--;
                    months += 12;
                    if (days < 0)
                    {
                        months--;
                    }
                }

                if (months == 12)
                {
                    months = 0;
                    years++;
                }

                if (days < 0)
                {
                    days += DateTime.DaysInMonth(this.BirthDay.Year, this.BirthDay.Month);
                }

                return new AgeStruct(years, months, days);
            } 
        }

        public Profile(int userId, string firstName, string middleName, string lastName, DateTime birthDay)
        {
            this.UserId = userId;
            this.FirstName = firstName;
            this.MiddleName = middleName;
            this.LastName = lastName;
            this.BirthDay = birthDay;
            this.Location = null;
            this.AvatarId = null;
            this.Skill = null;
        }

        public Profile(int userId, string firstName, string middleName, string lastName, DateTime birthDay, string location, Guid? avatarId, string skill) 
            : this(userId, firstName, middleName, lastName, birthDay)
        {
            this.Location = location;
            this.AvatarId = avatarId;
            this.Skill = skill;
        }
      
        public struct AgeStruct
        {
            public int Years { get; private set; }
            public int Months { get; private set; }
            public int Days { get; private set; }

            public AgeStruct(int years, int months, int days) : this()
            {
                this.Years = years;
                this.Months = months;
                this.Days = days;
            }
        }
    }
}
