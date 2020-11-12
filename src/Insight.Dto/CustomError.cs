using System;

namespace Insight.Dto
{
	public sealed record CustomError(string Message, Exception Exception = null);
}