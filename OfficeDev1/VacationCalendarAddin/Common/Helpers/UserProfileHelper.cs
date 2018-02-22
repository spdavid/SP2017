using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.UserProfiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Helpers
{
    public class UserProfileHelper
    {

        public static bool IsCurrentUserManagerOf(ClientContext ctx, string loginName)
        {
            ctx.Load(ctx.Web.CurrentUser);
            ctx.ExecuteQuery();
            return  IsCurrentUserManagerOf(ctx, ctx.Web.CurrentUser, loginName);

        }

        public static bool IsCurrentUserManagerOf(ClientContext ctx, User currrentUser,  string loginName)
        {
          User user =  ctx.Web.EnsureUser(loginName);
            ctx.Load(user);
            ctx.ExecuteQuery();

            PeopleManager peopleManager = new PeopleManager(ctx);
            PersonProperties personProperties = peopleManager.GetPropertiesFor(user.LoginName);
            ctx.Load(personProperties);
            ctx.ExecuteQuery();

            var manager = personProperties.UserProfileProperties["Manager"];

            if (currrentUser.LoginName == manager)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        }
}
