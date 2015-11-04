using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrientatietestS21m
{
    abstract class Verkoop
    {
        public int Aantal { get; set; }
        public decimal Bedrag { get; private set; }
        public DateTime Tijdstip { get; set; }
        public abstract BTWTarief BTWTarief { get; }
        public abstract decimal Prijs { get; }

        public Verkoop(int aantal)
        {
            this.Aantal = aantal;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
