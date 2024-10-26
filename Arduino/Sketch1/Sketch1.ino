#include <NimBLEDevice.h>

NimBLEServer* pServer = nullptr;
NimBLECharacteristic* pCharacteristic = nullptr;

#define SERVICE_UUID "4fafc201-1fb5-459e-8fcc-c5c9c331914b"
#define CHARACTERISTIC_UUID "beb5483e-36e1-4688-b7f5-ea07361b26a8"

bool deviceConnected = false;

// Callback class for connection events
class MyServerCallbacks : public NimBLEServerCallbacks {
    void onConnect(NimBLEServer* pServer) {
        deviceConnected = true;
        Serial.println("Device connected");
    }

    void onDisconnect(NimBLEServer* pServer) {
        deviceConnected = false;
        Serial.println("Device disconnected, restarting advertising");
        pServer->startAdvertising(); // Restart advertising
    }
};

void setup() {
    Serial.begin(115200);
    Serial.println("Setting up BLE server...");

    // Initialize NimBLE
    NimBLEDevice::init("ESP32_S3_BLE");
    NimBLEDevice::setPower(ESP_PWR_LVL_P9); // Optional: adjust transmit power

    // Create BLE server and set connection callbacks
    pServer = NimBLEDevice::createServer();
    pServer->setCallbacks(new MyServerCallbacks());

    // Create BLE service and characteristic
    NimBLEService* pService = pServer->createService(SERVICE_UUID);
    pCharacteristic = pService->createCharacteristic(
        CHARACTERISTIC_UUID,
        NIMBLE_PROPERTY::READ | NIMBLE_PROPERTY::WRITE | NIMBLE_PROPERTY::NOTIFY
    );

    // Set initial characteristic value
    pCharacteristic->setValue("Hello from ESP32!");

    // Start the service
    pService->start();

    // Start advertising
    NimBLEAdvertising* pAdvertising = NimBLEDevice::getAdvertising();
    pAdvertising->addServiceUUID(SERVICE_UUID);
    pAdvertising->setScanResponse(true); // Include additional advertising data
    pAdvertising->start();

    Serial.println("BLE server is running and advertising...");
}

void loop() {
    if (deviceConnected) {
        // Send a notification if connected
        pCharacteristic->setValue("Connected to ESP32");
        pCharacteristic->notify(); // Send notification to client
    }
    delay(1000); // Interval between notifications
}
