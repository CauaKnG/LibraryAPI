using LibraryAPI.Domain.Entities;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

public class RabbitMQConsumer : BackgroundService
{
    private readonly RabbitMQSettings _settings;
    private readonly IModel _channel;
    private readonly IConnection _connection;

    public RabbitMQConsumer(IOptions<RabbitMQSettings> settings)
    {
        _settings = settings.Value;

        var factory = new ConnectionFactory
        {
            HostName = _settings.Host,
            UserName = _settings.Username,
            Password = _settings.Password
        };

        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.QueueDeclare(queue: "EmprestimoQueue", durable: true, exclusive: false, autoDelete: false, arguments: null);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            var emprestimo = JsonSerializer.Deserialize<Emprestimo>(message);

            if (emprestimo != null)
            {
                SimulateEmailSending(emprestimo);
            }
        };

        _channel.BasicConsume(queue: "EmprestimoQueue", autoAck: true, consumer: consumer);

        return Task.CompletedTask;
    }

    private void SimulateEmailSending(Emprestimo emprestimo)
    {
        // Simular a criação do arquivo e envio do e-mail
        Console.WriteLine($"Email enviado: LivroId = {emprestimo.LivroId}, UsuarioId = {emprestimo.UsuarioId}, DataEmprestimo = {emprestimo.DataEmprestimo}, DataDevolucao = {emprestimo.DataDevolucao}");
    }

    public override void Dispose()
    {
        _channel.Close();
        _connection.Close();
        base.Dispose();
    }
}
