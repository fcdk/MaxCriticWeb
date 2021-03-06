﻿using CriticWeb.DataLayer;
using CriticWeb.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;

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
        private string _singersAndBands;
        private SongVM[] _songs;
        private PerformerVM[] _movieDirectors;
        private PerformerVM[] _moviePlotWriters;
        private PerformerVM[] _moviePrincipalCasts;
        private PerformerVM[] _movieCasts;
        private PerformerVM[] _movieProducers;
        private PerformerVM[] _gameCasts;
        private PerformerVM[] _tVCasts;
        private PerformerVM[] _albumSingers;
        private PerformerVM[] _albumBands;
        private AwardVM[] _awards;
        private Review[] _reviews;
        private int _positiveCriticReviewCount;
        private int _neutralCriticReviewCount;
        private int _negativeCriticReviewCount;
        private int _positiveUserReviewCount;
        private int _neutralUserReviewCount;
        private int _negativeUserReviewCount;
        private Review[] _criticReviews;
        private Review[] _userReviews;

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

        public string SingersAndBands
        {
            get { return _singersAndBands; }
        }

        public SongVM[] Songs
        {
            get { return _songs; }
        }

        public PerformerVM[] MovieDirectors
        {
            get { return _movieDirectors; }
        }

        public PerformerVM[] MoviePlotWriters
        {
            get { return _moviePlotWriters; }
        }

        public PerformerVM[] MoviePrincipalCasts
        {
            get { return _moviePrincipalCasts; }
        }

        public PerformerVM[] MovieCast
        {
            get { return _movieCasts; }
        }

        public PerformerVM[] MovieProducers
        {
            get { return _movieProducers; }
        }

        public PerformerVM[] GameCasts
        {
            get { return _gameCasts; }
        }

        public PerformerVM[] TVCasts
        {
            get { return _tVCasts; }
        }

        public PerformerVM[] AlbumSingers
        {
            get { return _albumSingers; }
        }

        public PerformerVM[] AlbumBands
        {
            get { return _albumBands; }
        }

        public AwardVM[] Awards
        {
            get { return _awards; }
        }

        public Review[] Reviews
        {
            get { return _reviews; }
        }

        public int PositiveCriticReviewCount
        {
            get { return _positiveCriticReviewCount; }
        }

        public int NeutralCriticReviewCount
        {
            get { return _neutralCriticReviewCount; }
        }

        public int NegativeCriticReviewCount
        {
            get { return _negativeCriticReviewCount; }
        }

        public int PositiveUserReviewCount
        {
            get { return _positiveUserReviewCount; }
        }

        public int NeutralUserReviewCount
        {
            get { return _neutralUserReviewCount; }
        }

        public int NegativeUserReviewCount
        {
            get { return _negativeUserReviewCount; }
        }

        public Review[] CriticReviews
        {
            get { return _criticReviews; }
        }

        public Review[] UserReviews
        {
            get { return _userReviews; }
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
            _singersAndBands = _entertainment.AlbumAuthors != null ? _entertainment.AlbumAuthors.Remove(_entertainment.AlbumAuthors.Length - 2, 2) : null;

            Song[] songsInAlbum = Song.GetSongsByAlbum(_entertainment.EntertainmentDL);
            if (songsInAlbum != null)
            {
                List<SongVM> songs = new List<SongVM>();
                foreach (var song in songsInAlbum)
                    songs.Add(new SongVM(song));
                _songs = songs.ToArray();
            }

            _movieDirectors = this.GetPerformerVMByEntertainmentVMAndRole(_entertainment, PerformerInEntertainment.Role.MovieDirector);
            _moviePlotWriters = this.GetPerformerVMByEntertainmentVMAndRole(_entertainment, PerformerInEntertainment.Role.MoviePlotWriter);
            _moviePrincipalCasts = this.GetPerformerVMByEntertainmentVMAndRole(_entertainment, PerformerInEntertainment.Role.MoviePrincipalCast);
            _movieCasts = this.GetPerformerVMByEntertainmentVMAndRole(_entertainment, PerformerInEntertainment.Role.MovieCast);
            _movieProducers = this.GetPerformerVMByEntertainmentVMAndRole(_entertainment, PerformerInEntertainment.Role.MovieProducer);
            _gameCasts = this.GetPerformerVMByEntertainmentVMAndRole(_entertainment, PerformerInEntertainment.Role.GameCast);
            _tVCasts = this.GetPerformerVMByEntertainmentVMAndRole(_entertainment, PerformerInEntertainment.Role.TVCast);
            _albumSingers = this.GetPerformerVMByEntertainmentVMAndRole(_entertainment, PerformerInEntertainment.Role.AlbumSinger);
            _albumBands = this.GetPerformerVMByEntertainmentVMAndRole(_entertainment, PerformerInEntertainment.Role.AlbumBand);

            _awards = this.GetAwardByEntertainment(_entertainment.EntertainmentDL);

            _reviews = Review.GetReviewByEntertainment(_entertainment.EntertainmentDL);

            if (_reviews != null)
            {
                _positiveCriticReviewCount = (from review in _reviews.AsParallel()
                                              where review.Point >= 70 && review.Publication != null && review.Publication != String.Empty
                                              select review).Count();
                _neutralCriticReviewCount = (from review in _reviews.AsParallel()
                                             where review.Point > 35 && review.Point < 70 && review.Publication != null && review.Publication != String.Empty
                                             select review).Count();
                _negativeCriticReviewCount = (from review in _reviews.AsParallel()
                                              where review.Point <= 35 && review.Publication != null && review.Publication != String.Empty
                                              select review).Count();
                _positiveUserReviewCount = (from review in _reviews.AsParallel()
                                            where review.Point >= 70 && (review.Publication == null || review.Publication == String.Empty) && review.CheckedByAdmin == true
                                            select review).Count();
                _neutralUserReviewCount = (from review in _reviews.AsParallel()
                                           where review.Point > 35 && review.Point < 70 && (review.Publication == null || review.Publication == String.Empty) && review.CheckedByAdmin == true
                                           select review).Count();
                _negativeUserReviewCount = (from review in _reviews.AsParallel()
                                            where review.Point <= 35 && (review.Publication == null || review.Publication == String.Empty) && review.CheckedByAdmin == true
                                            select review).Count();
                _criticReviews = (from review in _reviews.AsParallel()
                                  where review.Publication != null && review.Publication != String.Empty
                                  select review).OrderByDescending((rev) => rev.Time).Take(5).ToArray();
                _userReviews = (from review in _reviews.AsParallel()
                                where (review.Publication == null || review.Publication == String.Empty) && review.CheckedByAdmin == true
                                select review).OrderByDescending((rev) => rev.Time).Take(5).ToArray();
            }
        }

        public int? GetCurrentUserRating(string email)
        {
            byte[] currentUserRating = (from review in _reviews.AsParallel()
                                        where review.UserId == UserCritic.GetByEmail(email).Id
                                        select review.Point).ToArray();
            if (currentUserRating.Length == 1)
                return currentUserRating[0];
            return null;
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

        private PerformerVM[] GetPerformerVMByEntertainmentVMAndRole(EntertainmentVM entertainment, PerformerInEntertainment.Role role)
        {
            Performer[] performers = Performer.GetPerformerByEntertainmentAndRole(entertainment.EntertainmentDL, role);
            if (performers == null)
                return null;

            List<PerformerVM> result = new List<PerformerVM>();
            foreach (var performer in performers)
            {
                result.Add(new PerformerVM(performer));
            }
            return result.ToArray();
        }

        private AwardVM[] GetAwardByEntertainment(Entertainment entertainment)
        {
            Award[] awards = Award.GetAwardByEntertainment(entertainment);
            if (awards == null)
                return null;

            List<AwardVM> result = new List<AwardVM>();
            foreach (var award in awards)
                result.Add(new AwardVM(award));
            return result.ToArray();
        }

    }
}