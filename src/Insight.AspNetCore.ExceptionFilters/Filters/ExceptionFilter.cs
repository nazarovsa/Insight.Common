using System;
using System.Net;
using Insight.Dto;
using Insight.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Insight.AspNetCore.ExceptionFilters.Filters
{
	public sealed class ExceptionFilter : IExceptionFilter
	{
		private readonly ILogService _logService;

		public ExceptionFilter(ILogService logService)
		{
			_logService = logService ?? throw new ArgumentNullException(nameof(logService));
		}

		public void OnException(ExceptionContext context)
		{
			if (context.Result != null)
				return;

			context.Result = new ObjectResult("Unhandled exception was thrown")
			{
				StatusCode = (int) HttpStatusCode.InternalServerError,
				Value = new CustomError(context.Exception.Message, context.Exception)
			};

			_logService.Error(this, context.Exception, "Unhandled exception was thrown");
			context.ExceptionHandled = true;
		}
	}
}