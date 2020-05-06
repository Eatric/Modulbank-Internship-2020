using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using FakeItEasy;
using FinanceApp.Database.Interfaces;
using FinanceApp.Models;
using FluentAssertions;
using NUnit.Framework;

namespace FinanceApp.Tests
{
	[TestFixture]
	public class AuthServiceTests
	{
		[Test]
		public void TestCheckIsCorrectValidateUserPassword()
		{
			var password = "ModulBank";

			var user = new User("Faked", "Fakemail", password);
			var fakeModelRep = A.Fake<IModelRepository<User>>();

			A.CallTo(() => fakeModelRep.Get(user.Email)).Returns(user);

			var authService = new Auth.AuthService(fakeModelRep);

			authService.IsValidUser(user.Email, password).Should().BeTrue();
		}
	}
}
