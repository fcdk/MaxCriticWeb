using System;
using CriticWeb.DataLayer;

namespace CriticWeb.Models.Data
{
    public class PerformerInEntertainmentVM
    {
        private PerformerInEntertainment _performerInEntertainment;

        public PerformerInEntertainment PerformerInEntertainmentDL
        {
            get { return _performerInEntertainment; }
        }

        public Guid PerformerId
        {
            get { return _performerInEntertainment.PerformerId; }
            set { _performerInEntertainment.PerformerId = value; }
        }

        public Guid EntertainmentId
        {
            get { return _performerInEntertainment.EntertainmentId; }
            set { _performerInEntertainment.EntertainmentId = value; }
        }

        public PerformerInEntertainment.Role PerformerRole
        {
            get { return _performerInEntertainment.PerformerRole; }
            set { _performerInEntertainment.PerformerRole = value; }
        }

        public PerformerInEntertainmentVM(PerformerInEntertainment performerInEntertainment)
        {
            _performerInEntertainment = performerInEntertainment;
        }

        public PerformerInEntertainmentVM(PerformerVM performer, EntertainmentVM entertainment, PerformerInEntertainment.Role performerRole)
        {
            _performerInEntertainment = new PerformerInEntertainment(performer.PerformerDL, entertainment.EntertainmentDL, performerRole);
        }

    }
}
