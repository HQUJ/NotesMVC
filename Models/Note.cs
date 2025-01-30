using System.ComponentModel.DataAnnotations;

namespace NotesMVC.Models
{
    public class Note
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime LastUpdateOn { get; set; }
        [Required]
        public Client Client { get; set; }
    }
}
