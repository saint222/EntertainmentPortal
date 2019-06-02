using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EP.SeaBattle.Logic.Models
{
    public class Player
    {
        private Guid _id;
        private string _login;
        private bool _isBanned;
        private DateTime? _banExpire;
        private int _rating;

        public Guid Id { get => _id; set => _id = value; }

        /// <summary>
        /// User Login
        /// </summary>
        public string Login { get => _login; set => _login = value; }

        /// <summary>
        /// shows whether the user is banned
        /// </summary>
        /// <remarks>
        /// true if banned
        /// </remarks>
        public bool IsBanned { get => _isBanned; set => _isBanned = value; }

        /// <summary>
        /// Ban expiration date
        /// </summary>
        public DateTime? BanExpire { get => _banExpire; set => _banExpire = value; }

        /// <summary>
        /// User ladder rating
        /// </summary>
        public int Rating { get => _rating; set => _rating = value; }
    }
}
