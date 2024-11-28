using EasyBuy_Backend.Models;
using EasyBuy_Backend.Repositories.UserRepo;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace EasyBuy_Backend.Services.AuthSvc
{
	public class AuthService : IAuthService
	{
		private readonly IUserRepository _userRepository;
		private readonly IConfiguration _config;
		private readonly SymmetricSecurityKey _key;

		public AuthService(IUserRepository userRepository, IConfiguration config)
		{
			_userRepository = userRepository;
			_config = config;
			_key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SigningKey"]));
		}

		public async Task<string?> Register(User user)
		{
			try
			{
				user.Password = HashPassword(user.Password);

				if (_userRepository.Create(user))
				{
					return GenerateToken(user); 
				}

				return null; 
			}
			catch (Exception ex)
			{
				Debug.WriteLine("An error occurred: " + ex.Message);
				return null;
			}
		}

		public string Login(string email, string password)
		{
			try
			{
				var user = _userRepository.GetByEmail(email);
				if (user != null && VerifyPassword(user.Password, password))
				{
					return GenerateToken(user); 
				}

				return null; 			
			}
			catch (Exception ex)
			{
				Debug.WriteLine("An error occurred: " + ex.Message);
				return null;
			}
		}

		private string GenerateToken(User user)
		{
			if (_config["JWT:Issuer"] == null || _config["JWT:Audience"] == null || _config["JWT:SigningKey"] == null)
			{
				throw new InvalidOperationException("JWT configuration values are missing.");
			}

			if (user.Email == null || user.Name == null)
			{
				throw new ArgumentException("User must have a valid email and name.");
			}

			var claims = new List<Claim>
			{
				new Claim(JwtRegisteredClaimNames.Email, user.Email),
				new Claim(JwtRegisteredClaimNames.Name, user.Name)
			};

			var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

			var tokenDes = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(claims),
				Expires = DateTime.Now.AddDays(7),
				SigningCredentials = creds,
				Issuer = _config["JWT:Issuer"],
				Audience = _config["JWT:Audience"]
			};

			var tokenHandler = new JwtSecurityTokenHandler();
			var token = tokenHandler.CreateToken(tokenDes);

			return tokenHandler.WriteToken(token);
		}


		private string HashPassword(string password)
		{
			using (var sha256 = SHA256.Create())
			{
				var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
				return Convert.ToBase64String(bytes);
			}
		}

		private bool VerifyPassword(string hashedPassword, string password)
		{
			var hashedInputPassword = HashPassword(password);
			return hashedInputPassword == hashedPassword;
		}
	}
}
