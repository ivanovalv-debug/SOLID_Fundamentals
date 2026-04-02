using System;
using System.Collections.Generic;
using SOLID_Fundamentals;

namespace SOLID_Fundamentals.App
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== SOLID Principles Demo ===\n");

            
            Console.WriteLine("--- DIP Demo (Services.cs) ---");
            var emailService = new EmailService();
            var smsService = new SmsService();
            var orderService = new OrderService(emailService, smsService);

            var testOrder = new Order
            {
                Id = 1,
                CustomerEmail = "test@example.com",
                CustomerPhone = "+1234567890",
                TotalAmount = 100m
            };

            orderService.PlaceOrder(testOrder);
            Console.WriteLine();

            
            Console.WriteLine("--- OCP Demo (DiscountCalculator.cs) ---");
            var discountStrategies = new List<IDiscountStrategy>
            {
                new RegularDiscount(),
                new PremiumDiscount(),
                new VipDiscount(),
                new StudentDiscount(),
                new SeniorDiscount()
            };

            var shippingStrategies = new List<IShippingStrategy>
            {
                new StandardShipping(),
                new ExpressShipping(),
                new OvernightShipping(),
                new InternationalShipping()
            };

            var calculator = new DiscountCalculator(discountStrategies, shippingStrategies);

            Console.WriteLine($"Regular discount: {calculator.CalculateDiscount("Regular", 1000m):C}");
            Console.WriteLine($"Premium discount: {calculator.CalculateDiscount("Premium", 1000m):C}");
            Console.WriteLine($"VIP discount: {calculator.CalculateDiscount("VIP", 1000m):C}");
            Console.WriteLine();

            
            Console.WriteLine("--- SRP Demo (OrderProcessor.cs) ---");
            var processor = new OrderProcessor(
                new SimplePaymentProcessor(),
                new SimpleInventoryService(),
                new EmailService(),
                new ConsoleLoggerService(),
                new SimpleReceiptGenerator()
            );

            var order = new Order
            {
                Id = 2,
                TotalAmount = 250m,
                PaymentMethod = "CreditCard",
                Items = new List<string> { "Item1", "Item2" },
                CustomerEmail = "customer@example.com"
            };

            processor.ProcessOrder(order);
            Console.WriteLine();

            
            Console.WriteLine("--- LSP Demo (Bank.cs) ---");
            var bank = new BankService();

            var savingsAccount = new SavingsAccount();
            savingsAccount.Deposit(500m);
            if (bank.ProcessWithdrawal(savingsAccount, 200m, out var msg1))
                Console.WriteLine(msg1);
            else
                Console.WriteLine(msg1);

            var checkingAccount = new CheckingAccount();
            checkingAccount.Deposit(300m);
            if (bank.ProcessWithdrawal(checkingAccount, 400m, out var msg2))
                Console.WriteLine(msg2);
            else
                Console.WriteLine(msg2);
            Console.WriteLine();

            
            Console.WriteLine("--- ISP Demo (OrderOperations.cs) ---");
            IOrderManager customerPortal = new CustomerPortal();
            customerPortal.CreateOrder(testOrder);
            

            IOrderManager orderManager = new OrderManager();
            orderManager.CreateOrder(testOrder);
            ((IPaymentProcessor)orderManager).ProcessPayment(testOrder);

            Console.WriteLine("\n=== All demos completed successfully ===");
        }
    }
}