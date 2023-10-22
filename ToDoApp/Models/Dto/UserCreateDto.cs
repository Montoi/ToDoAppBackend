using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ToDoApp.Models.Dto
{
    public class UserCreateDto
    {
       
        public string Userr { get; set; }
        public string Password { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
