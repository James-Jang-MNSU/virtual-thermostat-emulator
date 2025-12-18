using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using System.IO;

namespace ThermostatEmulator
{
    public partial class MainWindow : Window
    {
        // class variables
        private Thermostat thermostat;
        private DispatcherTimer? simulationTimer; // simulates the time flow

        // constructor
        public MainWindow()
        {
            InitializeComponent();

            // initialize thermostat with (anInitialTemp: 20, aTargetTemp: 22)
            thermostat = new Thermostat(20.0, 22.0);

            // setup the display immediately
            UpdateDisplay();
        }

        // methods
        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            if (simulationTimer == null) // timer has not been created yet, or has been destroyed... the simulation is OFF
            {
                
                simulationTimer = new DispatcherTimer(); // create the timer
                simulationTimer.Interval = TimeSpan.FromSeconds(0.1); // set speed (every 0.1 seconds)
                simulationTimer.Tick += SimulationTick; // connect the method to run
                simulationTimer.Start();
                btnStart.Content = "POWER OFF";
            }
            else
            {
                // stop logic
                simulationTimer.Stop();
                simulationTimer = null; // delete the timer object
                btnStart.Content = "POWER ON";
            }
        }
        private void SimulationTick(object sender, EventArgs e)
        {
            thermostat.RunControlCycle(); // run the physics/logic 
            UpdateDisplay();
            
            // broadcasting data
            try
            {
                // try to write the current temperature to the file named "status.txt"
                string statusData = thermostat.CurrentTemperature.ToString("F2");
                File.WriteAllText("status.txt", statusData);
            }
            catch (IOException)
            {
                // If we catch an IOException (File is busy), 
                // we just do nothing and try again next tick.
                // This prevents the crash.
            }
        }
        private void UpdateDisplay()
        {
            // update text
            txtCurrentTemp.Text = thermostat.CurrentTemperature.ToString("F1") + "°";
            txtTargetTemp.Text = thermostat.TargetTemperature.ToString("F0") + "°";

            // check if simulation is running
            if (simulationTimer == null || !simulationTimer.IsEnabled)
            {
                // if stopped, show "OFF" in Grey
                txtStatus.Text = "OFF";
                txtStatus.Foreground = Brushes.Gray;
                txtTargetTemp.Foreground = Brushes.Gray;
                return; // Stop here! Don't run the rest of the logic.
            }

            // if running, show the actual status colors
            if (thermostat.IsHeating)
            {
                txtStatus.Text = "HEATING";
                txtStatus.Foreground = Brushes.OrangeRed;
                txtTargetTemp.Foreground = Brushes.OrangeRed;
            }
            else
            {
                txtStatus.Text = "ECO";
                txtStatus.Foreground = Brushes.DeepSkyBlue;
                txtTargetTemp.Foreground = Brushes.White;
            }
        }
        private void btnWarmer_Click(object sender, RoutedEventArgs e)
        {
            thermostat.TargetTemperature += 1.0;
            UpdateDisplay();
        }
        private void btnCooler_Click(object sender, RoutedEventArgs e)
        {
            thermostat.TargetTemperature -= 1.0;
            UpdateDisplay();
        }
    }
}