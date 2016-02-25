using Microsoft.AspNet.Identity;
using System.Web;

namespace CriticWeb.DataLayer
{
    public class ProfileCritic
    {
        private static ProfileCritic _instance;

        public UserCritic CurrentUserCritic
        {
            get
            {
                return UserCritic.GetByEmail(HttpContext.Current?.User?.Identity?.GetUserName());
            }
        }

        public static ProfileCritic Instance
        {
            get
            {
                if (_instance == null)
                    return _instance = new ProfileCritic();
                else return _instance;
            }
        }

        private ProfileCritic() { }
    }
}