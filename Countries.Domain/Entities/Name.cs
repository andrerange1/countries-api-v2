using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Countries.Domain.Entities
{
    public class Name
    {
        [JsonProperty("common")]
        public string Common { get; set; }
        [JsonProperty("official")]
        public string Official { get; set; }    

    }
}
