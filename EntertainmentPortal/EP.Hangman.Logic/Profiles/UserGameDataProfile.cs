using System.Collections.Generic;
using AutoMapper;
using EP.Hangman.Data.Models;
using EP.Hangman.Logic.Models;

namespace EP.Hangman.Logic.Profiles
{
    public class UserGameDataProfile : Profile
    {
        public UserGameDataProfile()
        {
            CreateMap<GameDb, UserGameData>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.CorrectLettersTempData, opt => opt.MapFrom(src => (IEnumerable<string>) src.CorrectLetters))
                .ForMember(dest=> dest.UserErrors, opt => opt.MapFrom(src => src.UserErrors))
                .ForMember(dest=> dest.AlphabetTempData, opt => opt.MapFrom(src => (IEnumerable<string>) src.Alphabet));
        }
    }
}