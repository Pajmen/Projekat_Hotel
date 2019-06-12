using Projekat_Hotel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Projekat_Hotel.MyRoleProvider
{
    public class Privilegije : RoleProvider
    {
       
            public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

            public override void AddUsersToRoles(string[] usernames, string[] roleNames)
            {
                throw new NotImplementedException();
            }

            public override void CreateRole(string roleName)
            {
                throw new NotImplementedException();
            }

            public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
            {
                throw new NotImplementedException();
            }

            public override string[] FindUsersInRole(string roleName, string usernameToMatch)
            {
                throw new NotImplementedException();
            }

            public override string[] GetAllRoles()
            {
                throw new NotImplementedException();
            }


            public override string[] GetRolesForUser(string username)
            {
                using (Hotel_DREntities db = new Hotel_DREntities())
                {
                    //string rola = (from r in db.Radniks where r.KorisnickoIme == username select r.Uloga.NazivUloge).FirstOrDefault();
                    string uloga = db.Radniks.Where(x => x.Uloga.NazivUloge == username).FirstOrDefault().Uloga.NazivUloge;
                    string[] result = { uloga };
                    return result;
                }

            }

            public override string[] GetUsersInRole(string roleName)
            {
                throw new NotImplementedException();
            }

            public override bool IsUserInRole(string username, string roleName)
            {
                throw new NotImplementedException();
            }

            public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
            {
                throw new NotImplementedException();
            }

            public override bool RoleExists(string roleName)
            {
                throw new NotImplementedException();
            }
        
    }
}