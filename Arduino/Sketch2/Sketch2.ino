void setup() {
    // Initialize serial communication at 115200 baud
    Serial.begin(115200);
    while (!Serial) {
            // Wait for serial port to connect
    }
    Serial.println("Starting serial output...");
}

void loop() {
    // Print a message to the serial monitor every second
    Serial.println("Hello from ESP32");
    delay(1000); // Wait for 1 second
}
