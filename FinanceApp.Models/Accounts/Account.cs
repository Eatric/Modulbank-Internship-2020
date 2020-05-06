using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceApp.Models.Accounts
{
	public class Account
	{
		public string Number { get; set; }
		public decimal Balance { get; set; }
		public EAccountStatus Status { get; set; }
		public Guid Owner { get; set; }

		public Account(Guid owner, decimal balance, string number)
		{
			Number = number;
			Balance = balance;
			Status = EAccountStatus.Open;
			Owner = owner;
		}

		public Account(string Number, decimal Balance, short Status, Guid Owner)
		{
			this.Number = Number;
			this.Balance = Balance;
			this.Status = (EAccountStatus)Status;
			this.Owner = Owner;
		}

		public static string GenerateNumber()
		{
			var random = new Random();
			return $"4{random.Next(0, 999999999)}";
		}
	}
}
