using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace labirintus_játék
{
    class Map
    {
        public char[,] map;
        public string text;
        public bool GameOver = false;
        public Jatekos p;

        public Map(string file, Jatekos p)
        {
            this.p = p;

            this.text = "";

            try
            {
                string[] lines = File.ReadAllLines(file);

                int sz = lines[0].Length;
                int m = lines.Length;

                char[,] BeolvasottTerkep = new char[sz, m];

                for (int i = 0; i < lines.Length; i++)
                {
                    for (int j = 0; j < lines[i].Length; j++)
                    {
                        char k = lines[i][j];
                        BeolvasottTerkep[i, j] = k;
                    }
                }

                this.map = BeolvasottTerkep;
            }
            catch(FileNotFoundException e)
            {
                Console.WriteLine("Hiba a fájl beolvasása közben!");
            }
        }

        public char[,] getMap()
        {
            return map;
        }

        public void Terkep()
        {
            int meret = this.map.GetLength(0);

            for (int sor = 0; sor < meret; sor++)
            {
                for (int oszlop = 0; oszlop < meret; oszlop++)
                {
                    Console.Write(this.map[sor, oszlop] + " ");
                }
                Console.WriteLine();
            }
        }

        public void Szoveg()
        {
            Console.WriteLine(this.text);
            this.text = "";
        }

        public int[] Pozicio()
        {
            int[] pos = { 0, 0 };

            int meret = this.map.GetLength(0);
            for (int sor = 0; sor < meret; sor++)
            {
                for (int oszlop = 0; oszlop < meret; oszlop++)
                {
                    if (this.map[sor, oszlop] == '@')
                    {
                        pos[0] = sor;
                        pos[1] = oszlop;
                    }
                }
            }

            return pos;
        }

        private void Move(int xx, int yy)
        {
            string[] RandomEllenNevek = { "gólem", "troll", "ork", "kobold" };
            string[] RandomFegyverNevek = { "rozsdás kard", "hegyes fadarab", "tompa fejsze", "konyhakés" };
            Random r = new Random();

            int[] pozicio = Pozicio();
            int x = pozicio[0];
            int y = pozicio[1];

            char KovetkezoMezo = map[x + xx, y + yy];

            try
            {
                if (KovetkezoMezo == '.')
                {
                    map[x + xx, y + yy] = '@';
                    map[x, y] = '.';
                }
                else if (KovetkezoMezo == 'X')
                {
                    this.text += "Kijutottál! Gratulálok!";
                    GameOver = true;
                }
                else if (KovetkezoMezo == 'L')
                {
                    Loot lada = new Loot(p);

                    if (r.Next(0, 101) > lada.FeltoresiNehezseg)
                    {
                        this.text += $"A ládában találsz {lada.ertek} pénzt és egy {lada.fegyver.nev}-t, ({lada.fegyver.dmg} sebzés), amivel lecseréled a jelenlegi fegyveredet.";
                        p.fegyver = lada.fegyver;
                        p.sebzes = p.SebzesSzamitas(p.fegyver, p.ero);
                        p.penz += lada.ertek;
                    }
                    else
                    {
                        this.text += "Nem sikerült feltörni a ládát.";
                    }

                    map[x + xx, y + yy] = '@';
                    map[x, y] = '.';
                }
                else if (KovetkezoMezo == 'E')
                {
                    Ellen ellenfel = new Ellen(RandomEllenNevek[r.Next(0, 4)], r.Next(0, 31), r.Next(0, 8), new Fegyver(r.Next(0, 7), RandomFegyverNevek[r.Next(0, 4)]));
                    if (fight(p, ellenfel))
                    {
                        map[x + xx, y + yy] = '@';
                        map[x, y] = '.';
                    }
                }
                else
                {
                    this.text += "Falba ütköztél!";
                }
            }
            catch(IndexOutOfRangeException e)
            {
            }
        }
        public void up()
        {
            Move(-1, 0);
        }
        public void down()
        {
            Move(1, 0);
        }
        public void right()
        {
            Move(0, 1);
        }
        public void left()
        {
            Move(0, -1);
        }
        public bool fight(Jatekos j, Ellen e) 
        {
            Random r = new Random();

            List<string> szoveg = e.Harc(j);

            foreach (var item in szoveg)
            {
                this.text += $"\n{item}";
            }

            if (szoveg[szoveg.Count() - 1] == "Végeztél ellenfeleddel.")
            {
                int RandomSzam = r.Next(10, 31);
                this.text += $"Szereztél {RandomSzam} pénzt.";
                j.penz += RandomSzam;
                return true;
            }
            else
            {
                this.text += "Megsérültél. (-10hp)";
                j.hp -= 10;
                return false;
            }
        }
    }
}
