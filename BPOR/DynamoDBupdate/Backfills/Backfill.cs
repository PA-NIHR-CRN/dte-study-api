using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DynamoDBupdate.Backfills
{
    public class Backfill
    {
        private readonly Stage2Backfill _stage2Backfill;
        private readonly CanonicalTownBackfill _canonicalTownBackfill;
        private readonly IConfiguration _configuration;
        private readonly ILogger<Backfill> _logger;

        public Backfill(
            Stage2Backfill stage2Backfill,
            CanonicalTownBackfill canonicalTownBackfill,
            IConfiguration configuration,
            ILogger<Backfill> logger)
        {
            _stage2Backfill = stage2Backfill;
            _canonicalTownBackfill = canonicalTownBackfill;
            _configuration = configuration;
            _logger = logger;
        }

         public async Task RunAsync(bool runStage2Backfill, bool runCanonicalTownBackfill, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Backfill RunAsync {runStage2Backfill}, {runCanonicalTownBackfill}", runStage2Backfill, runCanonicalTownBackfill);

            if (runStage2Backfill)
            {
                try
                {
                    await _stage2Backfill.RunAsync(cancellationToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error during Stage 2 Backfill.");
                }
            }

            if (runCanonicalTownBackfill)
            {
                try
                {
                    await _canonicalTownBackfill.RunAsync(cancellationToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error during Canonical Town Backfill.");
                }
            }
        }
    }
}