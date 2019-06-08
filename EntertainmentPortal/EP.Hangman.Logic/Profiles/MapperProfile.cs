using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CSharpFunctionalExtensions;
using EP.Hangman.Data.Models;
using EP.Hangman.Logic.Models;
using EP.Hangman.Logic.Converters;

namespace EP.Hangman.Logic.Profiles
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Result<UserGameData>, ControllerData>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Value.Id))
                .ForMember(dest => dest.UserErrors, opt => opt.MapFrom(src => src.Value.UserErrors))
                .ForMember(dest => dest.Alphabet, opt => opt.MapFrom(src => src.Value.Alphabet))
                .ForMember(dest => dest.CorrectLetters, opt => opt.MapFrom(src => src.Value.CorrectLetters))
                .ReverseMap();
            CreateMap<string, List<string>>().ConvertUsing(new ListStringTypeConverter());
            CreateMap<List<string>, string>().ConvertUsing(new StringTypeConverter());
            CreateMap<GameDb, Result<UserGameData>>()
                .ForMember(dest => dest.Value.CorrectLetters, opt => opt.MapFrom(src => src.CorrectLetters))
                .ForMember(dest => dest.Value.Alphabet, opt => opt.MapFrom(src => src.Alphabet))
                .ForMember(dest => dest.Value.UserErrors, opt => opt.MapFrom(src => src.UserErrors))
                .ForMember(dest => dest.Value.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Value.PickedWord, opt => opt.MapFrom(src => src.PickedWord))
                .ReverseMap()
                .ForMember(dest => dest.Alphabet, opt => opt.MapFrom(src => src.Value.Alphabet))
                .ForMember(dest => dest.CorrectLetters, opt => opt.MapFrom(src => src.Value.CorrectLetters))
                .ForMember(dest => dest.UserErrors, opt => opt.MapFrom(src => src.Value.UserErrors))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Value.Id))
                .ForMember(dest => dest.PickedWord, opt => opt.MapFrom(src => src.Value.PickedWord));

        }
    }
}