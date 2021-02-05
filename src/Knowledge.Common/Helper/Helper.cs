using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;

namespace Knowledge.Common.Helper
{
    public static class Helper
    {
        public static T ToEnum<T>(this HttpStatusCode statusCode)
            => (T)Enum.Parse(typeof(HttpStatusCode), statusCode.ToString());
    }
    /// <summary>
    /// String Helper
    /// </summary>
    public static class StringHelper
    {
        private const string Empty = "";

        public static Dictionary<string, string> ToDictionary<T>(this T input, string include = "")
        {
            var pros = input.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            var result = new Dictionary<string, string>();
            if (string.IsNullOrEmpty(include.ToLower()))
            {
                foreach (var pro in pros)
                {
                    if (pro != null) result.Add(pro.Name, pro.GetValue(input)?.ToString());
                }
            }
            else if (!string.IsNullOrWhiteSpace(include))
            {
                var includes = include.Split(',', StringSplitOptions.RemoveEmptyEntries);
                foreach (var item in includes)
                {
                    var pro = pros.Where(x => x.Name.ToLower() == item.ToLower()).FirstOrDefault();
                    if (pro != null)
                    {
                        result.Add(pro.Name, pro.GetValue(input)?.ToString());
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// Convert Object To Json
        /// </summary>
        /// var y = serializer[1..^1]; //cut string length -1
        /// var jsonModify = string.Join("'", serializer.Split('"'));//join " ' "
        public static string Serializer(this object input)
        {
            var stream = new MemoryStream();
            if (input is null || input.ToString() == Empty) return Empty;
            var serializer = JsonSerializer.Serialize(input);
            return serializer.Replace('"', '\'')[0x1..^1];
        }
        /// <summary>
        /// [DisplayName(Name ="Get display name Enum")]
        /// </summary>
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
