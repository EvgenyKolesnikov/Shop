using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Model
{
    public class Feature
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FeatureId { get; set; }
        
        [Required]
        public int CategoryId { get; set; }
        
        public string? FeatureName { get; set; }

        public List<FeatureValues>? FeatureValues { get; set; }  
        
    }
}
