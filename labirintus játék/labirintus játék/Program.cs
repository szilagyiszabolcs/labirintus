using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace labirintus_játék
{
    class Program
    {
        static void Main(string[] args)
        {
            Jatekos j = new Jatekos();

            Map m = new Map("palya.txt", j);

            m.text += "w: fel \na:bal \ns: le \nd: jobb \nh: help\ni: statok\n";

            bool jatek = true;
            m.Terkep();
            m.Szoveg();
            while (jatek)
            {
                char parancs = Console.ReadKey(true).KeyChar;

                Console.Clear();
                switch (parancs)
                {
                    case 'w': m.up(); break;
                    case 'a': m.left(); break;
                    case 's': m.down(); break;
                    case 'd': m.right(); break;
                    case 'h': m.text += "w: fel \na:bal \ns: le \nd: jobb \nh: help\ni: statok\n"; break;
                    case 'i': m.text += $"hp: {j.hp}\nerő: {j.ero}\npénz: {j.penz}\nfegyver: {j.fegyver.nev}\nfegyver sebzése: {j.fegyver.dmg}";  break;
                    default: m.text += "Nincs ilyen parancs!"; break;
                }

                if (m.GameOver)
                {
                    m.Szoveg();
                    jatek = false;
                }
                else
                {
                    m.Terkep();
                    m.Szoveg();
                }

                if (j.hp <= 0)
                {
                    Console.Clear();
                    Console.WriteLine("Meghaltál. Nem tudtál kijutni a labirintusból.");
                    jatek = false;
                }
            }

            Console.ReadKey();
        }
    }
}
