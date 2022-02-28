using Dapper.Contrib.Extensions;

namespace Dicon.Project.Switch.Test.Infrastructure.Repository
{
    public class ProductInfo
	{	
		public string ProductLine { get; set; }
		[Key]
		public string ProductName { get; set; }
		public string TestItems { get; set; }
		public string TestWLs { get; set; }
		public string MeasureControl { get; set; }
		public string MeasureStage { get; set; }
	}
}
