using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using _15904_KleynaPHOTO.Models;

namespace _15904_KleynaPHOTO.Controllers
{
    public class KlientController : Controller
    {
        string connectionString = @"Data Source=.;Initial Catalog=SKLEP_FOTO;Integrated Security=True";

        // GET: KlientController
        public ActionResult Index()
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select klient.id, adres.ulica + ' ' + adres.numer as Adres, adres.miejscowosc as Miasto, klient.imie, klient.nazwisko, " + 
                    "klient.pesel, klient.telefon, klient.email, klient.usuniety from(klient inner join adres on adres.id = klient.adres_id); ", sqlCon);
                sqlDataAdapter.Fill(dataTable);
            }

            return View(dataTable);
        }

        // GET: KlientController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: KlientController/Create
        public ActionResult Create()
        {
            return View(new Klient());
        }

        // POST: KlientController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Klient klient)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                string queryAdres = "insert into adres values(@AdresUlica, @AdresNumer, @AdresKod, @AdresMiasto)";
                SqlCommand sqlCommandAdres = new SqlCommand(queryAdres, sqlConnection);
                sqlCommandAdres.Parameters.AddWithValue("@AdresUlica", klient.AdresUlica);
                sqlCommandAdres.Parameters.AddWithValue("@AdresNumer", klient.AdresNumer);
                sqlCommandAdres.Parameters.AddWithValue("@AdresKod", klient.AdresKod);
                sqlCommandAdres.Parameters.AddWithValue("@AdresMiasto", klient.AdresMiasto);
                sqlCommandAdres.ExecuteNonQuery();

                queryAdres = "select id from adres where ulica=@AdresUlica and numer=@AdresNumer";
                sqlCommandAdres = new SqlCommand(queryAdres, sqlConnection);
                sqlCommandAdres.Parameters.AddWithValue("@AdresUlica", klient.AdresUlica);
                sqlCommandAdres.Parameters.AddWithValue("@AdresNumer", klient.AdresNumer);
                int id_adres = Convert.ToInt32(sqlCommandAdres.ExecuteScalar());


                string queryPracownik = "insert into klient values(@id_adres, @KlientImie, @KlientNazwisko, @KlientPesel, @KlientTelefon, @KlientEmail, @KlientUsuniety)";
                SqlCommand sqlCommand = new SqlCommand(queryPracownik, sqlConnection);
                sqlCommand.Parameters.AddWithValue("@id_adres", id_adres);
                sqlCommand.Parameters.AddWithValue("@KlientImie", klient.KlientImie);
                sqlCommand.Parameters.AddWithValue("@KlientNazwisko", klient.KlientNazwisko);
                sqlCommand.Parameters.AddWithValue("@KlientPesel", klient.KlientPesel);
                sqlCommand.Parameters.AddWithValue("@KlientTelefon", klient.KlientTelefon);
                sqlCommand.Parameters.AddWithValue("@KlientEmail", klient.KlientEmail);
                sqlCommand.Parameters.AddWithValue("@KlientUsuniety", klient.KlientUsuniety);
                sqlCommand.ExecuteNonQuery();

            }

            return RedirectToAction("Index");
        }

        // GET: KlientController/Edit/5
        public ActionResult Edit(int id)
        {
            Klient klient = new Klient();
            DataTable dataTable = new DataTable();

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                string query = "select adres.ulica, adres.numer, adres.miejscowosc, klient.imie, klient.nazwisko," +
                    " klient.pesel, klient.telefon, klient.email, klient.usuniety, adres.kod" +
                    " from(klient inner join adres on adres.id = klient.adres_id) where klient.id = @KlientID; ";

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, sqlConnection);
                sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@KlientID", id);

                sqlDataAdapter.Fill(dataTable);

            }

            if (dataTable.Rows.Count == 1)
            {
                klient.AdresUlica = dataTable.Rows[0][0].ToString();
                klient.AdresNumer = dataTable.Rows[0][1].ToString();
                klient.AdresMiasto = dataTable.Rows[0][2].ToString();
                klient.KlientImie = dataTable.Rows[0][3].ToString();
                klient.KlientNazwisko = dataTable.Rows[0][4].ToString();
                klient.KlientPesel = dataTable.Rows[0][5].ToString();
                klient.KlientTelefon = dataTable.Rows[0][6].ToString();
                klient.KlientEmail = dataTable.Rows[0][7].ToString();
                klient.KlientUsuniety = dataTable.Rows[0][8].ToString();
                klient.AdresKod = dataTable.Rows[0][9].ToString();

                return View(klient);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        // POST: KlientController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Klient klient)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();

                    string queryEdit = "update klient set imie=@KlientImie, nazwisko=@KlientNazwisko," +
                        " pesel=@KlientPesel, telefon=@KlientTelefon, email=@KlientEmail, usuniety=@KlientUsuniety" +
                        " where id=@KlientID;";

                    SqlCommand sqlCommand = new SqlCommand(queryEdit, sqlConnection);
                    sqlCommand.Parameters.AddWithValue("@KlientID", id);
                    sqlCommand.Parameters.AddWithValue("@KlientImie", klient.KlientImie);
                    sqlCommand.Parameters.AddWithValue("@KlientNazwisko", klient.KlientNazwisko);
                    sqlCommand.Parameters.AddWithValue("@KlientPesel", klient.KlientPesel);
                    sqlCommand.Parameters.AddWithValue("@KlientTelefon", klient.KlientTelefon);
                    sqlCommand.Parameters.AddWithValue("@KlientEmail", klient.KlientEmail);
                    sqlCommand.Parameters.AddWithValue("@KlientUsuniety", klient.KlientUsuniety);
                    sqlCommand.ExecuteNonQuery();

                    queryEdit = "select adres_id from klient where id=@KlientID";
                    sqlCommand = new SqlCommand(queryEdit, sqlConnection);
                    sqlCommand.Parameters.AddWithValue("@KlientID", id);
                    int id_adres = Convert.ToInt32(sqlCommand.ExecuteScalar());

                    queryEdit = "update adres SET ulica=@AdresUlica, numer=@AdresNumer, kod=@AdresKod, miejscowosc=@AdresMiasto" +
                        " where id=@id_adres";
                    sqlCommand = new SqlCommand(queryEdit, sqlConnection);
                    sqlCommand.Parameters.AddWithValue("@id_adres", id_adres);
                    sqlCommand.Parameters.AddWithValue("AdresUlica", klient.AdresUlica);
                    sqlCommand.Parameters.AddWithValue("AdresNumer", klient.AdresNumer);
                    sqlCommand.Parameters.AddWithValue("AdresKod", klient.AdresKod);
                    sqlCommand.Parameters.AddWithValue("AdresMiasto", klient.AdresMiasto);
                    sqlCommand.ExecuteNonQuery();
                }
                return RedirectToAction("Index");
            }
            else
            {
                return View(klient);
            }
        }

        // GET: KlientController/Delete/5
        public ActionResult Delete(int id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();

                string queryDelete = "select adres_id from klient where id=@KlientID";
                SqlCommand sqlCommand = new SqlCommand(queryDelete, sqlConnection);
                sqlCommand.Parameters.AddWithValue("@KlientID", id);
                int id_adres = Convert.ToInt32(sqlCommand.ExecuteScalar());

                queryDelete = "delete from adres where id=@id_adres";
                sqlCommand = new SqlCommand(queryDelete, sqlConnection);
                sqlCommand.Parameters.AddWithValue("@id_adres", id_adres);
                sqlCommand.ExecuteNonQuery();

                queryDelete = "delete from klient where id=@KlientID";
                sqlCommand = new SqlCommand(queryDelete, sqlConnection);
                sqlCommand.Parameters.AddWithValue("@KlientID", id);
                sqlCommand.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        //// POST: KlientController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
