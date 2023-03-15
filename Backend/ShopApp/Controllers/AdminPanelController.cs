using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Shop.AdminPanel;
using Shop.AdminPanel.ChangeCountProduct;
using Shop.AdminPanel.Commands;
using Shop.AdminPanel.CreateCategoryFeatures;
using Shop.AdminPanel.CreateProduct;
using Shop.AdminPanel.DeleteCategory;
using Shop.AdminPanel.DeleteFeature;
using Shop.AdminPanel.DeleteProduct;
using Shop.AdminPanel.EditCategory;
using Shop.AdminPanel.EditFeature;
using Shop.AdminPanel.EditProduct;
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


        /// <summary>
        /// Создать категорию
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("CreateCategory")]
        public async Task<CategoryResponse> CreateCategory(CreateCategoryCommand command)
        {
            var categoryResponse = await _mediator.Send(command);

            foreach(var feature in command.Features)
            {
                var featureCommand = new CreateCategoryFeaturesCommand() { CategoryId = categoryResponse.Category.Id, Name = feature };
                await _mediator.Send(featureCommand);
            }

            return categoryResponse;
        }

        /// <summary>
        /// Создать атрибут категории
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("CreateCategoryFeatures")]
        public async Task<FeaturesResponse> CreateFeature(CreateCategoryFeaturesCommand command)
        {
            var response = await _mediator.Send(command);
            
            return response;
        }


        /// <summary>
        /// Создать продукт
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("CreateProduct")]
        public async Task<ProductResponse> CreateProduct(CreateProductCommand command)
        {
            var response = await _mediator.Send(command);

            return response;
        }


        /// <summary>
        /// Удалить продукт
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("DeleteProduct/{id}")]
        public async Task<string> DeleteProduct([FromRoute] int id)
        {
            var response = await _mediator.Send(new DeleteProductCommand() { Id = id });
         
            return response;
        }

      
        /// <summary>
        /// Удалить категорию
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("DeleteCategory/{id}")]
        public async Task<string> DeleteCategory([FromRoute] int id)
        {
            var response = await _mediator.Send(new DeleteCategoryCommand() { Id = id });

            return response;
        }

        /// <summary>
        /// Удалить атрибут
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("DeleteFeature/{id}")]
        public async Task<string> DeleteFeature([FromRoute] int id)
        {
            var response = await _mediator.Send(new DeleteFeatureCommand() { Id = id });
            
            return response;
        }


        /// <summary>
        /// Редактирование продукта
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("EditProduct")]
        public async Task<ProductResponse> EditProduct(EditProductCommand command)
        {
            var reponse = await _mediator.Send(command);

            return reponse;
        }

        /// <summary>
        /// Редактирование категории
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("EditCategory")]
        public async Task<CategoryResponse> EditCategoryCommand(EditCategoryCommand command)
        {
            var response = await _mediator.Send(command);

            return response;
        }

        /// <summary>
        /// Редактирование атрибута
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("EditFeature")]
        public async Task<FeaturesResponse> EditFeature(EditFeatureCommand command)
        {
            var response = await _mediator.Send(command);
            
            return response;
        }

        /// <summary>
        /// Изменить кол-во товара
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("ChangeCountProduct")]
        public async Task<ProductResponse> ChangeCountProduct(ChangeCountProductCommand command)
        {
            var response = await _mediator.Send(command);

            return response;
        }

    }
}
