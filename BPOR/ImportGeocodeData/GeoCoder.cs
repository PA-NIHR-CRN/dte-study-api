using BPOR.Domain.Entities;
using CsvHelper.Configuration;
using CsvHelper;
using GeolocationDataImport;
using System.Globalization;
using Microsoft.Extensions.Options;
using Rbec.Postcodes;
using Microsoft.EntityFrameworkCore;

namespace ImportGeocodeData
{
    public class GeoCoder(ParticipantDbContext context, IOptions<GeocodingSettings> geocodingSettings, IPostcodeProvider postcodeProvider)
    {
        List<Address> _addresses = [];

        List<UkNation> _ukNationDistribution = [];
        public async Task ProcessAsync(CancellationToken token = default)
        {
            _ukNationDistribution = Enumerable.Range(0, 850).Select(x => UkNation.England)
                .Concat(Enumerable.Range(0, 80).Select(x => UkNation.Scotland))
                .Concat(Enumerable.Range(0, 45).Select(x => UkNation.Wales))
                .Concat(Enumerable.Range(0, 25).Select(x => UkNation.NorthernIreland))
                .ToList();

            ReadAddressFile(geocodingSettings.Value.AddressFilePath);

            await UpdateDatabaseWithAddresses();
        }

        void ReadAddressFile(string filePath)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false,
            };

            using var reader = new StreamReader(filePath);
            using var csv = new CsvReader(reader, config);
            csv.Context.RegisterClassMap<AddressMap>();

            int count = 0;
            while (csv.Read())
            {
                var record = csv.GetRecord<Address>();

                if (Postcode.TryParse(record.Postcode, out var postcode))
                {
                    record.Postcode = postcode.ToString();
                }
                else
                {
                    continue;
                }

                _addresses.Add(record);
                count++;
            }
        }

        public async Task SetRandomAddressFromFile(ParticipantAddress participantAddress, UkNation nation)
        {
            do
            {
                var i = Random.Shared.Next(0, _addresses.Count);

                var address = _addresses[i];

                participantAddress.AddressLine1 = address.HouseNumber;
                participantAddress.AddressLine2 = address.Street;
                participantAddress.Town = address.TownCity;
                participantAddress.Postcode = address.Postcode;
            } while (!await UpdateLocation(participantAddress));
        }

        public async Task SetRandomNationalPostcode(ParticipantAddress address, UkNation nation)
        {
            do
            {
                address.Postcode = postcodeProvider.GetRandomNationalPostcode(nation);
            } while (!await UpdateLocation(address));
        }

        private async Task<bool> UpdateLocation(ParticipantAddress address)
        {
            var location = await postcodeProvider.GetCoordinatesFromPostcodeAsync(address.Postcode, CancellationToken.None);

            if (location is not null)
            {
                if (address?.Participant?.ParticipantLocation is null)
                {
                    address.Participant.ParticipantLocation = new ParticipantLocation();
                }

                address.Participant.ParticipantLocation.Location = new NetTopologySuite.Geometries.Point(location.Longitude, location.Latitude) { SRID = 4326 };
                return true;
            }
            else
            {
                //Console.WriteLine($"Location not found for postcode '{address.Postcode}'.");
                return false;
            }
        }

        async Task UpdateDatabaseWithAddresses()
        {
            var totalAddresses = context.ParticipantAddress.Count();
            var i = 0;

            int offset = 0;
            int batchSize = 1000;
            var query = context.ParticipantAddress.Include(x => x.Participant).ThenInclude(x => x.ParticipantLocation).OrderBy(x => x.Id);

            var batch = query.Skip(offset).Take(batchSize).ToList();
            while (batch.Any())
            {
                foreach (var participant in batch)
                {
                    var nationIndex = Random.Shared.Next(0, _ukNationDistribution.Count);

                    var nation = _ukNationDistribution[nationIndex];

                    Func<ParticipantAddress, UkNation, Task> action = nation switch
                    {
                        UkNation.England or UkNation.Wales => SetRandomAddressFromFile,
                        UkNation.Scotland or UkNation.NorthernIreland => SetRandomNationalPostcode,
                        _ => (p, n) => Task.CompletedTask
                    };

                    await action(participant, nation);
                    i++;
                    
                }

                offset += batchSize;
                batch = query.Skip(offset).Take(batchSize).ToList();
                var percentageCompleted = (i / (double)totalAddresses) * 100d;

                Console.WriteLine($"{percentageCompleted}%");
            }

            await context.SaveChangesAsync();
        }
    }
}
