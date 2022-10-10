using MediatR;
using Microsoft.AspNetCore.Cors;
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

        [HttpDelete("DeleteProduct/{id}")]
        public async Task<string> DeleteProduct([FromRoute] int id)
        {
            var command = new DeleteProductCommand()
            {
                Id = id
            };
            return await _mediator.Send(command);
        }

      
        [HttpDelete("DeleteCategory/{id}")]
        public  Task<string> DeleteCategory([FromRoute]int id)
        {
            var command = new DeleteCategoryCommand()
            {
                Id = id
            };

            return  _mediator.Send(command);
        }

        [HttpDelete("DeleteFeature/{id}")]
        public Task<string> DeleteFeature([FromRoute] int id)
        {
            var command = new DeleteFeatureCommand()
            {
                Id = id
            };
            return _mediator.Send(command);
        }

        [HttpPut("EditProduct")]
        public async Task<string> EditProduct(EditProductCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}
