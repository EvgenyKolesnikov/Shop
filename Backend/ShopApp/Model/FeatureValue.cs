using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Shop.Model
{
    public class FeatureValue : Entity
    {
        public int ProductId { get; set; }

        [IgnoreDataMember]
        public virtual Product? Product { get; set; }
        public int FeatureId { get; set; }
        [IgnoreDataMember]
        public virtual Feature? Feature { get; set; }
        public string? Value { get; set; }
    }
}
