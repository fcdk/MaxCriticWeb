using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace CriticWeb.DataLayer
{
    [TableName("GenreInEntertainment")]
    [IdColumnName("GenreInEntertainmentId")]
    [NameColumnName(null)]
    public class GenreInEntertainment : Entity<GenreInEntertainment>
    {
        public Guid EntertainmentId
        {
            get { return (Guid)Row["EntertainmentId"]; }
            set { Row["EntertainmentId"] = value; }
        }
        public Guid GenreId
        {
            get { return (Guid)Row["GenreId"]; }
            set { Row["GenreId"] = value; }
        }

        public static GenreInEntertainment[] GetGenreInEntertainmentByEntertainment(Entertainment entertainment)
        {
            List<GenreInEntertainment> result = new List<GenreInEntertainment>();

            _dataAdapter.SelectCommand.CommandText = "SELECT * FROM " + _tableName + " WHERE EntertainmentId=@id";

            if (!_dataAdapter.SelectCommand.Parameters.Contains("@id"))
                _dataAdapter.SelectCommand.Parameters.Add(new SqlParameter("@id", entertainment.Id));
            else
                _dataAdapter.SelectCommand.Parameters["@id"].Value = entertainment.Id;

            _dataAdapter.Fill(_dataTable);
            var selectedRows = from row in _dataTable.AsEnumerable().AsParallel()
                               where (Guid)row["EntertainmentId"] == entertainment.Id
                               select row;
            foreach (DataRow dr in selectedRows)
            {
                result.Add(new GenreInEntertainment(dr));
            }
            if (result.Count != 0)
                return result.ToArray();
            return null;
        }

        public GenreInEntertainment(DataRow row) : base(row) { }
        public GenreInEntertainment(Entertainment entertainment, Genre genre) : base()
        {
            EntertainmentId = entertainment.Id;
            GenreId = genre.Id;
        }

    }
}
