using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;

namespace Knowledge.Web.IdentityProvider.Common
{
    public static class Helper
    {
        public static string ToJsonString(this object input)
        {
            var stream = new MemoryStream();
            if (input is null || input.ToString() == string.Empty) return string.Empty;
            var serializer = JsonSerializer.Serialize(input);
            return serializer.Replace('"', '\'')[1..^1];
        }
        public static string GetName(this Enum enumValue)
        {
            //First judge whether it is enum type data
            var type = enumValue.GetType();
            if (!type.IsEnum)
            {
                throw new ArgumentException("EnumValue must be of Enum type", nameof(enumValue));
            }
            //Find the corresponding Display Name for the enum
            MemberInfo[] member = type.GetMember(enumValue.ToString());
            if (member != null && member.Length > 0)
            {
                var AttributesData = member[0].GetCustomAttributesData().FirstOrDefault();

                if (AttributesData != null)
                {
                    //Pull out the value
                    return AttributesData.NamedArguments.FirstOrDefault().TypedValue.Value.ToString();
                }
            }
            //If you have no Display Name, just return the ToString of the enum
            return enumValue.ToString();
        }
    }
}
