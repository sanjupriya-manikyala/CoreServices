using AutoMapper;
using CoreServices.Profiles;

namespace CoreServices.Tests
{
    class ProductProfileTests
    {
        private readonly IMapper _mapper;
        public ProductProfileTests()
        {
            var mappingConfig = new MapperConfiguration(c =>
            {
                c.AddProfile(new ProductProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            _mapper = mapper;
            mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }        
    }
}
