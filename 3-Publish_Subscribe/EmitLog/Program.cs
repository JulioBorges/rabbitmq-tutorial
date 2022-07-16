﻿// See https://aka.ms/new-console-template for more information

using RabbitMQ.Client;
using System.Text;

static string GetMessage(string[] args)
{
    return ((args.Length > 0)
           ? string.Join(" ", args)
           : "info: Hello World!");
}

var factory = new ConnectionFactory() { HostName = "localhost" };
using (var connection = factory.CreateConnection())
using (var channel = connection.CreateModel())
{
    channel.ExchangeDeclare(exchange: "logs", type: ExchangeType.Fanout);

    var message = GetMessage(args);
    var body = Encoding.UTF8.GetBytes(message);
    channel.BasicPublish(exchange: "logs",
                         routingKey: "",
                         basicProperties: null,
                         body: body);
    Console.WriteLine(" [x] Sent {0}", message);
}

Console.WriteLine(" Press [enter] to exit.");
Console.ReadLine();