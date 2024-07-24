using Domain.Exceptions;
using Domain.Interfaces;
using Infrastructure.Utils;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
	public class EmailService : IEmailService
	{
		private EmailOptions EmailOptions { get; set; }

		public EmailService(IOptions<EmailOptions> emailOptions)
		{
			EmailOptions = emailOptions.Value;
		}

		public async Task Send(string to, string subject, string message)
		{
			try
			{
				using var emailMessage = new MimeMessage();

				emailMessage.From.Add(new MailboxAddress(EmailOptions.From, EmailOptions.Login));
				emailMessage.To.Add(new MailboxAddress("", to));
				emailMessage.Subject = subject;
				emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
				{
					Text = message
				};

				using var emailClient = new SmtpClient();

				await emailClient.ConnectAsync(EmailOptions.Host, int.Parse(EmailOptions.Port), true);
				await emailClient.AuthenticateAsync(EmailOptions.Login, EmailOptions.Password);
				await emailClient.SendAsync(emailMessage);
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}
