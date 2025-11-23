/* # Utcanyilvántartó program
A program célja egy utca telekadatainak feldolgozása és elemzése. Az adatok a kerites.txt fájlban találhatók, ahol minden sor egy telek adatait tartalmazza:

- Oldal (1 = páratlan, 0 = páros)
- Szélesség (méterben)
- Kerítés színe (egy karakter, pl. A, B, : vagy #)

A program feladatai:

1. Beolvassa az adatokat a fájlból és ellenőrzi az érvényességüket.
2. Kiírja, hány telek adata került beolvasásra.
3. Megállapítja, hogy az utolsó telek melyik oldalon van, és mi a házszáma.
4. Megkeresi az első olyan két szomszédos páratlan oldali telket, amelyek kerítésének színe megegyezik, és nem : vagy #.
5. Bekér egy házszámot, majd kiírja a hozzá tartozó kerítés színét, valamint megkeresi az előtte és utána lévő házszámokat, és javaslatot tesz a kerítés színére.
6. Kiírja két egymás alatti sorba a páratlan oldal első hét telkének a kerítésdszínét és alá a házszámokat.

A program ellenőrzi, hogy az adatok megfelelnek az előírásoknak (legalább 3 telek mindkét oldalon, egyik oldal hossza sem haladhatja meg az 1000 métert). Hibás adat esetén hibaüzenetet ad és leáll.

A feladat részletes leírása a feladat.pdf állományban található. Az adatok a kerites.txt állományban találhatóak.*/

