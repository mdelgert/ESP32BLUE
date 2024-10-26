# Enable serial debugging
arduino-cli compile --fqbn esp32:esp32:esp32s3:CDCOnBoot=cdc Sketch2.ino

arduino-cli upload --port /dev/ttyACM1 --fqbn esp32:esp32:esp32s3 Sketch2.ino