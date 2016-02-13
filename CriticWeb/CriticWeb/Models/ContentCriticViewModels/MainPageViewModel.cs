using CriticWeb.DataLayer;
using CriticWeb.Models.Data;
using System.Collections.Generic;
using System.Linq;

namespace CriticWeb.Models.ContentCriticViewModels
{
    public class MainPageViewModel
    {
        const uint _reviewCountForDisplayOnTop = 2;
        const uint _reviewCountForDisplayOnBottom = 1;

        private EntertainmentVM[] _lastBestMovies;
        private EntertainmentVM[] _lastBestGames;
        private EntertainmentVM[] _lastBestTVSeries;
        private EntertainmentVM[] _lastBestAlbums;
        private PerformerVM[] _lastTwoActors;
        private PerformerVM[] _lastTwoSingers;
        private EntertainmentVM[] _lastMovies;
        private EntertainmentVM[] _lastGames;
        private EntertainmentVM[] _lastTVSeries;
        private EntertainmentVM[] _lastAlbums;

        public EntertainmentVM[] LastBestMovies
        {
            get { return _lastBestMovies; }
        }

        public EntertainmentVM[] LastBestGames
        {
            get { return _lastBestGames; }
        }

        public EntertainmentVM[] LastBestTVSeries
        {
            get { return _lastBestTVSeries; }
        }

        public EntertainmentVM[] GetLastBestEntertainment(Entertainment.Type type)
        {
            List<EntertainmentVM> result = new List<EntertainmentVM>();
            Entertainment[] lastNEntertainmentByTypeAndReviewCount = Entertainment.GetLastNEntertainmentByTypeAndReviewCount(type, 4, _reviewCountForDisplayOnTop);
            if (lastNEntertainmentByTypeAndReviewCount == null)
                return null;
            foreach (Entertainment entertainment in lastNEntertainmentByTypeAndReviewCount)
                result.Add(new EntertainmentVM(entertainment));
            return result.OrderByDescending(ent => ent.EntertainmentDL.AverageCriticPointForOneEntertainment()).ToArray();
        }

        public EntertainmentVM[] LastBestAlbums
        {
            get { return _lastBestAlbums; }
        }

        public PerformerVM[] LastTwoActors
        {
            get { return _lastTwoActors; }
        }
        public PerformerVM[] GetLastTwoActors()
        {
            List<Performer> allActors = new List<Performer>();
            if (_lastBestMovies == null)
                return null;
            foreach (var entertainment in _lastBestMovies)
            {
                Performer[] allEntertainmentsActors = Performer.GetActorByEntertainment(entertainment.EntertainmentDL);
                if (allEntertainmentsActors != null)
                {
                    foreach (var actor in allEntertainmentsActors)
                    {
                        if(allActors.FindAll( (act) => act.Id == actor.Id ).Count == 0 )
                        {
                            allActors.Add(actor);
                        }
                    }
                }
                if (allActors.Count == 0)
                    return null;
            }

            Performer[] twoPerformers = allActors.OrderByDescending(actor => Entertainment.AverageCriticPointForEntertainments(Entertainment.GetEntertainmentByPerformer(actor))).Take(2).ToArray();

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
            if (_lastBestAlbums == null)
                return null;
            foreach (var entertainment in _lastBestAlbums)
            {
                Performer[] allEntertainmentsSingers = Performer.GetSingerByEntertainment(entertainment.EntertainmentDL);
                if (allEntertainmentsSingers != null)
                {
                    foreach (var singer in allEntertainmentsSingers)
                    {
                        if (allSingers.FindAll( (sing) => sing.Id == singer.Id ).Count == 0)
                        {
                            allSingers.Add(singer);
                        }
                    }
                }
                if (allSingers.Count == 0)
                    return null;
            }

            Performer[] twoPerformers = allSingers.OrderByDescending(singer => Entertainment.AverageCriticPointForEntertainments(Entertainment.GetEntertainmentByPerformer(singer))).Take(2).ToArray();

            List<PerformerVM> result = new List<PerformerVM>();
            foreach (var performer in twoPerformers)
                result.Add(new PerformerVM(performer));

            return result.ToArray();
        }

        public EntertainmentVM[] LastMovies
        {
            get { return _lastMovies; }
        }

        public EntertainmentVM[] LastGames
        {
            get { return _lastGames; }
        }

        public EntertainmentVM[] LastTVSeries
        {
            get { return _lastTVSeries; }
        }

        public EntertainmentVM[] LastAlbums
        {
            get { return _lastAlbums; }
        }

        public EntertainmentVM[] GetLastEntertainment(Entertainment.Type type)
        {
            List<EntertainmentVM> result = new List<EntertainmentVM>();
            Entertainment[] lastNEntertainmentByTypeAndReviewCount = Entertainment.GetLastNEntertainmentByTypeAndReviewCount(type, 10, _reviewCountForDisplayOnBottom);
            if (lastNEntertainmentByTypeAndReviewCount == null)
                return null;
            foreach (Entertainment entertainment in lastNEntertainmentByTypeAndReviewCount)
                result.Add(new EntertainmentVM(entertainment));
            return result.OrderByDescending(ent => ent.EntertainmentDL.AverageCriticPointForOneEntertainment()).ToArray();
        }

        public MainPageViewModel()
        {
            _lastBestMovies = GetLastBestEntertainment(Entertainment.Type.Movie);
            _lastBestGames = GetLastBestEntertainment(Entertainment.Type.Game);
            _lastBestTVSeries = GetLastBestEntertainment(Entertainment.Type.TVSeries);
            _lastBestAlbums = GetLastBestEntertainment(Entertainment.Type.Album);
            _lastTwoActors = GetLastTwoActors();
            _lastTwoSingers = GetLastTwoSingers();
            _lastMovies = GetLastEntertainment(Entertainment.Type.Movie);
            _lastGames = GetLastEntertainment(Entertainment.Type.Game);
            _lastTVSeries = GetLastEntertainment(Entertainment.Type.TVSeries);
            _lastAlbums = GetLastEntertainment(Entertainment.Type.Album);
        }

    }
}