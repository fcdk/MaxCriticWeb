using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace CriticWeb.DataLayer
{
    [TableName("Genre")]
    [IdColumnName("GenreId")]
    [NameColumnName("Name")]
    public class Genre : Entity<Genre>
    {
        public Guid? ParentGenreId
        {
            get { return Row["ParentGenreId"].Equals(DBNull.Value) ? default(Guid?) : (Guid)Row["ParentGenreId"]; }
            set
            {
                if (value != null)
                    Row["ParentGenreId"] = value;
                else Row["ParentGenreId"] = DBNull.Value;
            }
        }
        public string Name
        {
            get { return Row[_nameColumnName].ToString(); }
            set { Row[_nameColumnName] = value; }
        }
        public Entertainment.Type GenreType
        {
            get { return (Entertainment.Type)Enum.Parse(typeof(Entertainment.Type), Row["GenreType"].ToString()); }
            set { Row["GenreType"] = value; }
        }
        public string Summary
        {
            get { return Row["Summary"].ToString(); }
            set
            {
                if (value != String.Empty)
                    Row["Summary"] = value;
                else Row["Summary"] = DBNull.Value;
            }
        }

        public Genre(DataRow row) : base(row) { }
        public Genre(Genre parentGenre, string name, Entertainment.Type genreType, string summary) : base()
        {
            if (parentGenre == null)
                ParentGenreId = default(Guid?);
            else ParentGenreId = parentGenre.Id;
            Name = name;
            GenreType = genreType;
            Summary = summary;
        }

        public static Genre[] GetByName(string partOfName, Entertainment.Type? type = null)
        {            
            if (type == null)
                return Entity<Genre>.GetByName(partOfName);
            lock (_locker)
            {
                partOfName = partOfName.ToLower();

                List<Genre> result = new List<Genre>();
                _dataAdapter.SelectCommand.CommandText = "SELECT * FROM " + _tableName + " WHERE LOWER(" + _nameColumnName + ") LIKE '%' + @partOfName + '%' AND GenreType=@type";

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
                                   where ((Entertainment.Type)Enum.Parse(typeof(Entertainment.Type), row["GenreType"].ToString()) == type)
                                   && (row[_nameColumnName].ToString().ToLower().Contains(partOfName))
                                   select row;
                foreach (DataRow dr in selectedRows)
                {
                    result.Add(new Genre(dr));
                }
                if (result.Count != 0)
                    return result.ToArray();
                return null;
            }
        }

        public static Genre[] GetByNameExceptId(string partOfName, Entertainment.Type type, Guid id)
        {
            lock (_locker)
            {
                List<Genre> result = new List<Genre>();

                partOfName = partOfName.ToLower();

                _dataAdapter.SelectCommand.CommandText = "SELECT * FROM " + _tableName + " WHERE LOWER(" + _nameColumnName + ") LIKE '%' + @partOfName + '%' AND GenreType=@type AND " + _idColumnName + "!=@id";

                if (!_dataAdapter.SelectCommand.Parameters.Contains("@partOfName"))
                    _dataAdapter.SelectCommand.Parameters.Add(new SqlParameter("@partOfName", partOfName));
                else
                    _dataAdapter.SelectCommand.Parameters["@partOfName"].Value = partOfName;
                if (!_dataAdapter.SelectCommand.Parameters.Contains("@type"))
                    _dataAdapter.SelectCommand.Parameters.Add(new SqlParameter("@type", type.ToString()));
                else
                    _dataAdapter.SelectCommand.Parameters["@type"].Value = type.ToString();
                if (!_dataAdapter.SelectCommand.Parameters.Contains("@id"))
                    _dataAdapter.SelectCommand.Parameters.Add(new SqlParameter("@id", id));
                else
                    _dataAdapter.SelectCommand.Parameters["@id"].Value = id;

                _dataAdapter.Fill(_dataTable);
                var selectedRows = from row in _dataTable.AsEnumerable().AsParallel()
                                   where ((Entertainment.Type)Enum.Parse(typeof(Entertainment.Type), row["GenreType"].ToString()) == type)
                                   && (row[_nameColumnName].ToString().ToLower().Contains(partOfName))
                                   && ((Guid)row[_idColumnName] != id)
                                   select row;
                foreach (DataRow dr in selectedRows)
                {
                    result.Add(new Genre(dr));
                }
                if (result.Count != 0)
                    return result.ToArray();
                return null;
            }
        }

        public static Genre[] GetRandomFirstTen(Entertainment.Type? type = null)
        {
            if (type == null)
                return Entity<Genre>.GetRandomFirstTen();

            lock (_locker)
            {
                List<Genre> result = new List<Genre>();

                _dataAdapter.SelectCommand.CommandText = "SELECT TOP(10) * FROM " + _tableName + " WHERE GenreType=@type;";

                if (!_dataAdapter.SelectCommand.Parameters.Contains("@type"))
                    _dataAdapter.SelectCommand.Parameters.Add(new SqlParameter("@type", type.ToString()));
                else
                    _dataAdapter.SelectCommand.Parameters["@type"].Value = type.ToString();

                _dataAdapter.Fill(_dataTable);
                var selectedRows = (from row in _dataTable.AsEnumerable().AsParallel()
                                    where (Entertainment.Type)Enum.Parse(typeof(Entertainment.Type), row["GenreType"].ToString()) == type
                                    select row).Take(10);
                foreach (DataRow dr in selectedRows)
                {
                    result.Add(new Genre(dr));
                }
                if (result.Count != 0)
                    return result.ToArray();
                return null;
            }
        }

        public bool CanBeParentGenre(Genre genre)
        {
            if (genre.ParentGenreId == null)
                return true;
            if (genre.ParentGenreId == this.Id)
                return false;
            else return this.CanBeParentGenre(Genre.GetById((Guid)genre.ParentGenreId));
        }

        public static Genre[] GetGenreByEntertainment(Entertainment entertainment)
        {
            GenreInEntertainment[] genreInEntertainments = GenreInEntertainment.GetGenreInEntertainmentByEntertainment(entertainment);
            if (genreInEntertainments == null)
                return null;
            List<Guid> ids = new List<Guid>();
            foreach (var genreInEntertainment in genreInEntertainments)
                ids.Add(genreInEntertainment.GenreId);
            return Genre.GetByIds(ids.ToArray());
        }

    }
}
