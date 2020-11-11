using System.Net;
using Insight.Dto;
using Insight.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Insight.AspNetCore.ExceptionFilters.Filters
{
	public sealed class DomainExceptionFilter : IExceptionFilter
	{
		public void OnException(ExceptionContext context)
		{
			if (context?.Exception.GetType() != typeof(DomainException) || context?.Result != null)
				return;

			context.Result = new ObjectResult("Domain exception was thrown")
			{
				StatusCode = (int) HttpStatusCode.BadRequest,
				Value = CustomError.Create(context.Exception.Message)
			};

			context.ExceptionHandled = true;
		}
	}
}