using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Vjezba5.Pages.Transakcije
{
    public class CreateModel : PageModel
    {
        public TransakcijeInfo transakcijeInfo = new TransakcijeInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            transakcijeInfo.platitelj = Request.Form["platitelj"];
            transakcijeInfo.primatelj = Request.Form["primatelj"];
            transakcijeInfo.iban = Request.Form["iban"];
            transakcijeInfo.model = Request.Form["model"];
            transakcijeInfo.valuta = Request.Form["valuta"];
            transakcijeInfo.iznos = Request.Form["iznos"];

            if (transakcijeInfo.platitelj.Length == 0 || transakcijeInfo.primatelj.Length == 0 ||
                transakcijeInfo.iban.Length == 0 || transakcijeInfo.iznos.Length == 0)
            {
                errorMessage = "All the fields are required";
                return;
            }

            //save the new client into the database
            try
            {
                String connectionString = "Data Source=.;Initial Catalog=vjezba5;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO uplatnica " +
                                 "(platitelj, primatelj, iban, model, valuta, iznos) VALUES " +
                                 "(@platitelj, @primatelj, @iban, @model, @valuta, @iznos);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@platitelj", transakcijeInfo.platitelj);
                        command.Parameters.AddWithValue("@primatelj", transakcijeInfo.primatelj);
                        command.Parameters.AddWithValue("@iban", transakcijeInfo.iban);
                        command.Parameters.AddWithValue("@model", transakcijeInfo.model);
                        command.Parameters.AddWithValue("@valuta", transakcijeInfo.valuta);
                        command.Parameters.AddWithValue("@iznos", transakcijeInfo.iznos);
           

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            transakcijeInfo.platitelj = ""; transakcijeInfo.primatelj = ""; transakcijeInfo.iban = ""; transakcijeInfo.model = "";
            transakcijeInfo.valuta=""; transakcijeInfo.iznos="";
            successMessage = "New Client Added Correctly";

            Response.Redirect("/Transakcije/Index");
        }
    }
}


