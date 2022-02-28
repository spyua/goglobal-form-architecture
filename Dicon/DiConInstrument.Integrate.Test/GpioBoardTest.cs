using DiconInstrument.GpibInstrument;
using FluentAssertions;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace DiconInstrument.Integrate.Test
{
    public class GpioBoardTest
    {
        private GpibBoard Board;

        [SetUp]
        public void Setup()
        {
            Board = new GpibBoard();
        }

        [Test]
        public void If_have_gpib_device_it_can_read_their_addresses()
        {
            Task.Run(async () =>
            {
                var devices = await Board.ScanGpibDevMainFrames(3000);
                devices.ToList().Count.Should().BeGreaterThan(0);            
            }).GetAwaiter().GetResult();
          
        }

    }
}
