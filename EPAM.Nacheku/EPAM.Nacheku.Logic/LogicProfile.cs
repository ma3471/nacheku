using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPAM.Nacheku.Logic
{
    using DAL.MSSQL;
    using Entities;

    public class LogicProfile
    {

        internal static void AddNewProfile(Profile profile)
        {
            DataProfile.AddProfile(profile);
        }

        public static void UpdateProfile(Profile profile)
        {
            DataProfile.UpdateProfile(profile);
        }

        public static Profile GetProfileByUserId(int userId)
        {
            return DataProfile.GetProfileByUserId(userId);
        }

    }
}
