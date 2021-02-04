using System.Net;
using System.Text.Json.Serialization;

namespace Knowledge.Web.IdentityProvider.Models
{
    public class ResponseResult
    {
        public ResponseResult()
        {
            ContentType = DefaultContentType;
            Value = "";
        }

        [JsonPropertyName("StatusCode")]
        public HttpStatusCode? StatusCode { get; set; }
        [JsonPropertyName("Message")]
        public string Message { get; set; }
        [JsonPropertyName("RedirectUrl")]
        public string RedirectUrl { get; set; }
        [JsonPropertyName("Value")]
        public object Value { get; set; }
        /// <summary>
        /// Gets or sets the <see cref="Net.Http.Headers.MediaTypeHeaderValue"/> representing the Content-Type header of the response.
        /// </summary>
        public string ContentType { get; set; }
        public const string DefaultContentType = "application/json";
    }
}
