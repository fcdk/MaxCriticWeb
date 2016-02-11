using System;
using System.Data;

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

    }
}
