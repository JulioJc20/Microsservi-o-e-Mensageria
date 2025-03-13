using System.Text;
using System.Text.Json;
using System.Threading.Channels;
using API_Publicador.Domain;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using API_Consumidor.Repository;

namespace API_Consumidor.Consumidor
{
    public class Consumidor_Deletar
    {

        private readonly IModel _channel;
        private readonly PessoasRepository _pessoaRepository;
        private readonly IConnection _connection;
        public Consumidor_Deletar(IConfiguration configuration)
        {
            var factory = new ConnectionFactory { HostName = "localhost" };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _pessoaRepository = new PessoasRepository(configuration);
        }

        public void Consumir_Deletar()
        {


            _channel.QueueDeclare("Deletar",
                                  durable: false,
                                  exclusive: false,
                                  autoDelete: true,
                                  arguments: null);

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (mode, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                var pessoa = JsonSerializer.Deserialize<Pessoa>(message);

                if (pessoa != null)
                {
                    _pessoaRepository.DeletarPessoaBanco(pessoa);
                }
            };

            _channel.BasicConsume("Deletar",
                                   autoAck: true,
                                   consumer: consumer);



        }


    }
}

