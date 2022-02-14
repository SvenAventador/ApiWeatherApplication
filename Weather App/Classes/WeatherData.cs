using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Weather_App.Classes
{
    //public class WeatherData
    //{
        
    //}

    public class Temperature
    {
        public double temp;
        public double humidity;
    }
    public class Sun
    {
        public double sunrise;
        public double sunset;
    }

    public class WeatherResponse
    {
        public Temperature main;
        public Sun sys;
        public string name;
        public double timezone;
    }

}
