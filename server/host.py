import subprocess
import os

# This script is to get the WSL instance IP and pass it to the Kafka server, initializing it.
# listeners=PLAINTEXT://172.18.145.39:9092

# Getting the WSL IP address
def get_wsl_ip():
    # Run the hostname -I command
    #subprocess.run(["hostname", "-I"], shell=True, capture_output=True)
    #subprocess.Popen(["hostname", "-I"])
    ip = subprocess.check_output("hostname -I", shell=True).decode().strip()
    return ip


# Write WSl IP to kafka server
def write_kafka_server_ip(ip_address, port):
    # Path to server.properties file
    config_path = '/home/pelegolas-linux/wsl_dev/downloads/kafka_2.13-3.8.0/config/server.properties'
    #target_line = 'listeners=PLAINTEXT://'
    # Open the config file and search for the line containing our desired config
    with open(config_path, 'r+') as file_opened:
        # Reading lines until desired one appears
        file_lines = file_opened.readlines()

    # Loop the file until it finds a line starting with target_line
    new_lines = []
    for line in file_lines:
        if line.startswith("listeners="):
            # Replace the targeted line with updated ip
            new_lines.append(f"listeners=PLAINTEXT://{ip_address}:{port}\n")
        else:
            new_lines.append(line)

    # Write the new content to original file
    with open(config_path, 'w') as file:
        file.writelines(new_lines)


# Run kafka server
def run_kafka_server():
    kafka_server_path = '/home/pelegolas-linux/wsl_dev/downloads/kafka_2.13-3.8.0'
    #kafka_server_run_cmd = 'bin/kafka-server-start.sh config/server.properties'
    # Change working directory to kafka downloaded
    os.chdir(kafka_server_path)

    # Run server command
    subprocess.Popen(["bin/kafka-server-start.sh", "config/server.properties"])


if "__main__":
    ip = get_wsl_ip()
    write_kafka_server_ip(ip, 9092)
    run_kafka_server()