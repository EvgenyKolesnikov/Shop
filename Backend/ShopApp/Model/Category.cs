﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Shop.Model
{
    public class Category : Entity
    {
        public string? Name { get; set; }
       
        public int? ParentCategoryId { get; set; }
        public virtual Category? ParentCategory { get; set; } 

        public virtual List<Category> ChildCategories { get; set; } 
   
        public virtual List<Feature> Features { get; set; } = new List<Feature>();

        [IgnoreDataMember]
        public virtual List<Product>? Products { get; set; } = new List<Product>();
    }
}
