# BLDC PCB Testing DataLogger

## 📌 Project Overview

**BLDC PCB Testing DataLogger** is a Windows-based application project developed to support testing, monitoring, and data logging for Brushless DC (BLDC) motor controller PCBs.

The project is organized as a Visual Studio solution and includes a SQL script and a folder related to Modbus device communication. It is intended to support PCB testing workflows, device communication, and the management of test-related information.

The application can serve as a PC-based interface between testing equipment and a data-logging system, depending on the configured hardware and implemented software functionality.

## 🎯 Project Objectives

- Support BLDC PCB testing operations.
- Communicate with compatible test devices.
- Collect and process testing data.
- Maintain test records for quality control.
- Support analysis of PCB testing results.
- Improve production testing traceability.

## ⚙️ Key Features

- **PCB Testing Support:** Windows application for BLDC PCB testing workflows.
- **Device Communication:** Includes a project folder related to Modbus device communication.
- **Data Logging:** Project structure intended for recording and managing testing information.
- **Database Integration:** Includes an SQL script for database-related configuration or operations.
- **Windows Desktop Interface:** Visual Studio solution for application development.
- **Expandable Design:** Can be extended with reporting, test history, and production analytics.

*The exact implemented features should be confirmed against the application source code.*

## 🔌 Communication Protocol

The repository includes a folder named `Try to Modbus device`, suggesting that Modbus communication is part of the project exploration. The exact protocol configuration must be verified from the source code.

### Modbus communication options

| Protocol | Communication interface | Typical use |
|---|---|---|
| Modbus RTU | Serial communication, commonly RS-485 | Communication with industrial controllers and test instruments |
| Modbus TCP | Ethernet / TCP/IP | Communication with network-connected devices |
| Custom Serial Protocol | COM port using a device-specific message format | Communication with proprietary test equipment |

**Important:** Do not assume that all three protocols are supported. Document only the protocol and interface actually used by the application.

### Typical Modbus RTU communication flow

1. Configure the serial port, baud rate, parity, data bits, and stop bits.
2. Set the correct Modbus slave/device address.
3. Send a supported Modbus request.
4. Read the response from the connected device.
5. Validate the response and process the register values.
6. Save the relevant testing information to the data logger.

Common Modbus function codes include:

- `03` — Read Holding Registers
- `04` — Read Input Registers
- `06` — Write Single Register
- `16` (`0x10`) — Write Multiple Registers

These are standard Modbus function codes, not confirmation that every function is implemented in this project.

## 🏭 Typical Testing Workflow

1. Start the BLDC PCB Testing DataLogger application.
2. Connect the PCB testing equipment or supported device.
3. Configure the communication settings.
4. Initiate the testing process.
5. Receive the available device data and test measurements.
6. Process the test information.
7. Store the relevant records for quality inspection and reporting.

## 🛠️ Technology Stack

| Technology | Purpose |
|---|---|
| VB.NET / .NET | Windows application development, subject to source verification |
| Windows Forms | Desktop user interface, subject to source verification |
| Visual Studio | Solution development and debugging |
| Modbus | Possible device communication protocol; exact variant to be verified |
| SQL | Database-related script included in the repository |

## 💻 Requirements

- Windows operating system
- Compatible Microsoft Visual Studio version
- Required .NET Framework or .NET runtime
- Compatible BLDC PCB testing hardware
- Device communication interface and drivers
- Database engine and configuration, if required
- Correct device communication parameters

## 🚀 Getting Started

### Clone the repository

```bash
git clone https://github.com/imKunalSharma/BLDC-PCB-Testing-DataLogger.git
```

### Open the solution

1. Open Visual Studio.
2. Open `Testing2.sln`.
3. Restore any required dependencies.
4. Review the communication code and database configuration.
5. Build the solution.
6. Connect the supported testing device and configure its communication settings before running hardware tests.

## 📊 Potential Enhancements

- Automatic PASS/FAIL result classification
- PCB serial number or barcode tracking
- Test history and searchable records
- CSV and Excel report generation
- Real-time device communication status
- Communication timeout and error logging
- Statistical analysis of PCB test results
- Integration with manufacturing execution systems (MES)

## 👨‍💻 Developer

**Kunal Sharma**

VB.NET | Windows Application Development | Industrial Automation | Device Communication | Database Integration

## 🔗 Repository

[BLDC-PCB-Testing-DataLogger – GitHub](https://github.com/imKunalSharma/BLDC-PCB-Testing-DataLogger)

## 📄 License

Check the repository for the applicable license and permissions before redistributing or reusing the source code.
