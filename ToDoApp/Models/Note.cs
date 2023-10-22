using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoApp.Models
{
    public class Note
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

     
        public string Priority { get; set; }
        public string NoteText { get; set; }
        [ForeignKey("Color")]
        public int ColorId { get; set; }
        public Color Color { get; set; }


        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }
        public DateTime Date { get; set; }
    }
}
