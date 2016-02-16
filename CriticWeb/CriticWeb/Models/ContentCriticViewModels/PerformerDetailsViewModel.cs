using CriticWeb.DataLayer;
using CriticWeb.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CriticWeb.Models.ContentCriticViewModels
{
    public class PerformerDetailsViewModel
    {
        public PerformerVM PerformerViewModel{ get; private set; }

        public int? AvarageCriticPoint { get; private set; }
        public int? AvarageUserPoint { get; private set; }

        public string AvardsString { get; private set; }

        public EntertainmentVM[] EntertainmentVMs { get; private set; }
        public EntertainmentVM[] EntertainmentVMByDate { get; private set; }

        public PerformerDetailsViewModel(Guid id)
        {
            PerformerViewModel = new PerformerVM(Performer.GetById(id));

            Entertainment[] entertainmentByPerformer = Entertainment.GetEntertainmentByPerformer(PerformerViewModel.PerformerDL);
            if (entertainmentByPerformer != null)
            {
                AvarageCriticPoint = Entertainment.AverageCriticPointForEntertainments(entertainmentByPerformer);
                AvarageUserPoint = Entertainment.AverageUserPointForEntertainments(entertainmentByPerformer);
            }

            AvardsString = this.GetAwardStringByPerfomer();


            EntertainmentVMByDate = this.GetEntertainmentVMByPerformer().OrderByDescending( (ent) => ent.ReleaseDate ).ToArray();
        }

        private string GetAwardStringByPerfomer()
        {
            Award[] awards = Award.GetAwardByPerformer(PerformerViewModel.PerformerDL);
            if (awards == null)
                return null;

            string result = "";
            foreach (var award in awards)
            {
                AwardVM awardVM = new AwardVM(award);
                result += awardVM.ToString();
                result += ", ";
            }
            return result.Remove(result.Length - 2, 2);
        }

        private EntertainmentVM[] GetEntertainmentVMByPerformer()
        {
            Entertainment[] entertainments = Entertainment.GetEntertainmentByPerformer(PerformerViewModel.PerformerDL);

            if (entertainments == null)
                return null;

            List<EntertainmentVM> result = new List<EntertainmentVM>();
            foreach (var entertainment in entertainments)            
                result.Add(new EntertainmentVM(entertainment));

            return result.ToArray();
        }

    }
}