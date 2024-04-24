using Domain.Interfaces;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
	public class ConsoleEmailService : IEmailService
	{
		public Task Send(string to, string subject, string message)
		{
			Console.BackgroundColor = ConsoleColor.DarkGreen;
			Console.WriteLine();
			Console.WriteLine($"To: {to}");
			Console.WriteLine($"Subject: {subject}");
			Console.WriteLine($"Message: {message}");
			Console.WriteLine();
			Console.BackgroundColor = default;

			return Task.CompletedTask;
		}
	}
}
