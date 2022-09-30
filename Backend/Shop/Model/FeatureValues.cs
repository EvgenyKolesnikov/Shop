using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Model
{
    public class FeatureValues
    {
       [Key]
        public int ProductId { get; set; }
        public string? Value { get; set; }
    }
}
