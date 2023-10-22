using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ToDoApp.Models.Dto
{
    public class ColorUpdateDto
    {
        
        public int id { get; set; }
        public string color { get; set; }
        public DateTime UpdatedDate { get; set; }


    }
}
