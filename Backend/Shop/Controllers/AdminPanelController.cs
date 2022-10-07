using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shop.AdminPanel.Commands;
using Shop.AdminPanel.CreateCategoryFeatures;
using Shop.AdminPanel.DeleteCategory;
using Shop.AdminPanel.DeleteFeature;
using Shop.AdminPanel.DeleteProduct;
using Shop.AdminPanel.EditProduct;

namespace Shop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdminPanelController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AdminPanelController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("CreateCategory")]
        public async Task<string> CreateCategory(CreateCategoryCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPost("CreateCategoryFeatures")]
        public async Task<CreateCategoryFeaturesResponse> CreateFeature(CreateCategoryFeaturesCommand command)
        {
            return await _mediator.Send(command);
        }


        [HttpPost("CreateProduct")]
        public async Task<string> CreateProduct(CreateProductCommand command)
        {
           
            return await _mediator.Send(command);
        }

        [HttpDelete("DeleteProduct")]
        public async Task<string> DeleteProduct(DeleteProductCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpDelete("DeleteCategory")]
        public async Task<string> DeleteCategory(DeleteCategoryCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpDelete("DeleteFeature")]
        public async Task<string> DeleteFeature(DeleteFeatureCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPut("EditProduct")]
        public async Task<string> EditProduct(EditProductCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}
