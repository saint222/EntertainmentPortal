using AutoMapper;
using EP.TicTacToe.Data.Models;
using EP.TicTacToe.Logic.Models;

namespace EP.TicTacToe.Logic.Profiles
{
    public class HaunterProfile : Profile
    {
        public HaunterProfile()
        {
            CreateMap<HaunterDb, Haunter>()
                .ReverseMap();
        }
    }
}