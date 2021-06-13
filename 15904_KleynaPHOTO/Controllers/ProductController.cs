using _15904_KleynaPHOTO.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace _15904_KleynaPHOTO.Controllers
{
    public class ProductController : Controller
    {
        string connectionString = @"Data Source=.;Initial Catalog=SKLEP_FOTO;Integrated Security=True";

        // GET: ProductController
        [HttpGet]
        public ActionResult Index()
        {
            DataTable dataTable_Product = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select produkt.id, kategoria.nazwa as kategoria, podkategoria.nazwa as podkategoria, produkt.nazwa, produkt.cena_netto, produkt.podatek  from ((produkt inner join podkategoria on produkt.podkategoria_id = podkategoria.id)inner join kategoria on podkategoria.kategoria_id = kategoria.id); ", sqlCon);
                sqlDataAdapter.Fill(dataTable_Product);
            }

            return View(dataTable_Product);
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            
            DataTable dataTable_Producenci = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select * from producent", sqlCon);
                sqlDataAdapter.Fill(dataTable_Producenci);
            }
            ViewBag.DaneProducenci = dataTable_Producenci;

            DataTable dataTable_Podkategoria = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select id, nazwa from podkategoria", sqlCon);
                sqlDataAdapter.Fill(dataTable_Podkategoria);
            }
            ViewBag.DanePodkategorie = dataTable_Podkategoria;



            return View(new Product());
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {

            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "insert into produkt values(@ProductProducent, @ProductPodkategoriaID, @ProductNazwa, @ProductOpis, @ProductCenaNetto, @ProductPodatek, @ProductIlosc_Sztuk_Magazyn)";
                SqlCommand sqlCommand = new SqlCommand(query, sqlCon);
                sqlCommand.Parameters.AddWithValue("@ProductProducent", product.ProductProducent);
                sqlCommand.Parameters.AddWithValue("@ProductPodkategoriaID", product.ProductPodkategoriaID);
                sqlCommand.Parameters.AddWithValue("@ProductNazwa", product.ProductNazwa);
                sqlCommand.Parameters.AddWithValue("@ProductOpis", product.ProductOpis);
                sqlCommand.Parameters.AddWithValue("@ProductCenaNetto", product.ProductCenaNetto);
                sqlCommand.Parameters.AddWithValue("@ProductPodatek", product.ProductPodatek);
                sqlCommand.Parameters.AddWithValue("@ProductIlosc_Sztuk_Magazyn", product.ProductIlosc_Sztuk_Magazyn);
                sqlCommand.ExecuteNonQuery();

            }

            return RedirectToAction("Index");
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {


            Product productModel = new Product();
            DataTable dataTableProduct = new DataTable();

            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "select * from produkt where id = @ProductID";
                SqlDataAdapter sqlData = new SqlDataAdapter(query, sqlCon);
                sqlData.SelectCommand.Parameters.AddWithValue("@ProductID", id);

                sqlData.Fill(dataTableProduct);
            }
            if (dataTableProduct.Rows.Count == 1)
            {
                List<ProducentList> producentList = new List<ProducentList>();
                List<CategoryList> categoryList = new List<CategoryList>();
                List<SubcategoryList> subcategoryList = new List<SubcategoryList>();

                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {
                    string query = "select id, nazwa from producent;";
                    
                    using(var cmd = new SqlCommand(query, sqlCon))
                    {
                        sqlCon.Open();
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    var item = new ProducentList();
                                    item.id = reader.GetInt32(reader.GetOrdinal("id"));
                                    item.Producent = reader.GetString(reader.GetOrdinal("nazwa"));
                                    producentList.Add(item);
                                }
                            }
                        }
                    }

                    string queryCategory = "select id, nazwa from kategoria;";
                    using (var cmd = new SqlCommand(queryCategory , sqlCon))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    var item = new CategoryList();
                                    item.id = reader.GetInt32(reader.GetOrdinal("id"));
                                    item.Category = reader.GetString(reader.GetOrdinal("nazwa"));
                                    categoryList.Add(item);
                                }
                            }
                        }
                    }


                    string querySubCategory = "select id, nazwa from podkategoria;";
                    using (var cmd = new SqlCommand(querySubCategory, sqlCon))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    var item = new SubcategoryList();
                                    item.id = reader.GetInt32(reader.GetOrdinal("id"));
                                    item.Subcategory = reader.GetString(reader.GetOrdinal("nazwa"));
                                    subcategoryList.Add(item);
                                }
                            }
                        }
                    }



                }



                productModel.ProductID = Convert.ToInt32(dataTableProduct.Rows[0][0].ToString());
                productModel.ProductProducent = Convert.ToInt32(dataTableProduct.Rows[0][1].ToString());
                productModel.Producenci = producentList;
                productModel.Podkategorie = subcategoryList;
                productModel.ProductPodkategoriaID = Convert.ToInt32(dataTableProduct.Rows[0][2].ToString());
                productModel.ProductNazwa = dataTableProduct.Rows[0][3].ToString();
                productModel.ProductOpis = dataTableProduct.Rows[0][4].ToString();
                productModel.ProductCenaNetto = Convert.ToDecimal(dataTableProduct.Rows[0][5].ToString());
                productModel.ProductPodatek = Convert.ToInt32(dataTableProduct.Rows[0][6].ToString());
                productModel.ProductIlosc_Sztuk_Magazyn = Convert.ToInt32(dataTableProduct.Rows[0][7].ToString());
                return View(productModel);

            }
            else
                return RedirectToAction("Index");

        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product productModel)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "update produkt set producent_id = @ProductProducent , podkategoria_id = @ProductPodkategoriaID , nazwa = @ProductNazwa, opis = @ProductOpis , cena_netto = @ProductCenaNetto , podatek = @ProductPodatek , ilosc_sztuk_magazyn = @ProductIlosc_Sztuk_Magazyn WHERE id = @ProductID";
                SqlCommand sqlCommand = new SqlCommand(query, sqlCon);
                sqlCommand.Parameters.AddWithValue("@ProductID", productModel.ProductID);
                sqlCommand.Parameters.AddWithValue("@ProductProducent", productModel.ProductProducent);
                sqlCommand.Parameters.AddWithValue("@ProductPodkategoriaID", productModel.ProductPodkategoriaID);
                sqlCommand.Parameters.AddWithValue("@ProductNazwa", productModel.ProductNazwa);
                sqlCommand.Parameters.AddWithValue("@ProductOpis", productModel.ProductOpis);
                sqlCommand.Parameters.AddWithValue("@ProductCenaNetto", productModel.ProductCenaNetto);
                sqlCommand.Parameters.AddWithValue("@ProductPodatek", productModel.ProductPodatek);
                sqlCommand.Parameters.AddWithValue("@ProductIlosc_Sztuk_Magazyn", productModel.ProductIlosc_Sztuk_Magazyn);
                sqlCommand.ExecuteNonQuery();

            }

            return RedirectToAction("Index");
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "delete from produkt WHERE id = @ProductID";
                SqlCommand sqlCommand = new SqlCommand(query, sqlCon);
                sqlCommand.Parameters.AddWithValue("@ProductID", id);
                sqlCommand.ExecuteNonQuery();

            }

            return RedirectToAction("Index");
        }

        //// POST: ProductController/Delete/5
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
