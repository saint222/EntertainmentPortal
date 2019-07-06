using AutoMapper;
using EP.DotsBoxes.Data.Models;
using EP.DotsBoxes.Logic.Models;

namespace EP.DotsBoxes.Logic.Profiles
{
    public class PlayerProfile : Profile
    {
        public PlayerProfile()
        {
            CreateMap<Player, PlayerDb>()
                .ForMember(b => b.Id, opt => opt.MapFrom(b => b.Id))
                .ForMember(b => b.Name, opt => opt.MapFrom(b => b.Name))
                .ForMember(b => b.Color, opt => opt.MapFrom(b => b.Color))
                .ForMember(b => b.Score, opt => opt.MapFrom(b => b.Score))
                .ReverseMap();
        }
    }
}
