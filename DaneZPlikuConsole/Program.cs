using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaneZPlikuConsole
{
    class Program
    {
        static string TablicaDoString<T>(T[][] tab)
        {
            string wynik = "";
            for (int i = 0; i < tab.Length; i++)
            {
                for (int j = 0; j < tab[i].Length; j++)
                {
                    wynik += tab[i][j].ToString() + " ";
                }
                wynik = wynik.Trim() + Environment.NewLine;
            }

            return wynik;
        }

        static double StringToDouble(string liczba)
        {
            double wynik; liczba = liczba.Trim();
            if (!double.TryParse(liczba.Replace(',', '.'), out wynik) && !double.TryParse(liczba.Replace('.', ','), out wynik))
                throw new Exception("Nie udało się skonwertować liczby do double");

            return wynik;
        }


        static int StringToInt(string liczba)
        {
            int wynik;
            if (!int.TryParse(liczba.Trim(), out wynik))
                throw new Exception("Nie udało się skonwertować liczby do int");

            return wynik;
        }

        static string[][] StringToTablica(string sciezkaDoPliku)
        {
            string trescPliku = System.IO.File.ReadAllText(sciezkaDoPliku); // wczytujemy treść pliku do zmiennej
            string[] wiersze = trescPliku.Trim().Split(new char[] { '\n' }); // treść pliku dzielimy wg znaku końca linii, dzięki czemu otrzymamy każdy wiersz w oddzielnej komórce tablicy
            string[][] wczytaneDane = new string[wiersze.Length][];   // Tworzymy zmienną, która będzie przechowywała wczytane dane. Tablica będzie miała tyle wierszy ile wierszy było z wczytanego poliku

            for (int i = 0; i < wiersze.Length; i++)
            {
                string wiersz = wiersze[i].Trim();     // przypisuję i-ty element tablicy do zmiennej wiersz
                string[] cyfry = wiersz.Split(new char[] { ' ' });   // dzielimy wiersz po znaku spacji, dzięki czemu otrzymamy tablicę cyfry, w której każda oddzielna komórka to czyfra z wiersza
                wczytaneDane[i] = new string[cyfry.Length];    // Do tablicy w której będą dane finalne dokładamy wiersz w postaci tablicy integerów tak długą jak długa jest tablica cyfry, czyli tyle ile było cyfr w jednym wierszu
                for (int j = 0; j < cyfry.Length; j++)
                {
                    string cyfra = cyfry[j].Trim(); // przypisuję j-tą cyfrę do zmiennej cyfra
                    wczytaneDane[i][j] = cyfra; 
                }
            }
            return wczytaneDane;
        }

        static void Main(string[] args)
        {
            string sciezkaDoSystemuTestowego = @"SystemTestowy.txt";
            string sciezkaDoSystemuTreningowego = @"SystemTreningowy.txt";

            string[][] systemTestowy = StringToTablica(sciezkaDoSystemuTestowego);
            string[][] systemTreningowy = StringToTablica(sciezkaDoSystemuTreningowego);

            Console.WriteLine("Dane systemu testowego");
            string wynikTestowy = TablicaDoString(systemTestowy);
            Console.Write(wynikTestowy);

            Console.WriteLine("");
            Console.WriteLine("Dane systemu treningowego");

            string wynikTreningowy = TablicaDoString(systemTreningowy);
            Console.Write(wynikTreningowy);

            string liczba;
            // Przykład konwertowania string do double
            liczba = "1.4";
            double dliczba = StringToDouble(liczba);


            // Przykład konwertowania string do int
            liczba = "1";
            int iLiczba = StringToInt(liczba);

            /****************** Miejsce na rozwiązanie *********************************/

            /*
            int[,] tabTreningowy = new int[systemTreningowy.Length, systemTreningowy[0].Length];
            for (int i = 0; i < systemTreningowy.Length; i++)
            {
                for (int j = 0; j < systemTreningowy[0].Length; j++)
                {
                    tabTreningowy[i, j] = int.Parse(systemTreningowy[i][j]);
                }
            }
            int[,] tabTestowy = new int[systemTestowy.Length, systemTestowy[0].Length];
            for (int i = 0; i < systemTestowy.Length; i++)
            {
                for (int j = 0; j < systemTestowy[0].Length; j++)
                {
                    tabTestowy[i, j] = int.Parse(systemTestowy[i][j]);
                }
            }
            */

            int k = 2;

            int[,] tab2x = new int[systemTestowy.GetLength(0) + systemTreningowy.GetLength(0) , 5];


            int ilei = 0;
            

            for (int i = 0; i < systemTestowy.Length; i++)
            {
                ilei++;
                for (int j = 0; j < systemTestowy[0].Length; j++)
                {
                    tab2x[i, j] = int.Parse(systemTestowy[i][j]);
                }
            }
            for (int i = ilei; i < systemTreningowy.Length + systemTreningowy.Length; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    tab2x[i, j] = int.Parse(systemTreningowy[i-ilei][j]);
                }
            }
            for (int i = 0; i < tab2x.GetLength(0); i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Console.WriteLine(tab2x[i, j]);
                }
            }

            double[,] predykcja = new double[systemTestowy.GetLength(0), 8];

            for(int m = 0; m < tab2x.GetLength(0) / 2; m++)
            {
                for (int i = 0; i < tab2x.GetLength(0) / 2; i++)
                {
                    for( int j = 0; j < tab2x.GetLength(1); j += 4)
                    {
                        predykcja[i, j] = Math.Sqrt((Math.Pow(tab2x[m,j]-tab2x[i+8, j] ,2)) + (Math.Pow(tab2x[m, j+1] - tab2x[i + 8, j+1], 2)) + (Math.Pow(tab2x[m, j+2] - tab2x[i + 8, j+2], 2)) + (Math.Pow(tab2x[m, j+3] - tab2x[i + 8, j+3], 2)));
                    }
                }
            }

            for (int i = 0; i < predykcja.GetLength(0); i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Console.WriteLine(predykcja[i, j]);
                }
            }








            /****************** Koniec miejsca na rozwiązanie ********************************/
            Console.ReadKey();
        }
    }
}
