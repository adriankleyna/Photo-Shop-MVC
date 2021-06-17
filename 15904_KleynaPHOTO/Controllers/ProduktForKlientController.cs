using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _15904_KleynaPHOTO.Models;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Authorization;

namespace _15904_KleynaPHOTO.Controllers
{
    public class ProduktForKlientController : Controller
    {

        //string connectionString = @"Data Source=.;Initial Catalog=SKLEP_FOTO;Integrated Security=True";
        string connectionString = "Server=DESKTOP-OBAGE0C\\SQLEXPRESS;Database=SKLEP_FOTO;Trusted_Connection=True;";

        [Authorize]
        // GET: ProduktForKlientController
        public ActionResult Index()
        {
            DataTable dataTable_Product = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select produkt.id, kategoria.nazwa as kategoria, " +
                    "podkategoria.nazwa as podkategoria, produkt.nazwa, produkt.cena_netto, produkt.podatek  " +
                    "from ((produkt inner join podkategoria on produkt.podkategoria_id = podkategoria.id) " +
                    "inner join kategoria on podkategoria.kategoria_id = kategoria.id); ", sqlCon);
                sqlDataAdapter.Fill(dataTable_Product);
            }

            return View(dataTable_Product);
        }

        // GET: ProduktForKlientController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProduktForKlientController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProduktForKlientController/Create
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

        // GET: ProduktForKlientController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProduktForKlientController/Edit/5
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

        // GET: ProduktForKlientController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProduktForKlientController/Delete/5
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
