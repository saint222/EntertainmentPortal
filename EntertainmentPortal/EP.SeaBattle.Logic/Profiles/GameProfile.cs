using AutoMapper;
using EP.SeaBattle.Data.Models;
using EP.SeaBattle.Logic.Models;

namespace EP.SeaBattle.Logic.Profiles
{
    public class GameProfile : Profile
    {
        public GameProfile()
        {
            CreateMap<GameDb, Game>().ReverseMap();
        }
    }
}
