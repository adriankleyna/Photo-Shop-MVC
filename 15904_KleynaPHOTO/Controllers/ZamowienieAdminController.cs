using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _15904_KleynaPHOTO.Models;
using System.Data;
using System.Data.SqlClient;

namespace _15904_KleynaPHOTO.Controllers
{
    public class ZamowienieAdminController : Controller
    {

        //string connectionString = @"Data Source=.;Initial Catalog=SKLEP_FOTO;Integrated Security=True";
        string connectionString = "Server=DESKTOP-OBAGE0C\\SQLEXPRESS;Database=SKLEP_FOTO;Trusted_Connection=True;";

        // GET: ZamowienieAdminController
        public ActionResult Index()
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select zamowienie.id, pracownik.imie, pracownik.nazwisko, " +
                    "klient.imie, klient.nazwisko, zamowienie.data_zamowienia, zamowienie.cena_netto_dostawy, zamowienie.podatek, zamowienie.cena_netto_dostawy + (zamowienie.cena_netto_dostawy * zamowienie.podatek/100) As 'Wartość Dostawy Brutto', " +
                    "koszyk.cena_netto, koszyk.podatek, koszyk.cena_netto + (koszyk.cena_netto * koszyk.podatek/100) As 'Wartość Brutto', " +
                    "produkt.id, produkt.nazwa, produkt.cena_netto, produkt.podatek, produkt.cena_netto * (produkt.cena_netto * produkt.podatek/100) As 'Cena Poroduktu Brutto' " +
                    "from zamowienie " +
                    "INNER JOIN pracownik ON zamowienie.pracownik_id = pracownik.id " +
                    "INNER JOIN klient ON zamowienie.klient_id = klient.id " +
                    "INNER JOIN koszyk on zamowienie.id = koszyk.zamowienie_id INNER JOIN produkt ON koszyk.produkt_id = produkt.id;", sqlCon);
                sqlDataAdapter.Fill(dataTable);
            }

            return View(dataTable);
        }

        // GET: ZamowienieAdminController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ZamowienieAdminController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ZamowienieAdminController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: ZamowienieAdminController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ZamowienieAdminController/Edit/5
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

        // GET: ZamowienieAdminController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ZamowienieAdminController/Delete/5
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
