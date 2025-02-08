using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace NotesMVC.Models
{
    public class Client : IdentityUser
    {
        [Key]
        public int Id { get; set; }
        [Required]
        //promqna
        public string FirstName { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
