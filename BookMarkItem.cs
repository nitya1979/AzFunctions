using Newtonsoft.Json;

namespace My.Function
{
    public class BookMarkItem
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; } 
    }
}