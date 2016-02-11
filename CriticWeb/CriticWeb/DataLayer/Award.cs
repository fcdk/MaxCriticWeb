using System;
using System.Data;

namespace CriticWeb.DataLayer
{
    [TableName("Award")]
    [IdColumnName("AwardId")]
    [NameColumnName("Name")]
    public class Award : Entity<Award>
    {
        public Guid? PerformerId
        {
            get { return Row["PerformerId"].Equals(DBNull.Value) ? default(Guid?) : (Guid)Row["PerformerId"]; }
            set
            {
                if (value != null)
                    Row["PerformerId"] = value;
                else Row["PerformerId"] = DBNull.Value;
            }
        }
        public Guid? EntertainmentId
        {
            get { return Row["EntertainmentId"].Equals(DBNull.Value) ? default(Guid?) : (Guid)Row["EntertainmentId"]; }
            set
            {
                if (value != null)
                    Row["EntertainmentId"] = value;
                else Row["EntertainmentId"] = DBNull.Value;
            }
        }
        public string Name
        {
            get { return Row[_nameColumnName].ToString(); }
            set { Row[_nameColumnName] = value; }
        }
        public string Nomination
        {
            get { return Row["Nomination"].ToString(); }
            set
            {
                if (value != String.Empty)
                    Row["Nomination"] = value;
                else Row["Nomination"] = DBNull.Value;
            }
        }
        public DateTime Date
        {
            get { return (DateTime)Row["Date"]; }
            set { Row["Date"] = value; }
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

        public Award(DataRow row) : base(row)
        {
            ////Logger.Info("Award.Award", "Екземпляр Award створений.");
        }
        public Award(Performer performer, Entertainment entertainment, string name, string nomination, DateTime date,
        byte[] image) : base()
        {
            if (performer == null)
                PerformerId = default(Guid?);
            else PerformerId = performer.Id;
            if (entertainment == null)
                EntertainmentId = default(Guid?);
            else EntertainmentId = entertainment.Id;
            Name = name;
            Nomination = nomination;
            Date = date;
            Image = image;

            ////Logger.Info("Award.Award", "Екземпляр Award створений.");
        }
        
    }
}
