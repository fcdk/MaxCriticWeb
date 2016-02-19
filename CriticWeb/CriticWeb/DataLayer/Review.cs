using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace CriticWeb.DataLayer
{
    [TableName("Review")]
    [IdColumnName("ReviewId")]
    [NameColumnName(null)]
    public class Review : Entity<Review>
    {
        public Guid UserId
        {
            get { return (Guid)Row["UserId"]; }
            set { Row["UserId"] = value; }
        }
        public Guid EntertainmentId
        {
            get { return (Guid)Row["EntertainmentId"]; }
            private set { Row["EntertainmentId"] = value; }
        }
        public byte Point
        {
            get { return (byte)Row["Point"]; }
            set { Row["Point"] = value; }
        }
        public string Opinion
        {
            get { return Row["Opinion"].ToString(); }
            set { Row["Opinion"] = value; }
        }
        public DateTimeOffset Time
        {
            get { return (DateTimeOffset)Row["Time"]; }
            private set { Row["Time"] = value; }
        }
        public string Link
        {
            get { return Row["Link"].ToString(); }
            set
            {
                if (value != String.Empty)
                    Row["Link"] = value;
                else Row["Link"] = DBNull.Value;
            }
        }
        public string Publication
        {
            get { return Row["Publication"].ToString(); }
            set
            {
                if (value != String.Empty)
                    Row["Publication"] = value;
                else Row["Publication"] = DBNull.Value;
            }
        }
        public int Helpful
        {
            get { return (int)Row["Helpful"]; }
            set { Row["Helpful"] = value; }
        }
        public int Unhelpful
        {
            get { return (int)Row["Unhelpful"]; }
            set { Row["Unhelpful"] = value; }
        }
        public bool CheckedByAdmin
        {
            get { return (bool)Row["CheckedByAdmin"]; }
            set { Row["CheckedByAdmin"] = value; }
        }

        public Review(DataRow row) : base(row) { }
        public Review(UserCritic userCritic, Entertainment entertainment, byte point, string opinion, DateTimeOffset time,
        string link, string publication, int helpful, int unhelpful, bool checkedByAdmin) : base()
        {
            UserId = userCritic.Id;
            EntertainmentId = entertainment.Id;
            Point = point;
            Opinion = opinion;
            Time = time;
            Link = link;
            Publication = publication;
            Helpful = helpful;
            Unhelpful = unhelpful;
            CheckedByAdmin = checkedByAdmin;
        }

        public static Review[] GetReviewByEntertainment(Entertainment entertainment)
        {
            lock (_locker)
            {
                List<Review> result = new List<Review>();

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
                    result.Add(new Review(dr));
                }

                if (result.Count != 0)
                    return result.ToArray();
                return null;
            }
        }

        public static Review GetReviewByEntertainmentAndUser(Entertainment entertainment, UserCritic user)
        {
            lock (_locker)
            {
                var query = from row in _dataTable.AsEnumerable().AsParallel()
                            where (Guid)row["EntertainmentId"] == entertainment.Id && (Guid)row["UserId"] == user.Id
                            select row;
                DataRow[] result = query.ToArray();
                if (result.Length == 1)
                {
                    return new Review(result[0]);
                }
                else
                {
                    _dataAdapter.SelectCommand.CommandText = "SELECT * FROM " + _tableName + " WHERE EntertainmentId=@entertainmentId AND UserId=@userId;";

                    if (!_dataAdapter.SelectCommand.Parameters.Contains("@entertainmentId"))
                        _dataAdapter.SelectCommand.Parameters.Add(new SqlParameter("@entertainmentId", entertainment.Id));
                    else
                        _dataAdapter.SelectCommand.Parameters["@entertainmentId"].Value = entertainment.Id;
                    if (!_dataAdapter.SelectCommand.Parameters.Contains("@userId"))
                        _dataAdapter.SelectCommand.Parameters.Add(new SqlParameter("@userId", user.Id));
                    else
                        _dataAdapter.SelectCommand.Parameters["@userId"].Value = user.Id;

                    if (_dataAdapter.Fill(_dataTable) == 1)
                    {
                        var selectedRow = from row in _dataTable.AsEnumerable().AsParallel()
                                          where (Guid)row["EntertainmentId"] == entertainment.Id && (Guid)row["UserId"] == user.Id
                                          select row;
                        return new Review(selectedRow.ToArray()[0]);
                    }
                    return null;
                }
            }
        }

    }
}
