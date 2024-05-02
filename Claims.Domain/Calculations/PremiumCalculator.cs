using Claims.Domain.Enums;

namespace Claims.Domain.Calculations
{
    public static class PremiumCalculator
    {
        private const decimal _baseDayRate = 1250;
        
        public static decimal Compute(DateTimeOffset startDate, DateTimeOffset endDate, CoverType coverType)
        {

            var multiplier = GetMultiplierByCoverType(coverType);
            var premiumPerDay = _baseDayRate * multiplier;
            var insuranceLength = (endDate - startDate).TotalDays;
            var totalPremium = 0m;

            for (var i = 0; i < insuranceLength; i++)
            {
                totalPremium += CalculatePremiumForDay(i, premiumPerDay, coverType);
            }

            return totalPremium;
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

        private static decimal CalculatePremiumForDay(int dayIndex, decimal premiumPerDay, CoverType coverType)
        {
            if (dayIndex < 30)
            {
                return premiumPerDay;
            }
            if (dayIndex < 180)
            {
                return premiumPerDay * (coverType == CoverType.Yacht ? 0.95m : 0.98m);
            }
            return premiumPerDay * (coverType == CoverType.Yacht ? 0.92m : 0.97m);
        }
    }
}
