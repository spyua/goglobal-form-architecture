using NationalInstruments.NI4882;
using DiconInstrument.Core.Def;
using DiconInstrument.Base.Model;
using System;
using System.Threading;
using System.Threading.Tasks;


namespace DiconInstrument.Com
{
    public class GpibCotrol : IControl
    {
        public GpibDeviceModel Device { get; private set; }
        public Device Com { get; private set; }

        public GpibCotrol(GpibDeviceModel device)
        {
            Device = device;
        }

        public bool Connect()
        {
            try
            {
                var primaryAddress = Convert.ToByte(Device.PrimaryAddress);
                var secondaryAddress = Convert.ToByte(Device.SecondAddress);
                Com = new Device(Device.Boardnumber, primaryAddress, secondaryAddress, TimeoutValue.None);
                return true;
            }
            catch
            {
                throw;
            }
        }

        public bool DisConnect()
        {
            try
            {
                Com.Dispose();
                return true;
            }
            catch
            {
                throw;
            }
        }

        public bool SendCmd(string command)
        {
            try
            {
                Com.Write(command);
                return true;
            }
            catch
            {
                throw;
            }
        }
        public string ReadData()
        {
            try
            {
                return Com.ReadString();
            }
            catch
            {
                throw;
            }
        }
        private bool IsOperationComplete(string isComplete)
        {
            return isComplete.Trim().Equals(DeviceCcommand.Instance.OpComplete) ? true : false; 
        }

        public async Task<string> ReadDataAsync(bool waitResponse = false)
        {
            try
            {
                // Direct response
                if (!waitResponse)
                    return Com.ReadString();

                // Wait
                var resStr = string.Empty;
                var task = Task.Run(() => {
                    while (resStr.Equals(string.Empty))
                    {
                        resStr = Com.ReadString();
                    }
                });


                // Timeout => 10s
                if (await Task.WhenAny(task, Task.Delay(100000)) == task)
                {
                    return resStr;
                }
                else
                {
                    //Com.Reset();
                    throw new Exception("Read data time out!");
                }
                        
            }
            catch
            {
                throw;
            }
        }

        public async Task<string> SendCmdAsync(string command, bool waitResponse = false)
        {
            try
            {
                var resStr = string.Empty;

                Com.Write(command);

                if (waitResponse)
                    resStr = await ReadDataAsync(waitResponse);

                return resStr;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> SendCmdAsync(string command, int millisecondsDelayWaitComplete = 0)
        {
            try
            {
                Com.Write(command);

                var isComplete = DeviceCcommand.Instance.OpProcessing;

                var task = Task.Run(() => {
                    while (isComplete.Equals(DeviceCcommand.Instance.OpProcessing))
                    {
                        SpinWait.SpinUntil(() => false, 50);
                        Com.Write(IEEE4882Command.Instance.QueryOperationComplete);
                        isComplete = Com.ReadString();
                    }
                });

                if (await Task.WhenAny(task, Task.Delay(millisecondsDelayWaitComplete)) == task)
                {
                    return IsOperationComplete(isComplete);
                }

                return IsOperationComplete(isComplete);
            }
            catch
            {
                throw;
            }
        }

    
    }
}
