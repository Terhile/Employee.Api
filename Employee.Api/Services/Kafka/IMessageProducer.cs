using Confluent.Kafka;
using Employee.Api.Domain.Models;
using System.Threading.Tasks;

namespace Employee.Api.Services.Kafka
{
    public interface IMessageProducer
    {
        public void WriteMessage(EmployeeModel employee);
        void deliveryReportHandler(DeliveryReport<string, string> deliveryReport);
    }
}
