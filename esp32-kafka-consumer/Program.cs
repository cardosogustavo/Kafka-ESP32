
using Confluent.Kafka;


// TODO: add reading from beginning

public class Program
{
    static void Main(string[] args)
    {
        // Passing the configuration for ConsumerConfig
        var config = new ConsumerConfig
        {
            BootstrapServers = "172.18.145.39:9092",
            GroupId = "kafka-consumer",
            AutoOffsetReset = AutoOffsetReset.Earliest
        };

        const string topic = "sensor-data";

        CancellationTokenSource cts = new CancellationTokenSource();
        Console.CancelKeyPress += (_, e) =>
        {
            e.Cancel = true; // Prevent the process from terminating
            cts.Cancel();
        };

        using (var consumer = new ConsumerBuilder<Null, string>(config).Build())
        {
            // We need to first subscribe to a topic and read the data
            consumer.Subscribe(topic);
            try
            {
                while (true)
                {
                    var cr = consumer.Consume(cts.Token);
                    Console.WriteLine($"Consumed event from topic {topic}: {cr.Message.Value}");
                }
            }
            catch (OperationCanceledException)
            {
                // Ctrl C was pressed
            }
            finally
            {
                consumer.Close();
            }
        }
    }
}
