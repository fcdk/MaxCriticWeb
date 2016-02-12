using CriticWeb.DataLayer;
using CriticWeb.Models.Data;
using System;

namespace CriticWeb.Models.ContentCriticViewModels
{
    public class EntertainmentDetailsViewModel
    {
        private EntertainmentVM _entertainment;
        private int? _avarageCriticPoint;
        private int? _avarageUserPoint;
        private string _trailerLinkForFrame;

        public EntertainmentVM EntertainmentDetails
        {
            get { return _entertainment; }
        }

        public int? AvarageCriticPoint
        {
            get { return _avarageCriticPoint; }
        }

        public int? AvarageUserPoint
        {
            get { return _avarageUserPoint; }
        }

        public string TrailerLinkForFrame
        {
            get { return _trailerLinkForFrame; }
        } 

        public EntertainmentDetailsViewModel(Guid entertainmentId)
        {
            _entertainment = new EntertainmentVM(Entertainment.GetById(entertainmentId));
            _avarageCriticPoint = _entertainment.EntertainmentDL.AverageCriticPointForOneEntertainment();
            _avarageUserPoint = _entertainment.EntertainmentDL.AverageUserPointForOneEntertainment();
            _trailerLinkForFrame = _entertainment.TrailerLink.Replace("https://www.youtube.com/watch?v=", "https://www.youtube.com/embed/");
        }
    }
}