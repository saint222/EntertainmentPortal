using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EP.WordsMaker.Logic.Models
{
    /// <summary>
    /// Represents <c>Rules</c> class.
    /// </summary>
    public class Rules
    {
        /// <summary>
        /// Description property
        /// </summary>
        /// <value>Game description</value>
        public string _description;

        /// <summary>
        /// Min lenght word property
        /// </summary>
        /// <value>Words lenght restriction</value>
        public int MinWordLenght { get; set; }

    }
}
