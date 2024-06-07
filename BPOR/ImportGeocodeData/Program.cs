using BPOR.Domain.Entities;
using CsvHelper;
using CsvHelper.Configuration;
using GeolocationDataImport;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NetTopologySuite.Geometries;
using NIHR.Infrastructure.EntityFrameworkCore;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Formats.Asn1;
using System.Globalization;
using System.Net;

internal class Program
{
    private static void Main(string[] args)
    {
        var dbSettings = new DbSettings
        {
            Username = "dte-stream-s",
            Password = "I!iBtG!9QtC44qZ5",
            Database = "dte",
            Host = "crnccd-rds-aurora-dev-dte-cluster.cluster-cmkdakiyu6bm.eu-west-2.rds.amazonaws.com",
            Port = 3306
        };
  
        var connectionString = dbSettings.BuildConnectionString();
        var options = new DbContextOptionsBuilder<ParticipantDbContext>()
            .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), x =>
            {
                x.UseNetTopologySuite();
                x.CommandTimeout(300);
            })
            .Options;

        var context = new ParticipantDbContext(options);

        ProcessImport(context);
    }

    private static void ProcessImport(ParticipantDbContext context)
    {
        var csvFilePath = "C:\\Users\\paul.tennyson\\Desktop\\pp-complete.csv";
        var geoDataCsvFilePath = "C:\\Users\\paul.tennyson\\Desktop\\ONSPD_NOV_2023_UK\\Data\\ONSPD_NOV_2023_UK.csv";

        var addresses = ReadCsvFile(csvFilePath);
        UpdateAddressesWithGeoData(addresses, geoDataCsvFilePath);

        var addressesWithGeoData = addresses.Values.Where(a => a.Latitude != null).ToList();
        var englandAddresses = addresses.Values.Where(a => a.CountryName == "England").ToList();
        var walesAddresses = addresses.Values.Where(a => a.CountryName == "Wales").ToList();
        var scotlandAddresses = addresses.Values.Where(a => a.CountryName == "Scotland").ToList();
        var niAddresses = addresses.Values.Where(a => a.CountryName == "Northern Ireland").ToList();

        var totalAddresses = addresses.Count;
        var desiredCounts = CalculateDesiredCounts(totalAddresses);
        var addressesByCountry = SelectAddressesByCountry(addresses, desiredCounts);

        UpdateDatabaseWithAddresses(context, addresses);

        var x = "";
    }

    static Dictionary<string, Address> ReadCsvFile(string filePath)
    {
        int limit = 1000000;
        var addresses = new Dictionary<string, Address>();

        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = false,
        };

        using (var reader = new StreamReader(filePath))
        using (var csv = new CsvReader(reader, config))
        {
            csv.Context.RegisterClassMap<AddressMap>();

            int count = 0;
            while (csv.Read() && count < limit)
            {
                var record = csv.GetRecord<Address>();
                addresses[record.Postcode] = record;
                count++;
            }
        }
        return addresses;
    }

    static void UpdateAddressesWithGeoData(Dictionary<string, Address> addresses, string geoDataFilePath)
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true,
        };

        using (var reader = new StreamReader(geoDataFilePath))
        using (var csv = new CsvReader(reader, config))
        {
            csv.Context.RegisterClassMap<GeoDataMap>();

            while (csv.Read())
            {
                var record = csv.GetRecord<GeoData>();

                if (String.IsNullOrEmpty(record.DOTERM) && !String.IsNullOrEmpty(record.Latitude) && addresses.TryGetValue(record.Postcode, out var address))
                {
                    address.Latitude = record.Latitude;
                    address.Longitude = record.Longitude;
                    var country = Countries.FirstOrDefault(c => c.Code == record.CountryCode);
                    if (country != null)
                    {
                        address.CountryName = country.CountryName;
                        address.CountryCode = country.Code;
                    }
                }
            }
        }
    }

    static void UpdateDatabaseWithAddresses(ParticipantDbContext context, Dictionary<string, Address> addresses)
    {
        int i = 0;
        List<Address> addressList = addresses.Values.ToList();

        List<ParticipantAddress> participantAddresses = context.ParticipantAddress.ToList();

        foreach (var participantAddress in participantAddresses)
        {
            var address = addressList[i];

            if (!String.IsNullOrEmpty(address.Longitude) && !String.IsNullOrEmpty(address.Latitude))
            {
                participantAddress.AddressLine1 = address.HouseNumber;
                participantAddress.AddressLine2 = address.Street;
                participantAddress.AddressLine3 = null;
                participantAddress.AddressLine4 = null;
                participantAddress.Town = address.TownCity;
                participantAddress.Postcode = address.Postcode;

                var participantLocation = context.ParticipantLocation
                    .Where(pl => pl.ParticipantId == participantAddress.ParticipantId)
                    .FirstOrDefault();

                if (participantLocation != null)
                {
                    var longitude = Double.Parse(address.Longitude);
                    var latitude = Double.Parse(address.Latitude);

                    participantLocation.Location = new NetTopologySuite.Geometries.Point(longitude, latitude) { SRID = 4326 };
                }

                context.SaveChanges();
            }
            i++;
        }
    }

    public static List<Country> Countries { get; set; } = new List<Country>
    {
        new Country { Code = "E92000001", CountryName = "England" },
        new Country { Code = "S92000003", CountryName = "Scotland" },
        new Country { Code = "W92000004", CountryName = "Wales" },
        new Country { Code = "N92000002", CountryName = "Northern Ireland" }
    };


    public static Dictionary<string, int> CalculateDesiredCounts(int totalAddresses)
    {
        return new Dictionary<string, int>
        {
            { "E92000001", (int)(totalAddresses * 0.85) },  // England
            { "S92000003", (int)(totalAddresses * 0.08) },  // Scotland
            { "W92000004", (int)(totalAddresses * 0.045) }, // Wales
            { "N92000002", (int)(totalAddresses * 0.025) }  // Northern Ireland
        };
    }

    public static Dictionary<string, List<Address>> SelectAddressesByCountry(Dictionary<string, Address> addresses, Dictionary<string, int> desiredCounts)
    {
        var addressesByCountry = new Dictionary<string, List<Address>>();
        foreach (var countryCode in desiredCounts.Keys)
        {
            addressesByCountry[countryCode] = new List<Address>();
        }

        foreach (var address in addresses.Values)
        {
            if (address.CountryCode != null && addressesByCountry.ContainsKey(address.CountryCode))
            {
                addressesByCountry[address.CountryCode].Add(address);
            }
        }

        // Trim each list to the desired count
        foreach (var countryCode in desiredCounts.Keys)
        {
            if (addressesByCountry[countryCode].Count > desiredCounts[countryCode])
            {
                addressesByCountry[countryCode] = addressesByCountry[countryCode].Take(desiredCounts[countryCode]).ToList();
            }
        }

        return addressesByCountry;
    }
}