using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace labirintus_játék
{
    class Fegyver
    {
        public int dmg;
        public string nev;

        public Fegyver(int s, string n)
        {
            this.dmg = s;
            this.nev = n;
        }
    }

    class Karakterek
    {
        public int hp;
        public int ero;
        public int[] sebzes;
        public int penz;
        public Fegyver fegyver;

        public int[] SebzesSzamitas(Fegyver f, int s)
        {
            int[] sebzes = { 0, 0 };

            sebzes[0] = (int)Math.Floor((f.dmg + s) * 0.95);
            sebzes[0] = (int)Math.Ceiling((f.dmg + s) * 1.05);

            return sebzes;
        }
    }

    class Jatekos : Karakterek
    {
        public Jatekos()
        {
            this.hp = 60;
            this.penz = 20;
            this.ero = 8;
            this.fegyver = new Fegyver(6, "elázott faág");
            this.sebzes = this.SebzesSzamitas(this.fegyver, this.ero);
        }
    }

    class Ellen : Karakterek
    {
        public string name;
        public char jelzo;

        public Ellen(string n, int h, int e, Fegyver f)
        {
            this.name = n;
            this.jelzo = this.name.ToUpper()[0];
            this.hp = 50 + h;
            this.ero = 6 + e;
            this.fegyver = f;
            this.sebzes = this.SebzesSzamitas(this.fegyver, this.ero);
        }

        public List<string> Harc(Jatekos j)
        {
            int HarcElottiHP = j.hp;

            List<string> szovegek = new List<string>();

            Random r = new Random();

            bool EllenfelTamad = false;

            while (this.hp > 0 && j.hp > 0)
            {
                szovegek.Add($"Egy {this.name} megtámadott téged.");

                int sebzes_j = r.Next(j.sebzes[1], j.sebzes[0] + 1);
                int sebzes_e = r.Next(this.sebzes[1], this.sebzes[0] + 1);

                if (EllenfelTamad == false)
                {
                    this.hp -= sebzes_j;
                    szovegek.Add($"Ellenfeledre sebeztél {sebzes_j} pontot. Maradék élete: {this.hp}");
                    if (this.hp <= 0)
                    {
                        this.hp = 0;
                        szovegek.Add("Végeztél ellenfeleddel.");
                    }
                    EllenfelTamad = !EllenfelTamad;
                }
                else
                {
                    j.hp -= sebzes_e;
                    szovegek.Add($"Ellenfeled {sebzes_e} pontot sebzett rád. Maradék életed: {j.hp}");
                    if (j.hp <= 0)
                    {
                        j.hp = 0;
                        szovegek.Add("Ellenfeled végzett veled.");
                    }
                    EllenfelTamad = !EllenfelTamad;
                }
            }

            j.hp = HarcElottiHP;

            return szovegek;
        }
    }
}
