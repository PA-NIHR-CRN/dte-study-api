using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BPOR.Tests.Common;
using Microsoft.Extensions.Configuration;

namespace BPOR.Rms.Tests
{
    internal class MySqlFactAttribute : FactAttribute
    {
        public MySqlFactAttribute()
        {
            var configuration = TestConfiguration.GetStandardConfiguration();
            if (!configuration.GetValue<bool>("RunMySqlTests"))
            {
                Skip = "Skipped because MySQL tests are not enabled";
            }
        }
    }
}
