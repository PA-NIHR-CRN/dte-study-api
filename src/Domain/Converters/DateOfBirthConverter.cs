using System;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;

namespace Domain.Converters
{
    public class DateOfBirthConverter : IPropertyConverter
    {
        public DynamoDBEntry ToEntry(object value)
        {
            if (value == null)
                return DynamoDBNull.Null;

            if (value is not DateTime dt)
                throw new ArgumentException("DateOfBirth must be a DateTime", nameof(value));

            // Strip time and timezone meaning
            return new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0, DateTimeKind.Unspecified);
        }

        public object FromEntry(DynamoDBEntry entry)
        {
            if (entry == null || entry is DynamoDBNull)
                return null;

            var dt = entry.AsDateTime();

            return new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0, DateTimeKind.Unspecified);
        }
    }
}
