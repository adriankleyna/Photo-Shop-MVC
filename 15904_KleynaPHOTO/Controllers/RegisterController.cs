using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _15904_KleynaPHOTO.Models;
using System.Data.SqlClient;

namespace _15904_KleynaPHOTO.Controllers
{
    public class RegisterController : Controller
    {

        string connectionString = @"Data Source=.;Initial Catalog=SKLEP_FOTO;Integrated Security=True";
        // GET: RegisterController
        public ActionResult Index()
        {
            return View();
        }

        // GET: RegisterController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RegisterController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RegisterController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Klient collection)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();

                    string query = "insert into adres values(@AdresUlica, @AdresNumer, @AdresKod, @AdresMiasto);";
                    SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                    sqlCommand.Parameters.AddWithValue("@AdresUlica", collection.AdresUlica);
                    sqlCommand.Parameters.AddWithValue("@AdresNumer", collection.AdresNumer);
                    sqlCommand.Parameters.AddWithValue("@AdresKod", collection.AdresKod);
                    sqlCommand.Parameters.AddWithValue("@AdresMiasto", collection.AdresMiasto);
                    sqlCommand.ExecuteNonQuery();

                    query = "SELECT TOP 1 id FROM adres ORDER BY id DESC;";
                    sqlCommand = new SqlCommand(query, sqlConnection);
                    int id_adres = Convert.ToInt32(sqlCommand.ExecuteScalar());

                    query = "INSERT INTO konto VALUES(@KlientLogin, @KlientHaslo, @KlientRola);";
                    sqlCommand = new SqlCommand(query, sqlConnection);
                    sqlCommand.Parameters.AddWithValue("@KlientLogin", collection.KlientLogin);
                    sqlCommand.Parameters.AddWithValue("@KlientHaslo", collection.KlientHaslo);
                    sqlCommand.Parameters.AddWithValue("@KlientRola", "Klient");
                    sqlCommand.ExecuteNonQuery();

                    query = "SELECT TOP 1 id FROM konto ORDER BY id DESC;";
                    sqlCommand = new SqlCommand(query, sqlConnection);
                    int id_konto = Convert.ToInt32(sqlCommand.ExecuteScalar());


                    query = "insert into klient values(@id_adres, @KlientImie, @KlientNazwisko, @KlientPesel, @KlientTelefon, @KlientEmail, @KlientUsuniety, @id_konto)";
                    sqlCommand = new SqlCommand(query, sqlConnection);
                    sqlCommand.Parameters.AddWithValue("@id_adres", id_adres);
                    sqlCommand.Parameters.AddWithValue("@KlientImie", collection.KlientImie);
                    sqlCommand.Parameters.AddWithValue("@KlientNazwisko", collection.KlientNazwisko);
                    sqlCommand.Parameters.AddWithValue("@KlientPesel", collection.KlientPesel);
                    sqlCommand.Parameters.AddWithValue("@KlientTelefon", collection.KlientTelefon);
                    sqlCommand.Parameters.AddWithValue("@KlientEmail", collection.KlientEmail);
                    sqlCommand.Parameters.AddWithValue("@KlientUsuniety", "0");
                    sqlCommand.Parameters.AddWithValue("@id_konto", id_konto);
                    sqlCommand.ExecuteNonQuery();

                }
                TempData["Msg"] = "Twoje konto zostało stworzone!";
                return RedirectToAction("Login", "Home", null);
            }
            catch
            {
                return View();
            }
        }

        // GET: RegisterController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RegisterController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RegisterController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RegisterController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
