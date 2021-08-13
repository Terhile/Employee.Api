using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employee.Api.Domain.Configurations
{
    public class KafkaSettings
    {
        public string BootstrapServers { get; set; }
        public string GroupId { get; set; }
        public string SaslMechanism { get; set; }
        public string TopicName { get; set; }

        public string SecurityProtocol { get; set; }
    }
}
