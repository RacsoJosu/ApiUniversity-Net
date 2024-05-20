﻿using System.ComponentModel.DataAnnotations;

namespace ApiRestfull.Models
{
    public class Category : BaseEntity
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}
