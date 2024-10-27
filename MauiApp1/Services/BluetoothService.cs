using System;
using System.Text;
using System.Threading.Tasks;
using Plugin.BLE;
using Plugin.BLE.Abstractions.Contracts;

namespace MauiApp1.Services
{
    public class BluetoothService
    {
        private IDevice _espDevice;
        private ICharacteristic _writeCharacteristic;
        private readonly IAdapter _adapter;
        private int _counter = 1;

        private static readonly Guid ServiceUuid = new Guid("4fafc201-1fb5-459e-8fcc-c5c9c331914b"); // Replace with your ESP32's service UUID
        private static readonly Guid CharacteristicUuid = new Guid("beb5483e-36e1-4688-b7f5-ea07361b26a8"); // Replace with your ESP32's characteristic UUID

        public BluetoothService()
        {
            var ble = CrossBluetoothLE.Current;
            _adapter = CrossBluetoothLE.Current.Adapter;
            ble.StateChanged += (s, e) => Console.WriteLine($"Bluetooth state changed: {e.NewState}");
            _adapter.DeviceDiscovered += async (s, a) =>
            {
                if (a.Device.Name == "ESP32_S3") // Replace "ESP32_S3" with your ESP32's advertised name
                {
                    _espDevice = a.Device;
                    await ConnectToDeviceAsync();
                }
            };
        }

        public async Task StartScanningAsync()
        {
            await _adapter.StartScanningForDevicesAsync();
        }

        public async Task StopScanningAsync()
        {
            await _adapter.StopScanningForDevicesAsync();
        }

        private async Task ConnectToDeviceAsync()
        {
            await _adapter.ConnectToDeviceAsync(_espDevice);
            Console.WriteLine("Connected to ESP32");

            var service = await _espDevice.GetServiceAsync(ServiceUuid);
            _writeCharacteristic = await service.GetCharacteristicAsync(CharacteristicUuid);

            if (_writeCharacteristic == null)
            {
                Console.WriteLine("Characteristic not found!");
                return;
            }
        }

        public async Task SendMessageAsync()
        {
            if (_writeCharacteristic == null)
            {
                Console.WriteLine("Not connected to characteristic.");
                return;
            }

            var message = Encoding.UTF8.GetBytes($"hello {_counter}");
            await _writeCharacteristic.WriteAsync(message);
            Console.WriteLine($"Sent: hello {_counter}");
            _counter++; // Increment the counter for the next message
        }
    }
}
