using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EcommerceTech.Models
{
    public class Categoria
    {
        public int IDcategoria { get; set; }

        public string NomeCategoria { get; set; }

        // creo proprieta statica di tipo list
        public static List<SelectListItem> ListaCategoria { 
            get
            {
                List<SelectListItem> selectListItems = new List<SelectListItem>();

                SqlConnection connection = new SqlConnection();
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["ecommerceTech"].ToString();
                connection.Open();

                SqlCommand command = new SqlCommand();
                command.CommandText = "Select * from Categoria";
                command.Connection = connection;

                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        SelectListItem s = new SelectListItem();
                        s.Text = reader["NomeCategoria"].ToString();
                        s.Value = Convert.ToInt32(reader["IDcategoria"]).ToString();
                        selectListItems.Add(s);
                          
                    }

                }
                connection.Close();
                return selectListItems;
            }
                
                
         }
    }
}