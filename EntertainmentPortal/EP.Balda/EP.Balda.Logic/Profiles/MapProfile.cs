using AutoMapper;
using EP.Balda.Data.Models;
using EP.Balda.Logic.Models;

namespace EP.Balda.Logic.Profiles
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<MapDb, Map>()
                .ReverseMap();
        }
    }
}