using Confluent.Kafka;
using Employee.Api.Domain.Configurations;
using Microsoft.Extensions.Options;
using System;

namespace Employee.Api.Services.Kafka
{
    public class ProducerWrapper : IDisposable
    {
        private readonly IProducer<string, string> _kafkaProducer;
        private readonly KafkaSettings _kafkaSettings;
        public ProducerWrapper(IOptions<KafkaSettings> producerSettings)
        {
            _kafkaSettings = producerSettings.Value;
            var conf = new ProducerConfig { BootstrapServers = _kafkaSettings.BootstrapServers, };
            this._kafkaProducer = new ProducerBuilder<string, string>(conf).Build();
        }
        public void Dispose()
        {
            _kafkaProducer.Flush();
            _kafkaProducer.Dispose();
        }

        public Handle Handle { get => _kafkaProducer.Handle; }

    }
}
