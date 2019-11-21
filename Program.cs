using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;

namespace GuessTheWord
{
    class Program
    {
        static void Main(string[] args)
        {
            celaIgra();
        }
        public static void celaIgra() { 
            Random rnd = new Random();
            string[] reciZaPogadjanje = { "aligator", "helikopter", "cudoviste", "digital", "slon", "zirafa" };
            char[] pokusaji = new char[30];

            int i = 0, brBd, nasumican = rnd.Next(reciZaPogadjanje.Length);
            string izbor, pogodak, jednaRec = reciZaPogadjanje[nasumican];
            Console.WriteLine("**************************************\n     Dobrodosli u igru pogodi rec\n**************************************\n");
            Console.WriteLine(" 1) Igraj\n 2) Pravila\n 3) Ispisi ScoreBoard\n 4) Iskljuci igru");
            izbor = Console.ReadLine();
            switch (izbor)
            {
                case "1":
                    Console.WriteLine("Usli ste u igru:");
                    Tezina(out brBd);
                    do
                    {
                        if (i == 0) 
                        {
                            foreach (char a in jednaRec) 
                            {
                                Console.Write("*");
                            }
                            Console.WriteLine();
                        }
                        pogodak = Console.ReadLine();
                        if (pogodak.Length > 1)
                        {
                            if (jednaRec == pogodak)
                            {
                                brBd -= 2;
                                Pobeda(jednaRec,brBd);                           
                            }
                            else
                            {
                                brBd -= 2;
                                Console.WriteLine("Uneta rec nije ista kao trazena rec.");
                            }
                            Console.WriteLine($"Trenutan broj bodova je: {brBd}");
                        }
                        else
                        {
                            try
                            {
                                pokusaji[i] = char.ToLower(char.Parse(pogodak));
                            }
                            catch (Exception e)
                            {
                                GasenjeIgrice();
                            }
                            i++;
                            brBd--;
                            proveraSadrzajaSlova(pokusaji, jednaRec, brBd);
                            for (int j = 0; j < jednaRec.Length; j++)
                            {
                                for (int k = 0; k < i; k++)
                                {
                                    if (pokusaji[k] == jednaRec[j])
                                    {
                                        Console.Write(pokusaji[k]);
                                        break;
                                    }
                                    else if (pokusaji[i] != jednaRec[j] && k == i - 1)
                                    {
                                        Console.Write("*");
                                    }
                                }
                            }
                            Console.WriteLine($"\nTrenutan broj bodova je: {brBd}");
                        }
                    } while (brBd>0);
                    ponovo();
                    break;
                case "2":
                    Console.WriteLine(" *********\n  PRAVILA\n *********\n U pocetku birate tezinu igre, odnosno broj pokusaja.\n Svaki pokusaj nosi 5 bodova, a vi pocinjete sa 5,10,15 ili 30 pokusaja respektivno.\n Pogadjanje reci kosta 2 pokusaja dok pogadjanje slova kosta 1.\n U svakom momentu mozete ugasiti igricu pritiskom na bilo koje dugme osim slova, brojeva ili znakova.\n Igricu jos mozete ugasiti i odabirom \"Iskljuci igru\" iz menija.");
                    break;
                case "3":
                    ispisScoreBoarda();
                    break;
                case "4":
                    GasenjeIgrice();
                    break;
                default:
                    GasenjeIgrice();
                    break;
            }
           
            Console.ReadKey();
        }
        public static void Tezina(out int brBodova) 
        {
            Console.WriteLine("Molim vas izaberite tezinu igranja:\n 1) Hard  (5 pokusaja)\n 2) Medium(10 pokusaja)\n 3) Easy  (15 pokusaja)\n 4) Baby  (30 pokusaja)");
            string tezina =Console.ReadLine();
            switch (tezina) 
            {
                case "1":
                    brBodova = 5;
                    break;
                case "2":
                    brBodova = 10;
                    break;
                case "3":
                    brBodova = 15;
                    break;
                case "4":
                    brBodova = 30;
                    break;
                default:
                    brBodova = 0;
                    GasenjeIgrice();
                    break;

            }
        }
        public static void GasenjeIgrice()
        {
            Console.WriteLine(" Niste uneli odgovarajuci karakter.\n Igrica ce se ugasiti nakon pritiska na dugme.");
            Console.ReadKey();
            Environment.Exit(1);
        }
        public static void proveraSadrzajaSlova(char[] uneti,string rec,int bod) 
        {
            int brojac=1;
            foreach (char c in rec)
            {
                foreach(char v in uneti) 
                {
                    if (c==v)
                    {
                        brojac++;
                    }
                }
            }
            if (brojac > rec.Length)
            {
                Pobeda(rec,bod);
            }
        }
        public static void Pobeda( string jednaRec,int bod) 
        {
            Console.WriteLine($"CESTITAMO POGODILI STE REC!!!\n REC JE: {jednaRec}");
            Bodovi(bod,out int bodovi);
            RangLista(bodovi);
            ponovo();
        }
        public static void Bodovi(int pokusaji, out int bod) 
        {
            bod = pokusaji * 5;
            Console.WriteLine($"Pobedili ste sa {bod} bodova! Cestitamo!");
        }
        public static void ponovo()
        {
            Console.WriteLine("Da li zelite da pokusate ponovo?\n 1) Da\n 2) Ne");
            string ponovni = Console.ReadLine();
            if (ponovni.Equals("1"))
            {
                Console.Clear();
                Console.WriteLine("Igra ce poceti od pocetka.");
                celaIgra();
            }
            else if(ponovni.Equals("2")) 
            {
                Console.WriteLine("Igra ce se ugasiti.");
                Environment.Exit(1);
            }
            else
            {
                GasenjeIgrice();
            }
        }
        public static void RangLista(int brojBodova)
        {
            Console.WriteLine("Da li zelite da sacuvate svoj skor?\n 1) Da\n 2) Ne");
            char skor = Console.ReadKey().KeyChar;
            switch (skor)
            {
                case '1':
                    Console.Write("Unesite 3 slova svog naziva: ");
                    string imeIgraca="";
                    for (int i = 0; i < 3; i++)
                    {
                        char lokalniChar = Console.ReadKey().KeyChar;
                        imeIgraca += lokalniChar.ToString();
                    }
                    Console.WriteLine();
                    (string ime, int brBd) zaListu = (imeIgraca, brojBodova);
                    manipulacijaRanga(zaListu);
                    ispisScoreBoarda();
                    break;
                case '2':
                    Console.WriteLine("Igra ce se iskljuciti.");
                    Environment.Exit(1);
                    break;
                default:
                    GasenjeIgrice();
                    break;
            }
        }
    public static void manipulacijaRanga((string ime, int brBd) zaList)
        {
                var dodavanjeUListu = (from line in File.ReadLines("scoreboard.txt")
                                       let values = line.Split(',')
                                       select Tuple.Create(values[0], int.Parse(values[1]))).ToList();
            dodavanjeUListu.Add(zaList.ToTuple());
            dodavanjeUListu.Sort();
            File.Delete("scoreboard.txt");
            for (int i = 0; i < dodavanjeUListu.Count; i++)
            {
                File.AppendAllLines("scoreboard.txt",new string[] { dodavanjeUListu[i].Item1 + "," + dodavanjeUListu[i].Item2  });
            }
        }
       public static void ispisScoreBoarda()
        {
           string skorovi= File.ReadAllText("scoreboard.txt");
            Console.WriteLine(skorovi);
        }
    }
}
