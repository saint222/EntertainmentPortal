using AutoMapper;
using EP.SeaBattle.Data.Models;
using EP.SeaBattle.Logic.Models;
//using System.Collections.Generic;

namespace EP.SeaBattle.Logic.Profiles
{
    public class CellProfile : Profile
    {
        public CellProfile()
        {
            CreateMap<CellDb, Cell>().ReverseMap();
            //CreateMap<ICollection <CellDb>, ICollection<Cell>>().ReverseMap();
        }
    }
}
