using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace CriticWeb.DataLayer
{
    [TableName("PerformerInEntertainment")]
    [IdColumnName("PerformerInEntertainmentId")]
    [NameColumnName(null)]
    public class PerformerInEntertainment : Entity<PerformerInEntertainment>
    {
        public Guid PerformerId
        {
            get { return (Guid)Row["PerformerId"]; }
            set { Row["PerformerId"] = value; }
        }
        public Guid EntertainmentId
        {
            get { return (Guid)Row["EntertainmentId"]; }
            set { Row["EntertainmentId"] = value; }
        }
        public PerformerInEntertainment.Role PerformerRole
        {
            get { return (PerformerInEntertainment.Role)Enum.Parse(typeof(PerformerInEntertainment.Role), Row["PerformerRole"].ToString()); }
            set { Row["PerformerRole"] = value; }
        }

        public static PerformerInEntertainment[] GetPerformerInEntertainmentByEntertainmentAndRole(Entertainment entertainment, PerformerInEntertainment.Role role)
        {
            List<PerformerInEntertainment> result = new List<PerformerInEntertainment>();

            _dataAdapter.SelectCommand.CommandText = "SELECT * FROM " + _tableName + " WHERE EntertainmentId=@id AND PerformerRole=@role";

            if (!_dataAdapter.SelectCommand.Parameters.Contains("@id"))
                _dataAdapter.SelectCommand.Parameters.Add(new SqlParameter("@id", entertainment.Id));
            else
                _dataAdapter.SelectCommand.Parameters["@id"].Value = entertainment.Id;
            if (!_dataAdapter.SelectCommand.Parameters.Contains("@role"))
                _dataAdapter.SelectCommand.Parameters.Add(new SqlParameter("@role", role.ToString()));
            else
                _dataAdapter.SelectCommand.Parameters["@role"].Value = role.ToString();

            _dataAdapter.Fill(_dataTable);
            var selectedRows = from row in _dataTable.AsEnumerable().AsParallel()
                               where ((Guid)row["EntertainmentId"] == entertainment.Id)
                               && ((PerformerInEntertainment.Role)Enum.Parse(typeof(PerformerInEntertainment.Role), row["PerformerRole"].ToString()) == role)
                               select row;
            foreach (DataRow dr in selectedRows)
            {
                result.Add(new PerformerInEntertainment(dr));
            }
            if (result.Count != 0)
                return result.ToArray();
            return null;
        }

        public static PerformerInEntertainment[] GetAlbumAuthorsPerformerInEntertainmentsByEntertainment(Entertainment entertainment)
        {
            List<PerformerInEntertainment> result = new List<PerformerInEntertainment>();

            _dataAdapter.SelectCommand.CommandText = "SELECT * FROM " + _tableName + " WHERE EntertainmentId=@id AND (PerformerRole='AlbumBand' OR PerformerRole='AlbumSinger')";

            if (!_dataAdapter.SelectCommand.Parameters.Contains("@id"))
                _dataAdapter.SelectCommand.Parameters.Add(new SqlParameter("@id", entertainment.Id));
            else
                _dataAdapter.SelectCommand.Parameters["@id"].Value = entertainment.Id;

            _dataAdapter.Fill(_dataTable);
            var selectedRows = from row in _dataTable.AsEnumerable().AsParallel()
                               where ((Guid)row["EntertainmentId"] == entertainment.Id)
                               && (((PerformerInEntertainment.Role)Enum.Parse(typeof(PerformerInEntertainment.Role), row["PerformerRole"].ToString()) == PerformerInEntertainment.Role.AlbumSinger)
                               || ((PerformerInEntertainment.Role)Enum.Parse(typeof(PerformerInEntertainment.Role), row["PerformerRole"].ToString()) == PerformerInEntertainment.Role.AlbumBand))
                               select row;
            foreach (DataRow dr in selectedRows)
            {
                result.Add(new PerformerInEntertainment(dr));
            }
            if (result.Count != 0)
                return result.ToArray();
            return null;
        }

        public PerformerInEntertainment(DataRow row) : base(row) { }
        public PerformerInEntertainment(Performer performer, Entertainment entertainment, PerformerInEntertainment.Role performerRole) : base()
        {
            PerformerId = performer.Id;
            EntertainmentId = entertainment.Id;
            PerformerRole = performerRole;
        }

        public enum Role { MovieDirector, MoviePlotWriter, MoviePrincipalCast, MovieCast, MovieProducer, MovieProduction,
        GameCast, GameDeveloperCompany, GamePlatform, TVCast, TVNetwork, AlbumSinger, AlbumBand, AlbumRecordLabel }
    }
}
