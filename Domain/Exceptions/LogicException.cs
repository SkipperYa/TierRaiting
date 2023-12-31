using System;

namespace Domain.Exceptions
{
	public class LogicException : Exception
	{
		public LogicException(string message) : base(message)
		{

		}
	}
}
