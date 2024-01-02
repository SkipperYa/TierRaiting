using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Infrastructure.Utils
{
	public class AuthOptions
	{
		public const string ISSUER = "TierRaitingApi";
		public const string AUDIENCE = "TierRaitingClient";
		private const string KEY = "784a5735-469a-42b2-8797-bf0548cea115";

		public static SymmetricSecurityKey GetSymmetricSecurityKey()
		{
			return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
		}
	}
}
