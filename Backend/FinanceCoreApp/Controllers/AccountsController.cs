using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using FinanceApp.BusinessLogic.Accounts;
using FinanceApp.Models.Accounts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinanceApp.Core.Controllers
{
	[Authorize]
	[Produces("application/json")]
	[Route("api/accounts")]
	[ApiController]
	public class AccountsController : ControllerBase
	{
		private readonly AccountsRequestHandler _accountsRequestHandler;

		public AccountsController(AccountsRequestHandler getAccountsRequestHandler)
		{
			_accountsRequestHandler = getAccountsRequestHandler;
		}

		/// <summary>
		/// Получение полной информации о счёте по его номеру
		/// </summary>
		/// <param name="number">Номер счёта</param>
		/// <returns>Model of specific account</returns>
		[HttpGet]
		[Route("{number:maxlength(10)}")]
		public async Task<Account> GetAccountInfo(string number)
		{
			var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;

			return await _accountsRequestHandler.GetAccount(number, userEmail);
		}

		/// <summary>
		/// Получение списка всех счетов для данного аккаунта
		/// </summary>
		/// <returns>List Of Accounts</returns>
		[HttpGet]
		[Route("all")]
		public async Task<IEnumerable<Account>> GetAllAccountsForCurrentUser()
		{
			var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

			return await _accountsRequestHandler.GetAccountsOfUser(new Guid(userId));
		}

		/// <summary>
		/// Регистрация нового счёта для данного аккаунта
		/// </summary>
		/// <returns>Model of Account</returns>
		[HttpPost]
		[Route("register")]
		public async Task<ActionResult<Account>> RegisterNewAccount()
		{
			var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;

			return await _accountsRequestHandler.Register(userEmail);
		}

		/// <summary>
		/// Пополенение счёта данного аккаунта
		/// </summary>
		/// <param name="number">Номер счёта</param>
		/// <param name="amount">Сумма пополнения</param>
		/// <returns></returns>
		[HttpPost]
		[Route("add/{number:maxlength(10)}/{amount:decimal}")]
		public async Task<IActionResult> AddMoney(string number, decimal amount)
		{
			var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;

			return Ok(await _accountsRequestHandler.AddMoney(number, userEmail, amount));
		}

		/// <summary>
		/// Перевод денег между счетами
		/// </summary>
		/// <param name="transfer">Данные перевода</param>
		/// <returns></returns>
		[HttpPost]
		[Route("transfer")]
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