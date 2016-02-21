using CriticWeb.DataLayer;

namespace CriticWeb.Models.ContentCriticViewModels
{
    public class AllReviewsViewModel
    {
        public Review[] Reviews { get; private set; }

        public AllReviewsViewModel(Review[] reviews)
        {
            Reviews = reviews;
        }
    }
}