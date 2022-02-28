namespace DiconInstrument.Core.Def
{
    public class DeviceCcommand : IEEE4882Command
    {
        public enum HardwareInterfaceType { Gpib, Serial, Tcp }

        public static new class SingletonHolder
        {
            static SingletonHolder() { }
            internal static readonly DeviceCcommand INSTANCE = new DeviceCcommand();
        }

        public static new DeviceCcommand Instance { get { return SingletonHolder.INSTANCE; } }

        public int UpLimitSlotNum = 4;
        public string OpComplete = "1";
        public string OpProcessing = "0"; 
        public string VisaCmdFormat = "GPIB{0}::{1}::INSTR";

    }
}
