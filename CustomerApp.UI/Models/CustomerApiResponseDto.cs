using Newtonsoft.Json;

namespace CustomerApp.Ui.Models
{
    public class CustomerApiResponseDto<T>
    {
        [JsonProperty("isSuccess")]
        public bool IsSuccess { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("excepiton")]
        public Exception Exception { get; set; }
        [JsonProperty("data")]
        public T Data { get; set; }
    }



}
