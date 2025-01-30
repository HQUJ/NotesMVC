using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace NotesMVC.Models
{
    public class Client : IdentityUser
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
