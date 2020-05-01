using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceApp.Auth;
using FinanceApp.Core.BusinessLogic;
using Microsoft.AspNetCore.Mvc;

namespace FinanceApp.Core.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
	    private readonly GetUsersInfoRequestHandler _getUsersInfoRequestHandler;

	    public UsersController(GetUsersInfoRequestHandler getUsersInfoRequestHandler)
	    {
		    _getUsersInfoRequestHandler = getUsersInfoRequestHandler;
	    }

	    [HttpGet]
	    public Task<User> GetUserInfo(Guid id)
	    {
		    return _getUsersInfoRequestHandler.Handle(id);
	    }

	    [HttpPut]
	    public IActionResult Register([FromBody] UserCredentials user)
	    {
		    return Ok();
	    }
    }
}