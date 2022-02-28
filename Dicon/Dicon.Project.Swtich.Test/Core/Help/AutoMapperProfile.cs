using AutoMapper;
using Dicon.Project.Switch.Test.Infrastructure.Repository;
using Dicon.Project.Swtich.Testing.Model.ViewModel;

namespace Dicon.Project.Swtich.Testing.Core.Help
{
    /// <summary>
    /// Object Mapping Setting
    /// </summary>
    public class AutoMapperProfile : Profile
    {

        public AutoMapperProfile()
        {
            TestResultMapping();
        }
        private void TestResultMapping()
        {
            CreateMap<TestResult, COMP_TESTING_STAGE>();
        }
    }


 
}
