namespace Anyex.Survey.Domain;

public class SurveyResultAccess
{
    public SurveyResultsAccessType AccessType { get; }
    public string? PinCode { get; }

    public SurveyResultAccess(string? pinCode, SurveyResultsAccessType accessType)
    {
        if (string.IsNullOrEmpty(pinCode) && 
            accessType == SurveyResultsAccessType.ByPinCode)
        {
            throw new InvalidOperationException("Pin Code cannot be empty");
        }

        //
        // lets set the length for a pin code as 8
        //
        if (!string.IsNullOrEmpty(pinCode) && pinCode.Length > 8)
        {
            throw new ArgumentOutOfRangeException("PIN Code must be less or equal to 8");
        }

        AccessType = accessType;
        PinCode = pinCode;
    }
}