using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrientatietestS21m
{
    public abstract class Verhuur : IInkomsten
    {
        public int UrenVerhuurd { get; set; }

        private decimal bedrag;
        public decimal Bedrag
        {
            get
            {
                return PrijsPerUUr * UrenVerhuurd;
            }
            private set
            {
                bedrag = PrijsPerUUr * UrenVerhuurd;
            }            
        }
        public DateTime Tijdstip { get; set; }
        public abstract BTWTarief BTWTarief { get; }
        public abstract decimal PrijsPerUUr { get; }

        public Verhuur(DateTime tijdstip, int urenVerhuurd)
        {
            this.Tijdstip = tijdstip;
            this.UrenVerhuurd = urenVerhuurd;
        }

        public override string ToString()
        {
            return String.Format("{0}, {1} uren voor {2} totaal", Tijdstip, UrenVerhuurd, Bedrag);
        }
    }
}
