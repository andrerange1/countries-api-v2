using Newtonsoft.Json;

namespace Countries.Domain.Entities
{
    public class RegionalBlock
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("acronym")]
        public string Acronym { get; set; }
    }
}