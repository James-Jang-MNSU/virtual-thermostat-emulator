using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThermostatEmulator
{
    public class Thermostat
    {
        // class variables
        private double currentTemperature;
        private double targetTemperature;
        private bool isHeating;

        private const double Deadband = 1.0; // 1.0 degree Celsius buffer
        private readonly Random _random = new Random(); // used for simulating temperature drift

        // gets and sets
        public double CurrentTemperature
        {
            get { return this.currentTemperature; }
            private set { this.currentTemperature = value; }
        }
        public double TargetTemperature
        {
            get { return this.targetTemperature; }
            set { this.targetTemperature = value; }
        }
        public bool IsHeating
        {
            get { return this.isHeating; }
            private set { this.isHeating = value; }
        }

        // constructors
        public Thermostat(double anInitialTemp, double aTargetTemp)
        {
            this.currentTemperature = anInitialTemp;
            this.targetTemperature = aTargetTemp;

            UpdateHVACState(); // automatically set's the hvac state
        }
        
        // class methods
        public void ReadCurrentTemp()
        {
            double randomChange = (_random.NextDouble()* 2.0 - 1.0) * 0.05; // a random number between -0.05 and +0.05, used to simulate the noise in temperature readings
            double hvacInfluence = 0.0; // the amount of influence the current hvac state has on the temperature

            if (this.IsHeating)
            {
                hvacInfluence = 0.2; // when heating is ON, the hvac influence acts as positive bias
            }
            else
            {
                hvacInfluence = -0.1; // when heating is OFF, the hvac influence acts as negative bias
            }
            this.CurrentTemperature += randomChange + hvacInfluence;
        }
        public void UpdateHVACState()
        {
            if (!this.IsHeating && this.CurrentTemperature < this.TargetTemperature - Thermostat.Deadband)
            {
                IsHeating = true;
            }
            else if (this.IsHeating && this.CurrentTemperature >= this.TargetTemperature)
            {
                IsHeating = false;
            }
        }
        public void RunControlCycle()
        {
            this.ReadCurrentTemp();
            this.UpdateHVACState();
        }
    }
}
