using System;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthenticationJWT.Services
{
	public class AuthService
	{
		public AuthService()
		{
		}


		public string LoginUser(string p_name, string p_password)
		{
			if (!ValidUserCredentials(p_name, p_password)) return null;

			return GenerateJwtToken(p_name);

		}



        private string GenerateJwtToken(string username)
        {
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("YourSecretKeyThatHasToHaveAtLeast32CharactersLong"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
				new Claim(JwtRegisteredClaimNames.Sub, username),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
			};

            var token = new JwtSecurityToken(
                issuer: "YourIssuer",
                audience: "YourAudience",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }



        public bool ValidUserCredentials(string p_name, string p_password)
		{
			if (p_name != null && p_password != null) return true;
			return false;
		}


	}
}

