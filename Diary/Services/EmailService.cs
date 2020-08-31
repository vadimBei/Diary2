﻿using System.Threading.Tasks;
using Diary.Common;
using Diary.Services.Interfaces;
using MailKit.Net.Smtp;
using MimeKit;

namespace Diary.Services
{
	public class EmailService : IEmailService
	{
		public async Task SendAsync(string subject, string message, string email)
		{

			var emailMessage = new MimeMessage();

			emailMessage.From.Add(new MailboxAddress(EmailSettings.headerAdministrationSite, ""));
			emailMessage.To.Add(new MailboxAddress("", email));
			emailMessage.Subject = subject;
			emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
			{
				Text = message
			};

			using (var client = new SmtpClient())
			{
				await client.ConnectAsync(EmailSettings.smtpAdress, EmailSettings.port, EmailSettings.cancellationToken);
				await client.AuthenticateAsync(EmailSettings.userSender, EmailSettings.passwordSender);
				await client.SendAsync(emailMessage);
				await client.DisconnectAsync(true);
			}
		}
	}
}
