using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Insight.Json
{
	public sealed class PrivateFieldsJsonResolver : CamelCasePropertyNamesContractResolver
	{
		protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
		{
			var property = base.CreateProperty(member, memberSerialization);

			if (!property.Writable)
			{
				var info = member as PropertyInfo;
				if (info != null)
				{
					property.Writable = info.GetSetMethod(true) != null;
				}
			}

			return property;
		}
	}
}