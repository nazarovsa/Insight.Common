using System;
using System.Data;
using Dapper;

namespace Insight.DataAccess.Dapper.Handlers
{
	public sealed class DateTimeOffsetHandler : SqlMapper.TypeHandler<DateTimeOffset>
	{
		public override void SetValue(IDbDataParameter parameter, DateTimeOffset value)
		{
			parameter.Value = value.UtcDateTime;
		}

		public override DateTimeOffset Parse(object value)
		{
			return new DateTimeOffset(DateTime.SpecifyKind((DateTime)value, DateTimeKind.Utc).ToLocalTime());
		}
	}

}