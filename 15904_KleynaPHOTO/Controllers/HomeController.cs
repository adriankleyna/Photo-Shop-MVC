using _15904_KleynaPHOTO.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace _15904_KleynaPHOTO.Controllers
{
    public class HomeController : Controller
    {
        string connectionString = @"Data Source=.;Initial Catalog=SKLEP_FOTO;Integrated Security=True";

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        //strona odrzucenia z powodu praku uprawnien
        [HttpGet("denied")]
        public IActionResult Denied()
        {
            return View();
        }


        [Authorize]
        public IActionResult Secured()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }

        [HttpGet("login")]
        public IActionResult Login(string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Validate(string login, string password, string returnUrl)
        {

            if (string.IsNullOrEmpty(login))
            {
                TempData["Error"] = "Nie podałeś loginu";
                return View("login");
            }

            if (string.IsNullOrEmpty(password))
            {
                TempData["Error"] = "Nie podałeś hasła";
            }

            DataTable dataTableKonto = new DataTable();
            DataTable dataTablePracownik = new DataTable();
            DataTable dataTableKlient = new DataTable();

            using (SqlConnection sqlConnection = new SqlConnection(connectionString)) 
            {
                sqlConnection.Open();
                string query = "select * from konto where userLogin=@login and userPassword=@password;";
                SqlDataAdapter sqlData = new SqlDataAdapter(query, sqlConnection);
                sqlData.SelectCommand.Parameters.AddWithValue("@login", login);
                sqlData.SelectCommand.Parameters.AddWithValue("@password", password);
                sqlData.Fill(dataTableKonto);

                if (dataTableKonto.Rows.Count != 0)
                {
                    query = "select * from pracownik where konto_id=@ID;";
                    sqlData = new SqlDataAdapter(query, sqlConnection);
                    sqlData.SelectCommand.Parameters.AddWithValue("@ID", dataTableKonto.Rows[0][0]);
                    sqlData.Fill(dataTablePracownik);

                    if (dataTablePracownik.Rows.Count == 0)
                    {
                        query = "select * from klient where konto_id=@ID;";
                        sqlData = new SqlDataAdapter(query, sqlConnection);
                        sqlData.SelectCommand.Parameters.AddWithValue("@ID", dataTableKonto.Rows[0][0]);
                        sqlData.Fill(dataTableKlient);
                    }
                }
            }

            ViewData["ReturnUrl"] = returnUrl;

            if (dataTableKonto.Rows.Count != 0)
            {
                var claims = new List<Claim>();

                if (dataTablePracownik.Rows.Count == 0)
                {
                    claims.Add(new Claim("id", dataTableKlient.Rows[0][0].ToString()));
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, dataTableKlient.Rows[0][3].ToString()));
                    claims.Add(new Claim(ClaimTypes.Name, dataTableKlient.Rows[0][3].ToString()));
                    claims.Add(new Claim(ClaimTypes.Role, dataTableKonto.Rows[0][3].ToString()));

                }
                else
                {
                    claims.Add(new Claim("id", dataTablePracownik.Rows[0][0].ToString()));
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, dataTablePracownik.Rows[0][3].ToString()));
                    claims.Add(new Claim(ClaimTypes.Name, dataTablePracownik.Rows[0][3].ToString()));
                    claims.Add(new Claim(ClaimTypes.Role, @dataTableKonto.Rows[0][3].ToString()));
                }
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync(claimsPrincipal);
                if (!string.IsNullOrEmpty(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                return RedirectToAction(nameof(Index));
            }
            TempData["Error"] = "Błędne hasło lub login";
            return View("login");
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
