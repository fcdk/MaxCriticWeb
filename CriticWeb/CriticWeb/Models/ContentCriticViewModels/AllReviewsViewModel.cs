using CriticWeb.DataLayer;
using System;

namespace CriticWeb.Models.ContentCriticViewModels
{
    public class AllReviewsViewModel
    {
        public Review[] Reviews { get; private set; }
        public Guid PaginationId { get; private set; }

        public AllReviewsViewModel(Review[] reviews, Guid paginationId)
        {
            Reviews = reviews;
            PaginationId = paginationId;
        }
    }
}