using AutoMapper;
using EP.SeaBattle.Data.Models;
using EP.SeaBattle.Logic.Commands;
using EP.SeaBattle.Logic.Models;
using System;

namespace EP.SeaBattle.Logic.Profiles
{
    public class CellProfile : Profile
    {
        public CellProfile()
        {
            CreateMap<CellDb, Cell>()
                .ReverseMap();
            CreateMap<Cell, AddNewCellCommand>()
                .ReverseMap();
            CreateMap<CellDb, AddNewCellCommand>()
                .ReverseMap();
        }
    }
}