namespace UtcaFelosztas
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Utcanyilvántartó");

			// Lista létrehozása a telkek tárolására
			var Telkek = new List<Telek>();

			// 1. Feladat: Adatok beolvasása fájlból
			Console.WriteLine("1. Feladat: Adatok beolvasása");
			if (!Beolvas("kerites.txt", Telkek))
			{
				Console.WriteLine("Hiba a fájl beolvasásakor!");
				return; // Program leáll, ha hiba van
			}

			// 2. Feladat: Beolvasott telkek számának kiírása
			Console.WriteLine($"Sikeres beolvasás! {Telkek.Count} telek adatai kerültek beolvasásra.");

			// 3. Feladat: Utolsó telek oldala és házszáma
			Console.WriteLine("3. Feladat");
			var UtolsóTelekOLdala = Telkek[^1].PáratlanOldal ? "páratlan" : "páros";
			Console.WriteLine($"Az utolsó telek a {UtolsóTelekOLdala} oldalon van.");
			var UtolsóHázSzám = Telkek[^1].HázSzám;
			Console.WriteLine($"Az utolsó telek házszáma: {UtolsóHázSzám}");

			// 4. Feladat: Szomszédos páratlan telkek azonos kerítésszínnel
			Console.WriteLine("4. Feladat");
			var PáratlanTelkek = Telkek.Where(t => t.PáratlanOldal).ToList();
			for (int i = 1; i < PáratlanTelkek.Count; i++)
			{
				// Ellenőrizzük, hogy két egymás melletti telek kerítésszíne megegyezik
				if (PáratlanTelkek[i].KerítésSzín == PáratlanTelkek[i - 1].KerítésSzín &&
						PáratlanTelkek[i].KerítésSzín != ':' &&
						PáratlanTelkek[i].KerítésSzín != '#')
				{
					Console.WriteLine($"Kerítésszín egyezést találtam. Házszámok: {PáratlanTelkek[i - 1].HázSzám},{PáratlanTelkek[i].HázSzám} Kerítés színe: {PáratlanTelkek[i - 1].KerítésSzín}");
					break; // Első egyezés után kilépünk
				}
			}

			// 5. Feladat: Házszám bekérése, és kerítésszín kiírása
			Console.WriteLine("5. Feladat");
			bool siker = true;
			int házSzám = 0;
			do
			{
				Console.Write("Kérem a telek házszámát: ");
				string? beolvasottHázSzám = Console.ReadLine();
				if (beolvasottHázSzám == null) siker = false;
				else
				{
					if (!int.TryParse(beolvasottHázSzám, out int hsz))
						siker = false;
					else
						házSzám = hsz;
				}
			} while (!siker);

			// Megkeressük a telek kerítésszínét
			var KerítésSzín = Telkek.First(t => t.HázSzám == házSzám).KerítésSzín;
			Console.WriteLine($"A {házSzám} házszámú telek kerítésének színe: {KerítésSzín}");

			int ElőtteSzám = Math.Max((házSzám%2==0?2:1),házSzám-2);
			int UtánaSzám = Telkek.Find(t => t.HázSzám == házSzám + 2) == null ? házSzám : házSzám + 2;
			List<char> KerítésSzínek = new List<char>();
			for (char i = 'A'; i <= 'Z'; i++)
				KerítésSzínek.Add(i);
			var AktuálisTelek = Telkek.Find(t=>t.HázSzám==házSzám);
			if (AktuálisTelek != null)
				KerítésSzínek.Remove(AktuálisTelek.KerítésSzín);
			var ElőtteTelek = Telkek.Find(t => t.HázSzám == házSzám);
			if (ElőtteTelek != null)
				KerítésSzínek.Remove(ElőtteTelek.KerítésSzín);
			var UtánaTelek = Telkek.Find(t => t.HázSzám == házSzám);
			if (UtánaTelek != null)
				KerítésSzínek.Remove(UtánaTelek.KerítésSzín);
			Random rnd = new Random();
			var ÚjKerítésSzín = KerítésSzínek[rnd.Next(KerítésSzínek.Count)];
			if (ElőtteTelek != null && UtánaTelek!=null)
				Console.WriteLine($"Az előtte és utána levő telkek kerítésszínei: {ElőtteTelek.KerítésSzín}, {UtánaTelek.KerítésSzín}");
			Console.WriteLine($"A kerítés javasolt színe: {ÚjKerítésSzín}");

			// 6. feladat
			Console.WriteLine("6. feladat");
			// A páratlan oldalon levő telkek listája
			PáratlanTelkek = Telkek.Where(t => t.PáratlanOldal).ToList();
			// Két üres sztring a kiírni kívánt két sor tartalmának tárolására
			string s1 = "", s2 = "";
			for (int i = 0; i < 7 && i < PáratlanTelkek.Count; i++)
			{
				var t = PáratlanTelkek[i];
				// Minden telek esetében annyiszor helyezzü el a sztringben a kerítésszín kódját ahány méter széles a telek
				s1 += new string(t.KerítésSzín,(int)t.TelekSzélesség);
				// A telek méterben kifejezett szélességének megfelelő mezőszélességen balra igazítva elhelyezzük a telek házszámát  
				s2 += t.HázSzám.ToString().PadRight((int)t.TelekSzélesség);
			}
			Console.WriteLine($"A páratlan oldal:\n{s1}\n{s2}");

		}

		/// <summary>
		/// Beolvassa a telkek adatait a megadott fájlból.
		/// Ellenőrzi az adatok helyességét, és feltölti a listát.
		/// </summary>
		private static bool Beolvas(string fnev, List<Telek> telkek)
		{
			// Fájl létezésének ellenőrzése
			if (!File.Exists(fnev))
			{
				Console.WriteLine("A fájl nem található!");
				return false;
			}
			// Adatok beolvasása
			var sorok = File.ReadAllLines(fnev);
			// Mivel mindkét oldalon kell legyen legalább három telek, ezért összesen legalább 6 sornak kell lennie
			if (sorok.Length < 6)
			{
				Console.WriteLine("Nincs elég adat a fájlban!");
				return false;
			}
			// Lista ürítése
			telkek.Clear();
			// Telkek száma a páros és páratlan oldalakon
			// 0 = páros, 1 = páratlan
			int[] TelkekSzáma = [0, 0];
			// Összes oldalhossz a páros és páratlan oldalakon
			double[] ÖsszOldalHosszak = [0.0, 0.0];
			// Utolsó házszámok a páros és páratlan oldalakon
			// Kezdetben 0 és -1, mert az első telek után 2-vel növeljük
			int[] UtolsóHázszámok = [0, -1];

			foreach (var sor in sorok)
			{ // Sor feldolgozása
				var adatok = sor.Split(' ');
				if (adatok.Length != 3)
				{
					Console.WriteLine("Hibás adat a fájlban!");
					return false;
				}
				// Telek objektum létrehozása
				var telek = new Telek();
				// Oldal meghatározása (1 = páratlan)
				telek.PáratlanOldal = adatok[0] == "1";
				// Az adott oldal telkeinek számának növelése
				TelkekSzáma[telek.PáratlanOldal ? 1 : 0]++;
				// Az adott oldal utolsó házszámának növelése és hozzárendelése
				UtolsóHázszámok[telek.PáratlanOldal ? 1 : 0] += 2;
				telek.HázSzám = UtolsóHázszámok[telek.PáratlanOldal ? 1 : 0];
				// Kerítés színe (első karakter)
				telek.KerítésSzín = adatok[2][0];
				// Telek szélessége
				if (!double.TryParse(adatok[1], out double szélesség))
				{
					Console.WriteLine("Hibás adat a fájlban!");
					return false;
				}
				telek.TelekSzélesség = szélesség;
				// Az adott oldal összoldalhosszának növelése az aktuális telek szélességével
				ÖsszOldalHosszak[telek.PáratlanOldal ? 1 : 0] += szélesség;
				// Telek hozzáadása a listához
				telkek.Add(telek);
			}

			// Ellenőrzések: legalább 3 telek oldalanként, max. 1000 m oldalhossz
			if (TelkekSzáma[0] < 3 || TelkekSzáma[1] < 3)
			{
				Console.WriteLine($"Nincs elég telek az egyik oldalon! (Páros:{TelkekSzáma[0]},Páratlan:{TelkekSzáma[1]})");
				return false;
			}
			if (ÖsszOldalHosszak[0] > 1000 || ÖsszOldalHosszak[1] > 1000)
			{
				Console.WriteLine($"Az egyik oldal hossza meghaladja az 1000 métert! (Páros:{ÖsszOldalHosszak[0]},Páratlan:{ÖsszOldalHosszak[1]})");
				return false;
			}
			return true;
		}
	}
}
