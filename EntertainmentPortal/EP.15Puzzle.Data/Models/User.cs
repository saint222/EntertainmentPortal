using System;
using System.Collections.Generic;
using System.Text;

namespace EP._15Puzzle.Data.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }

        public User(int id, string name, string country)
        {
            Id = id;
            Name = name;
            Country = country;
        }
    }
}
