using AutoMapper;
using EP.TicTacToe.Data.Models;
using EP.TicTacToe.Logic.Models;

namespace EP.TicTacToe.Logic.Profiles
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