using System;
using CriticWeb.DataLayer;

namespace CriticWeb.Models.Data
{
    public class AwardVM
    {
        private Award _award;

        public Award AwardDL
        {
            get { return _award; }
        }

        public Guid? PerformerId
        {
            get { return _award.PerformerId; }
            set { _award.PerformerId = value; }
        }

        public Guid? EntertainmentId
        {
            get { return _award.EntertainmentId; }
            set { _award.EntertainmentId = value; }
        }

        public string Name
        {
            get { return _award.Name; }
            set { _award.Name = value; }
        }

        public string Nomination
        {
            get { return _award.Nomination; }
            set { _award.Nomination = value; }
        }

        public DateTime Date
        {
            get { return _award.Date; }
            set { _award.Date = value; }
        }

        public byte[] Image
        {
            get { return _award.Image; }
            set { _award.Image = value; }
        }

        public AwardVM(Award award)
        {
            _award = award;
        }

        public AwardVM(PerformerVM performer, EntertainmentVM entertainment, string name, string nomination, DateTime date, byte[] image)
        {
            _award = new Award(performer == null ? null : performer.PerformerDL, entertainment == null ? null : entertainment.EntertainmentDL, name, nomination, date, image);
        }

        public override string ToString()
        {
            return Name + " (" + Date.ToString("dd/MM/yyyy") + ")" + (Nomination == null ? String.Empty : ": " + Nomination);
        }

    }
}
