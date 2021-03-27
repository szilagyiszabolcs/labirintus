using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace labirintus_játék
{
    class Loot
    {
        Random r = new Random();
        public int ertek;
        public Fegyver fegyver;
        public int FeltoresiNehezseg;

        public Loot(Jatekos j)
        {
            string[] RandomFegyverNevek = { "rozsdás kard", "görbe íj", "tompa fejsze", "konyhakés" };
            ertek = r.Next((int)Math.Floor(j.penz * 0.3), (int)Math.Floor(j.penz * 0.7));
            fegyver = new Fegyver(r.Next(j.fegyver.dmg, j.fegyver.dmg * 2), RandomFegyverNevek[r.Next(0, 4)]);
            FeltoresiNehezseg = r.Next(0, 101);
        }
    }
}
