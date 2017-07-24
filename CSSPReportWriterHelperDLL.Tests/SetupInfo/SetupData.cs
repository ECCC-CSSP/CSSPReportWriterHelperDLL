using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSSPReportWriterHelperDLL.Tests.SetupInfo
{
    public class SetupData
    {
        public List<CultureInfo> cultureListGood { get; set; }
        public SetupData()
        {
            cultureListGood = new List<CultureInfo>() { new CultureInfo("en-CA"), new CultureInfo("fr-CA") };
        }
    }
}
