using AutoMapper;
using EP.TicTacToe.Data.Models;
using EP.TicTacToe.Logic.Models;

namespace EP.TicTacToe.Logic.Profiles
{
    public class FirstPlayerProfile : Profile
    {
        public FirstPlayerProfile()
        {
            CreateMap<FirstPlayerDb, FirstPlayer>()
                .ReverseMap();
        }
    }
}