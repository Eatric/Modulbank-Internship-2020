using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceApp.Database.Interfaces;
using FinanceApp.Models.Accounts;
using FinanceApp.Models.Users;
using Microsoft.AspNetCore.Authentication;

namespace FinanceApp.BusinessLogic.Accounts
{
	public class AccountsRequestHandler
	{
		private readonly IAccountsRepository _accountsRepository;
		private readonly IUsersRepository _usersRepository;
		private readonly IPaymentsRepository _paymentsRepository;

		public AccountsRequestHandler(IAccountsRepository accountsRepository, IUsersRepository usersRepository, IPaymentsRepository paymentsRepository)
		{
			_accountsRepository = accountsRepository;
			_usersRepository = usersRepository;
			_paymentsRepository = paymentsRepository;
		}

		public async Task<bool> AddMoney(string number, string email, decimal amount)
		{
			var account = await GetAccount(number, email);

			account.Balance += amount;
			return await _accountsRepository.Update(account);
		}

		public async Task<IEnumerable<Account>> GetAccountsOfUser(Guid id)
		{
			return await _accountsRepository.Read(id);
		}

		public async void AddPayment(string from, string to, decimal amount, EPaymentType paymentType)
		{
			var payment = new Payment(from, to, amount, paymentType);

			await _paymentsRepository.Create(payment);
		}

		public async Task<Account> GetAccount(string number, string email)
		{
			var user = await _usersRepository.Read(email);
			var accounts = (await GetAccountsOfUser(user.Id)).ToList();

			if (!accounts.Exists(x => x.Number == number))
			{
				throw new ArgumentException("Пользователь не имеет прав на этот счёт.");
			}

			return await _accountsRepository.Read(number);
		}

		public async Task<Account> Register(string email)
		{
			var user = await _usersRepository.Read(email);

			var number = Account.GenerateNumber();
			var existAccount = await _accountsRepository.Read(number);

			if (existAccount != null)
			{
				return await Register(email);
			}

			var account = new Account(user.Id, 0m, number);
			var result = await _accountsRepository.Create(account);

			while (!result)
			{
				result = await _accountsRepository.Create(account);
			}

			return account;
		}

		public async Task<bool> TransferMoney(string from, string email, string to, decimal amount)
		{
			var fromAccount = await GetAccount(from, email);
			var toAccount = await _accountsRepository.Read(to);

			if (fromAccount.Balance < amount)
			{
				throw new ArgumentException("Not enough money on account");
			}

			if (fromAccount.Number == toAccount.Number)
			{
				throw new ArgumentException("U can't transfer money to the same account");
			}

			AddPayment(fromAccount.Number, toAccount.Number, amount, EPaymentType.Transfer);

			fromAccount.Balance -= amount;
			toAccount.Balance += amount;

			return await _accountsRepository.Update(fromAccount) && await _accountsRepository.Update(toAccount);
		}
	}
}
