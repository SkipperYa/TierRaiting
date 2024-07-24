using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;
using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ControllerFilters
{
	public class LogicExceptionFilter : Attribute, IExceptionFilter
	{
		public void OnException(ExceptionContext context)
		{
			string actionName = context.ActionDescriptor.DisplayName;
			string exceptionStack = context.Exception.StackTrace;
			string exceptionMessage = context.Exception.Message;

			var stringBuilder = new StringBuilder();

			stringBuilder.Append($"ActionName: {actionName} \n");
			stringBuilder.Append($"ExceptionStack: {exceptionStack} \n");
			stringBuilder.Append($"ExceptionMessage: {exceptionMessage} \n");

			var error = stringBuilder.ToString();

			// Log to file if it LogicException
			if (context.Exception is not LogicException)
			{
				Log.Error(error);
				Log.CloseAndFlush();
			}

			// Send message to client if it LogicException and set StatusCode 500
			if (context.Exception is LogicException logicException)
			{
				context.Result = new JsonResult(logicException.Message)
				{
					StatusCode = (int)HttpStatusCode.InternalServerError
				};
			}
			// If it is TaskCanceledException set StatusCode 503 and set message Service Unavailable.
			else if (context.Exception is TaskCanceledException)
			{
				context.Result = new JsonResult("Service Unavailable.")
				{
					StatusCode = (int)HttpStatusCode.ServiceUnavailable
				};
			}
			// Otherwise set message Something went wrong and set StatusCode 500
			else
			{
				context.Result = new JsonResult("Something went wrong.")
				{
					StatusCode = (int)HttpStatusCode.InternalServerError
				};
			}
		}
	}
}
