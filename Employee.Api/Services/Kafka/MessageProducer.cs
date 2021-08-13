using Confluent.Kafka;
using Employee.Api.Domain.Configurations;
using Employee.Api.Domain.Models;
using Employee.Api.Services.Logger;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employee.Api.Services.Kafka
{
    public class MessageProducer : IMessageProducer
    {
        private readonly IProducer<string, string> producer;
        private readonly KafkaSettings _kafkaSettings;
        private readonly IActivityLogger _logger;

        public MessageProducer(ProducerWrapper producerWrapperHandle, IOptions<KafkaSettings> producerSettings, IActivityLogger logger)
        {
            producer = new DependentProducerBuilder<string, string>(producerWrapperHandle.Handle).Build();
            _kafkaSettings = producerSettings.Value;
            _logger = logger;
        }
        public void deliveryReportHandler(DeliveryReport<string, string> deliveryReport)
        {
            if (deliveryReport.Status == PersistenceStatus.NotPersisted)
                _logger.LogError($"Failed to log request time for path: {deliveryReport.Message.Key} Error : {deliveryReport.Error} ");
            else
                _logger.LogMessage($"employee info successfullly publish to topic {_kafkaSettings.TopicName},  Employee key: {deliveryReport.Message.Key}");

        }
        public void Flush(TimeSpan timeout)
           => this.producer.Flush(timeout);
        public void WriteMessage(EmployeeModel employee)
        {

            try
            {
                var empInfo = JsonConvert.SerializeObject(employee);  //should use schema registry here to handle schema validation or  serialization/deserialization of data model  
                producer.Produce(_kafkaSettings.TopicName, new Message<string, string> { Key = employee.EmployeeNumber, Value = empInfo }, deliveryReportHandler);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
            }

        }
    }
}
