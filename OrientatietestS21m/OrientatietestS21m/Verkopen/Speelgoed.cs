using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrientatietestS21m
{
    public class Speelgoed : Verkoop
    {
        private static BTWTarief BTWtarief = BTWTarief.Laag;
        private decimal prijs = 4.99M;

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

        public Speelgoed(int aantal)
            : base(aantal)
        {

        }

        public override string ToString()
        {
            return "Speelgoed: " + base.ToString();
        }
    }
}
