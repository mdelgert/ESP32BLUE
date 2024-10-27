
## Getting Started

### Prerequisites

- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/)
- [Arduino CLI](https://arduino.github.io/arduino-cli/0.19/installation/)

### Setting Up

1. **Clone the repository:**

    ```sh
    git clone https://github.com/yourusername/ESP32BLUE.git
    cd ESP32BLUE
    ```

2. **Open the solution in Visual Studio:**

    Open `Esp32Blue.sln` in Visual Studio 2022.

3. **Install Arduino Libraries:**

    ```sh
    arduino-cli lib search NimBLE
    arduino-cli lib install "NimBLE-Arduino"
    ```

### Building and Running

#### ConsoleBlue

1. Navigate to the `ConsoleBlue` directory.
2. Build and run the project:

    ```sh
    dotnet build
    dotnet run
    ```

#### MauiApp1

1. Navigate to the `MauiApp1` directory.
2. Build and run the project:

    ```sh
    dotnet build
    dotnet run
    ```

### Project Components

#### Arduino

Contains sketches for ESP32 devices. Refer to the [Arduino README](Arduino/README.MD) for more details.

#### ConsoleBlue

A .NET console application for Bluetooth communication. Refer to the [ConsoleBlue README](ConsoleBlue/README.MD) for more details.

#### MauiApp1

A .NET MAUI application for cross-platform development. Refer to the [MauiApp1 README](MauiApp1/README.MD) for more details.

## Contributing

Contributions are welcome! Please open an issue or submit a pull request.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.