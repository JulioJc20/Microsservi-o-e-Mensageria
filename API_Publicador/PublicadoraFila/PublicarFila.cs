using API_Publicador.Domain;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Data.Common;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using System.Threading.Channels;


namespace API_Publicador.Publicadora

{
    public class PublicarFila
    {
        private readonly IModel _channel;
        private readonly IConnection _connection;
        public PublicarFila()
        {
            var factory = new ConnectionFactory { HostName = "localhost" };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
        }
        public void PublicarMensagem(string fila, string message)
        {
            _channel.QueueDeclare(fila,
                                durable: false,
                                exclusive: false,
                                arguments: null);

            var body = Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish(exchange: string.Empty,
                                 fila,
                                 basicProperties: null,
                                 body: body);
        }




    }
}
