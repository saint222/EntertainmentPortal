using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using EP.Hangman.Data.Models;
using EP.Hangman.Logic.Models;
using EP.Hangman.Logic.Converters;

namespace EP.Hangman.Logic.Profiles
{
    public class GameDbUserGameDataProfile : Profile
    {
        public GameDbUserGameDataProfile()
        {
            CreateMap<string, List<string>>().ConvertUsing(new ListStringTypeConverter());
            CreateMap<List<string>, string>().ConvertUsing(new StringTypeConverter());
            CreateMap<GameDb, UserGameData>()
                //.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                //.ForMember(dest => dest.PickedWord, opt => opt.MapFrom(src => src.PickedWord))
                .ForMember(dest => dest.CorrectLetters,
                    opt => opt.MapFrom(src => src.CorrectLetters))
                //.ForMember(dest => dest.UserErrors, opt => opt.MapFrom(src => src.UserErrors))
                .ForMember(dest => dest.Alphabet, opt => opt.MapFrom(src => src.Alphabet))
                .ReverseMap()
                .ForMember(dest => dest.Alphabet, opt => opt.MapFrom(src => src.Alphabet))
                .ForMember(dest => dest.CorrectLetters, opt => opt.MapFrom(src => src.CorrectLetters));

        }
    }
}