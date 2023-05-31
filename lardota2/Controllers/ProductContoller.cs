using Domain.Interfaces;
using Domain.Models;
using lardota2.Contracts.User;
using Mapster;
using Microsoft.AspNetCore.Mvc;
namespace BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductService _userService;
        public ProductController(IProductService userService)
        {
            _userService = userService;
        }
        /// <summary>
        /// Поиск продукта
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        /// POST /Todo
        /// {
        /// "Product":"Пылесос"
        /// "ProductID":"3"
        /// "id":"251"
        /// }
        ///
        /// </remarks>
        /// <param name="model">Пользователь</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get(ProductRequest request)
        {
            var UserDTo = request.Adapt<User>();
            await _userService.Get(UserDTo);
            return Ok();
        }
        /// <summary>
        /// Удаление из корзины
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        /// POST /Todo
        /// {
        /// "Product": "Пылесос"
        /// "ProductID":"222"
        /// }
        ///
        /// </remarks>
        /// <param name="model">Пользователь</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> Delete(ProductRequest request)
        {
            var UserDTo = request.Adapt<User>();
            await _userService.Get(UserDTo);
            return Ok();
        }
    }
}
