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
            adm.VoegToe(new Feestzaal(DateTime.Now, 1));
            Assert.IsNotNull(adm);
            Assert.IsNotNull(adm.ListVerhuren[0]);
        }

        [TestMethod]
        public void AdminVoegToe2()
        {
            Administratie adm = new Administratie();
            adm.VoegToe(new SoftDrank(5));
            Assert.IsNotNull(adm);
            Assert.IsNotNull(adm.ListVerkopen[0]);
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
            foreach (Verhuur i in adm.Overzicht(test, test.AddHours(500)))
            {
                if (!(i.Tijdstip > test && i.Tijdstip.AddHours(i.UrenVerhuurd) < test.AddHours(500)))
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
            adm.VoegToe(new SoftDrank(5));
            adm.VoegToe(new Sterkedrank(5));
            adm.VoegToe(new Thee(5));
            adm.VoegToe(new Thee(5));
            adm.VoegToe(new SoftDrank(5));
            adm.VoegToe(new Feestzaal(DateTime.Now, 1));
            adm.VoegToe(new Herberg(DateTime.Now.AddHours(15), 5));
            adm.VoegToe(new Binnenzaal(DateTime.Now.AddDays(12), 7));
            adm.VoegToe(new Binnenzaal(DateTime.Now.AddHours(4), 9));
            adm.VoegToe(new Feestzaal(DateTime.Now, 10));
            return adm;
        }
    }
}
