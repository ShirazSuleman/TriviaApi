using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace TriviaApi
{
    public class SetOnlyContractResolver : CamelCasePropertyNamesContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);
            if (property != null && property.Readable)
            {
                var attributes = property.AttributeProvider.GetAttributes(typeof(SetOnlyJsonPropertyAttribute), true);
                if (attributes != null && attributes.Count > 0)
                    property.Readable = false;
            }
            return property;
        }
    }
}