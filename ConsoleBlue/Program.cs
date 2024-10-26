using System;
using System.Text;
using System.Threading.Tasks;
using Plugin.BLE;
using Plugin.BLE.Abstractions.Contracts;

namespace ConsoleBle
{
    class Program
    {
        private static IDevice _espDevice;
        private static ICharacteristic _writeCharacteristic;
        private static readonly Guid ServiceUuid = new Guid("4fafc201-1fb5-459e-8fcc-c5c9c331914b");         // Replace with your ESP32's service UUID
        private static readonly Guid CharacteristicUuid = new Guid("beb5483e-36e1-4688-b7f5-ea07361b26a8"); // Replace with your ESP32's characteristic UUID

        static async Task Main(string[] args)
        {
            var ble = CrossBluetoothLE.Current;
            var adapter = CrossBluetoothLE.Current.Adapter;

            ble.StateChanged += (s, e) => Console.WriteLine($"Bluetooth state changed: {e.NewState}");
            adapter.DeviceDiscovered += async (s, a) =>
            {
                if (a.Device.Name == "ESP32_S3") // Replace "ESP32_S3" with your ESP32's advertised name
                {
                    _espDevice = a.Device;
                    await ConnectAndSendMessages(adapter);
                }
            };

            await adapter.StartScanningForDevicesAsync();

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
            await adapter.StopScanningForDevicesAsync();
        }

        private static async Task ConnectAndSendMessages(IAdapter adapter)
        {
            await adapter.ConnectToDeviceAsync(_espDevice);
            Console.WriteLine("Connected to ESP32");

            var service = await _espDevice.GetServiceAsync(ServiceUuid);
            _writeCharacteristic = await service.GetCharacteristicAsync(CharacteristicUuid);

            if (_writeCharacteristic == null)
            {
                Console.WriteLine("Characteristic not found!");
                return;
            }

            while (true)
            {
                var message = Encoding.UTF8.GetBytes("hello");
                await _writeCharacteristic.WriteAsync(message);
                Console.WriteLine("Sent: hello");
                await Task.Delay(1000); // Send every second
            }
        }
    }
}
