﻿using System.ComponentModel.DataAnnotations;

namespace Database.Model
{
    public class Course : BaseModel
    {
        [Key]
        public int CourseId { get; set; }   // Automatically generating a new GUID

        [Required]
        public string Title { get; set; }  

        [Required]
        public string Description { get; set; } 

        [Required]
        public string Category { get; set; }  

        [Required]
        public bool IsPremium { get; set; }
        [Required]
        public string? SubCategory { get; set; }  
    }
}
