﻿namespace EPAM.Nacheku.UI.WebPages.App_Code.Security
{
    using System;
    using System.Web.Security;
    using EPAM.Nacheku.Logic;

    public class MyRoleProvider : RoleProvider
    {

        public override string[] GetAllRoles()
        {
            return Logic.LogicSecurity.GetAllRoles().ToArray();
        }

        public override string[] GetRolesForUser(string username)
        {
            return Logic.LogicSecurity.GetRolesForUser(username);
        }
        #region not Implemented
        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }
        #endregion
    }
}