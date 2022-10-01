using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Model
{
    public class Feature : Entity
    {        
        public virtual List<Category> Categories { get; set; }  = new List<Category>();
        public string? Name { get; set; }
    }
}
