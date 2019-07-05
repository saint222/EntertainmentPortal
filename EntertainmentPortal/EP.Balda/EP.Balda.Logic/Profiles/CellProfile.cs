using AutoMapper;
using EP.Balda.Data.Models;
using EP.Balda.Logic.Models;

namespace EP.Balda.Logic.Profiles
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