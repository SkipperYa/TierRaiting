using Microsoft.AspNetCore.Identity;
using System.Text;

namespace Infrastructure.Extension
{
	public static class Extensions
	{
		public static string GetIdentityErrorText(this IdentityResult result)
		{
			var stringBuilder = new StringBuilder();

			foreach (var error in result.Errors)
			{
				stringBuilder.Append(error.Description);
				stringBuilder.Append('\n');
			}

			return stringBuilder.ToString();
		}
	}
}
