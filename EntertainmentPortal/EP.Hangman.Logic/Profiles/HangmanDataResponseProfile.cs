using System.Collections.Generic;
using AutoMapper;
using EP.Hagman.Data;
using EP.Hangman.Logic.Models;

namespace EP.Hangman.Logic.Profiles
{
    public class HangmanDataResponseProfile : Profile
    {
        public HangmanDataResponseProfile()
        {
            CreateMap<HangmanTemporaryData, HangmanDataResponse>()
                .ForMember(dest => dest.CorrectLettersTempData, opt => opt.MapFrom(src => (IEnumerable<string>) src.temp.CorrectLettersTempData))
                .ForMember(dest=> dest.UserAttempts, opt => opt.MapFrom(src => src.temp.UserAttempts))
                .ForMember(dest=> dest.AlphabetTempData, opt => opt.MapFrom(src => (IEnumerable<string>) src.temp.AlphabetTempData));
        }
        
    }
}