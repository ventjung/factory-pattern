using System;
using System.Collections.Generic;
using System.Text;

namespace FactoryPattern._1__Biasanya
{
    public interface Pecel
    {
    }

    public class Ayam : Pecel
    {

    }

    public class Bebek : Pecel
    {

    }

    public class Program
    {
        public static void Implementasi()
        {
            Pecel pecel;
            bool lagiKolesterol = false;

            if(lagiKolesterol == false)
            {
                pecel = new Bebek();
            }
            else
            {
                pecel = new Ayam();
            }
        }
    }
}
