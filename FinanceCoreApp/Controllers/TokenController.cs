using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FinanceApp.Auth;
using FinanceApp.Core.Configurations;
using FinanceApp.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace FinanceApp.Core.Controllers
{
	[Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
	    private IUserInfoService _userInfoService;
	    private AuthOptions _authOptions;

	    public TokenController(IUserInfoService userInfoService, IOptions<AuthOptions> authOptions)
	    {
		    _userInfoService = userInfoService;
		    _authOptions = authOptions.Value;
	    }

	    [HttpPost]
	    public IActionResult Get([FromBody] UserCredentials user)
	    {
		    if (_userInfoService.IsValidUser(user.Email, user.Password))
		    {
			    var authClaims = new[]
			    {
				    new Claim(JwtRegisteredClaimNames.Email, user.Email),
				    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
			    };

				var token = new JwtSecurityToken(
					issuer: _authOptions.Issuer,
					audience: _authOptions.Audience,
					expires: DateTime.Now.AddMinutes(_authOptions.ExpiresInMinutes),
					claims: authClaims,
					signingCredentials: new SigningCredentials(
						new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authOptions.SecureKey)),
						SecurityAlgorithms.HmacSha256Signature)
					);

				return Ok(new
				{
					token = new JwtSecurityTokenHandler().WriteToken(token),
					expiration = token.ValidTo
				});
		    }

		    return Unauthorized();
	    }
    }
}