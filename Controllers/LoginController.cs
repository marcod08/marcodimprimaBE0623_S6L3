using S6L1.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace S6L1.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("UserPanel");
            return View();
        }

        [HttpPost]
        public ActionResult Index(Admin admin)
        {
            string connectString = ConfigurationManager.ConnectionStrings["DeliveryDb"].ToString();
            var conn = new SqlConnection(connectString);
            conn.Open();
            var command = new SqlCommand("Select * From AdminUser Where Username = @username AND Password = @password", conn);
            command.Parameters.AddWithValue("@username", admin.Username);
            command.Parameters.AddWithValue("@password", admin.Password);
            var reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                reader.Read();
                FormsAuthentication.SetAuthCookie(reader["Id"].ToString(), true);
                return RedirectToAction("UserPanel", "Login");
            }

            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult UserPanel()
        {
            var Id = HttpContext.User.Identity.Name;
            ViewBag.Id = Id;
            return View();
        }

        [Authorize, HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            if (HttpContext.User.Identity.IsAuthenticated) return RedirectToAction("Register");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Exclude = "AdminId")] Admin admin)
        {
            if (ModelState.IsValid)
            {
                string connString = ConfigurationManager.ConnectionStrings["DeliveryDb"].ToString();
                var conn = new SqlConnection(connString);
                conn.Open();
                var command = new SqlCommand(@"
                    INSERT INTO AdminUser
                    (Username, Password)
                    VALUES (@username, @password)
                ", conn);
                command.Parameters.AddWithValue("@username", admin.Username);

                command.Parameters.AddWithValue("@password", admin.Password);

                var countRows = command.ExecuteNonQuery();
                return RedirectToAction("Index");
            }
            // se almeno un campo non è valido si restituisce la view che presenterà anche gli errori
            // NO redirect in questo caso
            return View();
        }
    }
}