# KafkaEsp32

 A Kafka instance that reads data from an ESP32 microcontroller with a sensor

# Goals of this repository

This is an educational project that might have some business value as it tries to simulate a real-world application/scenario where streaming sensor data and process it is needed.

So, the overall goals are:
 - Learn Kafka and Streaming Pipelines
 - Learn some low-level stuff with the ESP32 board
 - Practice Python and C# programming
 - Practice systems design
 - Learn how to secure a full system and data
 - Learn how to wrap a full application in Docker

This project aims to get data from a sensor (HC-SR04) with ESP32, pass this data through an ETL process using Apache Kafka,
then present this data somehow with a visualization tool.


The communication protocol for the microcontroller communication I'll use (at least in the beginning) will be UART -> Universal Asynchronous Receiver-Transmiter. This was chosen
for ease-of-purpose, because I can only connect a USB cable to the computer and read the sensor's data.

The main thing on this protocol is the baud rate. Since the communication is made with only one wire, the baud rate is a fundamental
parameter that will set the at which speed the data will be read/written. Both devices should be on this same rate.

Because of this only wire, data transmission is much slower, hence why in the future I will switch the protocol to I2C or SPI for parallelism.
Since this is a learning project, UART is fine. And we also don't need that much timing precision.

# What you'll need

## Apache Kafka

Download and install at [the Official Quickstart guide](https://kafka.apache.org/quickstart). It is as simple as extracting the package and running a few scripts.
I'll be running my Kafka cluster in a WSL instance, so if you don't have it yet, [check the Microsoft Docs](https://learn.microsoft.com/en-us/windows/wsl/install). It is also as simple as running a command.

## ESP32 board (or another microcontroller like Arduino Uno) and HC-SR04 sensor

You can easily find the hardware needed on Amazon, they are very cheap.

[What is the HC-SR04 sensor](https://randomnerdtutorials.com/complete-guide-for-ultrasonic-sensor-hc-sr04/)

[What is Arduino](https://docs.arduino.cc/learn/starting-guide/whats-arduino/)

## Arduino IDE or PlatformIO for VScode

This is to setup the ESP32 code, a C/C++ script that you can upload to your board with the official Arduino IDE or a VScode extension called platformIO.

[Arduino IDE](https://www.arduino.cc/en/software)

[PlatformIO](https://docs.platformio.org/en/latest/integration/ide/vscode.html)

Both are great choices but they require some configuration for the ESP32 board. You can find some references here (Random Nerd Tutorials is an excellent site for Arduino-related stuff):

[PlatformIO ESP32 setup](https://randomnerdtutorials.com/vs-code-platformio-ide-esp32-esp8266-arduino/)

[Arduino IDE ESP32 setup](https://randomnerdtutorials.com/installing-the-esp32-board-in-arduino-ide-windows-instructions/)

Also, the code I have used (copied) for my ESP32 board is here: https://randomnerdtutorials.com/esp32-hc-sr04-ultrasonic-arduino/

## (Optional) Microsoft Visual Studio

Technically you don't **need** to install Visual Studio, I know it is a heavy program (you can easily surpass 40GB of space) but it's a great tool. You can achieve the same functionality in VScode with some extensions, but in my
experience it was not 100% reliable so I've chosen Visual Studio.

[Visual Studio Community Download](https://visualstudio.microsoft.com/pt-br/downloads/)

When installing Visual Studio, don't forget to select the **.NET development package**.

[Getting started with C# in Visual Studio Code](https://code.visualstudio.com/Docs/languages/csharp)


# Overall Architecture

This is subject to change as I'll add/remove things, but it's a great starting point.

![esp32-kafka-design](https://github.com/user-attachments/assets/95af858a-0dd2-4756-b161-06255b3bc0df)


# HC-SR04 sensor

How this sensor works? It uses sonar to determine the distance to an object. 

Two ultrasonic transmiter and receivers are present.
The first sonar sends a signal that the other one receives, and the time between
them is calculated.

# Break everything!

I very much enjoy Cybersecurity. After the entire application is finished and the Security practices are done, the goal is to break it and steal the data!

# TODOS:

**ESP32**

- Create the code that reads the sensor data, using Arduino IDE
- Send this data via Serial Port using *System.IO.Ports.SerialPort* class


**Kafka**

- Create local Kafka server on WSL instance
- Create Producer and Consumer in Visual Studio side
- Connect the console application to Kafka instance
- Create a topic and send data using the Producer instance with sensor data
- The consumer might be another console app
