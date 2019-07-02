using AutoMapper;
using EP.TicTacToe.Data.Models;
using EP.TicTacToe.Logic.Models;

namespace EP.TicTacToe.Logic.Profiles
{
    public class ChainProfile : Profile
    {
        public ChainProfile()
        {
            CreateMap<ChainDb, Chain>()
                .ReverseMap();
        }
    }
}