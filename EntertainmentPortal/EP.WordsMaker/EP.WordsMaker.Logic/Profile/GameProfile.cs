using AutoMapper;
using EP.WordsMaker.Data.Models;
using EP.WordsMaker.Logic.Models;

namespace EP.WordsMaker.Logic.Profiles
{
	public class GameProfile : Profile
	{
		public GameProfile()
        {
            CreateMap<GameDb, Game>().ReverseMap();
            CreateMap<PlayerDb, Player>().ReverseMap();
        }
	}
}