using DiconInstrument.Base;
using DiconInstrument.Base.Model;
using DiconInstrument.Core.Def;
using DiconInstrument.GpibInstrument.Base;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DiconInstrument.GpibInstrument
{
    public class MainFrame : GpibBaseDevice
    {
        public Dictionary<string, SlotDevice> SlotDevices { get; set; }

        public MainFrame(int boardnumber, int primaryAddress ) : base(boardnumber, primaryAddress)
        {
            SlotDevices = new Dictionary<string, SlotDevice>();
        }

        public async Task<IEnumerable<SlotDevice>> ScanSlotDevices(int timeOutSec)
        {
            Connect();

            var devices = new List<SlotDevice>();

            // Defense write no slot mainframe
            if (!InstrumentDevices.Instance.MainFrame.Contains(DeviceModel.Model))
                return devices;

            for (int i = 1; i <= DeviceCcommand.Instance.UpLimitSlotNum; i++)
            {
                SpinWait.SpinUntil(() => false, 20);

                var isEmpty = await SendCommandAsync($"slot{i}:empty?", true);

                if (isEmpty.Trim().Equals("1"))
                    continue;

                var deviceInfo = await SendCommandAsync($"slot{i}:idn?", true);

                if (deviceInfo.Trim() != string.Empty)
                {
                    var info = deviceInfo.Split(',');

                    var baseDevice = new GpibDeviceModel()
                    {
                        Boardnumber = DeviceModel.Boardnumber,
                        InterfaceType = DeviceModel.InterfaceType,
                        PrimaryAddress = DeviceModel.PrimaryAddress,
                        SecondAddress = DeviceModel.SecondAddress,
                        Vendor = info[0],
                        Model = info[1],
                        SerialNumber = info[2],
                        FirmwareVersion = info[3],
                    };

                    var slotDevice = new SlotDevice(DeviceModel.Boardnumber, DeviceModel.PrimaryAddress)
                    {
                        SlotNumber = i,
                        DeviceModel = baseDevice
                    };

                    var modelStr = slotDevice.DeviceModel.Model;

                    devices.Add(slotDevice);
                 

                    if (SlotDevices.ContainsKey(modelStr))
                        continue;

                    SlotDevices.Add(modelStr, slotDevice);
                }

            }

            return devices;
        }
    
        public IObservable<IEnumerable<SlotDevice>> ScanSlots(int timeOut)
        {

            var ob = Observable.Create<IEnumerable<SlotDevice>>( (obs) =>
            {
                var task = ScanSlotDevices(timeOut);

                try
                {                  
                    obs.OnNext(task.Result);
                    obs.OnCompleted();
                   
                }
                catch (Exception e)
                {
                    obs.OnError(e);
                }
               

                return task;
            });

            return ob;
        }
    }
}
