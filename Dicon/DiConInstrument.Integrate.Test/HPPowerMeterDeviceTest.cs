using DiconInstrument.GpibInstrument.PowerMeter;
using NUnit.Framework;
using System.Threading.Tasks;

namespace DiconInstrument.Integrate.Test
{
    public class HPPowerMeterDeviceTest
    {

        private HPPowerMeter PowerMeterDevice;

        [SetUp]
        public void Setup()
        {
            // Test Modify Parameter
            var Boardnumber = 0;
            var PrimaryAddress = 12;
            var Slot = 1;

            PowerMeterDevice = new HPPowerMeter(Boardnumber, PrimaryAddress, Slot);
            PowerMeterDevice.Connect();
            PowerMeterDevice.Init().SetUnit(1).AutoRange(true).SetAveTime(20); 
        }

        [Test]
        public void When_set_WaveLength_It_can_read_power_value()
        {

            PowerMeterDevice.SetWaveLength(1520);
            var power = PowerMeterDevice.ReadPower();
            Assert.IsTrue(power < -51);
            PowerMeterDevice.DisConnect();

        }

        [Test]
        public void When_set_WaveLength_and_wait_response_It_can_read_power_value()
        {

            Task.Run(async () =>
            {
                var isSetOK =  await PowerMeterDevice.SetWaveLengthAsync(1520);
                var power = isSetOK ? PowerMeterDevice.ReadPower() : 0;
                Assert.IsTrue(power < -51);
                PowerMeterDevice.DisConnect();

            }).GetAwaiter().GetResult();


            /*
            Task.Run(async () =>
            {
                return await PowerMeterDevice.SetWaveLengthAsync(1520);
            
            }).ContinueWith( x => {

                var isSetOK = x.Result;
                var power = isSetOK ? PowerMeterDevice.ReadPower() : 0;
                Assert.IsTrue(power < -51);
                PowerMeterDevice.DisConnect();

            }).GetAwaiter().GetResult();
            */

        }

        [Test]
        public  void When_set_WaveLength_It_can_async_read_power_value()
        {

            Task.Run(async () =>
            {
                PowerMeterDevice.SetWaveLength(1520);
                var power = await PowerMeterDevice.ReadPowerAsync();
                Assert.IsTrue(power < -51);
                PowerMeterDevice.DisConnect();

            }).GetAwaiter().GetResult();

        }
    }
}