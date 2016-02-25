using CriticWeb.Models.Data;
using System;

namespace CriticWeb.Models.ContentCriticViewModels
{
    public class AllEntertainmentsAndPerformersViewModel
    {
        public EntertainmentVM[] Movies { get; private set; }
        public Guid MoviePaginationId { get; private set; }
        public EntertainmentVM[] Games { get; private set; }
        public Guid GamePaginationId { get; private set; }
        public EntertainmentVM[] TVSeries { get; private set; }
        public Guid TVSeriesPaginationId { get; private set; }
        public EntertainmentVM[] Albums { get; private set; }
        public Guid AlbumPaginationId { get; private set; }
        public PerformerVM[] Performers { get; private set; }
        public Guid PerformerPaginationId { get; private set; }

        public string nameForSearch { get; set; }

        public AllEntertainmentsAndPerformersViewModel(EntertainmentVM[] movies, Guid moviePaginationId, EntertainmentVM[] games, Guid gamePaginationId, EntertainmentVM[] tVSeries, Guid tVSeriesPaginationId, EntertainmentVM[] albums, Guid albumPaginationId, PerformerVM[] performers, Guid performerPaginationId)
        {
            Movies = movies;
            MoviePaginationId = moviePaginationId;
            Games = games;
            GamePaginationId = gamePaginationId;
            TVSeries = tVSeries;
            TVSeriesPaginationId = tVSeriesPaginationId;
            Albums = albums;
            AlbumPaginationId = albumPaginationId;
            Performers = performers;
            PerformerPaginationId = performerPaginationId;
        }
    }
}