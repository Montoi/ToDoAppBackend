using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ToDoApp.Models.Dto
{
    public class ColorCreateDto
    {
       
        public string color { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
