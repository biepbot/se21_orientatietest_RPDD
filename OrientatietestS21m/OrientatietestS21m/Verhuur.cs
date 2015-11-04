using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrientatietestS21m
{
    abstract class Verhuur
    {
        public int UrenVerhuurd { get; set; }
        public decimal Bedrag { get; private set; }
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
