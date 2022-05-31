using System;
using System.IO;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Collections.Generic;

namespace Vjezba3Wpf.Modeli
{
	public class Transakcije
	{
		public static List<Transakcije.Hub> transakcije { get; set; }

		[Serializable()]
		public class Hub
		{
			public string platitelj { get; set; }
			public string primatelj { get; set; }
			public string iban { get; set; }
			public int iznos { get; set; }
			public Valuta valuta { get; set; }
			public string model { get; set; }
			public DateTime vrijemeUplate { get; set; }

			public enum Valuta
			{
				euro ,
				kuna ,
				dollar
			}

			public Hub() { }

			public Hub(string platitelj, string primatelj, string iban,
							  int iznos, string model, Valuta valuta, string opis, DateTime vrijemeUplate)
			{
				this.platitelj = platitelj;
				this.primatelj = primatelj;
				this.iban = iban;
				this.iznos = iznos;
				this.model = model;
				this.valuta = valuta;
				this.vrijemeUplate = vrijemeUplate;
			}


		}

		public static void NewTransaction(List<Hub> uplatnica)
		{

			try
			{
				using (Stream stream = File.Open("data.bin", FileMode.Create))
				{
					BinaryFormatter bin = new BinaryFormatter();
					bin.Serialize(stream, uplatnica);
				}
			}
			catch (IOException)
			{
			}
		}

	}
}