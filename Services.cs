namespace SOLID_Fundamentals
{
    using System;

   
    public interface INotificationService
    {
        void Send(string to, string message);
    }

    public class EmailService : INotificationService
    {
        public void Send(string to, string message)
        {
            Console.WriteLine($"Sending email to {to}: {message}");
        }
    }

    public class SmsService : INotificationService
    {
        public void Send(string to, string message)
        {
            Console.WriteLine($"Sending SMS to {to}: {message}");
        }
    }

   
    public class OrderService
    {
        private readonly INotificationService _emailService;
        private readonly INotificationService _smsService;

        public OrderService(INotificationService emailService, INotificationService smsService)
        {
            _emailService = emailService;
            _smsService = smsService;
        }

        public void PlaceOrder(Order order)
        {
            _emailService.Send(order.CustomerEmail, "Order Confirmation: Your order has been placed");
            _smsService.Send(order.CustomerPhone, "Your order has been placed");
        }
    }

    public class NotificationService
    {
        private readonly INotificationService _emailService;

        public NotificationService(INotificationService emailService)
        {
            _emailService = emailService;
        }

        public void SendPromotion(string email, string promotion)
        {
            _emailService.Send(email, $"Special Promotion: {promotion}");
        }
    }
}