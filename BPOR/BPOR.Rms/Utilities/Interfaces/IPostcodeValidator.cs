using Rbec.Postcodes;

namespace BPOR.Rms.Utilities.Interfaces;

public interface IPostcodeValidator
{
    bool IsValidUkPostcode(Postcode? postCode);
}
