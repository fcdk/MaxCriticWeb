using CriticWeb.DataLayerEF;
using System;
using System.Linq;

namespace CriticWeb.Models.ContentCriticViewModels
{
    public class UserProfileViewModel
    {
        public UserCritic user { get; private set; }

        public UserProfileViewModel(Guid id)
        {
            using (maxcriticEntities context = new maxcriticEntities())
            {
                UserCritic[] users = (from u in context.UserCritics.AsParallel()
                                      where u.UserId == id
                                      select u).ToArray();
                if (users.Length == 1)
                    user = users[0];
            }
        }
    }
}
