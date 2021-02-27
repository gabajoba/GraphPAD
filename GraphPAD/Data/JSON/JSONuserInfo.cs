using Newtonsoft.Json;

namespace GraphPAD.Data.JSON
{
    public class JSONuserInfo
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("role")]
        public string Role { get; set; }

    }
}
