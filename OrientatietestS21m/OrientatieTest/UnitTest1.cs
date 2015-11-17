using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrientatietestS21m;

namespace OrientatieTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void AdminVoegToe1()
        {
            Administratie adm = new Administratie();
            adm.VoegToe("Feestzaal", 1, DateTime.Now);
            Assert.IsNotNull(adm);
            Assert.IsNotNull(adm.ListAankopen[0]);
        }

        [TestMethod]
        public void AdminVoegToe2()
        {
            Administratie adm = new Administratie();
            adm.VoegToe("SoftDrank", 5);
            Assert.IsNotNull(adm);
            Assert.IsNotNull(adm.ListAankopen[0]);
        }

        [TestMethod]
        public void AdminBTWHoog()
        {
            Administratie adm = SetupSort();
            foreach (IInkomsten i in adm.Overzicht(BTWTarief.Hoog))
            {
                if (i.BTWTarief != BTWTarief.Hoog)
                {
                    Assert.Fail();
                    break;
                }
            }
        }

        [TestMethod]
        public void AdminBTWLaag()
        {
            Administratie adm = SetupSort();
            foreach (IInkomsten i in adm.Overzicht(BTWTarief.Laag))
            {
                if (i.BTWTarief != BTWTarief.Laag)
                {
                    Assert.Fail();
                    break;
                }
            }
        }

        [TestMethod]
        public void AdminBTWAlle()
        {
            Administratie adm = SetupSort();
            foreach (IInkomsten i in adm.AllOverzicht())
            {
                if (!adm.Overzicht(BTWTarief.Ongespecificeerd).Contains(i))
                {
                    Assert.Fail(); break;
                }
            }
        }

        [TestMethod]
        public void AdminBetweenDate()
        {
            Administratie adm = SetupSort();
            DateTime test = DateTime.Now;
            foreach (IInkomsten i in adm.Overzicht(test, test.AddHours(500), false))
            {
                if (!(i.Tijdstip > test && i.Tijdstip.AddHours(((Verhuur)i).UrenVerhuurd) < test.AddHours(500)))
                {
                    Assert.Fail();
                    break;
                }
            }
        }

        [TestMethod]
        public void AdminSortedonDate()
        {
            Administratie adm = SetupSort();
            IInkomsten a = null;
            IInkomsten b = null;
            foreach (IInkomsten i in adm.AllOverzicht())
            {
                a = i;
                if (a != null && b != null)
                {
                    if (a.Tijdstip > b.Tijdstip)
                    {
                        Assert.Fail();
                        break;
                    }
                }
                b = a;
            }
        }

        public Administratie SetupSort()
        {
            Administratie adm = new Administratie();
            adm.VoegToe("SoftDrank", 5);
            adm.VoegToe("Sterkedrank", 5);
            adm.VoegToe("Thee", 5);
            adm.VoegToe("Thee", 5);
            adm.VoegToe("SoftDrank", 5);
            adm.VoegToe("Feestzaal", 1, DateTime.Now);
            adm.VoegToe("Herberg", 5, DateTime.Now.AddHours(15));
            adm.VoegToe("Binnenzaal", 7, DateTime.Now.AddDays(12));
            adm.VoegToe("Binnenzaal", 9, DateTime.Now.AddDays(4));
            adm.VoegToe("Feestzaal", 10, DateTime.Now);
            return adm;
        }
    }
}
