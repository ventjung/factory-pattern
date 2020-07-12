using System;
using System.Collections.Generic;
using System.Text;

namespace FactoryPattern._4__Abstract_Factory
{
    public class Bahan
    {
        public string Nama { get; set; }
    }

    public class AyamKampung : Bahan
    {
        public AyamKampung()
        {
            Nama = "Ayam Kampung";
        }
    }

    public class AyamPejantan : Bahan
    {
        public AyamPejantan()
        {
            Nama = "Ayam Pejantan";
        }
    }

    public class BebekKampung : Bahan
    {
        public BebekKampung()
        {
            Nama = "Bebek Kampung";
        }
    }

    public class BebekPejantan : Bahan
    {
        public BebekPejantan()
        {
            Nama = "Bebek Pejantan";
        }
    }

    public abstract class BahanFactory
    {
        public string TipePecel { get; set; }
        
        public BahanFactory(string TipePecel)
        {
            this.TipePecel = TipePecel;
        }

        public abstract Bahan BikinBahan(); 
    }

    public class BahanJakartaFactory : BahanFactory
    {
        // inget kalo constructor itu adanya per class ya, jadi walaupun abstract classnya punya constructor
        // anaknya tetep mesti kita definisiin dan implement base nya
        public BahanJakartaFactory(string TipePecel) : base(TipePecel) { } 

        public override Bahan BikinBahan()
        {
            if(TipePecel == "ayam")
            {
                return new AyamPejantan();
            }
            else if(TipePecel == "bebek")
            {
                return new BebekPejantan();
            }
            else
            {
                return null;
            }
        }
    }

    public class BahanTegalFactory : BahanFactory
    {
        // inget kalo constructor itu adanya per class ya, jadi walaupun abstract classnya punya constructor
        // anaknya tetep mesti kita definisiin dan implement base nya
        public BahanTegalFactory(string TipePecel) : base(TipePecel) { }

        public override Bahan BikinBahan()
        {
            if (TipePecel == "ayam")
            {
                return new AyamKampung();
            }
            else if (TipePecel == "bebek")
            {
                return new BebekKampung();
            }
            else
            {
                return null;
            }
        }
    }

    public abstract class Pecel
    {
        public string Nama { get; set; }
        public Bahan Bahan { get; set; } // si pecel ga mau pusingin bahannya apa (mau ayam kampung atau ayam pejantan), pokonya selama bahan

        public void Goreng() // proses gimmick tambahan buat analogi
        {
            Console.WriteLine("Lagi ngegoreng " + Nama + " pake bahan " + Bahan.Nama); // superclass yang ga tau nama dan bahan apa yang akan digoreng
        }

        public void Sajikan() // proses gimmick tambahan buat analogi
        {
            Console.WriteLine("Selamat menikmati " + Nama); // superclass yang ga tau nama apa yang akan digoreng
        }
    }

    public class PecelAyamJakarta : Pecel
    {
        BahanFactory bahanFactory;

        public PecelAyamJakarta(BahanFactory bahanFactory)
        {
            this.bahanFactory = bahanFactory;
            
            Nama = "Pecel Ayam Jakarta";
            Bahan = bahanFactory.BikinBahan();
        }
    }

    public class PecelBebekJakarta : Pecel
    {
        BahanFactory bahanFactory;

        public PecelBebekJakarta(BahanFactory bahanFactory)
        {
            this.bahanFactory = bahanFactory;

            Nama = "Pecel Bebek Jakarta";
            Bahan = bahanFactory.BikinBahan();
        }
    }

    public class PecelAyamTegal : Pecel
    {
        BahanFactory bahanFactory;

        public PecelAyamTegal(BahanFactory bahanFactory)
        {
            this.bahanFactory = bahanFactory;

            Nama = "Pecel Ayam Tegal";
            Bahan = bahanFactory.BikinBahan();
        }
    }

    public class PecelBebekTegal : Pecel
    {
        BahanFactory bahanFactory;

        public PecelBebekTegal(BahanFactory bahanFactory)
        {
            this.bahanFactory = bahanFactory;

            Nama = "Pecel Bebek Tegal";
            Bahan = bahanFactory.BikinBahan();
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
            BahanFactory bahanFactory = new BahanJakartaFactory(TipePecel);

            if (TipePecel == "ayam")
            {
                return new PecelAyamJakarta(bahanFactory);
            }
            else if (TipePecel == "bebek")
            {
                return new PecelBebekJakarta(bahanFactory);
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
            BahanFactory bahanFactory = new BahanTegalFactory(TipePecel);

            if (TipePecel == "ayam")
            {
                return new PecelAyamTegal(bahanFactory);
            }
            else if (TipePecel == "bebek")
            {
                return new PecelBebekTegal(bahanFactory);
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
