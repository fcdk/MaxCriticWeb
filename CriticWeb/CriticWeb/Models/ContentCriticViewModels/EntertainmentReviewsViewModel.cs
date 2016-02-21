using CriticWeb.DataLayer;
using CriticWeb.Models.Data;
using System;
using System.Linq;

namespace CriticWeb.Models.ContentCriticViewModels
{
    public class EntertainmentReviewsViewModel
    {
        public bool IsCritic { get; private set; }
        public Review[] AllEntertainmentReviews { get; private set; }
        public EntertainmentVM EntertainmentViewModel { get; private set; }

        public EntertainmentReviewsViewModel(Guid id, bool isCritic)
        {
            EntertainmentViewModel = new EntertainmentVM(Entertainment.GetById(id));
            IsCritic = isCritic;
            if (IsCritic)
                AllEntertainmentReviews = Review.GetReviewByEntertainment(EntertainmentViewModel.EntertainmentDL).Where( (rev) => rev.Publication != null && rev.Publication != String.Empty).ToArray();
            else AllEntertainmentReviews = Review.GetReviewByEntertainment(EntertainmentViewModel.EntertainmentDL).Where((rev) => (rev.Publication == null || rev.Publication == String.Empty) && rev.CheckedByAdmin == true).ToArray();
        }
    }
}