namespace EPAM.Nacheku.Logic
{
    using System.Collections.Generic;
    using EPAM.Nacheku.DAL.MSSQL;

    public class LogicSecurity
    {
        public static List<string> GetAllRoles()
        {
            return DataSecurity.GetAllRoles();
        }

        public static string[] GetRolesForUser(string name)
        {
            return DataSecurity.GetRolesForUser(name);
        }
    }
}
