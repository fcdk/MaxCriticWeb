using CriticWeb.DataLayer;
using System;
using System.Linq;

namespace CriticWeb.Models.AdminViewModels
{
    public class ReviewsChekingViewModel
    {
        public Review[] UncheckedReviews { get; private set; }
        public Guid PaginationId { get; private set; }

        public ReviewsChekingViewModel()
        {
            UncheckedReviews = Review.GetUncheckedReviews()?.OrderBy( (rev) => rev.Time )?.ToArray();
            PaginationId = Guid.NewGuid();
        }
    }
}
