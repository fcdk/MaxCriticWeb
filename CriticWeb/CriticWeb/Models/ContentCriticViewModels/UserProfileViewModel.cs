using CriticWeb.DataLayer;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Linq;

namespace CriticWeb.Models.ContentCriticViewModels
{
    public class UserProfileViewModel
    {
        public DataLayerEF.UserCritic User { get; private set; }
        public Review[] Reviews { get; private set; }
        public Guid PaginationId { get; private set; }

        public UserProfileViewModel(Guid id)
        {
            using (DataLayerEF.maxcriticEntities context = new DataLayerEF.maxcriticEntities())
            {
                DataLayerEF.UserCritic[] users = (from u in context.UserCritics.AsParallel()
                                      where u.UserId == id
                                      select u).ToArray();
                if (users.Length == 1)
                    User = users[0];

                Reviews = Review.GetReviewByUser(UserCritic.GetById(User.UserId)).Where( rev => rev.CheckedByAdmin == true ).OrderByDescending( rev => rev.Time ).ToArray();

                PaginationId = Guid.NewGuid();
            }
        }
    }
}
