# Utcanyilvántartó program
A program célja egy utca telekadatainak feldolgozása és elemzése. Az adatok a kerites.txt fájlban találhatók, ahol minden sor egy telek adatait tartalmazza:

- Oldal (1 = páratlan, 0 = páros)
- Szélesség (méterben)
- Kerítés színe (egy karakter, pl. A, B, : vagy \#)

A program feladatai:

1. Beolvassa az adatokat a fájlból és ellenõrzi az érvényességüket.
2. Kiírja, hány telek adata került beolvasásra.
3. Megállapítja, hogy az utolsó telek melyik oldalon van, és mi a házszáma.
4. Megkeresi az elsõ olyan két szomszédos páratlan oldali telket, amelyek kerítésének színe megegyezik, és nem : vagy #.
5. Bekér egy házszámot, majd kiírja a hozzá tartozó kerítés színét, valamint megkeresi az elõtte és utána lévõ házszámokat, és javaslatot tesz a kerítés színére.
6. Kiírja két egymás alatti sorba a páratlan oldal elsõ hét telkének a kerítésdszínét és alá a házszámokat.

A program ellenõrzi, hogy az adatok megfelelnek az elõírásoknak (legalább 3 telek mindkét oldalon, egyik oldal hossza sem haladhatja meg az 1000 métert). Hibás adat esetén hibaüzenetet ad és leáll.

A feladat részletes leírása a feladat.pdf állományban található. Az adatok a kerites.txt állományban találhatóak