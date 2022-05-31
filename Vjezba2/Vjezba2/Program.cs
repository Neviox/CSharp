using System;
using System.IO;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
public class Transakcija
{
	[Serializable()]
	public class Hub
	{
		public string platitelj { get; set; }
		public string primatelj { get; set; }
		public string iban { get; set; }
		public int iznos { get; set; }
		public string model { get; set; }
		public string opis { get; set; }
		public DateTime vrijemeUplate { get; set; }

		public Hub() { }

		public Hub(string platitelj, string primatelj, string iban,
						  int iznos, string model, string opis, DateTime vrijemeUplate)
		{
			this.platitelj = platitelj;
			this.primatelj = primatelj;
			this.iban = iban;
			this.iznos = iznos;
			this.model = model;
			this.opis = opis;
			this.vrijemeUplate = vrijemeUplate;
		}


	}
	public static void Insert(List<Hub> uplatnica)
	{
		Console.WriteLine("Uneiste platitelja:");
		string platitelj = Console.ReadLine();

		Console.WriteLine("Primatelj:");
		string primatelj = Console.ReadLine();

		Console.WriteLine("Iban: ");
		string iban = Console.ReadLine();

		Console.WriteLine("Iznos:");
		int iznos = Convert.ToInt32(Console.ReadLine());

		Console.WriteLine("Model:");
		string model = Console.ReadLine();

		Console.WriteLine("Opis Placanja:");
		string opis = Console.ReadLine();

		DateTime vrijemeUplate = DateTime.Now;

		if (iban.Length == 21 && iban.Substring(0, 2) == "HR" && iznos > 0 && model.Substring(0, 2) == "HR")
		{
			uplatnica.Add(new Hub(platitelj, primatelj, iban, iznos, model, opis, vrijemeUplate));
		}
		else { Console.WriteLine("Krivo ispunjena uplatnica! Pokusajte ponovno"); } 
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

	public static void Deserialize()
		{
			try
			{
				using (Stream stream = File.Open("data.bin", FileMode.Open))
				{
					BinaryFormatter bin = new BinaryFormatter();

					var up2 = (List<Hub>)bin.Deserialize(stream);
					foreach (Hub uplatnica in up2)
					{
						Console.WriteLine("{0} -> {1} {2}kn , {3}",
							uplatnica.platitelj,
							uplatnica.primatelj,
							uplatnica.iznos,
							uplatnica.vrijemeUplate);
					}
				}
			}
			catch (IOException)
			{
			}

		}

	public static void Main()
	{

		while (true)
		{
			Console.WriteLine("1 = Nova uplatnica, 2 = Pregled");
			switch ((Console.ReadLine()))
			{
				case "1":
					List<Hub> upl = new List<Hub>();
                    Insert(upl);
					NewTransaction(upl);
					break;
				case "2":
					Deserialize();
					break;
				

			}
		}

	}
}