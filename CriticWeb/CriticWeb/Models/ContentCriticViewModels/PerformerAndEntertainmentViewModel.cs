using CriticWeb.DataLayer;
using CriticWeb.Models.Data;

namespace CriticWeb.Models.ContentCriticViewModels
{
    public class PerformerAndEntertainmentViewModel
    {
        public object PerformerOrEntertainment { get; private set; }
        public Content ContentType { get; private set; }
        public byte[] Image
        {
            get
            {
                PerformerVM performerVM = PerformerOrEntertainment as PerformerVM;
                if (performerVM != null)
                    return performerVM.Image;
                EntertainmentVM entertainmentVM = PerformerOrEntertainment as EntertainmentVM;
                if (entertainmentVM != null)
                    return entertainmentVM.Poster;
                return null;
            }
        }

        public int? AvarageCriticPoint
        {
            get
            {
                PerformerVM performerVM = PerformerOrEntertainment as PerformerVM;
                if (performerVM != null)
                    return Entertainment.AverageCriticPointForEntertainments(Entertainment.GetEntertainmentByPerformer(performerVM.PerformerDL));
                EntertainmentVM entertainmentVM = PerformerOrEntertainment as EntertainmentVM;
                if (entertainmentVM != null)
                    return entertainmentVM.EntertainmentDL.AverageCriticPointForOneEntertainment();
                return null;
            }
        }

        public int? AvarageUserPoint
        {
            get
            {
                PerformerVM performerVM = PerformerOrEntertainment as PerformerVM;
                if (performerVM != null)
                    return Entertainment.AverageUserPointForEntertainments(Entertainment.GetEntertainmentByPerformer(performerVM.PerformerDL));
                EntertainmentVM entertainmentVM = PerformerOrEntertainment as EntertainmentVM;
                if (entertainmentVM != null)
                    return entertainmentVM.EntertainmentDL.AverageUserPointForOneEntertainment();
                return null;
            }
        }

        public PerformerAndEntertainmentViewModel(EntertainmentVM entertainmentViewModel = null, PerformerVM performerViewModel = null)
        {
            if (entertainmentViewModel != null)
            {
                PerformerOrEntertainment = entertainmentViewModel;
                ContentType = Content.Entertainment;
            }
            if (performerViewModel != null)
            {
                PerformerOrEntertainment = performerViewModel;
                ContentType = Content.Performer;
            }         
        }

        public enum Content { Entertainment, Performer }
    }
}