using Dicon.Project.Switch.Test.Infrastructure.Repository;
using Dicon.Project.Swtich.Testing.Model.ViewModel;
using System.Collections.Generic;

namespace Dicon.Project.Swtich.Testing.Controller
{
    public interface IRepositoryController
    {
        void CreateCompTestingReport(TestResult data);

        void UpdateComTestingReport(TestResult data);

        //IEnumerable<ProductInfo> QueryProductInfo();

        string[] QueryProductName();
        string[] QueryProductLine();
    }
}
