using System.Net;
using Insight.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Insight.AspNetCore.ExceptionFilters.Filters
{
	public sealed class ExceptionFilter : IExceptionFilter
	{
		public void OnException(ExceptionContext context)
		{
			if (context.Result != null)
				return;

			context.Result = new ObjectResult("Unhandled exception was thrown")
			{
				StatusCode = (int) HttpStatusCode.InternalServerError,
				Value = new CustomError(context.Exception.Message, context.Exception)
			};

			context.ExceptionHandled = true;
		}
	}
}