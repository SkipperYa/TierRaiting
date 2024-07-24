using System;

namespace Domain.Exceptions
{
	/// <summary>
	/// Exception for send to client error text
	/// </summary>
	public class LogicException : Exception
	{
		public LogicException(string message) : base(message)
		{

		}
	}
}
