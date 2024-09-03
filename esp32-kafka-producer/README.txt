This project aims to get data from a sensor (HC-SR04) with ESP32, pass this data through an ETL process using Apache Spark and Kafka,
then present this data somehow with a visualization tool.



The communication protocol I'll use (at least in the beginning) will be UART -> Universal Asynchronous Receiver-Transmiter. This was chosen
for ease-of-purpose, because I can only connect a USB cable to the computer and read the sensor's data.
The main thing on this protocol is the baud rate. Since the communication is made with only one wire, the baud rate is a fundamental
parameter that will set the at which speed the data will be read/written. Both devices should be on this same rate.
Because of this only wire, data transmission is much slower, hence why in the future I will switch the protocol to I2C or SPI for parallelism.
Since this is a learning project, UART is fine. And we also don't need that much timing precision.

HC-SR04

How this sensor works? It uses sonar to determine the distance to an object. 

Two ultrasonic transimter and receivers are present
The first sonar sends a signal that the other one receives, and the time between
them is calculated.



TODOS:

ESP32

- Create the code that reads the sensor data, using Arduino IDE
- Send this data via Serial Port using System.IO.Ports.SerialPort class


Kafka

- Create local Kafka server on WSL instance
- Create Producer and Consumer in Visual Studio side
- Connect the console application to Kafka instance
- Create a topic and send data using the Producer instance with sensor data
- The consumer might be another console app