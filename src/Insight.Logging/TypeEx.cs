using System;
using System.Text;

namespace Insight.Logging
{
	public static class TypeEx
	{
		public static string GetContext(this Type type, bool root)
		{
			var builder = new StringBuilder();
			if (root)
				builder.Append(type.Namespace).Append(".");

			if (type.IsGenericType)
			{
				builder
					.Append(CleanTypeName(type.Name))
					.Append("[");

				var arguments = type.GetGenericArguments();
				for (var index = 0; index < arguments.Length; index++)
				{
					var argument = arguments[index];

					builder.Append(GetContext(argument, false));

					if (index < arguments.Length - 1)
						builder.Append(", ");
				}

				builder.Append("]");
			}
			else
			{
				builder.Append(type.Name);
			}

			return builder.ToString();
		}

		private static string CleanTypeName(string name)
		{
			return name.Remove(name.IndexOf('`'));
		}
	}
}