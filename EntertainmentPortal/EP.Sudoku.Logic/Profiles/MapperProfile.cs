using AutoMapper;
using EP.Sudoku.Data.Models;
using EP.Sudoku.Logic.Commands;
using EP.Sudoku.Logic.Models;


namespace EP.Sudoku.Logic.Profiles
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<PlayerDb, Player>()
                .ForMember(dest => dest.Icon, e => e.MapFrom(src => src.IconDb))
                .ForMember(dest => dest.GameSession, e => e.MapFrom(src => src.GameSessionDb));

            CreateMap<Player, PlayerDb>()
                .ForMember(dest => dest.IconDb, e => e.MapFrom(src => src.Icon))
                .ForMember(dest => dest.GameSessionDb, e => e.MapFrom(src => src.GameSession));

            CreateMap<SessionDb, Session>()
                .ForMember(dest => dest.Squares, e => e.MapFrom(src => src.SquaresDb))
                .ForMember(dest => dest.PlayerId, e => e.MapFrom(src => src.PlayerDbId));

            CreateMap<Session, SessionDb>()
                .ForMember(dest => dest.SquaresDb, e => e.MapFrom(src => src.Squares))
                .ForMember(dest => dest.PlayerDbId, e => e.MapFrom(src => src.PlayerId));

            CreateMap<SessionDb, CreateSessionCommand>()
                .ReverseMap();

            CreateMap<CellDb, Cell>()
                .ForMember(dest => dest.SessionId, e => e.MapFrom(src => src.SessionDbId))
                .ReverseMap()
                .ForMember(dest => dest.SessionDbId, e => e.MapFrom(src => src.SessionId));
        }

    }
}
