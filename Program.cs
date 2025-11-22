
namespace UtcaFelosztas
{

	internal class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Utcanyilvántartó");
			var Telkek=new List<Telek>();
			// 1. Feladat
			Console.WriteLine("1. Feladat: Adatok beolvasása");
			if (!Beolvas("kerites.txt",Telkek))
			{
				Console.WriteLine("Hiba a fájl beolvasásakor!");
				return;
			}
			Console.WriteLine($"Sikeres beolvasás! {Telkek.Count} telek adatai kerültek beolvasásra.");
		}

		private static bool Beolvas(string fnev, List<Telek> telkek)
		{
			if (!File.Exists(fnev))
			{
				Console.WriteLine("A fájl nem található!");
				return false;
			}
			var sorok=File.ReadAllLines(fnev);
			if (sorok.Length<6)
			{
				Console.WriteLine("Nincs elég adat a fájlban!");
				return false;
			}
			telkek.Clear();
			int[] TelkekSzáma = [0, 0];
			double[] ÖsszOldalHosszak = [0.0, 0.0];
			int[] UtolsóHázszámok = [0, -1];
			foreach(var sor in sorok)
			{
				var telek = new Telek();
				var adatok = sor.Split(' ');
				if(adatok.Length!=3)
				{
					Console.WriteLine("Hibás adat a fájlban!");
					return false;
				}
				telek.PáratlanOldal = adatok[0]=="1";
				TelkekSzáma[telek.PáratlanOldal ? 1 : 0]++;
				UtolsóHázszámok[telek.PáratlanOldal ? 1 : 0]+=2;
				telek.HázSzám = UtolsóHázszámok[telek.PáratlanOldal ? 1 : 0];
				telek.KerítésSzín = adatok[2][0];// első karakter
				if (!double.TryParse(adatok[1], out double szélesség))
				{
					Console.WriteLine("Hibás adat a fájlban!");
					return false;
				}
				telek.TelekSzélesség = szélesség;
				ÖsszOldalHosszak[telek.PáratlanOldal ? 1 : 0]+=szélesség;
				telkek.Add(telek);
			}
			if (TelkekSzáma[0]<3 || TelkekSzáma[1]<3)
			{
				Console.WriteLine($"Nincs elég telek az egyik oldalon! (Páros:{TelkekSzáma[0]},Páratlan:{TelkekSzáma[1]})");
				return false;
			}
			if (ÖsszOldalHosszak[0]>1000 || ÖsszOldalHosszak[1]>1000)
			{
				Console.WriteLine($"Az egyik oldal hossza meghaladja az 1000 métert! (Páros:{ÖsszOldalHosszak[0]},Páratlan:{ÖsszOldalHosszak[1]})");
				return false;
			}
			return true;
		}
	}
}
