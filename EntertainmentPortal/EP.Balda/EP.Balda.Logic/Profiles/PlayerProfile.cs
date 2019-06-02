using AutoMapper;
using EP.Balda.Data.Entity;
using EP.Balda.Logic.Models;

namespace EP.Balda.Logic.Profiles
{
    public class PlayerProfile : Profile
    {
        public PlayerProfile()
        {
            CreateMap<PlayerDb, Player>();
        }
    }
}