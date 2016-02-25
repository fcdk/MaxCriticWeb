using CriticWeb.DataLayer;
using CriticWeb.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CriticWeb.Models.ContentCriticViewModels
{
    public class EntertainmentAndPerformerSearchViewModel
    {
        public EntertainmentVM[] Movies { get; private set; }
        public Guid MoviePaginationId { get; private set; }
        public EntertainmentVM[] Games { get; private set; }
        public Guid GamePaginationId { get; private set; }
        public EntertainmentVM[] TVSeries { get; private set; }
        public Guid TVSeriesPaginationId { get; private set; }
        public EntertainmentVM[] Albums { get; private set; }
        public Guid AlbumsPaginationId { get; private set; }
        public PerformerVM[] Performers { get; private set; }
        public Guid PerformerPaginationId { get; private set; }

        public EntertainmentAndPerformerSearchViewModel(string nameForSearch = null, Entertainment.Type? type = null)
        {
            EntertainmentVM[] entertainmentsVM = null;
            PerformerVM[] performersVM = null;
            if ((nameForSearch == null || nameForSearch == String.Empty) && type == null)
            {
                Entertainment[] entertainments = Entertainment.GetLastNEntertainmentByReviewCount(50, 1);
                if (entertainments != null)
                {
                    List<EntertainmentVM> entertainmentsListVM = new List<EntertainmentVM>();
                    foreach (var entertainment in entertainments)
                        entertainmentsListVM.Add(new EntertainmentVM(entertainment));
                    entertainmentsVM = entertainmentsListVM.ToArray();
                }                

                Performer[] performers = Performer.GetRandomFirstTen();
                if (performers != null)
                {
                    List<PerformerVM> performersListVM = new List<PerformerVM>();
                    foreach (var performer in performers)
                        performersListVM.Add(new PerformerVM(performer));
                    performersVM = performersListVM.ToArray();
                }                
            }

            if (nameForSearch != null && nameForSearch != String.Empty && type == null)
            {
                Entertainment[] entertainments = Entertainment.GetByName(nameForSearch);
                if (entertainments != null)
                {
                    List<EntertainmentVM> entertainmentsListVM = new List<EntertainmentVM>();
                    foreach (var entertainment in entertainments)
                        entertainmentsListVM.Add(new EntertainmentVM(entertainment));
                    entertainmentsVM = entertainmentsListVM.ToArray();
                }

                Performer[] performers = Performer.GetByName(nameForSearch);
                if (performers != null)
                {
                    List<PerformerVM> performersListVM = new List<PerformerVM>();
                    foreach (var performer in performers)
                        performersListVM.Add(new PerformerVM(performer));
                    performersVM = performersListVM.ToArray();
                }
            }

            if ((nameForSearch == null || nameForSearch == String.Empty) && type != null)
            {
                Entertainment[] entertainments = null;
                switch (type)
                {
                    case Entertainment.Type.Movie:
                        entertainments = Entertainment.GetLastNEntertainmentByTypeAndReviewCount(Entertainment.Type.Movie, 50, 1);
                        break;
                    case Entertainment.Type.Game:
                        entertainments = Entertainment.GetLastNEntertainmentByTypeAndReviewCount(Entertainment.Type.Game, 50, 1);
                        break;
                    case Entertainment.Type.TVSeries:
                        entertainments = Entertainment.GetLastNEntertainmentByTypeAndReviewCount(Entertainment.Type.TVSeries, 50, 1);
                        break;
                    case Entertainment.Type.Album:
                        entertainments = Entertainment.GetLastNEntertainmentByTypeAndReviewCount(Entertainment.Type.Album, 50, 1);
                        break;
                    default:
                        break;
                }                
                if (entertainments != null)
                {
                    List<EntertainmentVM> entertainmentsListVM = new List<EntertainmentVM>();
                    foreach (var entertainment in entertainments)
                        entertainmentsListVM.Add(new EntertainmentVM(entertainment));
                    entertainmentsVM = entertainmentsListVM.ToArray();
                }
            }

            Performers = performersVM;            
            if (entertainmentsVM != null)
            {
                Movies = entertainmentsVM.Where(e => e.EntertainmentType == DataLayer.Entertainment.Type.Movie).OrderByDescending( e => e.ReleaseDate ).ToArray();
                Games = entertainmentsVM.Where(e => e.EntertainmentType == DataLayer.Entertainment.Type.Game).OrderByDescending(e => e.ReleaseDate).ToArray();
                TVSeries = entertainmentsVM.Where(e => e.EntertainmentType == DataLayer.Entertainment.Type.TVSeries).OrderByDescending(e => e.ReleaseDate).ToArray();
                Albums = entertainmentsVM.Where(e => e.EntertainmentType == DataLayer.Entertainment.Type.Album).OrderByDescending(e => e.ReleaseDate).ToArray();
            }

            MoviePaginationId = Guid.NewGuid();
            GamePaginationId = Guid.NewGuid();
            TVSeriesPaginationId = Guid.NewGuid();
            AlbumsPaginationId = Guid.NewGuid();
            PerformerPaginationId = Guid.NewGuid();            
        }
    }
}
