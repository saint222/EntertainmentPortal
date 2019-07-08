using AutoMapper;
using EP.Sudoku.Data.Models;
using EP.Sudoku.Logic.Commands;
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
                .ForMember(dest => dest.PlayerId, e => e.MapFrom(src => src.PlayerDbId));

            CreateMap<Session, SessionDb>()
               //.ForMember(dest => dest.ParticipantDb, e => e.MapFrom(src => src.Participant))
               .ForMember(dest => dest.SquaresDb, e => e.MapFrom(src => src.Squares))              
               .ForMember(dest => dest.PlayerDbId, e => e.MapFrom(src => src.PlayerId));


            CreateMap<SessionDb, CreateSessionCommand>()
                .ForMember(dest => dest.PlayerId, e => e.MapFrom(src => src.PlayerDbId))
                .ReverseMap()
                .ForMember(dest => dest.PlayerDbId, e => e.MapFrom(src => src.PlayerId));
        }

    }
}
