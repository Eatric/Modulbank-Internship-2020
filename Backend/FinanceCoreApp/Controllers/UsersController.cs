using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using FinanceApp.BusinessLogic.Users;
using FinanceApp.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceApp.Core.Controllers
{
	[Route("api/users")]
	[Produces("application/json")]
    [ApiController]
    public class UsersController : ControllerBase
    {
	    private readonly UsersRequestHandler _usersRequestHandler;

	    public UsersController(UsersRequestHandler getUsersInfoRequestHandler)
	    {
		    _usersRequestHandler = getUsersInfoRequestHandler;
	    }

		/// <summary>
		/// Получение информации о пользователе
		/// </summary>
		/// <param name="id">Id пользователя</param>
		/// <returns>Model Of User</returns>
		[Authorize]
	    [HttpGet]
		[Route("{id}")]
	    public async Task<User> GetUserInfo(Guid id)
	    {
		    return await _usersRequestHandler.GetUser(id);
	    }

		/// <summary>
		/// Регистрация нового пользователя
		/// </summary>
		/// <param name="user">Данные для регистрации</param>
		/// <returns>Model Of User</returns>
	    [HttpPut]
		[Route("register")]
	    public async Task<ActionResult<User>> Register([FromBody, Required] UserRegisterCredentials user)
	    {
		    if (!string.Equals(user.Password, user.RepeatedPassword))
		    {
			    ModelState.AddModelError("Password", "Пароли не совпадают.");
		    }

		    if (!ModelState.IsValid)
		    {
			    return BadRequest(ModelState);
		    }

		    return Ok(await _usersRequestHandler.Register(user));
	    }
    }
}