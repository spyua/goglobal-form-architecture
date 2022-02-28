using DiconInstrument.Com;
using DiconInstrument.Base.Model;
using System;
using System.Threading.Tasks;
using static DiconInstrument.Core.Def.DeviceCcommand;
using Dicon.Util;

namespace DiconInstrument.GpibInstrument.Base
{
    public class GpibBaseDevice 
    {

        private IControl Com;

        public GpibDeviceModel DeviceModel { get;  internal set; }

        public GpibBaseDevice(int boardnumber, int primaryAddress, HardwareInterfaceType comType = HardwareInterfaceType.Gpib)
        {
            DeviceModel = new GpibDeviceModel();
            DeviceModel.InterfaceType = comType;
            DeviceModel.Boardnumber = boardnumber;
            DeviceModel.PrimaryAddress = primaryAddress;

            Com = new GpibCotrol(DeviceModel);
        }

        public bool Connect()
        {
            try
            {
                Com.Connect();

                return true;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.GetExceptionDetail());
            }
            
        }

        public bool DisConnect()
        {
            try
            {
                Com.DisConnect();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.GetExceptionDetail());
            }

        }

        // 暫時棄用
        /*
        public void SetComMode(HardwareInterfaceType comType)
        {
            DeviceModel.InterfaceType = comType;
            Com = ControlFactory.CreateConnection(DeviceModel);
        }
        */
        protected bool SendCommand(string command)
        {
            try
            {
                Com.SendCmd(command);

                return true;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.GetExceptionDetail());
            }
        }
        protected string ReadData()
        {
            try
            {
                return Com.ReadData();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.GetExceptionDetail());
            }
        }

        protected async Task<string> ReadDataAsync()
        {
            try
            {
                return await Com.ReadDataAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.GetExceptionDetail());
            }
        }

        protected async Task<bool> SendCmdAsync(string command, int millisecondsDelayWaitComplete)
        {
            try
            {
                return await Com.SendCmdAsync(command, millisecondsDelayWaitComplete);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.GetExceptionDetail());
            }
        }

        protected async Task<string> SendCommandAsync(string command, bool waitResponse = false)
        {
            try
            {
                return await Com.SendCmdAsync(command, waitResponse);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.GetExceptionDetail());
            }
        }

  
   
    }
}
