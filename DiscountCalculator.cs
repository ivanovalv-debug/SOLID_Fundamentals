namespace SOLID_Fundamentals
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    
    public interface IDiscountStrategy
    {
        string CustomerType { get; }
        decimal CalculateDiscount(decimal orderAmount);
    }

    public class RegularDiscount : IDiscountStrategy
    {
        public string CustomerType => "Regular";
        public decimal CalculateDiscount(decimal orderAmount) => orderAmount * 0.05m;
    }

    public class PremiumDiscount : IDiscountStrategy
    {
        public string CustomerType => "Premium";
        public decimal CalculateDiscount(decimal orderAmount) => orderAmount * 0.10m;
    }

    public class VipDiscount : IDiscountStrategy
    {
        public string CustomerType => "VIP";
        public decimal CalculateDiscount(decimal orderAmount) => orderAmount * 0.15m;
    }

    public class StudentDiscount : IDiscountStrategy
    {
        public string CustomerType => "Student";
        public decimal CalculateDiscount(decimal orderAmount) => orderAmount * 0.08m;
    }

    public class SeniorDiscount : IDiscountStrategy
    {
        public string CustomerType => "Senior";
        public decimal CalculateDiscount(decimal orderAmount) => orderAmount * 0.07m;
    }

    
    public interface IShippingStrategy
    {
        string MethodName { get; }
        decimal CalculateCost(decimal weight, string destination);
    }

    public class StandardShipping : IShippingStrategy
    {
        public string MethodName => "Standard";
        public decimal CalculateCost(decimal weight, string destination) => 5.00m + (weight * 0.5m);
    }

    public class ExpressShipping : IShippingStrategy
    {
        public string MethodName => "Express";
        public decimal CalculateCost(decimal weight, string destination) => 15.00m + (weight * 1.0m);
    }

    public class OvernightShipping : IShippingStrategy
    {
        public string MethodName => "Overnight";
        public decimal CalculateCost(decimal weight, string destination) => 25.00m + (weight * 2.0m);
    }

    public class InternationalShipping : IShippingStrategy
    {
        public string MethodName => "International";
        public decimal CalculateCost(decimal weight, string destination)
        {
            return destination switch
            {
                "USA" => 30.00m,
                "Europe" => 35.00m,
                "Asia" => 40.00m,
                _ => 50.00m
            };
        }
    }

   
    public class DiscountCalculator
    {
        private readonly Dictionary<string, IDiscountStrategy> _discountStrategies;
        private readonly Dictionary<string, IShippingStrategy> _shippingStrategies;

        public DiscountCalculator(
            IEnumerable<IDiscountStrategy> discountStrategies,
            IEnumerable<IShippingStrategy> shippingStrategies)
        {
            _discountStrategies = discountStrategies.ToDictionary(s => s.CustomerType);
            _shippingStrategies = shippingStrategies.ToDictionary(s => s.MethodName);
        }

        public decimal CalculateDiscount(string customerType, decimal orderAmount)
        {
            return _discountStrategies.TryGetValue(customerType, out var strategy)
                ? strategy.CalculateDiscount(orderAmount)
                : 0;
        }

        public decimal CalculateShippingCost(string shippingMethod, decimal weight, string destination)
        {
            return _shippingStrategies.TryGetValue(shippingMethod, out var strategy)
                ? strategy.CalculateCost(weight, destination)
                : 0;
        }
    }
}