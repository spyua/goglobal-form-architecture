using System;

namespace Dicon.Project.Swtich.Testing.Model.ViewModel
{
    public class TestResult
    {
        public string COMP_SN { get; set; } = string.Empty;

        public int CHANNEL_NO { get; set; } = 0;

        public DateTime CREATE_DATE { get; set; } = DateTime.Now;
        public DateTime EEPROM_DATE { get; set; } = DateTime.Now;
        public DateTime BR_DATE { get; set; } = DateTime.Now;
    }
}
