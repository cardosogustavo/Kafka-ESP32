using Confluent.Kafka;

namespace esp32_kafka_producer;

/// <summary>
///  Producer logic:
///  When a new instance is created, we should pass the topic.
///  It has a method that writes data to the topic.This method requires a string (the message to be written)
/// </summary>


public class Producer
{

    // The Kafka topic to write to
    const string topic = "sensor-data";

    public void WriteDataToTopic(string bootstrapServer, string message)
    {

        // Configuration for the producer builder
        var config = new ProducerConfig
        {
            BootstrapServers = bootstrapServer,
            Acks = Acks.All // TODO: search this
        };

        // Now the weird syntax to write
        using (var producer = new ProducerBuilder<Null, string>(config).Build())
        {
            //producer.Produce(topic, new Message<Null, string> { Value = message },
            //    (deliveryReport) =>
            //    {
            //        if (deliveryReport.Error.Code != ErrorCode.NoError)
            //        {
            //            Console.WriteLine($"Failed to deliver message: {deliveryReport.Error.Reason}");
            //        }
            //        else
            //        {
            //            Console.WriteLine($"Produce event to topic {topic}: key = {user}, value = {item}");
            //        }
            //    });
            producer.Produce(topic, new Message<Null, string> { Value = message });

            producer.Flush(TimeSpan.FromSeconds(10));
            Console.WriteLine("Successfully written message to topic!!");
        }


    }


}