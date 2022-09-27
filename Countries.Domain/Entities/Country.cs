using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Countries.Domain.Entities
{
    public class Country
    {
        public string Name { get; set; }
        [JsonProperty("alpha3Code")]
        public string Initials { get; set; }

        public int Population{ get; set; }

        public List<string> Timezones { get; set; }
        public List<string> Borders { get; set; }

        public List<Currency> Currencies { get; set; }
        public List<Language> Languages { get; set; }

        [JsonProperty("regionalBlocs")]
        public List<RegionalBlock> regionalBlocks { get; set; }

        public string Capital { get; set; }
        public string Region { get; set; }
        public string Flag { get; set;}

    }
}
