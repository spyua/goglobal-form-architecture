using AutoMapper;

namespace Dicon.Project.Swtich.Testing.Core.Help
{
    /// <summary>
    /// Object Mapping Factory
    /// </summary>
    public class AutoMapperFactory
    {
        private readonly AutoMapperProfile _profile;

        public AutoMapperFactory(AutoMapperProfile profile)
        {
            _profile = profile;
        }

        public IMapper Create()
        {
            var mappingConfig = new MapperConfiguration(c =>
            {
                c.AddProfile(_profile);
            });

            return mappingConfig.CreateMapper();
        }

    }
}
