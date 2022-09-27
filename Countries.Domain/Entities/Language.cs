using Newtonsoft.Json;

namespace Countries.Domain.Entities
{
    public class Language
    {

        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("nativeName")]
        public string NativeName { get; set; }

    }
}