using CriticWeb.DataLayer;
using CriticWeb.Models.Data;
using System.Collections.Generic;

namespace CriticWeb.Models.ContentCriticViewModels
{
    public class PerformerMiniViewModel
    {
        private PerformerVM _performer;
        public PerformerVM Performer
        {
            get{ return _performer; }
        }

        public int? AvaragePerformerPoint { get; private set; }

        public EntertainmentVM[] LastThreePerformersEntertainments { get; private set; }

        public PerformerMiniViewModel(PerformerVM performer, PerformerInEntertainment.Role role)
        {
            _performer = performer;

            Entertainment[] entertainmentByPerformer = Entertainment.GetEntertainmentByPerformer(performer.PerformerDL);
            if (entertainmentByPerformer != null)
                AvaragePerformerPoint = Entertainment.AverageCriticPointForEntertainments(entertainmentByPerformer);

            if (role == PerformerInEntertainment.Role.MovieCast)
            {
                Entertainment[] lastThreeActorsEntertainments = Entertainment.GetLastThreeEntertainmentByActor(performer.PerformerDL);
                if (lastThreeActorsEntertainments != null)
                {
                    List<EntertainmentVM> result = new List<EntertainmentVM>();
                    foreach (var entertainment in lastThreeActorsEntertainments)
                        result.Add(new EntertainmentVM(entertainment));
                    LastThreePerformersEntertainments = result.ToArray();
                }
            }
            if (role == PerformerInEntertainment.Role.AlbumSinger)
            {
                Entertainment[] lastThreeActorsEntertainments = Entertainment.GetLastThreeEntertainmentBySinger(performer.PerformerDL);
                if (lastThreeActorsEntertainments != null)
                {
                    List<EntertainmentVM> result = new List<EntertainmentVM>();
                    foreach (var entertainment in lastThreeActorsEntertainments)
                        result.Add(new EntertainmentVM(entertainment));
                    LastThreePerformersEntertainments = result.ToArray();
                }
            }
        }
    }
}