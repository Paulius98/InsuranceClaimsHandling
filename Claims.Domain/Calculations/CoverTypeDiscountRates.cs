using Claims.Domain.Enums;

namespace Claims.Domain.Calculations;

public class CoverTypeDiscountRates
{
    private const decimal _secondTierDefaultRate = 0.98m;
    private const decimal _yachtSecondTierRate = 0.95m;

    private const decimal _thirdTierDefaultRate = 0.97m;
    private const decimal _yachtThirdTierRate = 0.92m;

    public static decimal GetSecondTierRateByCoverType(CoverType type) =>
        type == CoverType.Yacht ? _yachtSecondTierRate : _secondTierDefaultRate;

    public static decimal GetThirdTierRateByCoverType(CoverType type) =>
        type == CoverType.Yacht ? _yachtThirdTierRate : _thirdTierDefaultRate;
}
