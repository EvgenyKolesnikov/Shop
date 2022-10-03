using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Shop.Model
{
    public class Feature : Entity
    {
        [IgnoreDataMember]
        public virtual List<Category> Categories { get; set; } = new List<Category>();
        public string? Name { get; set; }
    }
}
