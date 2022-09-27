using Countries.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Countries.Domain.Interfaces.Repositories
{
    public interface ICountryRepository
    {
        public Task<List<Country>> GetAllAsync();

    }
}
