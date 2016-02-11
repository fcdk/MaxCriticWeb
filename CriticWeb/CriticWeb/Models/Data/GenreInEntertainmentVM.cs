using System;
using CriticWeb.DataLayer;

namespace CriticWeb.Models.Data
{
    public class GenreInEntertainmentVM
    {

        private GenreInEntertainment _genreInEntertainment;

        public GenreInEntertainment GenreInEntertainmentDL
        {
            get { return _genreInEntertainment; }
        }

        public Guid GenreId
        {
            get { return _genreInEntertainment.GenreId; }
            set { _genreInEntertainment.GenreId = value; }
        }
        public Guid EntertainmentId
        {
            get { return _genreInEntertainment.EntertainmentId; }
            set { _genreInEntertainment.EntertainmentId = value; }
        }

        public GenreInEntertainmentVM(GenreInEntertainment genreInEntertainment)
        {
            _genreInEntertainment = genreInEntertainment;
        }

        public GenreInEntertainmentVM(GenreVM genre, EntertainmentVM entertainment)
        {
            _genreInEntertainment = new GenreInEntertainment(entertainment.EntertainmentDL, genre.GenreDL);
        }

    }
}
