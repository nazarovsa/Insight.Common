using System.Linq;
using System.Net;
using FluentValidation;
using Insight.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Insight.AspNetCore.ExceptionFilters.Filters
{
	public sealed class ValidationExceptionFilter : IExceptionFilter
	{
		public static string ErrorMessageText = "Validation exception:";

		public void OnException(ExceptionContext context)
		{
			if (!(context?.Exception is ValidationException validationException) || context?.Result != null)
				return;

			var message = $@"{ErrorMessageText} {string.Join(", ", validationException.Errors
				.Select(x => x.ErrorMessage))}";

			context.Result = new ObjectResult("Validation exception was thrown")
			{
				StatusCode = (int) HttpStatusCode.BadRequest,
				Value = new CustomError(message, context.Exception)
			};

			context.ExceptionHandled = true;
		}
	}
}