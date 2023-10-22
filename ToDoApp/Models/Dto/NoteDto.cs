using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoApp.Models.Dto
{
    public class NoteDto
    {
        public int Id { get; set; }

        public string Priority { get; set; }
        [Required]
        public string NoteText { get; set; }
        [Required]
        public int ColorId { get; set; }
        [Required]
        public int UserId { get; set; }
    }
}
