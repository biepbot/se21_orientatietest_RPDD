using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrientatietestS21m
{
    public class Sportartiekelen : Verkoop
    {
        private static BTWTarief BTWtarief = BTWTarief.Laag;
        private static decimal prijs = 15.00M;

        override public BTWTarief BTWTarief
        {
            get
            {
                return BTWtarief;
            }
        }
        override public decimal Prijs
        {
            get
            {
                return prijs;
            }
        }

        public Sportartiekelen(int aantal)
            : base(aantal)
        {

        }

        public override string ToString()
        {
            return "Sportartiekelen: " + base.ToString();
        }
    }
}
