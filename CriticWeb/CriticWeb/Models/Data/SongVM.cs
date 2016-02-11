using System;
using CriticWeb.DataLayer;

namespace CriticWeb.Models.Data
{
    public class SongVM
    {
        private Song _song;

        public Song SongDL
        {
            get { return _song; }
        }

        public string Name
        {
            get { return _song.Name; }
            set { _song.Name = value; }
        }

        public TimeSpan Duration
        {
            get { return _song.Duration; }
            set { _song.Duration = value; }
        }

        public string Lyrics
        {
            get { return _song.Lyrics; }
            set { _song.Lyrics = value; }
        }

        public SongVM(Song song)
        {
            _song = song;
        }

        public SongVM(string name, TimeSpan duration, string lyrics)
        {
            _song = new Song(name, duration, lyrics);
        }

        public override string ToString()
        {
            return Name + " " + Duration.ToString(@"hh\:mm\:ss");
        }

    }
}
