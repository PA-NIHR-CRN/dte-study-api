namespace BPOR.Rms.Tests
{
    public static class StatsUtils
    {
        public static double ChiSqStat(int[] observed, double[] expected)
        {
            double sum = 0.0;
            for (int i = 0; i < observed.Length; ++i)
            {
                sum += ((observed[i] - expected[i]) *
                          (observed[i] - expected[i])) / expected[i];
            }
            return sum;
        }
    }
}
