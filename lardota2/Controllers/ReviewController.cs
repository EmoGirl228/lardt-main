using Domain.Interfaces;
using Domain.Models;
using lardota2.Contracts.User;
using Mapster;
using Microsoft.AspNetCore.Mvc;
namespace BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private IReviewBase _userService;
        public ReviewController(IReviewBase userService)
        {
            _userService = userService;
        }
        /// <summary>
        /// Отзыв
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        /// POST /Todo
        /// {
        /// "UserID":"Пылесос"
        /// "Rating":"2"
        /// "Comment":"Пылесос не пылесосит"
        /// }
        ///
        /// </remarks>
        /// <param name="model">Пользователь</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(ReviewUserRequest request)
        {
            var UserDTo = request.Adapt<User>();
            await _userService.Create(UserDTo);
            return Ok();
        }
        /// <summary>
        /// Отзыв
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
        public async Task<IActionResult> Delete(ReviewUserRequest request)
        {
            var UserDTo = request.Adapt<User>();
            await _userService.Delete(UserDTo);
            return Ok();
        }
        /// <summary>
        /// Отзыв
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        /// POST /Todo
        /// {
        /// "UserID":"2"
        /// "rating": "4"
        /// "Comment":"Пылесос пылесосит"
        /// }
        ///
        /// </remarks>
        /// <param name="model">Пользователь</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update(ReviewUserRequest request)
        {
            var UserDTo = request.Adapt<User>();
            await _userService.Update(UserDTo);
            return Ok();
        }


    }
}
