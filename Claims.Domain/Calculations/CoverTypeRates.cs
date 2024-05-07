using Claims.Domain.Enums;

namespace Claims.Domain.Calculations;

public static class CoverTypeRates
{
    private const decimal _yacht = 1.1m;
    private const decimal _passengerShip = 1.2m;
    private const decimal _tanker = 1.5m;
    private const decimal _other = 1.3m;

    public static decimal GetRateByCoverType(CoverType coverType)
    {
        switch (coverType)
        {
            case CoverType.Yacht:
                return _yacht;
            case CoverType.PassengerShip:
                return _passengerShip;
            case CoverType.Tanker:
                return _tanker;
            default:
                return _other;
        }
    }
}
