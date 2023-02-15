using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Shop.AdminPanel.Commands;
using Shop.AdminPanel.CreateCategoryFeatures;
using Shop.AdminPanel.DeleteCategory;
using Shop.AdminPanel.DeleteFeature;
using Shop.AdminPanel.DeleteProduct;
using Shop.AdminPanel.EditCategory;
using Shop.AdminPanel.EditFeature;
using Shop.AdminPanel.EditProduct;
using Shop.AdminPanel.SeedDatabase;
using Shop.Model;
using Swashbuckle.AspNetCore.Annotations;

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
        public async Task<Category> CreateCategory(CreateCategoryCommand command)
        {
            var category = await _mediator.Send(command);

            foreach(var feature in command.Features)
            {
                var featureCommand = new CreateCategoryFeaturesCommand() { CategoryId = category.Id, Name = feature };
                await _mediator.Send(featureCommand);
            }


            return category;
        }

        [HttpPost("CreateCategoryFeatures")]
        public async Task<CreateCategoryFeaturesResponse> CreateFeature(CreateCategoryFeaturesCommand command)
        {
            return await _mediator.Send(command);
        }


        [HttpPost("CreateProduct")]
        public async Task<Product> CreateProduct(CreateProductCommand command)
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


        /// <summary>
        /// Редактирование продукта
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
    
        [HttpPut("EditProduct")]
        public async Task<string> EditProduct(EditProductCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPut("EditCategory")]
        public async Task<Category> EditCategoryCommand(EditCategoryCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPut("EditFeature")]
        public async Task<Feature> EditFeature(EditFeatureCommand command)
        {
            return await _mediator.Send(command);
        }


        /// <summary>
        ///  Сброс данных
        /// </summary>
        /// <returns></returns>
        [HttpPost("SeedDatabase")]
        public async Task<string> SeedDatabase()
        {

            var command = new SeedDatabaseCommand();

            return await _mediator.Send(command);
        }
    }
}
