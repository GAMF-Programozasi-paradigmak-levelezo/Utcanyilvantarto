using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtcaFelosztas
{
	internal class Telek
	{
		public bool PáratlanOldal { get; set; }
		public int HázSzám { get; set; }
		public double TelekSzélesség { get; set; }
		public char KerítésSzín { get; set; }
		public Telek(bool páratlanOldal, int házSzám, double telekSzélesség, char kerítésSzín)
		{
			PáratlanOldal = páratlanOldal;
			HázSzám = házSzám;
			TelekSzélesség = telekSzélesség;
			KerítésSzín = kerítésSzín;
		}
		public Telek() : this(false, 0, 0.0, '\0') { }
	}
}
