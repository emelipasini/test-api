﻿using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Street
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string City { get; set; }

        public Street(int id, string name, string city)
        {
            Id = id;
            Name = name;
            City = city;
        }
    }
}
