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
        public EntertainmentVM[] Movies { get; private set; }
        public EntertainmentVM[] Games { get; private set; }
        public EntertainmentVM[] TVSeries { get; private set; }
        public EntertainmentVM[] Albums { get; private set; }
        public EntertainmentVM[] MoviesByDate { get; private set; }
        public EntertainmentVM[] GamesByDate { get; private set; }
        public EntertainmentVM[] TVSeriesByDate { get; private set; }
        public EntertainmentVM[] AlbumsByDate { get; private set; }
        public EntertainmentVM[] MoviesByCriticPoint { get; private set; }
        public EntertainmentVM[] GamesByCriticPoint { get; private set; }
        public EntertainmentVM[] TVSeriesByCriticPoint { get; private set; }
        public EntertainmentVM[] AlbumsByCriticPoint { get; private set; }

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

            EntertainmentVMs = this.GetEntertainmentVMByPerformer();
            Movies = Array.FindAll(EntertainmentVMs, (ent) => ent.EntertainmentType == Entertainment.Type.Movie ).ToArray();
            Games = Array.FindAll(EntertainmentVMs, (ent) => ent.EntertainmentType == Entertainment.Type.Game).ToArray();
            TVSeries = Array.FindAll(EntertainmentVMs, (ent) => ent.EntertainmentType == Entertainment.Type.TVSeries).ToArray();
            Albums = Array.FindAll(EntertainmentVMs, (ent) => ent.EntertainmentType == Entertainment.Type.Album).ToArray();

            MoviesByDate = Movies.OrderByDescending((ent) => ent.ReleaseDate).ToArray();
            GamesByDate = Games.OrderByDescending((ent) => ent.ReleaseDate).ToArray();
            TVSeriesByDate = TVSeries.OrderByDescending((ent) => ent.ReleaseDate).ToArray();
            AlbumsByDate = Albums.OrderByDescending((ent) => ent.ReleaseDate).ToArray();

            MoviesByCriticPoint = Movies.OrderByDescending((ent) => ent.EntertainmentDL.AverageCriticPointForOneEntertainment()).ToArray();
            GamesByCriticPoint = Games.OrderByDescending((ent) => ent.EntertainmentDL.AverageCriticPointForOneEntertainment()).ToArray();
            TVSeriesByCriticPoint = TVSeries.OrderByDescending((ent) => ent.EntertainmentDL.AverageCriticPointForOneEntertainment()).ToArray();
            AlbumsByCriticPoint = Albums.OrderByDescending((ent) => ent.EntertainmentDL.AverageCriticPointForOneEntertainment()).ToArray();
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