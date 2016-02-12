using System;
using System.Text;
using CriticWeb.DataLayer;

namespace CriticWeb.Models.Data
{
    public class EntertainmentVM
    {
        private Entertainment _entertainment;

        private string _albumAuthors; 

        public Entertainment EntertainmentDL
        {
            get { return _entertainment; }
        }

        public string AlbumAuthors
        {
            get
            {
                if (EntertainmentType == Entertainment.Type.Album)
                    return _albumAuthors;
                else return null;
            }
            set
            {
                if (EntertainmentType == Entertainment.Type.Album)
                    _albumAuthors = value;
            }
        }

        public Entertainment.Type EntertainmentType
        {
            get { return _entertainment.EntertainmentType; }
            set{ _entertainment.EntertainmentType = value; }
        }

        public string EntertainmentTypeUkr
        {
            get { return EntertainmentTypeToUkrString(EntertainmentType); }
        }

        public string Name
        {
            get { return _entertainment.Name; }
            set { _entertainment.Name = value; }
        }

        public DateTime ReleaseDate
        {
            get { return _entertainment.ReleaseDate; }
            set { _entertainment.ReleaseDate = value; }
        }

        public string Company
        {
            get { return _entertainment.Company; }
            set { _entertainment.Company = value; }
        }

        public byte[] Poster
        {
            get { return _entertainment.Poster; }
            set { _entertainment.Poster = value; }
        }

        public string Summary
        {
            get { return _entertainment.Summary; }
            set { _entertainment.Summary = value; }
        }

        public string BuyLink
        {
            get { return _entertainment.BuyLink; }
            set { _entertainment.BuyLink = value; }
        }

        public string MainLanguage
        {
            get { return _entertainment.MainLanguage; }
            set { _entertainment.MainLanguage = value; }
        }

        public string Rating
        {
            get { return _entertainment.Rating; }
            set { _entertainment.Rating = value; }
        }

        public string RatingComment
        {
            get { return _entertainment.RatingComment; }
            set { _entertainment.RatingComment = value; }
        }

        public int? MovieRuntimeMinute
        {
            get { return _entertainment.MovieRuntimeMinute; }
            set { _entertainment.MovieRuntimeMinute = value; }
        }

        public string OfficialSite
        {
            get { return _entertainment.OfficialSite; }
            set { _entertainment.OfficialSite = value; }
        }

        public string MovieCountries
        {
            get { return _entertainment.MovieCountries; }
            set { _entertainment.MovieCountries = value; }
        }

        public byte? TVSeason
        {
            get { return _entertainment.TVSeason; }
            set { _entertainment.TVSeason = value; }
        }

        public decimal? Budget
        {
            get { return _entertainment.Budget; }
            set { _entertainment.Budget = value; }
        }

        public string TrailerLink
        {
            get { return _entertainment.TrailerLink; }
            set { _entertainment.TrailerLink = value; }
        }

        public EntertainmentVM(Entertainment entertainment)
        {
            _entertainment = entertainment;

            this.AuthorsUpdate();
        }

        public EntertainmentVM(Entertainment.Type entertainmentType, string name, DateTime releaseDate, string company, byte[] poster,
        string summary, string buyLink, string mainLanguage, string rating, string ratingComment, int? movieRuntimeMinute,
        string officialSite, string movieCountries, byte? tVSeason, decimal? budget, string trailerLink)
        {
            _entertainment = new Entertainment(entertainmentType, name, releaseDate, company, poster, summary, buyLink, mainLanguage,
            rating, ratingComment, movieRuntimeMinute, officialSite, movieCountries, tVSeason, budget, trailerLink);

            this.AuthorsUpdate();
        }

        public override string ToString()
        {
            return (AlbumAuthors == null ? String.Empty : AlbumAuthors + " ") + Name + (TVSeason == null ? String.Empty : ": сезон " + TVSeason);
        }

        public static string EntertainmentTypeToUkrString(Entertainment.Type type)
        {
            switch (type)
            {
                case Entertainment.Type.Movie:
                    return "Фільм";
                case Entertainment.Type.Game:
                    return "Гра";
                case Entertainment.Type.TVSeries:
                    return "Серіал";
                case Entertainment.Type.Album:
                    return "Музика";
                default:
                    return "";
            }
        }

        public static Entertainment.Type? EntertainmentTypeUkrStringToEngEnum(string type)
        {
            switch (type)
            {
                case "Фільм":
                    return Entertainment.Type.Movie;
                case "Гра":
                    return Entertainment.Type.Game;
                case "Серіал":
                    return Entertainment.Type.TVSeries;
                case "Музика":
                    return Entertainment.Type.Album;
                default:
                    return null;
            }
        }

        public void AuthorsUpdate()
        {
            if (EntertainmentType != Entertainment.Type.Album)
                return;
            Performer[] authors = Performer.GetAlbumAuthorsByAlbum(_entertainment);
            if (authors != null)
            {
                StringBuilder authorsStringBuilder = new StringBuilder("");
                foreach (var author in authors)
                {
                    authorsStringBuilder.Append(author.Name);
                    if (author.Surname != null && author.Surname != String.Empty)
                    {
                        authorsStringBuilder.Append(" ");
                        authorsStringBuilder.Append(author.Surname);
                    }
                    authorsStringBuilder.Append(", ");
                }
                authorsStringBuilder.Length -= 2;
                authorsStringBuilder.Append(" -");
                AlbumAuthors = authorsStringBuilder.ToString();
            }
            else AlbumAuthors = String.Empty;
        }

    }
}
