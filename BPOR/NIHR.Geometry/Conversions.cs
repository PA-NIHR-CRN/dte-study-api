using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NIHR.Geometry
{
    public static class Conversions
    {
        public const double MetresPerMile = 1609.344;
        public static double MilesToMetres(double miles) => miles * MetresPerMile;

    }
}
