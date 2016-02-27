using CriticWeb.Models.Data;
using System;

namespace CriticWeb.Models.ContentCriticViewModels
{
    public class PerformerOrEntertainmentArrayViewModel
    {
        public PerformerVM[] PerformerArray { get; private set; }
        public EntertainmentVM[] EntertainmentArray { get; private set; }
        public Guid PaginationId { get; private set; }
        public string Label { get; private set; }

        //нельзя передавать два массива сразу, иначе второй просто пропустит
        public PerformerOrEntertainmentArrayViewModel(PerformerVM[] performerArray, EntertainmentVM[] entertainmentArray, 
        Guid paginationId, string label)
        {
            PerformerArray = performerArray;
            if (PerformerArray == null)
                EntertainmentArray = entertainmentArray;
            PaginationId = paginationId;
            Label = label;
        }
    }
}