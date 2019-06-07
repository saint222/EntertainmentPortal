using AutoMapper;
using EP.Sudoku.Data.Models;
using EP.Sudoku.Logic.Models;

namespace EP.Sudoku.Logic.Profiles
{
    public class SessionProfile : Profile
    {
        public SessionProfile()
        {
            CreateMap<SessionDb, Session>().ForMember(dest => dest.Square, e => e.MapFrom(src => src.SquareDb))
                .ReverseMap().ForMember(dest => dest.SquareDb, e => e.MapFrom(src => src.Square));
        }

    }
}
