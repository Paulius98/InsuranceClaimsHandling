using Claims.Domain.Enums;

namespace Claims.Domain.Calculations;

public static class PremiumCalculator
{
    private const decimal _baseDayRate = 1250;
    private const int _firstTierDays = 30;
    private const int _secontTierDays = 150;

    public static decimal Compute(DateTimeOffset startDate, DateTimeOffset endDate, CoverType coverType)
    {

        var multiplier = GetMultiplierByCoverType(coverType);
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
        if (insuranceTotalDays <= 0) return 0m;
        var days = Math.Min(insuranceTotalDays, _firstTierDays);
        return premiumPerDay * days;
    }

    private static decimal SecondTierPremiumPrice(int insuranceTotalDays, decimal premiumPerDay, CoverType coverType)
    {
        insuranceTotalDays -= _firstTierDays;
        if (insuranceTotalDays <= 0) return 0m;
        var days = Math.Min(insuranceTotalDays, _secontTierDays);
        return premiumPerDay * days * (coverType == CoverType.Yacht ? 0.95m : 0.98m);
    }

    private static decimal ThirdTierPremiumPrice(int insuranceTotalDays, decimal premiumPerDay, CoverType coverType)
    {
        insuranceTotalDays -= _firstTierDays + _secontTierDays;
        if (insuranceTotalDays <= 0) return 0m;
        return premiumPerDay * insuranceTotalDays * (coverType == CoverType.Yacht ? 0.92m : 0.97m);
    }

    private static decimal GetMultiplierByCoverType(CoverType coverType)
    {
        switch (coverType)
        {
            case CoverType.Yacht:
                return CoverTypeRates.Yacht;
            case CoverType.PassengerShip:
                return CoverTypeRates.PassengerShip;
            case CoverType.Tanker:
                return CoverTypeRates.Tanker;
            default:
                return CoverTypeRates.Other;
        }
    }
}
