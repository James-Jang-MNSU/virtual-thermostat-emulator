# Virtual Thermostat Emulator (HIL Simulation)

## 📌 Project Overview
This project is a software simulation of a Smart Thermostat designed to demonstrate **Hardware-in-the-Loop (HIL)** testing concepts. It consists of a WPF-based emulator that mimics physical device physics (thermal inertia, heating lag, sensor noise) and a separate "Watchdog" console application that monitors the device for critical failures.

## 🚀 Key Features
* **Physics Simulation:** accurately models room temperature decay and HVAC heating curves.
* **Realistic Sensor Noise:** Implements random drift to mimic real-world sensor fluctuations using C#.
* **Automated Watchdog:** A separate process monitors the thermostat's output via file I/O and triggers an alarm if safety thresholds (>100°C) are exceeded.
* **Fault Tolerance:** Implements robust I/O handling to manage race conditions between the read/write processes.

## 🛠️ Tech Stack
* **Language:** C# (.NET 8.0)
* **Frontend:** WPF (Windows Presentation Foundation) / XAML
* **Architecture:** Event-Driven, Multi-Process Communication
* **Tools:** Visual Studio 2022

## ⚙️ How to Run
1.  Clone the repository.
2.  Open `ThermostatEmulator.sln` in Visual Studio.
3.  Right-click the Solution in Solution Explorer -> **Set Startup Projects**.
4.  Select **Multiple Startup Projects**.
5.  Set both `ThermostatEmulator` and `WatchdogScript` to **Start**.
6.  Press **F5** to run the simulation.

## 📄 License
This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.