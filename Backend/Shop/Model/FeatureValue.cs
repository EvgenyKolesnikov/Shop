using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Model
{
    public class FeatureValue : Entity
    {
        public int ProductId { get; set; }
        public virtual Product? Product { get; set; }
        public int FeatureId { get; set; }
        public virtual Feature? Feature { get; set; }
        public string? Value { get; set; }
    }
}
