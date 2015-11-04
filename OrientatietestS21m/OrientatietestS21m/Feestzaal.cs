using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrientatietestS21m
{
    class Feestzaal : Verhuur
    {
        private BTWTarief BTWtarief = BTWTarief.Ongespecificeerd;
        private decimal prijsPerUUr = 45.50M;

        override public BTWTarief BTWTarief
        {
            get
            {
                return BTWtarief;
            }
        }
        override public decimal PrijsPerUUr
        {
            get
            {
                return prijsPerUUr;
            }
        }

        public Feestzaal(DateTime tijdstip, int urenVerhuurd) : base(tijdstip, urenVerhuurd)
        {

        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
