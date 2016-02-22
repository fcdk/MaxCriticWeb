using CriticWeb.DataLayerEF;
using System;
using System.Linq;

namespace CriticWeb.Models.AdminViewModels
{
    public class UsersAdministratingViewModel
    {
        public UserCritic[] UsersCritic { get; private set; }
        public Guid PaginationId { get; private set; }

        public UsersAdministratingViewModel(string username = null)
        {
            using (maxcriticEntities context = new maxcriticEntities())
            {
                if (username == null)
                    UsersCritic = (from user in context.UserCritics
                                   select user)?.Take(50).OrderBy( u => u.Username ).ToArray();
            }
            PaginationId = Guid.NewGuid();
        }

    }
}