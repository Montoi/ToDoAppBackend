﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoApp.Models.Dto
{
    public class NoteCreateDto
    {

        public string Priority { get; set; }
        [Required]
        public string NoteText { get; set; }
        public DateTime Date { get; set; }
        [Required]
        public int ColorId { get; set; }
        [Required]
        public int UserId { get; set; }
    }
}
