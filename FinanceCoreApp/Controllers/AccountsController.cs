using System.Security.Claims;
using System.Threading.Tasks;
using FinanceApp.BusinessLogic.Accounts;
using FinanceApp.Models.Accounts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceApp.Core.Controllers
{
	[Authorize]
	[Route("api/accounts")]
	[ApiController]
	public class AccountsController : ControllerBase
	{
		private readonly AccountsRequestHandler _accountsRequestHandler;

		public AccountsController(AccountsRequestHandler getAccountsRequestHandler)
		{
			_accountsRequestHandler = getAccountsRequestHandler;
		}

		[HttpGet]
		public async Task<Account> GetAccountInfo(string number)
		{
			var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;

			return await _accountsRequestHandler.GetAccount(number, userEmail);
		}

		[HttpPost]
		[Route("register")]
		public async Task<ActionResult<Account>> RegisterNewAccount()
		{
			var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;

			return await _accountsRequestHandler.Register(userEmail);
		}

		[HttpPost]
		public async Task<IActionResult> Transfer([FromBody] TransferCredentials transfer)
		{
			var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;

			if (string.Equals(transfer.From, transfer.To))
			{
				ModelState.AddModelError("Same", "Вы не можете перевести деньги сами себе.");
			}

			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			return Ok(await _accountsRequestHandler.TransferMoney(transfer.From, userEmail, transfer.To, transfer.Amount));
		}
	}
}