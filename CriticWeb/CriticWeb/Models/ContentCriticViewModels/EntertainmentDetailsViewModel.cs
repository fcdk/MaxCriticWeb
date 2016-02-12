using CriticWeb.DataLayer;
using CriticWeb.Models.Data;
using System;

namespace CriticWeb.Models.ContentCriticViewModels
{
    public class EntertainmentDetailsViewModel
    {
        private EntertainmentVM _entertainment;
        private int? _avarageCriticPoint;
        private int? _avarageUserPoint;
        private string _trailerLinkForFrame;
        private string _movieProductions;
        private string _gameDeveloperCompanys;
        private string _gamePlatforms;
        private string _tVNetwork;
        private string _albumRecordLabel;
        private string _genres;

        public EntertainmentVM EntertainmentDetails
        {
            get { return _entertainment; }
        }

        public int? AvarageCriticPoint
        {
            get { return _avarageCriticPoint; }
        }

        public int? AvarageUserPoint
        {
            get { return _avarageUserPoint; }
        }

        public string TrailerLinkForFrame
        {
            get { return _trailerLinkForFrame; }
        } 

        public string MovieProductions
        {
            get { return _movieProductions; }
        }

        public string GameDeveloperCompanys
        {
            get { return _gameDeveloperCompanys; }
        }

        public string GamePlatforms
        {
            get { return _gamePlatforms; }
        }

        public string TVNetwork
        {
            get { return _tVNetwork; }
        }

        public string AlbumRecordLabel
        {
            get { return _albumRecordLabel; }
        }

        public string Genres
        {
            get { return _genres; }
        }
        
        public EntertainmentDetailsViewModel(Guid entertainmentId)
        {
            _entertainment = new EntertainmentVM(Entertainment.GetById(entertainmentId));
            _avarageCriticPoint = _entertainment.EntertainmentDL.AverageCriticPointForOneEntertainment();
            _avarageUserPoint = _entertainment.EntertainmentDL.AverageUserPointForOneEntertainment();
            _trailerLinkForFrame = _entertainment.TrailerLink.Replace("https://www.youtube.com/watch?v=", "https://www.youtube.com/embed/");

            _movieProductions = this.GetPerformersStringByRole(PerformerInEntertainment.Role.MovieProduction);
            _gameDeveloperCompanys = this.GetPerformersStringByRole(PerformerInEntertainment.Role.GameDeveloperCompany);
            _gamePlatforms = this.GetPerformersStringByRole(PerformerInEntertainment.Role.GamePlatform);
            _tVNetwork = this.GetPerformersStringByRole(PerformerInEntertainment.Role.TVNetwork);
            _albumRecordLabel = this.GetPerformersStringByRole(PerformerInEntertainment.Role.AlbumRecordLabel);
            _genres = this.GetGenreString();
        }

        private string GetPerformersStringByRole(PerformerInEntertainment.Role role)
        {
            Performer[] performers = Performer.GetPerformerByEntertainmentAndRole(_entertainment.EntertainmentDL, role);
            if (performers == null)
                return null;

            string result = "";
            foreach (var performer in performers)
            {
                PerformerVM performerVM = new PerformerVM(performer);
                result += performerVM.ToString();
                result += ", ";
            }
            return result.Remove(result.Length - 2, 2);           
        }

        private string GetGenreString()
        {
            Genre[] genres = Genre.GetGenreByEntertainment(_entertainment.EntertainmentDL);
            if (genres == null)
                return null;

            string result = "";
            foreach (var genre in genres)
            {
                GenreVM genreVM = new GenreVM(genre);
                result += genreVM.ToString();
                result += ", ";
            }
            return result.Remove(result.Length - 2, 2);
        }

    }
}