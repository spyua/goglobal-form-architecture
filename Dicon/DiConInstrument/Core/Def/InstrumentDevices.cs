using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiconInstrument.Core.Def
{
    public class InstrumentDevices
    {
        public static class SingletonHolder
        {
            static SingletonHolder() { }
            internal static readonly InstrumentDevices INSTANCE = new InstrumentDevices();
        }

        public static InstrumentDevices Instance { get { return SingletonHolder.INSTANCE; } }

        public HashSet<string> PowerMeter;

        public HashSet<string> MainFrame;


        internal InstrumentDevices()
        {
            CreatePowerMeterSet();
            CreateMainFrameSet();
        }

        private void CreateMainFrameSet()
        {
            MainFrame = new HashSet<string>()
            {
                "HP8163A",
                "HP8164A"
            };
        }

        private void CreatePowerMeterSet()
        {
            PowerMeter = new HashSet<string>()
            {
               "8168A",
               "8164A"
            };
        }
    }
}
