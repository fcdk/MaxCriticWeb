using CriticWeb.DataLayer;
using System.ComponentModel.DataAnnotations;

namespace CriticWeb.Annotations
{
    public class UserNameEditAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value != null)
            {
                UserCritic[] users = UserCritic.GetByName(value.ToString());
                if (users == null || users[0].Username == ProfileCritic.Instance.CurrentUserCritic.Username)
                    return true;
            }
            return false;
        }
    }
}