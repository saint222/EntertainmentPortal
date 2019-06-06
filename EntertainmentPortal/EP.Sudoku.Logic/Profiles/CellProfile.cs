using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using EP.Sudoku.Data.Models;
using EP.Sudoku.Logic.Models;

namespace EP.Sudoku.Logic.Profiles
{
    public class CellProfile : Profile
    {
        public CellProfile()
        {
            CreateMap<CellDb, Cell>().ForMember(dest => dest.CellPencil, e => e.MapFrom(src => src.CellPencilDb))
                .ReverseMap().ForMember(dest => dest.CellPencilDb, e => e.MapFrom(src => src.CellPencil));
        }
    }
}
