using AutoMapper;
using EP.TicTacToe.Data.Models;
using EP.TicTacToe.Logic.Models;

namespace EP.TicTacToe.Logic.Profiles
{
    public class PlayerProfile: Profile
    {
        public PlayerProfile()
        {
            CreateMap<PlayerDb, Player>()
                .ReverseMap();
        }
    }
}
