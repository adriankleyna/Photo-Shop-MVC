using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using _15904_KleynaPHOTO.Models;
using Microsoft.AspNetCore.Authorization;

namespace _15904_KleynaPHOTO.Controllers
{
    public class PracownikController : Controller
    {

        string connectionString = @"Data Source=.;Initial Catalog=SKLEP_FOTO;Integrated Security=True";

        // GET: PracownikController
        [HttpGet]
        public ActionResult Index()
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select pracownik.id, adres.ulica + ' ' + adres.numer as Adres, adres.miejscowosc as Miasto, pracownik.imie, pracownik.nazwisko, pracownik.stanowisko, pracownik.pesel, pracownik.data_zatrudnienia, pracownik.pensja, pracownik.dodatek from(pracownik inner join adres on adres.id = pracownik.adres_id); ", sqlCon);
                sqlDataAdapter.Fill(dataTable);
            }

            return View(dataTable);
        }

        // GET: PracownikController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        // GET: PracownikController/Create
        public ActionResult Create()
        {
            return View(new Pracownik());
        }

        [Authorize(Roles = "Admin")]
        // POST: PracownikController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Pracownik pracownik)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                string query = "insert into adres values(@AdresUlica, @AdresNumer, @AdresKod, @AdresMiasto)";
                SqlCommand sqlCommandAdres = new SqlCommand(query, sqlConnection);
                sqlCommandAdres.Parameters.AddWithValue("@AdresUlica", pracownik.AdresUlica);
                sqlCommandAdres.Parameters.AddWithValue("@AdresNumer", pracownik.AdresNumer);
                sqlCommandAdres.Parameters.AddWithValue("@AdresKod", pracownik.AdresKod);
                sqlCommandAdres.Parameters.AddWithValue("@AdresMiasto", pracownik.AdresMiasto);
                sqlCommandAdres.ExecuteNonQuery();

                query = "select id from adres where ulica=@AdresUlica and numer=@AdresNumer";
                sqlCommandAdres = new SqlCommand(query, sqlConnection);
                sqlCommandAdres.Parameters.AddWithValue("@AdresUlica", pracownik.AdresUlica);
                sqlCommandAdres.Parameters.AddWithValue("@AdresNumer", pracownik.AdresNumer);
                int id_adres = Convert.ToInt32(sqlCommandAdres.ExecuteScalar());


                query = "INSERT INTO KONTO VALUES(@PracownikLogin, @PracownikHaslo, 'Admin');";
                SqlCommand sqlCommandKonto = new SqlCommand(query, sqlConnection);
                sqlCommandKonto.Parameters.AddWithValue("@PracownikLogin", pracownik.PracownikLogin);
                sqlCommandKonto.Parameters.AddWithValue("@PracownikHaslo", pracownik.PracownikHaslo);
                sqlCommandKonto.ExecuteNonQuery();

                query = "SELECT id from KONTO" +
                       " WHERE userLogin=@PracownikLogin and userPassword=@PracownikHaslo";
                sqlCommandKonto = new SqlCommand(query, sqlConnection);
                sqlCommandKonto.Parameters.AddWithValue("@PracownikLogin", pracownik.PracownikLogin);
                sqlCommandKonto.Parameters.AddWithValue("@PracownikHaslo", pracownik.PracownikHaslo);
                int id_konto = Convert.ToInt32(sqlCommandKonto.ExecuteScalar());


                string queryPracownik = "insert into pracownik values(@id_adres, @PracownikImie, @PracownikNazwisko, @PracownikStanowisko, @PracownikPesel, @PracownikData, @PracownikPensja, @PracownikDodatek, @id_konto)";
                SqlCommand sqlCommand = new SqlCommand(queryPracownik, sqlConnection);
                sqlCommand.Parameters.AddWithValue("@id_adres", id_adres);
                sqlCommand.Parameters.AddWithValue("@PracownikImie", pracownik.PracownikImie);
                sqlCommand.Parameters.AddWithValue("@PracownikNazwisko", pracownik.PracownikNazwisko);
                sqlCommand.Parameters.AddWithValue("@PracownikStanowisko", pracownik.PracownikStanowisko);
                sqlCommand.Parameters.AddWithValue("@PracownikPesel", pracownik.PracownikPesel);
                sqlCommand.Parameters.AddWithValue("@PracownikData", pracownik.PracownikData);
                sqlCommand.Parameters.AddWithValue("@PracownikPensja", pracownik.PracownikPensja);
                sqlCommand.Parameters.AddWithValue("@PracownikDodatek", pracownik.PracownikDodatek);
                sqlCommand.Parameters.AddWithValue("@id_konto", id_konto);
                sqlCommand.ExecuteNonQuery();

            }

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        // GET: PracownikController/Edit/5
        public ActionResult Edit(int id)
        {
            Pracownik pracownik = new Pracownik();
            DataTable dataTable = new DataTable();

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                string query = "select adres.ulica, adres.numer, adres.miejscowosc, pracownik.imie, pracownik.nazwisko," +
                    " pracownik.stanowisko, pracownik.pesel, pracownik.data_zatrudnienia, pracownik.pensja, pracownik.dodatek, adres.kod," +
                    " konto.userLogin, konto.userPassword" +
                    " from pracownik inner join adres on adres.id = pracownik.adres_id " +
                    " INNER JOIN KONTO on pracownik.konto_id = konto.id where pracownik.id = @PracownikID; ";

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, sqlConnection);
                sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@PracownikID", id);

                sqlDataAdapter.Fill(dataTable);

            }

            if (dataTable.Rows.Count == 1)
            {
                pracownik.AdresUlica = dataTable.Rows[0][0].ToString();
                pracownik.AdresNumer = dataTable.Rows[0][1].ToString();
                pracownik.AdresMiasto = dataTable.Rows[0][2].ToString();
                pracownik.PracownikImie = dataTable.Rows[0][3].ToString();
                pracownik.PracownikNazwisko = dataTable.Rows[0][4].ToString();
                pracownik.PracownikStanowisko = dataTable.Rows[0][5].ToString();
                pracownik.PracownikPesel = dataTable.Rows[0][6].ToString();
                pracownik.PracownikData = dataTable.Rows[0][7].ToString();
                pracownik.PracownikPensja = Convert.ToDouble(dataTable.Rows[0][8]);
                pracownik.PracownikDodatek = Convert.ToDouble(dataTable.Rows[0][9]);
                pracownik.AdresKod = dataTable.Rows[0][10].ToString();
                pracownik.PracownikLogin = dataTable.Rows[0][11].ToString();
                pracownik.PracownikHaslo = dataTable.Rows[0][12].ToString();

                return View(pracownik);
            }
            else
            {
                return RedirectToAction("Index");
            }

        }

        [Authorize(Roles = "Admin")]
        // POST: PracownikController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Pracownik pracownik)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();

                    string queryEdit = "update pracownik set imie=@PracownikImie, nazwisko=@PracownikNazwisko, stanowisko=@PracownikStanowisko," +
                        " pesel=@PracownikPesel, data_zatrudnienia=@PracownikData, pensja=@PracownikPensja, dodatek=@PracownikDodatek" +
                        " where id=@PracownikID;";

                    SqlCommand sqlCommand = new SqlCommand(queryEdit, sqlConnection);
                    sqlCommand.Parameters.AddWithValue("@PracownikID", id);
                    sqlCommand.Parameters.AddWithValue("@PracownikImie", pracownik.PracownikImie);
                    sqlCommand.Parameters.AddWithValue("@PracownikNazwisko", pracownik.PracownikNazwisko);
                    sqlCommand.Parameters.AddWithValue("@PracownikStanowisko", pracownik.PracownikStanowisko);
                    sqlCommand.Parameters.AddWithValue("@PracownikPesel", pracownik.PracownikPesel);
                    sqlCommand.Parameters.AddWithValue("@PracownikData",  DateTime.Parse(pracownik.PracownikData));
                    sqlCommand.Parameters.AddWithValue("@PracownikPensja", pracownik.PracownikPensja);
                    sqlCommand.Parameters.AddWithValue("@PracownikDodatek", pracownik.PracownikDodatek);
                    sqlCommand.ExecuteNonQuery();

                    queryEdit = "select adres_id from pracownik where id=@PracownikID";
                    sqlCommand = new SqlCommand(queryEdit, sqlConnection);
                    sqlCommand.Parameters.AddWithValue("@PracownikID", id);
                    int id_adres = Convert.ToInt32(sqlCommand.ExecuteScalar());

                    queryEdit = "update adres SET ulica=@AdresUlica, numer=@AdresNumer, kod=@AdresKod, miejscowosc=@AdresMiasto" +
                        " where id=@id_adres";
                    sqlCommand = new SqlCommand(queryEdit, sqlConnection);
                    sqlCommand.Parameters.AddWithValue("@id_adres", id_adres);
                    sqlCommand.Parameters.AddWithValue("@AdresUlica", pracownik.AdresUlica);
                    sqlCommand.Parameters.AddWithValue("@AdresNumer", pracownik.AdresNumer);
                    sqlCommand.Parameters.AddWithValue("@AdresKod", pracownik.AdresKod);
                    sqlCommand.Parameters.AddWithValue("@AdresMiasto", pracownik.AdresMiasto);
                    sqlCommand.ExecuteNonQuery();

                    queryEdit = "select konto_id from pracownik where id=@PracownikID";
                    sqlCommand = new SqlCommand(queryEdit, sqlConnection);
                    sqlCommand.Parameters.AddWithValue("@PracownikID", id);
                    int id_konto = Convert.ToInt32(sqlCommand.ExecuteScalar());

                    queryEdit = "update KONTO set userLogin=@PracownikLogin, userPassword=@PracownikHaslo" +
                        " where id=@id_konto";
                    sqlCommand = new SqlCommand(queryEdit, sqlConnection);
                    sqlCommand.Parameters.AddWithValue("@id_konto", id_konto);
                    sqlCommand.Parameters.AddWithValue("@PracownikLogin", pracownik.PracownikLogin);
                    sqlCommand.Parameters.AddWithValue("@PracownikHaslo", pracownik.PracownikHaslo);
                    sqlCommand.ExecuteNonQuery();

                }
                return RedirectToAction("Index");
            }
            else
            {
                return View(pracownik);
            }
        }

        [Authorize(Roles = "Admin")]
        // GET: PracownikController/Delete/5
        public ActionResult Delete(int id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();

                string queryDelete = "select adres_id from pracownik where id=@PracownikID";
                SqlCommand sqlCommand = new SqlCommand(queryDelete, sqlConnection);
                sqlCommand.Parameters.AddWithValue("@PracownikID", id);
                int id_adres = Convert.ToInt32(sqlCommand.ExecuteScalar());

                queryDelete = "select konto_id from pracownik where id=@PracownikID";
                sqlCommand = new SqlCommand(queryDelete, sqlConnection);
                sqlCommand.Parameters.AddWithValue("@PracownikID", id);
                int id_konto = Convert.ToInt32(sqlCommand.ExecuteScalar());

                queryDelete = "delete from adres where id=@id_adres";
                sqlCommand = new SqlCommand(queryDelete, sqlConnection);
                sqlCommand.Parameters.AddWithValue("@id_adres", id_adres);
                sqlCommand.ExecuteNonQuery();

                queryDelete = "delete from KONTO where id=@id_konto";
                sqlCommand = new SqlCommand(queryDelete, sqlConnection);
                sqlCommand.Parameters.AddWithValue("@id_konto", id_konto);
                sqlCommand.ExecuteNonQuery();

                queryDelete = "delete from pracownik where id=@PracownikID";
                sqlCommand = new SqlCommand(queryDelete, sqlConnection);
                sqlCommand.Parameters.AddWithValue("@PracownikID", id);
                sqlCommand.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

    }
}
