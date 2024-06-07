using CsvHelper.Configuration;
using CsvHelper;
using GeolocationDataImport;
using Microsoft.Extensions.Options;
using NIHR.Infrastructure;
using NIHR.Infrastructure.Models;
using System.Globalization;
using Rbec.Postcodes;

namespace ImportGeocodeData
{
    public interface IPostcodeProvider : IPostcodeMapper, INationalPostcodeProvider
    {

    }

    public class StaticFilePostcodeMapper : IPostcodeProvider
    {
        private readonly IOptions<GeocodingSettings> _geocodingSettings;

        private readonly Dictionary<string, GeoData> _postCodeLocationMap = [];

        private readonly Dictionary<UkNation, List<string>> _nationPostCodeMap = [];

        public StaticFilePostcodeMapper(IOptions<GeocodingSettings> geocodingSettings)
        {
            _geocodingSettings = geocodingSettings;

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
            };

            using var reader = new StreamReader(_geocodingSettings.Value.LocationFilePath);
            using var csv = new CsvReader(reader, config);
            csv.Context.RegisterClassMap<GeoDataMap>();
            while (csv.Read())
            {
                var record = csv.GetRecord<GeoData>();

                if (Postcode.TryParse(record.Postcode, out var postcode)){
                    record.Postcode = postcode.ToString();
                }
                else
                {
                    continue;
                }

                if (string.IsNullOrEmpty(record.DOTERM) && !string.IsNullOrEmpty(record.Latitude) && !string.IsNullOrEmpty(record.Longitude))
                {
                    _postCodeLocationMap.Add(record.Postcode, record);

                    var nation = record.CountryCode switch
                    {
                        "E92000001" => UkNation.England,
                        "S92000003" => UkNation.Scotland,
                        "W92000004" => UkNation.Wales,
                        "N92000002" => UkNation.NorthernIreland,
                        _ => UkNation.England
                    };

                    _nationPostCodeMap.TryAdd(nation, []);
                    _nationPostCodeMap[nation].Add(record.Postcode);
                }
            }
        }

        public Task<IEnumerable<PostcodeAddressModel>> GetAddressesByPostcodeAsync(string postcode, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<CoordinatesModel> GetCoordinatesFromPostcodeAsync(string postcode, CancellationToken cancellationToken)
        {
            if (_postCodeLocationMap.TryGetValue(postcode, out var coordinates))
            {
                return Task.FromResult(new CoordinatesModel { Latitude = double.Parse(coordinates.Latitude), Longitude = double.Parse(coordinates.Longitude) });
            }

            return Task.FromResult((CoordinatesModel)null);
        }

        public string GetRandomNationalPostcode(UkNation nation)
        {
            var nationList = _nationPostCodeMap[nation];

            var i = Random.Shared.Next(0, nationList.Count - 1);

            return nationList[i];
        }
    }
}
