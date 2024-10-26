/*

using System;
using System.Text;
using System.Threading.Tasks;
using Plugin.BLE;
using Plugin.BLE.Abstractions.Contracts;

namespace ConsoleBle
{
    class Program
    {
        private static readonly Guid ServiceUuid = new Guid("4fafc201-1fb5-459e-8fcc-c5c9c331914b");
        private static readonly Guid CharacteristicUuid = new Guid("beb5483e-36e1-4688-b7f5-ea07361b26a8");

        static async Task Main(string[] args)
        {
            var ble = CrossBluetoothLE.Current;
            var adapter = CrossBluetoothLE.Current.Adapter;

            adapter.DeviceDiscovered += (s, a) =>
            {
                Console.WriteLine($"Discovered device: {a.Device.Name} ({a.Device.Id})");
            };

            Console.WriteLine("Starting scan for BLE devices...");
            await adapter.StartScanningForDevicesAsync();

            var device = await FindDeviceAsync(adapter, "ESP32_S3_BLE");

            if (device == null)
            {
                Console.WriteLine("Device not found.");
                return;
            }

            Console.WriteLine($"Connecting to device: {device.Name}");
            await adapter.ConnectToDeviceAsync(device);

            var service = await device.GetServiceAsync(ServiceUuid);
            var characteristic = await service.GetCharacteristicAsync(CharacteristicUuid);

            // Corrected ReadAsync usage with tuple deconstruction
            var (data, resultCode) = await characteristic.ReadAsync();
            if (resultCode == 0) // Check for successful read; resultCode == 0 typically indicates success
            {
                Console.WriteLine("Initial value: " + Encoding.UTF8.GetString(data));
            }
            else
            {
                Console.WriteLine($"Read failed with result code: {resultCode}");
            }

            // Set up characteristic notification
            characteristic.ValueUpdated += (s, e) =>
            {
                var updatedValue = Encoding.UTF8.GetString(e.Characteristic.Value);
                Console.WriteLine("Notification received: " + updatedValue);
            };
            await characteristic.StartUpdatesAsync();

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();

            await characteristic.StopUpdatesAsync();
            await adapter.DisconnectDeviceAsync(device);
        }

        private static async Task<IDevice> FindDeviceAsync(IAdapter adapter, string deviceName)
        {
            IDevice foundDevice = null;
            adapter.DeviceDiscovered += (s, a) =>
            {
                if (a.Device.Name == deviceName)
                {
                    foundDevice = a.Device;
                }
            };

            await adapter.StartScanningForDevicesAsync();
            await Task.Delay(5000);
            await adapter.StopScanningForDevicesAsync();

            return foundDevice;
        }
    }
}

*/