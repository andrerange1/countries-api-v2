using Countries.Domain.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Countries.Contracts.DTOs
{
    public class CountryResponse
    {
        public string Name { get; set; }
        public int Inititals { get; set; }
        public List<string> Currencies { get; set; }
        public string Flag { get; set; }
        public List<RegionalBlock> regionalBlocks { get; set; }
    }
}
