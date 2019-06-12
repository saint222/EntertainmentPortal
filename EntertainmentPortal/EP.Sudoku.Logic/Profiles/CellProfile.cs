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
            CreateMap<CellDb, Cell>()
                .ReverseMap();
        }
    }
}
