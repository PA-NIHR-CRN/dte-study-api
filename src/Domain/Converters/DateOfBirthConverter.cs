using System;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;

namespace Domain.Converters
{
    public class DateOfBirthConverter : IPropertyConverter
    {
        public DynamoDBEntry ToEntry(object value)
        {
            if (value is not DateTime dt) return null;

            var normalised = DateTime.SpecifyKind(dt.Date, DateTimeKind.Utc);

            return normalised;
        }

        public object FromEntry(DynamoDBEntry entry)
        {
            if (entry == null) return null;

            var dt = entry.AsDateTime();

            return DateTime.SpecifyKind(dt.Date, DateTimeKind.Utc);
        }
    }

}
