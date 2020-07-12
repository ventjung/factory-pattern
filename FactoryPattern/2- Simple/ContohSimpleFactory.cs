using System;
using System.Collections.Generic;
using System.Text;

namespace FactoryPattern._2__Simple
{
    public abstract class Pecel
    {
        public void Goreng() // proses gimmick tambahan buat analogi
        {

        }

        public void Sajikan() // proses gimmick tambahan buat analogi
        {

        }
    }

    public class Ayam : Pecel
    {

    }

    public class Bebek : Pecel
    {

    }

    public class PecelFactory
    {
        public Pecel BikinPecel(string TipePecel) // disini proses pembuatan object pecel sesuai pilihan terjadi
        {
            Pecel pecel = null;

            if(TipePecel == "ayam")
            {
                pecel = new Ayam();
            }
            else if(TipePecel == "bebek")
            {
                pecel = new Bebek();
            }

            return pecel;
        }
    }

    public class WarungPecel // di contoh simple belum ada cabang jakarta dan tegal ya, masih satu aja
    {
        PecelFactory factory;

        public WarungPecel(PecelFactory Factory)
        {
            this.factory = Factory;
        }

        public Pecel PesenPecel(string TipePecel)
        {
            Pecel pecel;

            pecel = factory.BikinPecel(TipePecel); // encapsulate object creation, simpelnya sih kita bisa milih mau bikin apa, dan tau jadi aja

            pecel.Goreng();
            pecel.Sajikan();

            return pecel;
        }
    }

    public class Program
    {
        public static void Implementasi()
        {
            WarungPecel warungPecel = new WarungPecel(new PecelFactory());

            var ayam = warungPecel.PesenPecel("ayam");
            var bebek = warungPecel.PesenPecel("bebek");
        }
    }
}
