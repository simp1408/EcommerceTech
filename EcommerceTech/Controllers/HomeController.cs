using EcommerceTech.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EcommerceTech.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //creiamo una lista e ci passiamo il metodo
            //List<Prodotto> List = new List<Prodotto>();
            
            //List = Prodotto.GetProdottoList();
            return View();
        }

        public ActionResult PartialViewListaProdotti()
        {
            //creiamo una lista e ci passiamo il metodo
            List<Prodotto> List = new List<Prodotto>();
            List = Prodotto.GetProdottoList();
            return PartialView("_PartialViewListaProdotti",List);
        }

        public ActionResult Create()
        {
            //con la viebag vado a visualizzare i selectItems(Smartphone,Tablet,Computer) 
            //mi vado a popolare il dropdown
            ViewBag.ListaCategoria = Categoria.ListaCategoria;
            return View();
        }

        [HttpPost]
        public ActionResult Create(Prodotto p,HttpPostedFileBase FileUpload)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["ecommerceTech"].ToString();
            connection.Open();

            
            string fileName = "";
            if (FileUpload != null)
            {
                if (FileUpload.ContentLength > 0)
                {
                    fileName = FileUpload.FileName;
                    //andiamo a salvare il file immagine mettendoci il percorso
                    string Path = Server.MapPath("/Content/img/" + fileName);
                    FileUpload.SaveAs(Path);

                }
            }


            SqlCommand command = new SqlCommand();
            command.Parameters.AddWithValue("NomeProdotto", p.NomeProdotto);
            command.Parameters.AddWithValue("Description",p.Description);
            command.Parameters.AddWithValue("Price", p.Price);
            command.Parameters.AddWithValue("IdCategoria",p.IdCategoria);
            command.Parameters.AddWithValue("UrlImmagine",fileName);
            
            command.CommandText = "insert into prodotto VALUES(@NomeProdotto,@Description,@Price,@IdCategoria,@UrlImmagine)";
            command.Connection = connection;

            int row = command.ExecuteNonQuery();
            if (row > 0) 
            {
                ViewBag.ListaCategoria = Categoria.ListaCategoria;
                ViewBag.ConfermaMessaggio = "Prodotto inserito con successo";
            }

            connection.Close();
            return View();
        }

        public ActionResult Details(int id)
        {

            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["ecommerceTech"].ToString();
            connection.Open();

            SqlCommand command = new SqlCommand();
            command.Parameters.AddWithValue("@IDprodotto", id);
            command.CommandText = "SELECT * FROM PRODOTTO WHERE IDprodotto=@iDprodotto";
            command.Connection = connection;    
            SqlDataReader reader= command.ExecuteReader();

            Prodotto p = new Prodotto();

            while (reader.Read()) 
            {
               
                p.NomeProdotto = reader["NomeProdotto"].ToString() ;
                p.Description= reader["Description"].ToString() ;
                p.Price = Convert.ToDecimal(reader["Price"]);
                p.UrlImmagine = reader["UrlImmagine"].ToString();
            }

            connection.Close();
            return View(p);

        }


    }
}