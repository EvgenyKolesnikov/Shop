﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Shop.Model
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Name { get; set; }   
        public int CategoryId { get; set; }
        public string? Info { get; set; }
        public float Price { get; set; }
        public string? Rating { get; set; }
    }
}
