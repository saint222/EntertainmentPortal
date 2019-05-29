using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace EP.DotsBoxes.Logic.Models
{
    /// <summary>
    /// The PlayerDb class represents the player and contains his data.
    /// </summary>
    public class Player
    {
        /// <value>Gets/sets player Id value.</value>
        public int Id { get; set; }
        /// <value>Gets/sets player Name value.</value>
        public string Name { get; set; }
        /// <value>Gets/sets player Color value.</value>
        public string Color { get; set; }
        /// <value>Gets/sets player Score value.</value>
        public int Score { get; set; }
    }
    
}
