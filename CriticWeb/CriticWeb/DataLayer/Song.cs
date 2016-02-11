using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace CriticWeb.DataLayer
{
    [TableName("Song")]
    [IdColumnName("SongId")]
    [NameColumnName("Name")]
    public class Song : Entity<Song>
    {
        public string Name
        {
            get { return Row[_nameColumnName].ToString(); }
            set { Row[_nameColumnName] = value; }
        }
        public TimeSpan Duration
        {
            get { return (TimeSpan)Row["Duration"]; }
            set { Row["Duration"] = value; }
        }
        public string Lyrics
        {
            get { return Row["Lyrics"].ToString(); }
            set
            {
                if (value != String.Empty)
                    Row["Lyrics"] = value;
                else Row["Lyrics"] = DBNull.Value;
            }
        }

        public static Song[] GetSongsByAlbum(Entertainment album)
        {
            if (album.EntertainmentType != Entertainment.Type.Album)
                return null;

            List<Song> result = new List<Song>();

            _dataAdapter.SelectCommand.CommandText = "SELECT " + _tableName + ".SongId, Name, Duration, Lyrics FROM " + _tableName + ", " + SongInEntertainment._tableName + " WHERE EntertainmentId=@id;";

            if (!_dataAdapter.SelectCommand.Parameters.Contains("@id"))
                _dataAdapter.SelectCommand.Parameters.Add(new SqlParameter("@id", album.Id.ToString()));
            else
                _dataAdapter.SelectCommand.Parameters["@id"].Value = album.Id.ToString();

            DataTable dataTable = new DataTable();
            if (_dataAdapter.Fill(dataTable) > 0)
            {
                _dataTable.Merge(dataTable);

                var ids = from row in dataTable.AsEnumerable().AsParallel()
                          select (Guid)row[_idColumnName];

                var selectedRows = from row in _dataTable.AsEnumerable().AsParallel()
                                   where ids.Contains((Guid)row[_idColumnName])
                                   select row;

                foreach (DataRow dr in selectedRows)
                {
                    result.Add(new Song(dr));
                }
                return result.ToArray();
            }
            return null;
        }

        public Song(DataRow row) : base(row) { }
        public Song(string name, TimeSpan duration, string lyrics) : base()
        {
            Name = name;
            Duration = duration;
            Lyrics = lyrics;
        }

    }
}
