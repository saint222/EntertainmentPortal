using AutoMapper;
using EP.SeaBattle.Data.Models;
using EP.SeaBattle.Logic.Commands;
using EP.SeaBattle.Logic.Models;

namespace EP.SeaBattle.Logic.Profiles
{
    public class GameProfile : Profile
    {
        public GameProfile()
        {
            CreateMap<GameDb, Game>().ReverseMap();
            CreateMap<GameDb, FinishGameCommand>().ReverseMap();
            CreateMap<Game, FinishGameCommand>().ReverseMap();
            CreateMap<GameDb, StartGameCommand>().ReverseMap();
            CreateMap<Game, StartGameCommand>().ReverseMap();
        }
    }
}
