using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrientatietestS21m
{
    public class Theater : Verhuur
    {
        private static BTWTarief BTWtarief = BTWTarief.Hoog;
        private decimal prijsPerUUr = 300.00M;

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

        public Theater(DateTime tijdstip, int urenVerhuurd)
            : base(tijdstip, urenVerhuurd)
        {

        }

        public override string ToString()
        {
            return "Theater: " + base.ToString();
        }
    }
}
