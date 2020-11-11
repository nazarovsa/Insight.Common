using System.Net;
using FluentValidation;
using Insight.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Insight.AspNetCore.ExceptionFilters.Filters
{
	public sealed class ValidationExceptionFilter : IExceptionFilter
	{
		public void OnException(ExceptionContext context)
		{
			if (context?.Exception.GetType() != typeof(ValidationException) || context?.Result != null)
				return;

			context.Result = new ObjectResult("Validation exception was thrown")
			{
				StatusCode = (int) HttpStatusCode.BadRequest,
				Value = CustomError.Create(context.Exception.Message, context.Exception)
			};

			context.ExceptionHandled = true;
		}
	}
}