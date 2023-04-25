using System.Text.Json.Serialization;

namespace Permissions.ViewModels
{
    public class ResultViewModel
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }
        [JsonPropertyName("message")]
        public string Message { get; set; }
        [JsonPropertyName("arabicMessage")]
        public string ArabicMessage { get; set; }
        [JsonPropertyName("returnObject")]
        public Object ReturnObject { get; set; }
        [JsonPropertyName("statusCode")]
        public int StatusCode { get; set; }

        public ResultViewModel BindResultViewModel(bool success, string message, string arabicMessage, int statusCode, Object returnObject)
        {
            Success = success;
            Message = message;
            ArabicMessage = arabicMessage;
            StatusCode = statusCode;
            ReturnObject = returnObject;

            return this;
        }
    }
}
