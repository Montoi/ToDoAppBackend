﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ToDoApp.Models.Dto
{
    public class UserDto
    {
        
        public int Id { get; set; }
        public string Userr { get; set; }
        public string Password { get; set; }
    }
}
