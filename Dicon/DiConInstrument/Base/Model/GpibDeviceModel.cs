using static DiconInstrument.Core.Def.DeviceCcommand;

namespace DiconInstrument.Base.Model
{
    public class GpibDeviceModel
    {
        public HardwareInterfaceType InterfaceType { get; set; }
        public int PrimaryAddress { get; set; }
        public int SecondAddress { get; set; }
        public int Boardnumber { get; set; } 
        public string Vendor { get; set; }
        public string Model { get; set; }
        public string SerialNumber { get; set; }
        public string FirmwareVersion { get; set; }

        public string DeviceInformation { 
            get {                
                var msg = Model + " " + InterfaceType + "::" + PrimaryAddress;
                return msg;
            } 
        }
        public override string ToString()
        {
            return PrimaryAddress+":"+Boardnumber+":"+Vendor+","+Model+"-"+SerialNumber+"=>"+FirmwareVersion;
        }

    }
}
