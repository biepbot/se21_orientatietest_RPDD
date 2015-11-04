using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrientatietestS21m
{
    public class Herberg : Verhuur
    {
        private BTWTarief BTWtarief = BTWTarief.Hoog;
        private decimal prijsPerUUr = 30.00M;

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

        public Herberg(DateTime tijdstip, int urenVerhuurd)
            : base(tijdstip, urenVerhuurd)
        {

        }

        public override string ToString()
        {
            return "Herberg: " + base.ToString();
        }
    }
}
