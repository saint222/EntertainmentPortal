using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;

namespace EP.Hangman.Logic.Converters
{
    public class StringTypeConverter : ITypeConverter<List<string>, string>
    {
        public string Convert(List<string> source, string destination, ResolutionContext context)
        {
            return string.Join(',', source);
        }
    }
}
