using CriticWeb.Models.Data;

namespace CriticWeb.Models.ContentCriticViewModels
{
    public class AllEntertainmentsAndPerformersViewModel
    {
        public EntertainmentVM[] Entertainments { get; private set; }
        public PerformerVM[] Performers { get; private set; }
        public string nameForSearch { get; set; }

        public AllEntertainmentsAndPerformersViewModel(EntertainmentVM[] entertainments, PerformerVM[] performers)
        {
            
        }
    }
}