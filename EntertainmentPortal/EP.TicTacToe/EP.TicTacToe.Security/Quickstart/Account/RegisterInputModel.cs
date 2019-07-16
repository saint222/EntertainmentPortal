using System.ComponentModel.DataAnnotations;

namespace EP.TicTacToe.Security.Quickstart.Account
{
    public class RegisterInputModel
    {
        [Required] public string UserName { get; set; }
        [Required] public string Password { get; set; }
    }
}