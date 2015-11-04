using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrientatietestS21m
{
     public abstract class Verkoop : IInkomsten
    {
        public int Aantal { get; set; }

        private decimal bedrag;
        public decimal Bedrag
        {
            get
            {
                return Prijs * Aantal;
            }
            private set
            {
                bedrag = Prijs * Aantal;
            }     
        }
        public DateTime Tijdstip { get; set; }
        public abstract BTWTarief BTWTarief { get; }
        public abstract decimal Prijs { get; }

        public Verkoop(int aantal)
        {
            this.Aantal = aantal;
        }

        public override string ToString()
        {
            return String.Format("{0}: {1} voor {2} totaal", Tijdstip, Aantal, Bedrag);
        }
    }
}
