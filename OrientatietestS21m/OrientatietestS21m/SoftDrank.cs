using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrientatietestS21m
{
    public class SoftDrank : Verkoop
    {
        private BTWTarief BTWtarief = BTWTarief.Laag;
        private decimal prijs = 1.50M;

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

        public SoftDrank(int aantal)
            : base(aantal)
        {

        }

        public override string ToString()
        {
            return "Softdrink: " + base.ToString();
        }
    }
}
