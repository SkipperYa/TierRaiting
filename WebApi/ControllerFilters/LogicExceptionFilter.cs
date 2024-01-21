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

			if (context.Exception is not LogicException)
			{
				Log.Error(error);
				Log.CloseAndFlush();
			}

			if (context.Exception is LogicException logicException)
			{
				context.Result = new JsonResult(logicException.Message)
				{
					StatusCode = (int)HttpStatusCode.InternalServerError
				};
			}
			else if (context.Exception is TaskCanceledException)
			{
				context.Result = new JsonResult("Service Unavailable.")
				{
					StatusCode = (int)HttpStatusCode.ServiceUnavailable
				};
			}
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
