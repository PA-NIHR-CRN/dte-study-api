using BPOR.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Diagnostics;

namespace DynamoDBupdate.Backfills
{
    public class CanonicalTownBackfill
    {
        private readonly ParticipantDbContext _participantDbContext;
        private readonly ILogger<CanonicalTownBackfill> _logger;
        private readonly HttpClient _httpClient;
        private readonly IOptions<OsSettings> _osSettings;
        private Dictionary<string, string> _postcodeCache = new();

        public CanonicalTownBackfill(
            ParticipantDbContext participantDbContext,
            ILogger<CanonicalTownBackfill> logger,
            HttpClient httpClient,
            IOptions<OsSettings> osSettings
            )
        {
            _participantDbContext = participantDbContext;
            _logger = logger;
            _httpClient = httpClient;
            _osSettings = osSettings;
            InitialisePostcodeCache(osSettings);
        }

        private void InitialisePostcodeCache(IOptions<OsSettings> osSettings)
        {
            var f = File.ReadAllLines(osSettings.Value.CachePath);

            foreach (var line in f)
            {
                var columns = line.Split('\t');
                _postcodeCache.TryAdd(columns[0], columns[1]);
            }
        }

        public async Task RunAsync(CancellationToken cancellationToken)
        {
            if (!_osSettings.Value.RunCanonicalTownBackfill)
            {
                _logger.LogInformation("Not running {job}", nameof(CanonicalTownBackfill));
                return;
            }

            var participantsToBeUpdated = await _participantDbContext.ParticipantAddress
                .Where(x => x.CanonicalTown == null && x.Postcode != null && !x.IsDeleted)
                .ToListAsync(cancellationToken);

            int totalRecords = participantsToBeUpdated.Count;
            int currentRecordNum = 0;
            int recordsInError = 0;
            _logger.LogInformation("Total number of accounts to be updated with canonical town: {Count}", totalRecords);

            var sw = new Stopwatch();
            sw.Start();
            await Parallel.ForEachAsync(participantsToBeUpdated, cancellationToken, async (toBeUpdated, ct) =>
            {
                Interlocked.Increment(ref currentRecordNum);
                string? canonicalTown = null;

                if (!Rbec.Postcodes.Postcode.TryParse(toBeUpdated?.Postcode, out var postcode))
                {
                    Interlocked.Increment(ref recordsInError);
                    canonicalTown = null;
                    
                    _logger.LogError("Invalid postcode: {postcode}", toBeUpdated?.Postcode);
                }
                else if (IsCanonicalAddress(toBeUpdated))
                {
                    canonicalTown = toBeUpdated?.Town;

                    _logger.LogInformation("Using existing town '{town}' for postcode '{postcode}'", toBeUpdated?.Town, toBeUpdated?.Postcode);
                }
                else if (_postcodeCache.TryGetValue(postcode.ToString(), out var result))
                {
                    canonicalTown = result;

                    _logger.LogInformation("Retrieved from cache {postcode}\t{canonicalTown}", postcode, canonicalTown);
                }
                else
                {
                    // Call OS places API
                    Thread.Sleep(100);
                    canonicalTown = await GetTownAsync(postcode.ToString(), ct);
                }

                if (string.IsNullOrWhiteSpace(canonicalTown))
                {
                    _logger.LogWarning("No canonical town found for postcode {Postcode} (participant {ParticipantId})", postcode.ToString(), toBeUpdated.Id);
                    canonicalTown = null;
                }

                _logger.LogInformation("{Current}/{Total} {s}s Updating participant {ParticipantId} with '{Town}' from postcode '{Postcode}'",
currentRecordNum, totalRecords, (int)sw.ElapsedMilliseconds / 1000, toBeUpdated.Id, canonicalTown, toBeUpdated.Postcode);

                toBeUpdated.CanonicalTown = canonicalTown?.Trim()?.ToUpperInvariant();
            });


            await _participantDbContext.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Number of accounts in error: {Count}", recordsInError);
        }

        private static bool IsCanonicalAddress(ParticipantAddress? address)
        {
            // Determine if the current address has been selected from the lookup (returns true) or manually entered (returns false).
            // Addresses from the location API have all fields in UPPERCASE. Manual addresses are likely to have Mixed Case in at least one field.
            return address?.AddressLine1?.ToUpperInvariant() == address?.AddressLine1
                                && address?.AddressLine2?.ToUpperInvariant() == address?.AddressLine2
                                && address?.AddressLine3?.ToUpperInvariant() == address?.AddressLine3
                                && address?.AddressLine4?.ToUpperInvariant() == address?.AddressLine4
                                && address?.Town?.ToUpperInvariant() == address?.Town
                                && address?.Town is not null
                                && address?.Town != "-";
        }

        private async Task<string?> GetTownAsync(string postcode, CancellationToken cancellationToken)
        {
            var httpRequest = new HttpRequestMessage
            {
                RequestUri = new Uri($"search/places/v1/postcode?{OrdnanceSurveyCountries.CodeQueryParams}&lr=EN&postcode={postcode}&maxresults=1", UriKind.Relative),
                Method = HttpMethod.Get
            };

            var response = await _httpClient.SendAsync(httpRequest, cancellationToken);
            var content = await response.Content.ReadAsStringAsync(cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                var address = JsonConvert.DeserializeObject<OrdnanceSurveyAddressResponse>(content);

                return address?.Results?.First().Dpa.PostTown;
            }
            else
            {
                _logger.LogWarning(content);
                return null;
            }
        }
    }
}