using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;

namespace Esercizio.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string ProfilePicture { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
