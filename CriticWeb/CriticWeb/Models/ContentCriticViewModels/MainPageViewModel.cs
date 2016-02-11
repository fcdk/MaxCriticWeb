using CriticWeb.DataLayer;
using CriticWeb.Models.Data;
using System.Collections.Generic;
using System.Linq;

namespace CriticWeb.Models.ContentCriticViewModels
{
    public class MainPageViewModel
    {
        private EntertainmentVM[] _lastBestMovies;
        private EntertainmentVM[] _lastBestGames;
        private EntertainmentVM[] _lastBestTVSeries;
        private EntertainmentVM[] _lastBestAlbums;
        private PerformerVM[] _lastTwoActors;
        private PerformerVM[] _lastTwoSingers;

        public EntertainmentVM[] LastBestMovies
        {
            get { return _lastBestMovies; }
        }
        public EntertainmentVM[] GetLastBestMovies()
        {
            List<EntertainmentVM> result = new List<EntertainmentVM>();
            foreach (Entertainment entertainment in Entertainment.GetLastBestEntertainmentByType(Entertainment.Type.Movie))
                result.Add(new EntertainmentVM(entertainment));
            return result.OrderByDescending(ent => ent.EntertainmentDL.AverageCriticPointForOneEntertainment()).ToArray();
        }


        public EntertainmentVM[] LastBestGames
        {
            get { return _lastBestGames; }
        }
        public EntertainmentVM[] GetLastBestGames()
        {
            List<EntertainmentVM> result = new List<EntertainmentVM>();
            foreach (Entertainment entertainment in Entertainment.GetLastBestEntertainmentByType(Entertainment.Type.Game))
                result.Add(new EntertainmentVM(entertainment));
            return result.OrderByDescending(ent => ent.EntertainmentDL.AverageCriticPointForOneEntertainment()).ToArray();
        }

        public EntertainmentVM[] LastBestTVSeries
        {
            get { return _lastBestTVSeries; }
        }
        public EntertainmentVM[] GetLastBestTVSeries()
        {
            List<EntertainmentVM> result = new List<EntertainmentVM>();
            foreach (Entertainment entertainment in Entertainment.GetLastBestEntertainmentByType(Entertainment.Type.TVSeries))
                result.Add(new EntertainmentVM(entertainment));
            return result.OrderByDescending(ent => ent.EntertainmentDL.AverageCriticPointForOneEntertainment()).ToArray();
        }

        public EntertainmentVM[] LastBestAlbums
        {
            get { return _lastBestAlbums; }
        }
        public EntertainmentVM[] GetLastBestAlbums()
        {
            List<EntertainmentVM> result = new List<EntertainmentVM>();
            foreach (Entertainment entertainment in Entertainment.GetLastBestEntertainmentByType(Entertainment.Type.Album))
                result.Add(new EntertainmentVM(entertainment));
            return result.OrderByDescending(ent => ent.EntertainmentDL.AverageCriticPointForOneEntertainment()).ToArray();
        }

        public PerformerVM[] LastTwoActors
        {
            get { return _lastTwoActors; }
        }
        public PerformerVM[] GetLastTwoActors()
        {
            List<Performer> allActors = new List<Performer>();
            foreach (var entertainment in _lastBestMovies)
            {
                Performer[] allEntertainmentsActors = Performer.GetActorByEntertainment(entertainment.EntertainmentDL);
                if (allEntertainmentsActors != null)
                {
                    allActors.AddRange(allEntertainmentsActors);
                }              
            }

            Performer[] twoPerformers = allActors.OrderByDescending(actor => Entertainment.AverageCriticPointForEntertainments(Entertainment.GetEntertainmentByActor(actor))).Take(2).ToArray();

            List<PerformerVM> result = new List<PerformerVM>();
            foreach (var performer in twoPerformers)
                result.Add(new PerformerVM(performer));

            return result.ToArray();
        }

        public PerformerVM[] LastTwoSingers
        {
            get { return _lastTwoSingers; }
        }
        public PerformerVM[] GetLastTwoSingers()
        {
            List<Performer> allSingers = new List<Performer>();
            foreach (var entertainment in _lastBestAlbums)
            {
                Performer[] allEntertainmentsSingers = Performer.GetSingerByEntertainment(entertainment.EntertainmentDL);
                if (allEntertainmentsSingers != null)
                {
                    allSingers.AddRange(allEntertainmentsSingers);
                }
            }

            Performer[] twoPerformers = allSingers.OrderByDescending(singer => Entertainment.AverageCriticPointForEntertainments(Entertainment.GetEntertainmentBySinger(singer))).Take(2).ToArray();

            List<PerformerVM> result = new List<PerformerVM>();
            foreach (var performer in twoPerformers)
                result.Add(new PerformerVM(performer));

            return result.ToArray();
        }

        public MainPageViewModel()
        {
            _lastBestMovies = GetLastBestMovies();
            _lastBestGames = GetLastBestGames();
            _lastBestTVSeries = GetLastBestTVSeries();
            _lastBestAlbums = GetLastBestAlbums();
            _lastTwoActors = GetLastTwoActors();
            _lastTwoSingers = GetLastTwoSingers();
        }

    }
}