using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPAM.Nacheku.Entities
{
    public class UserState
    {
        public DateTime LastActivityDateTime { get; private set; }

        public StateEnum State { get; set; }

        public int RelativeUserId { get; set; }

        public UserState(StateEnum state, int corrUserId) 
        {
            State = state;
            RelativeUserId = corrUserId;
            LastActivityDateTime = DateTime.Now;
        }
    }

    public enum StateEnum
    {
        LooksPage,
        InChat,
        InForum,
        CommentsOn
    }
}
