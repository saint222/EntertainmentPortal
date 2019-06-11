using AutoMapper;
using EP.Balda.Data.Models;
using EP.Balda.Logic.Models;

namespace EP.Balda.Logic.Profiles
{
    public class PlayerProfile : Profile
    {
        public PlayerProfile()
        {
            CreateMap<PlayerDb, Player>()
                .ReverseMap();
        }
    }
}