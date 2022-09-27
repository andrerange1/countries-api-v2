using Countries.Domain.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Countries.Contracts.DTOs
{
    public class CountryDetailResponse
    {
        public string Name { get; set; }
        public int Population { get; set; }
        public List<string> Timezones { get; set; }
        public List<string> Currencies { get; set; }
        public List<Language> Languages { get; set; }
        public string Capital { get; set; }
        public List<RegionalBlock> regionalBlocks { get; set; }
        public List<string> Borders { get; set; }

        
    }
}
