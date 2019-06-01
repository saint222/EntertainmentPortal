using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using EP.DotsBoxes.Data.Models;
using EP.DotsBoxes.Logic.Models;

namespace EP.DotsBoxes.Logic.Profiles
{
    public class GameBoardProfile : Profile 
    {
        public GameBoardProfile()
        {
            CreateMap<GameBoard, GameBoardDb>();
        }
    }
}
