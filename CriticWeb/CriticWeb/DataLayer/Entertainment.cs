using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace CriticWeb.DataLayer
{
    [TableName("Entertainment")]
    [IdColumnName("EntertainmentId")]
    [NameColumnName("Name")]
    public class Entertainment : Entity<Entertainment>
    {
        public Entertainment.Type EntertainmentType
        {
            get { return (Entertainment.Type)Enum.Parse(typeof(Entertainment.Type), Row["EntertainmentType"].ToString()); }
            set { Row["EntertainmentType"] = value; }
        }
        public string Name
        {
            get { return Row[_nameColumnName].ToString(); }
            set { Row[_nameColumnName] = value; }
        }
        public DateTime ReleaseDate
        {
            get { return (DateTime)Row["ReleaseDate"]; }
            set { Row["ReleaseDate"] = value; }
        }
        public string Company
        {
            get { return Row["Company"].ToString(); }
            set { Row["Company"] = value; }
        }
        public byte[] Poster
        {
            get { return (byte[])Row["Poster"]; }
            set { Row["Poster"] = value; }
        }
        public string Summary
        {
            get { return Row["Summary"].ToString(); }
            set { Row["Summary"] = value; }
        }
        public string BuyLink
        {
            get { return Row["BuyLink"].ToString(); }
            set
            {
                if (value != String.Empty)
                    Row["BuyLink"] = value;
                else Row["BuyLink"] = DBNull.Value;
            }
        }
        public string MainLanguage
        {
            get { return Row["MainLanguage"].ToString(); }
            set { Row["MainLanguage"] = value; }
        }
        public string Rating
        {
            get { return Row["Rating"].ToString(); }
            set
            {
                if (value != String.Empty)
                    Row["Rating"] = value;
                else Row["Rating"] = DBNull.Value;
            }
        }
        public string RatingComment
        {
            get { return Row["RatingComment"].ToString(); }
            set
            {
                if (value != String.Empty)
                    Row["RatingComment"] = value;
                else Row["RatingComment"] = DBNull.Value;
            }
        }
        public int? MovieRuntimeMinute
        {
            get { return Row["MovieRuntimeMinute"].Equals(DBNull.Value) ? default(int?) : (int)Row["MovieRuntimeMinute"]; }
            set
            {
                if (value != null)
                    Row["MovieRuntimeMinute"] = value;
                else Row["MovieRuntimeMinute"] = DBNull.Value;
            }
        }
        public string OfficialSite
        {
            get { return Row["OfficialSite"].ToString(); }
            set
            {
                if (value != String.Empty)
                    Row["OfficialSite"] = value;
                else Row["OfficialSite"] = DBNull.Value;
            }
        }
        public string MovieCountries
        {
            get { return Row["MovieCountries"].ToString(); }
            set
            {
                if (value != String.Empty)
                    Row["MovieCountries"] = value;
                else Row["MovieCountries"] = DBNull.Value;
            }
        }
        public byte? TVSeason
        {
            get { return Row["TVSeason"].Equals(DBNull.Value) ? default(byte?) : (byte)Row["TVSeason"]; }
            set
            {
                if (value != null)
                    Row["TVSeason"] = value;
                else Row["TVSeason"] = DBNull.Value;
            }
        }
        public decimal? Budget
        {
            get { return Row["Budget"].Equals(DBNull.Value) ? default(decimal?) : (decimal)Row["Budget"]; }
            set
            {
                if (value != null)
                    Row["Budget"] = value;
                else Row["Budget"] = DBNull.Value;
            }
        }
        public string TrailerLink
        {
            get { return Row["TrailerLink"].ToString(); }
            set
            {
                if (value != String.Empty)
                    Row["TrailerLink"] = value;
                else Row["TrailerLink"] = DBNull.Value;
            }
        }

        public Entertainment(DataRow row) : base(row)
        {
            ////Logger.Info("Entertainment.Entertainment", "Створено екземпляр Entertainment.");
        }
        public Entertainment(Entertainment.Type entertainmentType, string name, DateTime releaseDate, string company, byte[] poster,
        string summary, string buyLink, string mainLanguage, string rating, string ratingComment, int? movieRuntimeMinute,
        string officialSite, string movieCountries, byte? tVSeason, decimal? budget, string trailerLink) : base()
        {
            EntertainmentType = entertainmentType;
            Name = name;
            ReleaseDate = releaseDate;
            Company = company;
            Poster = poster;
            Summary = summary;
            BuyLink = buyLink;
            MainLanguage = mainLanguage;
            Rating = rating;
            RatingComment = ratingComment;
            MovieRuntimeMinute = movieRuntimeMinute;
            OfficialSite = officialSite;
            MovieCountries = movieCountries;
            TVSeason = tVSeason;
            Budget = budget;
            TrailerLink = trailerLink;

            ////Logger.Info("Entertainment.Entertainment", "Створено екземпляр Entertainment.");
        }

        public static Entertainment[] GetRandomFirstTen(Entertainment.Type? type = null)
        {
            lock (_locker)
            {
                if (type == null)
                    return Entity<Entertainment>.GetRandomFirstTen();

                ////Logger.Info("Entertainment.GetRandomFirstTen", "Спроба взяти з БД перші 10 записів Entertainment за типом.");

                List<Entertainment> result = new List<Entertainment>();

                _dataAdapter.SelectCommand.CommandText = "SELECT TOP(10) * FROM " + _tableName + " WHERE EntertainmentType=@type;";

                if (!_dataAdapter.SelectCommand.Parameters.Contains("@type"))
                    _dataAdapter.SelectCommand.Parameters.Add(new SqlParameter("@type", type.ToString()));
                else
                    _dataAdapter.SelectCommand.Parameters["@type"].Value = type.ToString();

                _dataAdapter.Fill(_dataTable);
                var selectedRows = (from row in _dataTable.AsEnumerable().AsParallel()
                                    where (Entertainment.Type)Enum.Parse(typeof(Entertainment.Type), row["EntertainmentType"].ToString()) == type
                                    select row).Take(10);
                foreach (DataRow dr in selectedRows)
                {
                    result.Add(new Entertainment(dr));
                }

                ////Logger.Info("Entertainment.GetRandomFirstTen", "Зчитано з БД перші 10 записів Entertainment за типом.");

                if (result.Count != 0)
                    return result.ToArray();
                return null;
            }
        }

        // по дефолту, если type=null, то запускаем поиск по всем типам
        public static Entertainment[] GetByName(string partOfName, Entertainment.Type? type = null)
        {
            lock (_locker)
            {
                if (type == null)
                    return Entity<Entertainment>.GetByName(partOfName);

                ////Logger.Info("Entertainment.GetByName", "Спроба взяти з БД Entertainment за ім'ям та типом.");

                partOfName = partOfName.ToLower();

                List<Entertainment> result = new List<Entertainment>();
                _dataAdapter.SelectCommand.CommandText = "SELECT * FROM " + _tableName + " WHERE LOWER(" + _nameColumnName + ") LIKE '%' + @partOfName + '%' AND EntertainmentType=@type";

                if (!_dataAdapter.SelectCommand.Parameters.Contains("@partOfName"))
                    _dataAdapter.SelectCommand.Parameters.Add(new SqlParameter("@partOfName", partOfName));
                else
                    _dataAdapter.SelectCommand.Parameters["@partOfName"].Value = partOfName;
                if (!_dataAdapter.SelectCommand.Parameters.Contains("@type"))
                    _dataAdapter.SelectCommand.Parameters.Add(new SqlParameter("@type", type.ToString()));
                else
                    _dataAdapter.SelectCommand.Parameters["@type"].Value = type.ToString();

                _dataAdapter.Fill(_dataTable);
                var selectedRows = from row in _dataTable.AsEnumerable().AsParallel()
                                   where ((Entertainment.Type)Enum.Parse(typeof(Entertainment.Type), row["EntertainmentType"].ToString()) == type)
                                   && (row[_nameColumnName].ToString().ToLower().Contains(partOfName))
                                   select row;
                foreach (DataRow dr in selectedRows)
                {
                    result.Add(new Entertainment(dr));
                }

                ////Logger.Info("Entertainment.GetByName", "Зчитано з БД Entertainment за ім'ям та типом.");

                if (result.Count != 0)
                    return result.ToArray();
                return null;
            }
        }

        public static Entertainment[] GetForAutoCompleteBox(string partOfName)
        {
            lock (_locker)
            {
                ////Logger.Info("Entertainment.GetForAutoCompleteBox", "Спроба взяти з БД Entertainment для AutoCompleteBox.");

                List<Entertainment> result = new List<Entertainment>();
                partOfName = partOfName.ToLower();

                _dataAdapter.SelectCommand.CommandText = "SELECT * FROM " + _tableName + " WHERE LOWER(Name) + ' (' + FORMAT(ReleaseDate, 'yyyy') + ')' LIKE '%' + @partOfName + '%'";

                if (!_dataAdapter.SelectCommand.Parameters.Contains("@partOfName"))
                    _dataAdapter.SelectCommand.Parameters.Add(new SqlParameter("@partOfName", partOfName));
                else
                    _dataAdapter.SelectCommand.Parameters["@partOfName"].Value = partOfName;

                _dataAdapter.Fill(_dataTable);
                var selectedRows1 = from row in _dataTable.AsEnumerable().AsParallel()
                                    where (row["Name"].ToString().ToLower() + " (" + ((DateTime)row["ReleaseDate"]).ToString("yyyy") + ")").Contains(partOfName)
                                    select row;
                foreach (DataRow dr in selectedRows1)
                {
                    result.Add(new Entertainment(dr));
                }

                ////Logger.Info("Entertainment.GetForAutoCompleteBox", "Зчитано з БД Entertainment для AutoCompleteBox.");

                if (result.Count != 0)
                    return result.ToArray();
                return null;
            }
        }

        public static Entertainment[] GetLastNEntertainmentByTypeAndReviewCount(Entertainment.Type type, uint N, uint reviewCount)
        {
            lock (_locker)
            {
                _dataAdapter.SelectCommand.CommandText = "SELECT TOP(" + N + ") Entertainment." + _idColumnName + ", Entertainment.ReleaseDate FROM " + _tableName + ",Review WHERE Review." + _idColumnName + "=Entertainment." + _idColumnName + " AND Review.Publication IS NOT NULL AND EntertainmentType=@type GROUP BY Entertainment." + _idColumnName + ", Entertainment.ReleaseDate HAVING COUNT(Review.ReviewId)>=" + reviewCount + " ORDER BY Entertainment.ReleaseDate DESC";

                if (!_dataAdapter.SelectCommand.Parameters.Contains("@type"))
                    _dataAdapter.SelectCommand.Parameters.Add(new SqlParameter("@type", type.ToString()));
                else
                    _dataAdapter.SelectCommand.Parameters["@type"].Value = type.ToString();

                DataTable dataTable = new DataTable();
                if (_dataAdapter.Fill(dataTable) == 0)
                    return null;
                List<Guid> ids = new List<Guid>();
                foreach (DataRow dataRow in dataTable.Rows)
                    ids.Add((Guid)dataRow[_idColumnName]);

                List<Entertainment> result = new List<Entertainment>();
                result.AddRange(Entertainment.GetByIds(ids.ToArray()));
                return result.OrderByDescending(entertainment => entertainment.ReleaseDate).ToArray();
            }
        }

        public int? AverageCriticPointForOneEntertainment()
        {
            lock (_locker)
            {
                _dataAdapter.SelectCommand.CommandText = "SELECT AVG(Review.Point) AS AvgPoint FROM Review, Entertainment WHERE Review.EntertainmentId=@id AND Review.Publication IS NOT NULL GROUP BY Review.EntertainmentId";

                if (!_dataAdapter.SelectCommand.Parameters.Contains("@id"))
                    _dataAdapter.SelectCommand.Parameters.Add(new SqlParameter("@id", this.Id));
                else
                    _dataAdapter.SelectCommand.Parameters["@id"].Value = this.Id;

                DataTable dataTable = new DataTable();
                if (_dataAdapter.Fill(dataTable) == 1)
                    return (int)dataTable.Rows[0][0];
                return null;
            }       
        }

        public int? AverageUserPointForOneEntertainment()
        {
            lock (_locker)
            {
                _dataAdapter.SelectCommand.CommandText = "SELECT AVG(Review.Point) AS AvgPoint FROM Review, Entertainment WHERE Review.EntertainmentId=@id AND Review.Publication IS NULL GROUP BY Review.EntertainmentId";

                if (!_dataAdapter.SelectCommand.Parameters.Contains("@id"))
                    _dataAdapter.SelectCommand.Parameters.Add(new SqlParameter("@id", this.Id));
                else
                    _dataAdapter.SelectCommand.Parameters["@id"].Value = this.Id;

                DataTable dataTable = new DataTable();
                if (_dataAdapter.Fill(dataTable) == 1)
                    return (int)dataTable.Rows[0][0];
                return null;
            }
        }

        public static Entertainment[] GetLastThreeEntertainmentByActor(Performer performer)
        {
            lock (_locker)
            {
                _dataAdapter.SelectCommand.CommandText = "SELECT TOP(3) Entertainment." + _idColumnName + ", Entertainment.ReleaseDate FROM " + _tableName + ",PerformerInEntertainment WHERE PerformerInEntertainment.PerformerId=@id AND PerformerInEntertainment." + _idColumnName + "=Entertainment." + _idColumnName + " AND (PerformerInEntertainment.PerformerRole='MoviePrincipalCast' OR PerformerInEntertainment.PerformerRole='MovieCast') AND Entertainment.ReleaseDate<SYSDATETIME() ORDER BY Entertainment.ReleaseDate DESC";

                if (!_dataAdapter.SelectCommand.Parameters.Contains("@id"))
                    _dataAdapter.SelectCommand.Parameters.Add(new SqlParameter("@id", performer.Id));
                else
                    _dataAdapter.SelectCommand.Parameters["@id"].Value = performer.Id;

                DataTable dataTable = new DataTable();
                if (_dataAdapter.Fill(dataTable) == 0)
                    return null;
                List<Guid> ids = new List<Guid>();
                foreach (DataRow dataRow in dataTable.Rows)
                    ids.Add((Guid)dataRow[_idColumnName]);

                List<Entertainment> result = new List<Entertainment>();
                result.AddRange(Entertainment.GetByIds(ids.ToArray()));
                return result.OrderByDescending(entertainment => entertainment.ReleaseDate).ToArray();
            }
        }

        public static Entertainment[] GetLastThreeEntertainmentBySinger(Performer performer)
        {
            lock (_locker)
            {
                _dataAdapter.SelectCommand.CommandText = "SELECT TOP(3) Entertainment." + _idColumnName + ", Entertainment.ReleaseDate FROM " + _tableName + ",PerformerInEntertainment WHERE PerformerInEntertainment.PerformerId=@id AND PerformerInEntertainment." + _idColumnName + "=Entertainment." + _idColumnName + " AND PerformerInEntertainment.PerformerRole='AlbumSinger' AND Entertainment.ReleaseDate<SYSDATETIME() ORDER BY Entertainment.ReleaseDate DESC";

                if (!_dataAdapter.SelectCommand.Parameters.Contains("@id"))
                    _dataAdapter.SelectCommand.Parameters.Add(new SqlParameter("@id", performer.Id));
                else
                    _dataAdapter.SelectCommand.Parameters["@id"].Value = performer.Id;

                DataTable dataTable = new DataTable();
                if (_dataAdapter.Fill(dataTable) == 0)
                    return null;
                List<Guid> ids = new List<Guid>();
                foreach (DataRow dataRow in dataTable.Rows)
                    ids.Add((Guid)dataRow[_idColumnName]);

                List<Entertainment> result = new List<Entertainment>();
                result.AddRange(Entertainment.GetByIds(ids.ToArray()));
                return result.OrderByDescending(entertainment => entertainment.ReleaseDate).ToArray();
            }
        }

        public static Entertainment[] GetEntertainmentByActor(Performer performer)
        {
            lock (_locker)
            {
                _dataAdapter.SelectCommand.CommandText = "SELECT Entertainment." + _idColumnName + " FROM " + _tableName + ",PerformerInEntertainment WHERE PerformerInEntertainment.PerformerId=@id AND PerformerInEntertainment." + _idColumnName + "=Entertainment." + _idColumnName + " AND (PerformerInEntertainment.PerformerRole='MoviePrincipalCast' OR PerformerInEntertainment.PerformerRole='MovieCast' OR PerformerInEntertainment.PerformerRole='GameCast' OR PerformerInEntertainment.PerformerRole='TVCast')";

                if (!_dataAdapter.SelectCommand.Parameters.Contains("@id"))
                    _dataAdapter.SelectCommand.Parameters.Add(new SqlParameter("@id", performer.Id));
                else
                    _dataAdapter.SelectCommand.Parameters["@id"].Value = performer.Id;

                DataTable dataTable = new DataTable();
                if (_dataAdapter.Fill(dataTable) == 0)
                    return null;
                List<Guid> ids = new List<Guid>();
                foreach (DataRow dataRow in dataTable.Rows)
                    ids.Add((Guid)dataRow[_idColumnName]);

                return Entertainment.GetByIds(ids.ToArray());
            }
        }


        public static Entertainment[] GetEntertainmentBySinger(Performer performer)
        {
            lock (_locker)
            {
                _dataAdapter.SelectCommand.CommandText = "SELECT Entertainment." + _idColumnName + " FROM " + _tableName + ",PerformerInEntertainment WHERE PerformerInEntertainment.PerformerId=@id AND PerformerInEntertainment." + _idColumnName + "=Entertainment." + _idColumnName + " AND PerformerInEntertainment.PerformerRole='AlbumSinger'";

                if (!_dataAdapter.SelectCommand.Parameters.Contains("@id"))
                    _dataAdapter.SelectCommand.Parameters.Add(new SqlParameter("@id", performer.Id));
                else
                    _dataAdapter.SelectCommand.Parameters["@id"].Value = performer.Id;

                DataTable dataTable = new DataTable();
                if (_dataAdapter.Fill(dataTable) == 0)
                    return null;
                List<Guid> ids = new List<Guid>();
                foreach (DataRow dataRow in dataTable.Rows)
                    ids.Add((Guid)dataRow[_idColumnName]);

                return Entertainment.GetByIds(ids.ToArray());
            }
        }

        public static Entertainment[] GetEntertainmentByPerformer(Performer performer)
        {
            lock (_locker)
            {
                _dataAdapter.SelectCommand.CommandText = "SELECT Entertainment." + _idColumnName + " FROM " + _tableName + ",PerformerInEntertainment WHERE PerformerInEntertainment.PerformerId=@id AND PerformerInEntertainment." + _idColumnName + "=Entertainment." + _idColumnName;

                if (!_dataAdapter.SelectCommand.Parameters.Contains("@id"))
                    _dataAdapter.SelectCommand.Parameters.Add(new SqlParameter("@id", performer.Id));
                else
                    _dataAdapter.SelectCommand.Parameters["@id"].Value = performer.Id;

                DataTable dataTable = new DataTable();
                if (_dataAdapter.Fill(dataTable) == 0)
                    return null;
                List<Guid> ids = new List<Guid>();
                foreach (DataRow dataRow in dataTable.Rows)
                    ids.Add((Guid)dataRow[_idColumnName]);

                return Entertainment.GetByIds(ids.ToArray());
            }
        }

        public static int? AverageCriticPointForEntertainments(Entertainment[] entertainments)
        {
            lock (_locker)
            {
                double? average = (from entertainment in entertainments
                                   select entertainment.AverageCriticPointForOneEntertainment()).Average();
                if (average == null)
                    return null;
                return (int)average;
            }
        }

        public static int? AverageUserPointForEntertainments(Entertainment[] entertainments)
        {
            lock (_locker)
            {
                double? average = (from entertainment in entertainments
                                   select entertainment.AverageUserPointForOneEntertainment()).Average();
                if (average == null)
                    return null;
                return (int)average;
            }
        }

        public enum Type { Movie, Game, TVSeries, Album }

    }
}
