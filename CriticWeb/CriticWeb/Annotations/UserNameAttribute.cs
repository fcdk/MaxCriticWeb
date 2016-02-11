using CriticWeb.DataLayer;
using System.ComponentModel.DataAnnotations;

namespace CriticWeb.Annotations
{
    public class UserNameAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value != null)
            {
                UserCritic[] users = UserCritic.GetByName(value.ToString());
                if (users == null)
                    return true;
            }
            return false;
        }
    }
}