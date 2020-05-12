using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceApp.Models.Accounts
{
	public class Payment
	{
		public long Id { get; set; }
		public string FromAccount { get; set; }
		public string ToAccount { get; set; }
		public decimal Amount { get; set; }
		public DateTime When { get; set; }
		public EPaymentType Type { get; set; }

		public Payment(string from, string to, decimal amount, EPaymentType paymentType)
		{
			FromAccount = from;
			ToAccount = to;
			Amount = amount;
			When = DateTime.Now;
			Type = paymentType;
		}

		public Payment(long Id, string FromAccount, string ToAccount, decimal Amount, DateTime When, short Type)
		{
			this.Id = Id;
			this.FromAccount = FromAccount;
			this.ToAccount = ToAccount;
			this.Amount = Amount;
			this.When = When;
			this.Type = (EPaymentType) Type;
		}
	}
}
