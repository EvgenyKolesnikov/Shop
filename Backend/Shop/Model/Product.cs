using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Shop.Model
{
    public class Product : Entity
    {
        public string? Name { get; set; }   
        public int CategoryId { get; set; }
        public virtual Category? Category { get; set; }
        public virtual List<FeatureValue> Features { get; set; } = new List<FeatureValue>();
        public string? Info { get; set; }
        public float Price { get; set; }
        public string? Rating { get; set; }
    }
}
