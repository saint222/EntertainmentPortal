using AutoMapper;
using EP.Hagman.Data;
using EP.Hangman.Logic.Models;

namespace EP.Hangman.Logic.Profiles
{
    public class HangmanDataResponseProfile : Profile
    {
        public HangmanDataResponseProfile()
        {
            CreateMap<HangmanTemporaryData, HangmanDataResponse>();
        }
        
    }
}