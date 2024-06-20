namespace ImportGeocodeData
{
    public enum UkNation
    {
        England,
        Scotland,
        Wales,
        NorthernIreland
    }

    public interface INationalPostcodeProvider
    {
        string GetRandomNationalPostcode(UkNation nation);
    }
}