using AutoMapper;
using Countries.Contracts.Maps;

namespace Countries.Infra
{
    public class AutoMapperConfig
    {
        public MapperConfiguration Config() => new MapperConfiguration(config => { config.AddProfile<MappingProfile>(); });

    }
}
