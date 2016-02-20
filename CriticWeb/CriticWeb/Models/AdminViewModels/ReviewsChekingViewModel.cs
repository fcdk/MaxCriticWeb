using CriticWeb.DataLayer;

namespace CriticWeb.Models.AdminViewModels
{
    public class ReviewsChekingViewModel
    {
        public Review[] ReviewsUnchecked { get; private set; }

        public ReviewsChekingViewModel()
        {
            ReviewsUnchecked = Review.GetUncheckedReviews();
        }  
    }
}
