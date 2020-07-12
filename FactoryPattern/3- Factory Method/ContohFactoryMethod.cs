using FactoryPattern._1__Biasanya;
using System;
using System.Collections.Generic;
using System.Text;

namespace FactoryPattern._3__Factory_Method
{
    public abstract class Pecel
    {
        public string Nama { get; set; }
        public string Bahan { get; set; }

        public void Goreng() // proses gimmick tambahan buat analogi
        {
            Console.WriteLine("Lagi ngegoreng " + Nama + " pake bahan " + Bahan); // superclass yang ga tau nama dan bahan apa yang akan digoreng
        }

        public void Sajikan() // proses gimmick tambahan buat analogi
        {
            Console.WriteLine("Selamat menikmati " + Nama); // superclass yang ga tau nama apa yang akan digoreng
        }
    }

    public class PecelAyamJakarta : Pecel
    {
        public PecelAyamJakarta()
        {
            Nama = "Pecel Ayam Jakarta";
            Bahan = "Ayam Pejantan";
        }
    }

    public class PecelBebekJakarta : Pecel
    {
        public PecelBebekJakarta()
        {
            Nama = "Pecel Bebek Jakarta";
            Bahan = "Bebek Pejantan";
        }
    }

    public class ProductAyamTegal : Pecel
    {
        public ProductAyamTegal()
        {
            Nama = "Pecel Ayam Tegal";
            Bahan = "Ayam Kampung";
        }
    }

    public class ProductBebekTegal : Pecel
    {
        public ProductBebekTegal()
        {
            Nama = "Pecel Bebek Tegal";
            Bahan = "Bebek Kampung";
        }
    }

    public abstract class WarungPecel // sekarang warung pecel jadi abstract, biar bisa handle cabang, jakarta dan tegal
    {
        public WarungPecel()
        {
        }

        public Pecel PesenPecel(string TipePecel)
        {
            Pecel pecel;

            pecel = BikinPecel(TipePecel); // disini manggil abstract factory method

            // kalau creation object pecelnya dibikin disini, dibilangnya dependant class
            // dan prinsip Dependency Inversion bilang kalau kita mesti depend upon abstraction
            // dan sebisa mungkin ga depend ke concrete class, thats why kita pecah jadi warung jakarta
            // dan warung tegal (dimana mereka handle sendiri penciptaan pecel mereka)

            pecel.Goreng();
            pecel.Sajikan();

            return pecel;
        }
        protected abstract Pecel BikinPecel(string TipePecel); // sekarang factory methodnya bikin pecel jadi abstract

    }

    public class WarungPecelJakarta : WarungPecel
    {
        protected override Pecel BikinPecel(string TipePecel)
        {
            if(TipePecel == "ayam")
            {
                return new PecelAyamJakarta();
            }
            else if(TipePecel == "bebek")
            {
                return new PecelBebekJakarta();
            }
            else
            {
                return null;
            }
        }
    }

    public class WarungPecelTegal : WarungPecel
    {
        protected override Pecel BikinPecel(string TipePecel)
        {
            if (TipePecel == "ayam")
            {
                return new ProductAyamTegal();
            }
            else if (TipePecel == "bebek")
            {
                return new ProductBebekTegal();
            }
            else
            {
                return null;
            }
        }
    }

    public class Program
    {
        public static void Implementasi()
        {
            WarungPecelJakarta pecelJakarta = new WarungPecelJakarta();
            WarungPecelTegal pecelTegal = new WarungPecelTegal();

            var ayamJakarta = pecelJakarta.PesenPecel("ayam");
            var ayamTegal = pecelTegal.PesenPecel("bebek");
        }
    }
}
