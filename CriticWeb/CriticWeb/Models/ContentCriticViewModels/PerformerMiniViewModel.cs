using CriticWeb.DataLayer;
using CriticWeb.Models.Data;
using System.Collections.Generic;
using System.Linq;

namespace CriticWeb.Models.ContentCriticViewModels
{
    public class PerformerMiniViewModel
    {
        private PerformerVM _performer;
        public PerformerVM Performer
        {
            get{ return _performer; }
        }

        public int? AvaragePerformerPoint { get; }

        public EntertainmentVM[] LastThreePerformersEntertainments { get; }

        public PerformerMiniViewModel(PerformerVM performer, PerformerInEntertainment.Role role)
        {
            _performer = performer;

            if (role == PerformerInEntertainment.Role.MovieCast)
            {
                Entertainment[] allActorsEntertainments = Entertainment.GetEntertainmentByActor(performer.PerformerDL);
                AvaragePerformerPoint = Entertainment.AverageCriticPointForEntertainments(allActorsEntertainments);
                Entertainment[] lastThreeActorsEntertainments = Entertainment.GetLastThreeEntertainmentByActor(performer.PerformerDL);
                List<EntertainmentVM> result = new List<EntertainmentVM>();
                foreach (var entertainment in lastThreeActorsEntertainments)
                    result.Add(new EntertainmentVM(entertainment));
                LastThreePerformersEntertainments = result.ToArray();
            }
            if (role == PerformerInEntertainment.Role.AlbumSinger)
            {
                Entertainment[] allSingersEntertainments = Entertainment.GetEntertainmentBySinger(performer.PerformerDL);
                AvaragePerformerPoint = Entertainment.AverageCriticPointForEntertainments(allSingersEntertainments);
                Entertainment[] lastThreeActorsEntertainments = Entertainment.GetLastThreeEntertainmentBySinger(performer.PerformerDL);
                List<EntertainmentVM> result = new List<EntertainmentVM>();
                foreach (var entertainment in lastThreeActorsEntertainments)
                    result.Add(new EntertainmentVM(entertainment));
                LastThreePerformersEntertainments = result.ToArray();
            }
        }
    }
}