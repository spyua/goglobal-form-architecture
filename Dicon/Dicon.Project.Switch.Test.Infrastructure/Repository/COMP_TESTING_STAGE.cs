using Dapper.Contrib.Extensions;
using System;

namespace Dicon.Project.Switch.Test.Infrastructure.Repository
{
    public class COMP_TESTING_STAGE
	{
		[ExplicitKey]
		public string COMP_SN { get; set; }
		public int CHANNEL_NO { get; set; }
		public string CUT_CH { get; set; } = string.Empty;
		public string TEMP { get; set; } = string.Empty;
		public float WL_IL { get; set; }
		public string MODEL_NO { get; set; } = string.Empty;
		public string TECH_ID { get; set; } = string.Empty;
		public int M_INDEX { get; set; }
		public int G_INDEX { get; set; }
		public double IL { get; set; }
		public double IL_MIN { get; set; }
		public double IL_MAX { get; set; }
		public double DATASHIFT { get; set; }
		public double CT0 { get; set; }
		public float WL_START { get; set; }
		public float WL_STOP { get; set; }
		public double WDL { get; set; }
		public double PDL { get; set; }
		public double BR { get; set; }
		public DateTime CREATE_DATE { get; set; }
		public string CHIP_VER { get; set; } = string.Empty;
		public string STATION_NO { get; set; } = string.Empty;
		public string BOX_NO { get; set; } = string.Empty;
		public string ORIGINAL { get; set; } = string.Empty;
		public string METHOD { get; set; } = string.Empty;
		public string KIND { get; set; } = string.Empty;
		public string PCB_NO { get; set; } = string.Empty;
		public double V1 { get; set; }
		public double V2 { get; set; }
		public double CROSSTALK { get; set; }
		public string Fail { get; set; } = string.Empty;
		public int Head_NO { get; set; }
		public double BR_IN { get; set; }
		public double BR_INV_index { get; set; }
		public double BR_INV_0_0 { get; set; }
		public DateTime EEPROM_DATE { get; set; }
		public double BR_INV_65535 { get; set; }
		public string CH_NO { get; set; } = string.Empty;
		public double Repeatability { get; set; }
		public string BR_ID { get; set; } = string.Empty;
		public DateTime BR_DATE { get; set; }
		public string BR_STATION_NO { get; set; } = string.Empty;
		public string LEAKAGE { get; set; } = string.Empty;
		public int NX1_CROSSTALK { get; set; }
		public int NX1_CT_IDX { get; set; }
		public string NX1_CT00 { get; set; } = string.Empty;
		public string CalibrationWL { get; set; } = string.Empty;
		public string CalibrationTemp { get; set; } = string.Empty;
		public string XT_Detail { get; set; } = string.Empty;
		public string WDL_Detail { get; set; } = string.Empty;
		public string Fiber { get; set; } = string.Empty;
		public int Rep_Times { get; set; }
	}
}
