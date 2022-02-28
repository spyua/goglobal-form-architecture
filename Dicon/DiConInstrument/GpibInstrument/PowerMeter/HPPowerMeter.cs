using System;
using System.Threading.Tasks;

namespace DiconInstrument.GpibInstrument.PowerMeter
{
    public class HPPowerMeter : SlotDevice
    {
        public HPPowerMeter(int boardnumber, int primaryAddress, int slot) : base(boardnumber, primaryAddress)
        {
            SlotNumber = slot;
            DeviceModel.Boardnumber = boardnumber;
            DeviceModel.PrimaryAddress = primaryAddress;
        }

        public HPPowerMeter Init()
        {
            var cmd = "*CLS;*CLS;*SRE 0;*ESE 0";
            SendCommand(cmd);
            return this;
        }

     

        /// <summary>
        /// Set unit of power meter
        /// </summary>
        /// <param name="units">1(dBm), 0(W)</param>
        public HPPowerMeter SetUnit(int units)
        {
            var cmd = units == 1 ? ":POW:UNIT DBM" : ":POW:UNIT W";
            SendSENSCommand(cmd);
            return this;
        }

        /// <summary>
        /// Set power meter to receiver light source range
        /// </summary>
        /// <param name="States">Used to indicate status</param>
        public HPPowerMeter AutoRange(bool States)
        {
            var cmd = States ? ":POW:RANG:AUTO ON" : ":POW:RANG:AUTO OFF";
            SendSENSCommand(cmd);
            return this;
        }

        /// <summary>
        /// Scan power meter once in a while
        /// </summary>
        /// <param name="avgT">average time</param>
        public HPPowerMeter SetAveTime(int avgT)
        {

            var cmd = ":POW:ATIME " + Convert.ToString(avgT) + "MS";
            SendSENSCommand(cmd);
            return this;
        }

        /// <summary>
        /// Set wavelength of power meter
        /// </summary>
        /// <param name="WL">wavelength</param>
        public HPPowerMeter SetWaveLength(double WL)
        {
            var cmd = ":POW:WAVE " + Convert.ToString(WL) + "NM";
            SendSENSCommand(cmd);
            return this;
        }

        public async Task<bool> SetWaveLengthAsync(double WL)
        {
            var cmd = ":POW:WAVE " + Convert.ToString(WL) + "NM";
            return  await SendSendCommandAsync(cmd, 3000);
        }

        public double ReadPower()
        {
            var cmd = ":POW?";
            SendReadCommand(cmd);

            var data = ReadData();
            var Pow = Convert.ToDouble(data);

            if (Pow > 1000000)
                return -99; //1000
            else
                return Pow;
        }

        public async Task<double> ReadPowerAsync()
        {
            var cmd = ":POW?";
            SendReadCommand(cmd);

            var data = await ReadDataAsync();
            var Pow = Convert.ToDouble(data);

            if (Pow > 1000000)
                return -99; //1000
            else
                return Pow;
        }

        private async Task<bool> SendSendCommandAsync(string cmd, int millisecondsDelayWaitComplete)
        {
            return await SendCmdAsync(cmd, millisecondsDelayWaitComplete);
        }

        private void SendSENSCommand(string cmd)
        {
            SendCommand("SENS" + Convert.ToString(SlotNumber) + cmd);
        }

        private void SendReadCommand(string cmd)
        {
            SendCommand("READ" + Convert.ToString(SlotNumber) + cmd);
        }

    }
}
