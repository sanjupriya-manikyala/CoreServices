using AutoMapper;
using CoreServices.Profiles;

namespace CoreServices.Tests
{
    class ProductProfileTests
    {
        public ProductProfileTests()
        {
            var mappingConfig = new MapperConfiguration(c =>
            {
                c.AddProfile(new ProductProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }        
    }
}
