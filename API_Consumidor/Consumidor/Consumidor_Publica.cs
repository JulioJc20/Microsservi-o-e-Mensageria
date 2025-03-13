using API_Consumidor.Repository;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text.Json;
using System.Text;
using API_Publicador.Domain;
using Microsoft.AspNetCore.Mvc;
using API_Publicador.Model.Dto;

namespace API_Consumidor.Consumidor
{
    public class Consumidor_Publica
    {


        private readonly IModel _channel;
        private readonly PessoasRepository _pessoaRepository;
        private readonly IConnection _connection;

        public Consumidor_Publica(IConfiguration configuration)
        {
            var factory = new ConnectionFactory { HostName = "localhost" };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _pessoaRepository = new PessoasRepository(configuration);
        }

        public void Consumir_Publicar()
        {

            _channel.QueueDeclare("Criar",
                                  durable: false,
                                  exclusive: false,
                                  autoDelete: true,
                                  arguments: null);

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (mode, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                Console.WriteLine("Mensagem recebida: " + message);

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var pessoa = JsonSerializer.Deserialize<PessoaDto>(message, options);


                if (pessoa == null)
                {
                }
                else if (pessoa.Endereco == null)
                {
                    Console.WriteLine("Pessoa veio nulo");
                }
                else
                {
                    _pessoaRepository.InserirPessoaBanco(pessoa);
                }
            };

            _channel.BasicConsume("Criar",
                                   autoAck: true,
                                   consumer: consumer);

        }

    }
}
