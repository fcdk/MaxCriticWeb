using System;
using CriticWeb.DataLayer;

namespace CriticWeb.Models.Data
{
    public class GenreVM
    {
        private Genre _genre;

        public Genre GenreDL
        {
            get { return _genre; }
        }

        public Guid? ParentGenreId
        {
            get { return _genre.ParentGenreId; }
            set { _genre.ParentGenreId = value; }
        }

        public string Name
        {
            get { return _genre.Name; }
            set { _genre.Name = value; }
        }

        public Entertainment.Type GenreType
        {
            get { return _genre.GenreType; }
            set
            {  _genre.GenreType = value; }
        }

        public string GenreTypeUkr
        {
            get { return EntertainmentVM.EntertainmentTypeToUkrString(GenreType); }
        }

        public string Summary
        {
            get { return _genre.Summary; }
            set { _genre.Summary = value; }
        }

        public GenreVM(Genre genre)
        {
            _genre = genre;
        }

        public GenreVM(GenreVM parentGenre, string name, Entertainment.Type genreType, string summary)
        {
            if (parentGenre != null)
                _genre = new Genre(parentGenre.GenreDL, name, genreType, summary);
            else _genre = new Genre(null, name, genreType, summary);
        }

        public override string ToString()
        {
            return Name;
        }
        
        public bool CanBeParentGenre(GenreVM genre)
        {
            return this.GenreDL.CanBeParentGenre(genre.GenreDL);
        }

    }
}
