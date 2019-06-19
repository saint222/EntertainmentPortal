using System;
using System.Collections.Generic;
using System.Text;

namespace EP._15Puzzle.Data.Models
{
    /// <summary>
    /// Represents <c>User</c> class.
    /// </summary>
    public class User
    {
        /// <summary>
        /// ID property
        /// </summary>
        /// <value>Represents unique id of user</value>
        public int Id { get; set; }

        /// <summary>
        /// Name property
        /// </summary>
        /// <value>Represents username of user</value>
        public string Name { get; set; }

        /// <summary>
        /// Country property
        /// </summary>
        /// <value>Represents country where user from</value>
        public string Country { get; set; }
        
    }
}
