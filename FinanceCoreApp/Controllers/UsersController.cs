using System;
using System.Threading.Tasks;
using FinanceApp.BusinessLogic.Users;
using FinanceApp.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceApp.Core.Controllers
{
	[Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
	    private readonly UsersRequestHandler _usersRequestHandler;

	    public UsersController(UsersRequestHandler getUsersInfoRequestHandler)
	    {
		    _usersRequestHandler = getUsersInfoRequestHandler;
	    }

		[Authorize]
	    [HttpGet]
	    public async Task<User> GetUserInfo(Guid id)
	    {
		    return await _usersRequestHandler.GetUser(id);
	    }

	    [HttpPut]
	    public async Task<ActionResult<User>> Register([FromBody] UserRegisterCredentials user)
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