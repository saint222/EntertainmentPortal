using AutoMapper;
using EP.DotsBoxes.Data.Models;
using EP.DotsBoxes.Logic.Models;

namespace EP.DotsBoxes.Logic.Profiles
{
    public class CellProfile : Profile
    {
        public CellProfile()
        {
            CreateMap<Cell, CellDb>()
                .ForMember(b => b.Id, opt => opt.MapFrom(b => b.Id))
                .ForMember(b => b.Row, opt => opt.MapFrom(b => b.Row))
                .ForMember(b => b.Column, opt => opt.MapFrom(b => b.Column))
                .ForMember(b => b.Top, opt => opt.MapFrom(b => b.Top))
                .ForMember(b => b.Bottom, opt => opt.MapFrom(b => b.Bottom))
                .ForMember(b => b.Right, opt => opt.MapFrom(b => b.Right))
                .ForMember(b => b.Left, opt => opt.MapFrom(b => b.Left))
                .ForMember(b => b.Name, opt => opt.MapFrom(b => b.Name))
                .ReverseMap();          
        }
    }
}
