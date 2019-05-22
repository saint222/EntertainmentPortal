using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EP.SeaBattle.Web.Models
{
    public class Player
    {
        private Guid _id;
        private string _login;
        private string _password;
        private Social _social;

        public Guid Id { get => _id; set => _id = value; }

        public string Login { get => _login; set => _login = value; }

        public string Password { get => _password; set => _password = value; }

        public Social Social { get => _social; set => _social = value; }
    }
}
