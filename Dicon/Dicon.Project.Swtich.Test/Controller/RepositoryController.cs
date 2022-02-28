using AutoMapper;
using Dicon.Project.Switch.Test.Infrastructure.Repository;
using Dicon.Project.Swtich.Testing.Model.ViewModel;
using System.Linq;

namespace Dicon.Project.Swtich.Testing.Controller
{
    public class RepositoryController : IRepositoryController
    {
        // 單純CRUD不包Service直接使用Repo
        private ICompTestingStageRepository _compTestingStageRepo;
        private IProductInfoRepository _prodRepo;
        private IMapper _mapper;
        public RepositoryController(IMapper mapper,
                                   ICompTestingStageRepository compTestingStageRepo,
                                   IProductInfoRepository prodRepo)
        {
            _compTestingStageRepo = compTestingStageRepo;
            _prodRepo = prodRepo;
            _mapper = mapper;


        }

        public void CreateCompTestingReport(TestResult data)
        {
            var entity = _mapper.Map<COMP_TESTING_STAGE>(data);

            _compTestingStageRepo.Add(entity);

        }

        public void UpdateComTestingReport(TestResult data)
        {
            var entity = _mapper.Map<COMP_TESTING_STAGE>(data);

            _compTestingStageRepo.Update(entity);

        }


        /*
        public IEnumerable<ProductInfo> QueryProductInfo()
        {
            return _prodRepo.ReadRange();
        }
        */

        public string[] QueryProductName()
        {
            var prodsName = _prodRepo.ReadRange().ToList().Select(x => x.ProductName).Distinct().ToArray();
            return prodsName;
        }

        public string[] QueryProductLine()
        {
            var prodsLine = _prodRepo.ReadRange().ToList().Select(x => x.ProductLine).Distinct().ToArray();
            return prodsLine;
        }
    } 
}
