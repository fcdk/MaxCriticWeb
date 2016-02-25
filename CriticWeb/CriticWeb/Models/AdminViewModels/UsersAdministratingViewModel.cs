using CriticWeb.App_Data.DataLayerEF;
using System;
using System.Linq;

namespace CriticWeb.Models.AdminViewModels
{
    public class UsersAdministratingViewModel
    {
        public UserCritic[] UsersCritic { get; private set; }
        public Guid PaginationId { get; private set; }
        public string usernameForSearch { get; set; }

        public UsersAdministratingViewModel(string username = null)
        {
            using (maxcriticEntities context = new maxcriticEntities())
            {
                if (username == null || username == String.Empty)
                    UsersCritic = (from user in context.UserCritics.AsParallel()
                                   select user)?.Take(50).OrderBy( u => u.Username ).ToArray();
                else
                    UsersCritic = (from user in context.UserCritics.AsParallel()
                                   where user.Username.ToLower().Contains(username.ToLower())
                                   select user)?.Take(50).OrderBy(u => u.Username).ToArray();
            }
            PaginationId = Guid.NewGuid();
        }

    }
}