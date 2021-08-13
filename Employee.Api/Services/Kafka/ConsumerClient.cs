using Confluent.Kafka;
using Employee.Api.Domain.Configurations;
using Employee.Api.Domain.Models;
using Employee.Api.Services.Logger;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Employee.Api.Services.Kafka
{
    public class ConsumerClient : BackgroundService
    {
        private readonly IActivityLogger _logger;
        private readonly KafkaSettings _kafkaSettings;
        private readonly IConsumer<string, string> kafkaConsumer;

        public ConsumerClient(IActivityLogger logger, IOptions<KafkaSettings> producerSettings)
        {

            _logger = logger;
            _kafkaSettings = producerSettings.Value;
            var conf = new ConsumerConfig
            {

                BootstrapServers = _kafkaSettings.BootstrapServers,
                GroupId = _kafkaSettings.GroupId,
                AutoOffsetReset = AutoOffsetReset.Earliest,
                EnableAutoCommit = true,
                EnableAutoOffsetStore = true,
            };
            this.kafkaConsumer = new ConsumerBuilder<string, string>(conf).Build();
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            new Thread(() => ReadMessages(stoppingToken)).Start();

            return Task.CompletedTask;
        }

        private void ReadMessages(CancellationToken cancellationToken)
        {
            kafkaConsumer.Subscribe(_kafkaSettings.TopicName);

            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    var cr = this.kafkaConsumer.Consume(cancellationToken);

                    EmployeeModel employee = JsonConvert.DeserializeObject<EmployeeModel>(cr.Message?.Value);

                    _logger.LogMessage($"Employee with ID '{employee.EmployeeNumber}' read from '{cr.Topic}' at timestamp' {cr.Message.Timestamp.UtcDateTime}'");

                }
                catch (OperationCanceledException)
                {

                    break;
                }
                catch (ConsumeException e)
                {
                    _logger.LogError($"Consume error: {e.Error.Reason}");

                    if (e.Error.IsFatal)
                    {
                        break;
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError(e);
                    break;
                }
            }
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogMessage(
                "Consume Scoped Service Hosted Service is stopping.");

            await base.StopAsync(stoppingToken);
        }
    }
}
