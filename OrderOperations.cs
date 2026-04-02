namespace SOLID_Fundamentals
{
    using System;

    
    public interface IOrderManager
    {
        void CreateOrder(Order order);
        void UpdateOrder(Order order);
        void DeleteOrder(int orderId);
    }

    public interface IPaymentProcessor
    {
        void ProcessPayment(Order order);
    }

    public interface IShippingService
    {
        void ShipOrder(Order order);
    }

    public interface IInvoiceGenerator
    {
        void GenerateInvoice(Order order);
    }

    public interface IReportService
    {
        void GenerateReport(DateTime from, DateTime to);
        void ExportToExcel(string filePath);
    }

    public interface IDatabaseAdmin
    {
        void BackupDatabase();
        void RestoreDatabase();
    }

   
    public class OrderManager : IOrderManager, IPaymentProcessor, IShippingService, IInvoiceGenerator
    {
        public void CreateOrder(Order order) => Console.WriteLine("Order created");
        public void UpdateOrder(Order order) => Console.WriteLine("Order updated");
        public void DeleteOrder(int orderId) => Console.WriteLine("Order deleted");
        public void ProcessPayment(Order order) => Console.WriteLine("Payment processed");
        public void ShipOrder(Order order) => Console.WriteLine("Order shipped");
        public void GenerateInvoice(Order order) => Console.WriteLine("Invoice generated");
    }

   
    public class CustomerPortal : IOrderManager
    {
        public void CreateOrder(Order order) => Console.WriteLine("Order created by customer");
        public void UpdateOrder(Order order) => Console.WriteLine("Order updated by customer");
        public void DeleteOrder(int orderId) => Console.WriteLine("Order deleted by customer");
    }
}