using AutoMapper;
using EP.DotsBoxes.Data.Models;
using EP.DotsBoxes.Logic.Models;

namespace EP.DotsBoxes.Logic.Profiles
{
    public class GameBoardProfile : Profile 
    {
        public GameBoardProfile()
        {
            CreateMap<GameBoardDb, GameBoard>()
                .ForMember(b => b.Id, opt => opt.MapFrom(b => b.Id))
                .ForMember(b => b.Rows, opt => opt.MapFrom(b => b.Rows))
                .ForMember(b => b.Columns, opt => opt.MapFrom(b => b.Columns))
                .ForMember(b => b.Cells, opt => opt.MapFrom(b => b.Cells))
                .ForMember(b => b.Players, opt => opt.MapFrom(b => b.Players))
                .ReverseMap();          
        }
    }
}
