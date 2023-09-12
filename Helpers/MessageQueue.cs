using RabbitMQ.Client;
using System;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Net.NetworkInformation;
using ShopCart.Dtos;
using Newtonsoft.Json;
using System.Text;
using Microsoft.Extensions.Primitives;
using RabbitMQ.Client.Events;

namespace ShopCart.Helpers
{
    public static class MessageQueue
    {

        private const string Username = "guest";
        private const string Password = "guest";
        private const string HostName = "localhost";

        public static void PublishMessage(int productId, int orderId)
        {
            ConnectionFactory factory = new ConnectionFactory()
            {

                HostName = HostName,
                UserName = Username,
                Password = Password,
            };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare("order-placed-by-user", durable: false, exclusive: false, autoDelete: false);

            var orderEvent = new UserOrderEvent
            {
                OrderId = orderId,
                ProductId = productId,
            };
            var message = JsonConvert.SerializeObject(orderEvent);
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: "", routingKey: "order-placed-by-user", basicProperties: null, body: body);


        }

        public static string Consumer()
        {
            try
            {
                ConnectionFactory factory = new ConnectionFactory()
                {

                    HostName = HostName,
                    UserName = Username,
                    Password = Password,
                };

                using var connection = factory.CreateConnection();
                using var channel = connection.CreateModel();

                channel.QueueDeclare("order-placed-by-user", durable: false, exclusive: false, autoDelete: false);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    try
                    {
                        var body = ea.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);

                        // Deserialize the message (order event)
                        var orderEvent = JsonConvert.DeserializeObject<UserOrderEvent>(message);

                        // Simulate updating the stock (replace with actual logic)
                        Console.WriteLine($"Received order event: OrderId={orderEvent.OrderId}, ProductId={orderEvent.ProductId}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error while processing message: {ex.Message}");
                    }
                };

                // Start consuming messages
                channel.BasicConsume(queue: "order-placed-by-user", autoAck: true, consumer: consumer);

                Console.WriteLine("Waiting for order placed events. Press [Enter] to exit.");
                Console.ReadLine();

                return "Consumer started successfully.";
            }
            catch (Exception ex)
            {
                return $"Error starting consumer: {ex.Message}";
            }
        }


    }
}
