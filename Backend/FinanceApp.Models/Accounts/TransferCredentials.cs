using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FinanceApp.Models.Accounts
{
	public class TransferCredentials
	{
		[Required]
		[RegularExpression(@"^4\d{9}$", ErrorMessage = "Неправильный тип счёта.")]
		public string From { get; set; }

		[Required]
		[RegularExpression(@"^4\d{9}$", ErrorMessage = "Неправильный тип счёта.")]
		public string To { get; set; }

		[Required]
		[Range(1d, double.MaxValue, ErrorMessage = "Не правильная сумма.")]
		public decimal Amount { get; set; }
	}
}
