using CriticWeb.DataLayer;
using System.Linq;

namespace CriticWeb.Models.AdminViewModels
{
    public class ReviewsChekingViewModel
    {
        public Review[] UncheckedReviews { get; private set; }

        public ReviewsChekingViewModel()
        {
            UncheckedReviews = Review.GetUncheckedReviews().OrderByDescending( (rev) => rev.Time ).ToArray();
        }
    }
}
