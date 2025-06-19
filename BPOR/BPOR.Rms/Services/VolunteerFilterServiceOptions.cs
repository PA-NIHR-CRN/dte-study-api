namespace BPOR.Rms.Services
{
    public class VolunteerFilterServiceOptions
    {
        /// <summary>
        /// The factor applied to the page size of a batched filter operation when a timeout occurs.
        /// </summary>
        public double TimeoutPageSizeFactor { get; set; } = 0.5;
        /// <summary>
        /// The minimum filter page size to attempt before failing the operation.
        /// </summary>
        public double MinPageSize { get; set; } = 0.05;
        /// <summary>
        /// The initial page size to attempt for all filter operations.
        /// </summary>
        public double InitialPageSize { get; set; } = 0.5;

        public int FilterCountBatchSize { get; set; } = 100_000;
    }
}
