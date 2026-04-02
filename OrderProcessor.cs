namespace SOLID_Fundamentals
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

   
    public interface IOrderRepository
    {
        void Add(Order order);
        Order GetById(int id);
        IEnumerable<Order> GetAll();
    }

    public interface IPaymentProcessorService
    {
        void Process(string paymentMethod, decimal amount);
    }

    public interface IInventoryService
    {
        void Update(IEnumerable<string> items);
    }

    public interface ILoggerService
    {
        void Log(string message);
    }

    public interface IReceiptGeneratorService
    {
        void Generate(Order order);
    }

    
    public class OrderProcessor
    {
        private readonly IPaymentProcessorService _paymentProcessor;
        private readonly IInventoryService _inventoryService;
        private readonly INotificationService _notificationService;
        private readonly ILoggerService _logger;
        private readonly IReceiptGeneratorService _receiptGenerator;

        public OrderProcessor(
            IPaymentProcessorService paymentProcessor,
            IInventoryService inventoryService,
            INotificationService notificationService,
            ILoggerService logger,
            IReceiptGeneratorService receiptGenerator)
        {
            _paymentProcessor = paymentProcessor;
            _inventoryService = inventoryService;
            _notificationService = notificationService;
            _logger = logger;
            _receiptGenerator = receiptGenerator;
        }

        public void ProcessOrder(Order order)
        {
            if (order == null || order.TotalAmount <= 0)
                throw new ArgumentException("Invalid order amount", nameof(order));

            Console.WriteLine($"Processing order {order.Id}");

            _paymentProcessor.Process(order.PaymentMethod, order.TotalAmount);
            _inventoryService.Update(order.Items);
            _notificationService.Send(order.CustomerEmail, $"Order {order.Id} processed");
            _logger.Log($"Order {order.Id} processed at {DateTime.Now}");
            _receiptGenerator.Generate(order);
        }
    }

    
    public class OrderReportService
    {
        private readonly IEnumerable<Order> _orders;

        public OrderReportService(IEnumerable<Order> orders)
        {
            _orders = orders;
        }

        public void GenerateMonthlyReport()
        {
            var totalRevenue = _orders.Sum(o => o.TotalAmount);
            var totalOrders = _orders.Count();
            Console.WriteLine($"Monthly Report: {totalOrders} orders, Revenue: {totalRevenue:C}");
        }

        public void ExportToExcel(string filePath)
        {
            Console.WriteLine($"Exporting orders to {filePath}");
        }
    }

   
    public class InMemoryOrderRepository : IOrderRepository
    {
        private readonly List<Order> _orders = new List<Order>();
        public void Add(Order order) => _orders.Add(order);
        public Order GetById(int id) => _orders.FirstOrDefault(o => o.Id == id);
        public IEnumerable<Order> GetAll() => _orders;
    }

    public class SimplePaymentProcessor : IPaymentProcessorService
    {
        public void Process(string paymentMethod, decimal amount)
        {
            Console.WriteLine($"Processing payment: {amount:C} via {paymentMethod}");
        }
    }

    public class SimpleInventoryService : IInventoryService
    {
        public void Update(IEnumerable<string> items)
        {
            Console.WriteLine($"Updating inventory for {items.Count()} items");
        }
    }

    public class ConsoleLoggerService : ILoggerService
    {
        public void Log(string message) => Console.WriteLine($"[LOG] {message}");
    }

    public class SimpleReceiptGenerator : IReceiptGeneratorService
    {
        public void Generate(Order order) => Console.WriteLine($"Generating receipt for order {order.Id}");
    }
}