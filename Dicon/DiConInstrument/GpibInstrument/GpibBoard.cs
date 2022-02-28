using NationalInstruments.NI4882;
using DiconInstrument.Core.Def;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DiconInstrument.Base.Model;
using System.Reactive.Linq;

namespace DiconInstrument.GpibInstrument
{
    /// <summary>
    /// Board控制器
    /// </summary>
    public class GpibBoard
    {
        public Board Board { get; private set; }

        // ModelName, DeviceInfo
        public Dictionary<string, MainFrame> MainFrames { get; private set; }

        public AddressCollection Addresses { get; private set; } 

        public bool IsConnect;

        public GpibBoard()
        {
            IsConnect = false;
            MainFrames = new Dictionary<string, MainFrame>();
            Addresses = new AddressCollection();
        }

        public void Connect()
        {
            try
            {
                Board = new Board();
                // Resets the GPIB by sending the interface clear message.
                Board.SendInterfaceClear();
            }
            catch
            {
                throw new Exception("Pls check gpib line is connected");
            }
            IsConnect = true;
        }

        public async Task<MainFrame> FindGpibDevices(string Model, int timeOutSec=5000)
        {
            if (MainFrames.Count > 0 && MainFrames.ContainsKey(Model))
                return MainFrames[Model];

            await ScanGpibDevMainFrames(timeOutSec);

            return MainFrames[Model];
        }

        public void InvaildConnect()
        {
            if (!IsConnect)
                throw new Exception("Pls check gpib line is connected");
        }

        public async Task<IEnumerable<MainFrame>> ScanGpibDevMainFrames( int timeOutSec)
        {
            InvaildConnect();

            var devices = new List<MainFrame>();
            var findAddressTask = await FindGpibDevListeners(timeOutSec);
            
            for (int i = 0; i < findAddressTask.Count; i++)
            {
                var paddr = findAddressTask[i];
                using (Device tempDev = new Device(0, paddr))
                {
                    tempDev.Write(DeviceCcommand.Instance.QueryIdentifyDevice);
                    SpinWait.SpinUntil(() => false, 50);
                    var msg = tempDev.ReadString();

                    if (msg.Trim().Equals(string.Empty))
                        continue;

                    var msgArray = msg.Split(',');
                    var pAdd = paddr.PrimaryAddress;
                    var sAdd = paddr.SecondaryAddress;

                    // Wait Clean
                    var deviceModel = new GpibDeviceModel {
                        PrimaryAddress = pAdd,
                        SecondAddress = sAdd,
                        Vendor = msgArray[0],
                        Model = msgArray[1],
                        SerialNumber = msgArray[2],
                        FirmwareVersion = msgArray[3],
                    };

                    var mainFrame = new MainFrame(0, pAdd)
                    {
                        DeviceModel = deviceModel
                    };

                    var keyStr = mainFrame.DeviceModel.PrimaryAddress.ToString();

                    devices.Add(mainFrame);

                    if (MainFrames.ContainsKey(keyStr))
                        continue;

                    MainFrames.Add(keyStr, mainFrame);
                  
                }
            }

            return devices;
        }
      
        // Oberable
        public IObservable<IEnumerable<MainFrame>> ScanMainFrames(int timeOutSec)
        {
            var ob = Observable.Create<IEnumerable<MainFrame>>((obs) =>
            {
                InvaildConnect();

                var task = ScanGpibDevMainFrames(timeOutSec);
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

        private async Task<AddressCollection> FindGpibDevListeners(int timeOutSec)
        {
            var addresses = new AddressCollection();

            var task = Task.Run(() => {
                while (addresses.Count <= 0)
                {
                    addresses = Board.FindListeners();
                }
            });

            if (await Task.WhenAny(task, Task.Delay(timeOutSec)) == task)
            {   
                return addresses;
            }
            else
            {
                throw new TimeoutException(DeviceCode.ScanTimeOut.Description);
            }
        }

    }
}
