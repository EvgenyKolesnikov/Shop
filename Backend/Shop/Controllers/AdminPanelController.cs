using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shop.AdminPanel.Commands;
using Shop.AdminPanel.CreateCategoryFeatures;

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
        public async Task<int> CreateCategory(CreateCategoryCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPost("CreateCategoryFeatures")]
        public async Task<CreateCategoryFeaturesResponse> CreateFeature(CreateCategoryFeaturesCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPost("LinkFeatureWithCategoryCommand")]
        public async Task LinkFeature(LinkFeatureWithCategoryCommand command)
        {
            await _mediator.Send(command);
        }

        [HttpPost("CreateProduct")]
        public async Task<int> CreateProduct(CreateProductCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}
