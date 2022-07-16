// See https://aka.ms/new-console-template for more information
using RabbitMQ.Client;
using System.Text;

var factory = new ConnectionFactory() { HostName = "localhost" };
using (var connection = factory.CreateConnection())
using (var channel = connection.CreateModel())
{
    channel.QueueDeclare(queue: "hello",
                         durable: false,
                         exclusive: false,
                         autoDelete: false,
                         arguments: null);

    Console.WriteLine("Send messages with [Enter], send :q to close this application");

    string message = Console.ReadLine() ?? "";

    while (message != ":q")
    {
        if (!string.IsNullOrEmpty(message))
        {
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: "",
                                 routingKey: "hello",
                                 basicProperties: null,
                                 body: body);
            Console.WriteLine(" [x] Sent {0}", message);
        }

        message = Console.ReadLine() ?? "";
    }
}

Console.WriteLine("Sender has closed !");