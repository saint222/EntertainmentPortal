using EP.SeaBattle.Data.Context;
using EP.SeaBattle.Logic.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EP.SeaBattle.Logic.Validators
{
    public class ShotValidator
    {
        SeaBattleDbContext _context;
        public ShotValidator(SeaBattleDbContext context)
        {
            _context = context;
        }
    }
}
