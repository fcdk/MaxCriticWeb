using CriticWeb.DataLayer;
using CriticWeb.Models.Data;
using System.Collections.Generic;
using System.Linq;

namespace CriticWeb.Models.ContentCriticViewModels
{
    public class MainPageViewModel
    {
        public EntertainmentVM[] LastBestMovies
        {
            get
            {
                List<EntertainmentVM> result = new List<EntertainmentVM>();
                foreach (Entertainment entertainment in Entertainment.GetLastBestEntertainmentByType(Entertainment.Type.Movie))
                    result.Add(new EntertainmentVM(entertainment));
                return result.OrderBy(ent => ent.EntertainmentDL.AverageCriticPoint()).ToArray();
            }
        }
        public EntertainmentVM[] LastBestGames
        {
            get
            {
                List<EntertainmentVM> result = new List<EntertainmentVM>();
                foreach (Entertainment entertainment in Entertainment.GetLastBestEntertainmentByType(Entertainment.Type.Game))
                    result.Add(new EntertainmentVM(entertainment));
                return result.OrderBy(ent => ent.EntertainmentDL.AverageCriticPoint()).ToArray();
            }
        }
        public EntertainmentVM[] LastBestTVSeries
        {
            get
            {
                List<EntertainmentVM> result = new List<EntertainmentVM>();
                foreach (Entertainment entertainment in Entertainment.GetLastBestEntertainmentByType(Entertainment.Type.TVSeries))
                    result.Add(new EntertainmentVM(entertainment));
                return result.OrderBy(ent => ent.EntertainmentDL.AverageCriticPoint()).ToArray();
            }
        }
        public EntertainmentVM[] LastBestAlbums
        {
            get
            {
                List<EntertainmentVM> result = new List<EntertainmentVM>();
                foreach (Entertainment entertainment in Entertainment.GetLastBestEntertainmentByType(Entertainment.Type.Album))
                    result.Add(new EntertainmentVM(entertainment));
                return result.OrderBy(ent => ent.EntertainmentDL.AverageCriticPoint()).ToArray();
            }
        }

    }
}