using AutoMapper;
using EP.Balda.Data.Models;
using EP.Balda.Logic.Models;

namespace EP.Balda.Logic.Profiles
{
    public class GameProfile : Profile
    {
        public GameProfile()
        {
            CreateMap<GameDb, Game>()
                .ReverseMap();
        }
    }
}