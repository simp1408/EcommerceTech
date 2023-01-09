using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;

namespace EcommerceTech.Models
{
    public class Prodotto
    {
        public int IDprodotto { get; set; }
        [Display(Name = "Nome Prodotto")]
        public string NomeProdotto { get; set; }

        [Display(Name = "Descrizione Prodotto")]

        public string Description { get; set; }

        [Display(Name = "Prezzo Prodotto")]
        [DisplayFormat(DataFormatString ="{0:C}")]
        public decimal Price { get; set; }

        [Display(Name ="Categoria")]
        public int IdCategoria { get; set; }
        [Display(Name = "Immagine Prodotto")]

        public string UrlImmagine { get; set; }

        public string Categoria { get; set; }

        //metodo che restituisce una lista
        public static List<Prodotto> GetProdottoList()
        {
            List<Prodotto>List = new List<Prodotto>();

            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["ecommerceTech"].ToString();
            connection.Open();

            SqlCommand command = new SqlCommand();
         
            command.CommandText = "Select * from Prodotto inner join Categoria on Prodotto.IdCategoria=Categoria.IDcategoria";
            command.Connection = connection;


            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows) 
            {
                while (reader.Read()) 
                {
                    Prodotto p = new Prodotto();
                    p.IDprodotto= Convert.ToInt32(reader["IDprodotto"]);
                    p.NomeProdotto = reader["NomeProdotto"].ToString() ;
                    p.Description = reader["Description"].ToString();
                    p.Price = Convert.ToDecimal(reader["Price"]);
                    p.UrlImmagine = reader["UrlImmagine"].ToString();
                    p.IdCategoria = Convert.ToInt32(reader["IdCategoria"]);
                    p.Categoria = reader["NomeCategoria"].ToString();
                    List.Add(p);
                }
            
            }
            connection.Close();
            return List;
        
        
        }
    }
}