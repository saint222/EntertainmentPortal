using AutoMapper;
using EP.TicTacToe.Data.Models;
using EP.TicTacToe.Logic.Models;

namespace EP.TicTacToe.Logic.Profiles
{
    public class SecondPlayerProfile : Profile
    {
        public SecondPlayerProfile()
        {
            CreateMap<SecondPlayerDb, SecondPlayer>()
                .ReverseMap();
        }
    }
}