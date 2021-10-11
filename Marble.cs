using System;
using System.Collections.Generic;
using System.Text;

namespace TwelveMarbles
{
    public class Marble
    {
        public string Id { get; set; }
        public double Weight { get; set; } 
        public bool Anomaly { get; set; } = false;
        public string AnomalyMsg { get; set; } = string.Empty;
      


    }
}
