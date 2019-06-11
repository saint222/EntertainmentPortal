using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;

namespace EP.Hangman.Logic.Converters
{
    public class ListStringTypeConverter : ITypeConverter<string, List<string>>
    {
        public List<string> Convert(string source, List<string> destination, ResolutionContext context)
        {
            return source.Split(',').ToList();
        }
    }
}
