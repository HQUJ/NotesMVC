using System.ComponentModel.DataAnnotations;

namespace NotesMVC.Models
{
    public class Note
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
        
        //promqna
        [ScaffoldColumn(false)]
        public DateTime LastUpdateOn { get; set; }
        
        [ScaffoldColumn(false)]
        public string? ClientEmail { get; set; }
    }
}
