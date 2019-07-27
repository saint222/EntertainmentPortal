using AutoMapper;
using EP.SeaBattle.Data.Models;
using EP.SeaBattle.Logic.Models;

namespace EP.SeaBattle.Logic.Profiles
{
    public class PlayerProfile : Profile
    {
        public PlayerProfile()
        {
            CreateMap<PlayerDb, Player>().ReverseMap();
        }
    }
}
