using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Vjezba5.Pages.Transakcije
{
    public class IndexModel : PageModel
    {
        public List<TransakcijeInfo> listTransakcije = new List<TransakcijeInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=.;Initial Catalog=vjezba5;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        String sql = "SELECT * FROM uplatnica";
                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {

                                TransakcijeInfo transakcijeInfo = new TransakcijeInfo();
                                transakcijeInfo.id =""+ reader.GetInt32(0);
                                transakcijeInfo.platitelj = reader.GetString(1);
                                transakcijeInfo.primatelj = reader.GetString(2);
                                transakcijeInfo.iban = reader.GetString(3);
                                transakcijeInfo.model = reader.GetString(4);
                                transakcijeInfo.iznos =""+ reader.GetInt32(5);
                                transakcijeInfo.valuta = reader.GetString(6);
                                transakcijeInfo.vrijemeUplate = reader.GetDateTime(7).ToString();

                                listTransakcije.Add(transakcijeInfo);
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
    
        public class TransakcijeInfo
        {
            public String? id;
            public String? platitelj;
            public String? primatelj;
            public String? iznos;
            public String? valuta;
            public String? model;
            public String? iban;
            public String? vrijemeUplate;
        }
}
