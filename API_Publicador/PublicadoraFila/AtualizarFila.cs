using System.Text;
using RabbitMQ.Client;

namespace API_Publicador.Publicadora
{
    public class AtualizarFila
    {
        private readonly IModel _channel;
        private readonly IConnection _connection;
        public AtualizarFila()
        {
            var factory = new ConnectionFactory { HostName = "localhost" };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
        }
        public void AtualizarMensagem(string fila, string message)
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
