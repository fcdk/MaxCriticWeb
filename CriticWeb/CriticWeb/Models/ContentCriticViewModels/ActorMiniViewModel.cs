using CriticWeb.DataLayer;
using CriticWeb.Models.Data;
using System.Collections.Generic;

namespace CriticWeb.Models.ContentCriticViewModels
{
    public class ActorMiniViewModel
    {
        private PerformerVM _performer;
        public PerformerVM Performer
        {
            get{ return _performer; }
        }

        public int? AvarageActorPoint { get; }

        public EntertainmentVM[] LastThreeActorsEntertainments { get; }

        public ActorMiniViewModel(PerformerVM performer)
        {
            _performer = performer;

            Entertainment[] allActorsEntertainments = Entertainment.GetEntertainmentByActor(performer.PerformerDL);
            AvarageActorPoint = Entertainment.AverageCriticPointForEntertainments(allActorsEntertainments);
            Entertainment[] lastThreeActorsEntertainments = Entertainment.GetLastThreeEntertainmentByActor(performer.PerformerDL);
            List<EntertainmentVM> result = new List<EntertainmentVM>();
            foreach (var entertainment in lastThreeActorsEntertainments)
                result.Add(new EntertainmentVM(entertainment));
            LastThreeActorsEntertainments = result.ToArray();
        }
    }
}