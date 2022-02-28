using DiconInstrument.GpibInstrument.Base;

namespace DiconInstrument.GpibInstrument
{
    public class SlotDevice : GpibBaseDevice
    {
        public int SlotNumber { get; set; }

        public SlotDevice(int boardnumber, int primaryAddress) : base(boardnumber, primaryAddress)
        {

        }
    }
}
