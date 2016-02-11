using System;
using CriticWeb.DataLayer;

namespace CriticWeb.Models.Data
{
    public class SongInEntertainmentVM
    {
        private SongInEntertainment _songInEntertainment;

        public SongInEntertainment SongInEntertainmentDL
        {
            get { return _songInEntertainment; }
        }

        public Guid SongId
        {
            get { return _songInEntertainment.SongId; }
            set { _songInEntertainment.SongId = value; }
        }
        public Guid EntertainmentId
        {
            get { return _songInEntertainment.EntertainmentId; }
            set { _songInEntertainment.EntertainmentId = value; }
        }

        public SongInEntertainmentVM(SongInEntertainment songInEntertainment)
        {
            _songInEntertainment = songInEntertainment;
        }

        public SongInEntertainmentVM(SongVM song, EntertainmentVM entertainment)
        {
            _songInEntertainment = new SongInEntertainment(song.SongDL, entertainment.EntertainmentDL);
        }

    }
}
