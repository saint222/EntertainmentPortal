using AutoMapper;
using EP.TicTacToe.Data.Models;
using EP.TicTacToe.Logic.Models;

namespace EP.TicTacToe.Logic.Profiles
{
    public class CellProfile : Profile
    {
        public CellProfile()
        {
            CreateMap<CellDb, Cell>()
                .ReverseMap();
        }
    }
}