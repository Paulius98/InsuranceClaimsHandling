using Claims.Domain.Enums;

namespace Claims.Domain.Calculations;

public static class PremiumCalculator
{
    private const decimal _baseDayRate = 1250;
    private const int _firstTierDays = 30;
    private const int _secontTierDays = 150;

    public static decimal Compute(DateTimeOffset startDate, DateTimeOffset endDate, CoverType coverType)
    {

        var multiplier = CoverTypeRates.GetRateByCoverType(coverType);
        var premiumPerDay = _baseDayRate * multiplier;
        var insuranceTotalDays = (int)(endDate - startDate).TotalDays;

        return CalculatePremium(insuranceTotalDays, premiumPerDay, coverType);
    }

    private static decimal CalculatePremium(int insuranceTotalDays, decimal premiumPerDay, CoverType coverType)
    {
        var totalPremium = 0m;

        totalPremium += FirstTierTotalPremiumPrice(insuranceTotalDays, premiumPerDay);
        totalPremium += SecondTierPremiumPrice(insuranceTotalDays, premiumPerDay, coverType);
        totalPremium += ThirdTierPremiumPrice(insuranceTotalDays, premiumPerDay, coverType);

        return totalPremium;
    }

    private static decimal FirstTierTotalPremiumPrice(int insuranceTotalDays, decimal premiumPerDay)
    {
        return CalculateTierPremium(insuranceTotalDays, _firstTierDays, premiumPerDay);
    }

    private static decimal SecondTierPremiumPrice(int insuranceTotalDays, decimal premiumPerDay, CoverType coverType)
    {
        var insuranceDays = insuranceTotalDays - _firstTierDays;
        var discountFactor = CoverTypeDiscountRates.GetSecondTierRateByCoverType(coverType);
        return CalculateTierPremium(insuranceDays, _secontTierDays, premiumPerDay, discountFactor);
    }

    private static decimal ThirdTierPremiumPrice(int insuranceTotalDays, decimal premiumPerDay, CoverType coverType)
    {
        var insuranceDays = insuranceTotalDays - (_firstTierDays + _secontTierDays);
        var discountFactor = CoverTypeDiscountRates.GetThirdTierRateByCoverType(coverType);
        return CalculateTierPremium(insuranceDays, insuranceDays, premiumPerDay, discountFactor);
    }

    private static decimal CalculateTierPremium(int insuranceDays, int tierDays, decimal premiumPerDay, decimal discountFactor = 1.00m)
    {
        if (insuranceDays <= 0) return 0m;
        int days = Math.Min(insuranceDays, tierDays);
        return premiumPerDay * days * discountFactor;
    }
}
