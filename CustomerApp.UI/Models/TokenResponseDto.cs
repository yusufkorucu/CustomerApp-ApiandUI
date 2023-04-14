using Newtonsoft.Json;

namespace CustomerApp.Ui.Models
{
    public class TokenResponseDto
    {
        [JsonProperty("token")]
        public string Token { get; set; }
    }
}
