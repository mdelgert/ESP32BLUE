using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using InTheHand.Net.Bluetooth;
using InTheHand.Net.Sockets;
using System.Linq; // Add this using directive

namespace ConsoleBlue
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string deviceName = "ESP32"; // Replace with your device's name
            string message = "Hello ESP32";

            try
            {
                // Discover devices
                BluetoothClient client = new BluetoothClient();
                var devices = client.DiscoverDevices().ToArray(); // Convert to array

                // Find the ESP32 device
                BluetoothDeviceInfo esp32Device = null;
                foreach (var device in devices)
                {
                    if (device.DeviceName == deviceName)
                    {
                        esp32Device = device;
                        break;
                    }
                }

                if (esp32Device == null)
                {
                    Console.WriteLine("ESP32 device not found.");
                    return;
                }

                // Connect to the device
                client.Connect(esp32Device.DeviceAddress, BluetoothService.SerialPort);

                // Send the message
                NetworkStream stream = client.GetStream();
                byte[] buffer = Encoding.ASCII.GetBytes(message);
                await stream.WriteAsync(buffer, 0, buffer.Length);
                Console.WriteLine("Message sent to ESP32.");

                // Close the connection
                stream.Close();
                client.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
