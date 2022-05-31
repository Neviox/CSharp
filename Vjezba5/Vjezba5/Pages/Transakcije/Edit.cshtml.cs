using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Vjezba5.Pages.Transakcije
{
    public class EditModel : PageModel
    {
        public TransakcijeInfo transakcijeInfo = new TransakcijeInfo();
        public String errorMessage = "";
        public String successMessage = "";

        public void OnGet()
        {
            String id = Request.Query["id"];

            try
            {
                String connectionString = "Data Source=.;Initial Catalog=vjezba5;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM uplatnica WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                       
                                transakcijeInfo.platitelj = reader.GetString(1);
                                transakcijeInfo.primatelj = reader.GetString(2);
                                transakcijeInfo.iban = reader.GetString(3);
                                transakcijeInfo.model = reader.GetString(4);
                                transakcijeInfo.iznos =""+ reader.GetInt32(5);
                                transakcijeInfo.valuta = reader.GetString(6);
                                transakcijeInfo.vrijemeUplate = reader.GetDateTime(7).ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }

        public void OnPost()
        {
            transakcijeInfo.id = Request.Form["id"];
            transakcijeInfo.platitelj = Request.Form["platitelj"];
            transakcijeInfo.primatelj = Request.Form["primatelj"];
            transakcijeInfo.iban = Request.Form["iban"];
            transakcijeInfo.model = Request.Form["model"];
            transakcijeInfo.valuta = Request.Form["valuta"];
            transakcijeInfo.iznos = Request.Form["iznos"];

            if (transakcijeInfo.platitelj.Length == 0 || transakcijeInfo.primatelj.Length == 0 ||
                transakcijeInfo.iban.Length == 0 || transakcijeInfo.iznos.Length == 0)
            {
                errorMessage = "All fields are required";
                return;
            }

            try
            {
                String connectionString = "Data Source=.;Initial Catalog=vjezba5;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE uplatnica " +
                                 "SET platitelj=@platitelj, primatelj=@primatelj, iban=@iban, iznos=@iznos, model=@model, valuta=@valuta " +
                                 "WHERE id=@id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@platitelj", transakcijeInfo.platitelj);
                        command.Parameters.AddWithValue("@primatelj", transakcijeInfo.primatelj);
                        command.Parameters.AddWithValue("@iban", transakcijeInfo.iban);
                        command.Parameters.AddWithValue("@model", transakcijeInfo.model);
                        command.Parameters.AddWithValue("@valuta", transakcijeInfo.valuta);
                        command.Parameters.AddWithValue("@iznos", transakcijeInfo.iznos);
                        command.Parameters.AddWithValue("@id", transakcijeInfo.id);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("/Transakcije/Index");
        }
    }
}
