using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace CriticWeb.DataLayer
{
    [TableName("UserCritic")]
    [IdColumnName("UserId")]
    [NameColumnName("Username")] // поиск по имени юзера в классе Entity будет по его никнейму
    public class UserCritic : Entity<UserCritic>
    {
        public string Username
        {
            get { return Row["Username"].ToString(); }
            set { Row["Username"] = value; }
        }

        public string Name
        {
            get { return Row["Name"].ToString(); }
            set
            {
                if (value != String.Empty)
                    Row["Name"] = value;
                else Row["Name"] = DBNull.Value;
            }
        }
        public string Surname
        {
            get { return Row["Surname"].ToString(); }
            set
            {
                if (value != String.Empty)
                    Row["Surname"] = value;
                else Row["Surname"] = DBNull.Value;
            }
        }
        public DateTime? DateOfBirth
        {
            get { return Row["DateOfBirth"].Equals(DBNull.Value) ? default(DateTime?) : (DateTime)Row["DateOfBirth"]; }
            set
            {
                if (value != null)
                    Row["DateOfBirth"] = value;
                else Row["DateOfBirth"] = DBNull.Value;
            }
        }
        public GenderType? Gender
        {
            get { return Row["Gender"].Equals(DBNull.Value) ? default(GenderType?) : (GenderType)Enum.Parse(typeof(GenderType), Row["Gender"].ToString()); }
            set
            {
                if (value != null)
                    Row["Gender"] = value;
                else Row["Gender"] = DBNull.Value;
            }
        }
        public string Country
        {
            get { return Row["Country"].ToString(); }
            set
            {
                if (value != String.Empty)
                    Row["Country"] = value;
                else Row["Country"] = DBNull.Value;
            }
        }
        public string PublicationCompany
        {
            get { return Row["PublicationCompany"].ToString(); }
            set
            {
                if (value != String.Empty)
                    Row["PublicationCompany"] = value;
                else Row["PublicationCompany"] = DBNull.Value;
            }
        }
        public UserCritic.Role UserRole
        {
            get { return (UserCritic.Role)Enum.Parse(typeof(UserCritic.Role), Row["UserRole"].ToString()); }
            set { Row["UserRole"] = value; }
        }
        public string Email
        {
            get { return Row["Email"].ToString(); }
            set { Row["Email"] = value; }
        }
        public byte[] Image
        {
            get { return Row["Image"].Equals(DBNull.Value) ? null : (byte[])Row["Image"]; }
            set
            {
                if (value != null)
                    Row["Image"] = value;
                else Row["Image"] = DBNull.Value;
            }
        }

        public UserCritic(DataRow row) : base(row) { }
        public UserCritic(string username, string name, string surname, DateTime? dateOfBirth, GenderType? gender, string country,
        string publicationCompany, UserCritic.Role userRole, string email, byte[] image) : base()
        {
            Username = username;
            Name = name;
            Surname = surname;
            DateOfBirth = dateOfBirth;
            Gender = gender;
            Country = country;
            PublicationCompany = publicationCompany;
            UserRole = userRole;
            Email = email;
            Image = image;
        }

        public override string ToString()
        {
            return Username;
        }

        public static UserCritic GetByEmail(string email)
        {
            email = email.ToLower();

            var query = from row in _dataTable.AsEnumerable().AsParallel()
                        where row["Email"].ToString().ToLower() == email
                        select row;
            DataRow[] result = query.ToArray();
            if (result.Length == 1)
            {
                return new UserCritic(result[0]);
            }
            else
            {
                _dataAdapter.SelectCommand.CommandText = "SELECT * FROM " + _tableName + " WHERE LOWER(Email)=@email";

                if (!_dataAdapter.SelectCommand.Parameters.Contains("@email"))
                    _dataAdapter.SelectCommand.Parameters.Add(new SqlParameter("@email", email));
                else
                    _dataAdapter.SelectCommand.Parameters["@email"].Value = email;

                if (_dataAdapter.Fill(_dataTable) == 1)
                {
                    var selectedRow = from row in _dataTable.AsEnumerable().AsParallel()
                                       where row["Email"].ToString().ToLower() == email
                                       select row;
                    return new UserCritic(selectedRow.ToArray()[0]);
                }
                return null;
            }
        }

        public enum GenderType { Male, Female }
        public enum Role { Admin, User, Critic }
    }
}
