using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceCoreApp.BusinessLogic;
using FinanceCoreApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinanceCoreApp.Controllers
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
    }
}