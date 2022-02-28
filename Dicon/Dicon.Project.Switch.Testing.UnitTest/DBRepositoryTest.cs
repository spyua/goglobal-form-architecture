using Dicon.Project.Switch.Test.Infrastructure.Connection;
using Dicon.Project.Switch.Test.Infrastructure.Repository;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Dicon.Project.Switch.Testing.UnitTest
{
    public class DBRepositoryTest
    {


        private CompTestingStageRepository _comTestingStageRepo;
        private ProductInfoRepository _productInfoRepository;

        private string LocalConnStr(string dbName)
        {
            return $"Data Source=GFI_E0373\\SQLEXPRESS;Initial Catalog={dbName};Persist Security Info=True;User ID=sa;Password=laser99";
        }

        [SetUp]
        public void Setup()
        {
            _comTestingStageRepo = new CompTestingStageRepository(new SqlDbFactory(LocalConnStr("1XN_MEMS_SWITCH_DB")));
            _productInfoRepository = new ProductInfoRepository(new SqlDbFactory(LocalConnStr("ERmeasurement")));
        }



        [Test]
        public void When_CompTestingStageRepo_create_data_it_can_read_value()
        {
            var compSn = "125";

            var data = new COMP_TESTING_STAGE()
            {
                COMP_SN = compSn,
                CHANNEL_NO = 10,
                CREATE_DATE = DateTime.Now,
                EEPROM_DATE = DateTime.Now,
                BR_DATE = DateTime.Now
            };

            _comTestingStageRepo.Add(data);
            var readData = _comTestingStageRepo.ReadOne(compSn);
            readData.COMP_SN.Should().Be(compSn);

            var removeData = new COMP_TESTING_STAGE()
            {
                COMP_SN = compSn
            };
            _comTestingStageRepo.Remove(data);

        }


        [Test]
        public void When_CompTestingStageRepo_update_data_it_can_read_update_value()
        {
            var compSn = "999";
            var updateChannelNo = 20;
            var data = new COMP_TESTING_STAGE()
            {
                COMP_SN = compSn,
                CHANNEL_NO = 10,
                CREATE_DATE = DateTime.Now,
                EEPROM_DATE = DateTime.Now,
                BR_DATE = DateTime.Now
            };

            _comTestingStageRepo.Add(data);

            data.CHANNEL_NO = updateChannelNo;
            _comTestingStageRepo.Update(data);

            var readData = _comTestingStageRepo.ReadOne(compSn);
            readData.CHANNEL_NO.Should().Be(updateChannelNo);

            var removeData = new COMP_TESTING_STAGE()
            {
                COMP_SN = compSn
            };
            _comTestingStageRepo.Remove(data);

        }

        [Test]
        public void When_Query_Product_Info_Can_Read_Data()
        {
            var product = _productInfoRepository.ReadRange();

            product.Count().Should().BeGreaterThan(0);
        }
    }
}