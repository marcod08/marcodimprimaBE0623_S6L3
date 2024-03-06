using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;
using S6L1.Models;

namespace S6L1.Controllers
{
    public class DeliveryController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            List<Expedition> expe = new List<Expedition>();

            string connString = ConfigurationManager.ConnectionStrings["DeliveryDb"].ToString();

            using (var conn = new SqlConnection(connString))
            {
                conn.Open();

                string query = @"
                    SELECT Id, IdPrivateUser, IdCompanyUser, SellerName, ExpeditionStartDate, 
                           Weight, City, Address, ClientName, ExpiditionPrice, ExpeditionEndDate 
                    FROM Expedition";

                using (var command = new SqlCommand(query, conn))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var expedition = new Expedition
                            {
                                Id = (int)reader["Id"],
                                IdPrivateUser = (int)reader["IdPrivateUser"],
                                IdCompanyUser = (int)reader["IdCompanyUser"],
                                SellerName = (string)reader["SellerName"],
                                ExpeditionStartDate = (DateTime)reader["ExpeditionStartDate"],
                                Weight = (double)reader["Weight"],
                                City = (string)reader["City"],
                                Address = (string)reader["Address"],
                                ClientName = (string)reader["ClientName"],
                                ExpeditionPrice = (double)reader["ExpiditionPrice"],
                                ExpeditionEndDate = (DateTime)reader["ExpeditionEndDate"]
                            };

                            expe.Add(expedition);
                        }
                    }
                }
            }

            return View(expe);
        }
    }
}
