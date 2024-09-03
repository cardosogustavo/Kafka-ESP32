using System.IO.Ports;

namespace esp32_kafka_producer;

/// <summary>
///     Main functionality
///     
///     1. Connect to the serial port and open communication
///     2. Create Producer and Consumer instances. Ensure the communication with the Kafka server is enabled. TODO: maybe in the future
///     add a function to start a Kafka server automatically
///     3. Start the loop that will read the serial port's data. We need to defined an interval, but maybe this is done on Arduino IDE
///     4. The loop will:
///         - Read sensor data
///         - Pass it to the Producer instance
///         - Producer writes to Kafka topic
///         - Sleep for X seconds?
///     5. Consumer does not need to run right after Producer, it is a separate entity
///     6. So in this case, maybe Consumer can be another console application
///     7. Use Docker to wrap up everything when it's finished TODO
/// </summary>
class Program
{

    static void Main(string[] args)
    {

        // Sensor info for Serial communication initialization
        string sensorPortName = "COM3";
        int sensorBaudRate = 115200;
        SerialPort serialPort = new SerialPort();
        InitializeSensor(serialPort, sensorPortName, sensorBaudRate);

        // Loop to read sensor data and print to screen
        //bool _continue = true;

        // Open port for communication
        //serialPort.Open();

        // First let's try the Producer. To get the WSL instance's IP, open a terminal and run the command 'hostname -I'
        string bootstrapServer = "172.18.145.39:9092";
        Producer producer = new Producer();

        // Testing a write loop
        for (int i = 0; i < 10; i++)
        {
            //producer.WriteDataToTopic(bootstrapServer, ReadSensorData(serialPort));
            producer.WriteDataToTopic(bootstrapServer, $"test message number {i} from producer");
        }

        // TODO: fix the data, it is not being read from the start. For example, we have cases of:
        // (inch): 30.00
        // inch): 40.00
        // Distance (cm): 50.00
        // Data is not consistent, I think the fix is on ESP32 code

        //while (_continue)
        //{
        //    try
        //    {
        //        ReadSensorData(serialPort);
        //    }
        //    catch (TimeoutException) { }
        //}



        // ReadLine() is coming in this format:
        // Distance (cm): 30.00
        // Distance (inch): 30.00


        /*
         * Next steps after reading the sensor data?
         * send to kafka server
         * 
         * Kafka server is running successfully on WSL. The variable server is the IP + port (default 9092)
         * We need a producer and consumer from host machine side
         * Server is on WSL instance (the Kafka broker).
         * 
         * How to break the parts inside our program?
         * 
         * 1. Producer and consumer are separate classes to create and read events, respectively.
         * 2. Producer and consumer need a function that write and read, that main function will call when needed.
         * 3. Main function calls the write/read in a determined interval.
         * 4. The executable file should run everything at once, maybe we pass the interval as a CLI argument?
         * 
         * 
        */
    }

    public static void InitializeSensor(SerialPort serialObject, string port, int rate)
    {
        // Setting the SerialPort object
        serialObject.BaudRate = rate;
        serialObject.PortName = port;

    }

    // Read sensor data
    public static string ReadSensorData(SerialPort serialPort)
    {

        // Reads a line form the serial port and prints to screen
        string message = serialPort.ReadLine();
        Console.WriteLine(message);

        return message;

    }

}