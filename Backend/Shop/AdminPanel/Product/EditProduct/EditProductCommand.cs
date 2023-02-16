using MediatR;
using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace Shop.AdminPanel.EditProduct
{
    public class EditProductCommand : IRequest<EditProductResponse>
    {
        public int ProductId { get; set; }
        public string? Name { get; set; }
        public int? CategoryId { get; set; } 

        public string? Info { get; set; }
        public float? Price { get; set; }


        /// <example>{"FeatureId":1,"FeatureValue":"Red Color"} </example>
        public Dictionary<int,string> FeatureValue { get; set; }

    }
}
