using AutoMapper;
using EP.Sudoku.Data.Models;
using EP.Sudoku.Logic.Models;

namespace EP.Sudoku.Logic.Profiles
{
    public class SessionProfile : Profile
    {
        public SessionProfile()
        {
            CreateMap<SessionDb, Session>()
                //.ForMember(dest => dest.Participant, e => e.MapFrom(src => src.ParticipantDb))
                .ForMember(dest => dest.Squares, e => e.MapFrom(src => src.SquaresDb))
                .ReverseMap()
                //.ForMember(dest => dest.ParticipantDb, e => e.MapFrom(src => src.Participant))
                .ForMember(dest => dest.SquaresDb, e => e.MapFrom(src => src.Squares));
        }

    }
}
