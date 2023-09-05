using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.Mvc;
using TareaASPInfotep.Models;

namespace TareaASPInfotep.Controllers
{
    public class ContactoController : Controller
    {
        private static string conexion = ConfigurationManager.ConnectionStrings["cadena"].ToString();
        private static List<Contacto> olista = new List<Contacto>();

        // GET: Contacto
        public ActionResult Inicio()
        {
            using (SqlConnection oconexion = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM CONTACTO", oconexion);
                cmd.CommandType = CommandType.Text;
                oconexion.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while(dr.Read())
                    {
                        Contacto nuevoContacto = new Contacto();
                        nuevoContacto.ID = Convert.ToInt32(dr["ID"]);
                        nuevoContacto.Nombre = dr["Nombre"].ToString();
                        nuevoContacto.Telefono = dr["Telefono"].ToString();
                        nuevoContacto.Correo = dr["Correo"].ToString();
                        nuevoContacto.Direccion = dr["Direccion"].ToString();

                        olista.Add(nuevoContacto);
                    }
                }

            }
                return View(olista);
        }

        [HttpGet]
        public ActionResult Registrar()
        {

            return View();
        }

        [HttpGet]
        public ActionResult Editar(int? id)
        {
            if (id == null)
                return RedirectToAction("Inicio", "Contacto");


            Contacto ocontacto = olista.Where(c => c.ID == id).FirstOrDefault();


            return View(ocontacto);
        }

        [HttpGet]
        public ActionResult Eliminar(int? id)
        {
            if (id == null)
                return RedirectToAction("Inicio", "Contacto");


            Contacto ocontacto = olista.Where(c => c.ID == id).FirstOrDefault();
            return View(ocontacto);
        }
        [HttpPost]
        public ActionResult Registrar(Contacto ocontacto)
        {
            using (SqlConnection oconexion = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("sp_Registrar", oconexion);
                cmd.Parameters.AddWithValue("Nombre", ocontacto.Nombre);
                cmd.Parameters.AddWithValue("Telefono", ocontacto.Telefono);
                cmd.Parameters.AddWithValue("Correo", ocontacto.Correo);
                cmd.Parameters.AddWithValue("Direccion", ocontacto.Direccion);
                cmd.CommandType = CommandType.StoredProcedure;
                oconexion.Open();
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Inicio", "Contacto");
        }


        [HttpPost]
        public ActionResult Editar(Contacto ocontacto)
        {
            using (SqlConnection oconexion = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("sp_Editar", oconexion);
                cmd.Parameters.AddWithValue("ID", ocontacto.ID);
                cmd.Parameters.AddWithValue("Nombre", ocontacto.Nombre);
                cmd.Parameters.AddWithValue("Telefono", ocontacto.Telefono);
                cmd.Parameters.AddWithValue("Correo", ocontacto.Correo);
                cmd.Parameters.AddWithValue("Direccion", ocontacto.Direccion);
                cmd.CommandType = CommandType.StoredProcedure;
                oconexion.Open();
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Inicio", "Contacto");
        }


        [HttpPost]
        public ActionResult Eliminar(string IdContacto)
        {
            using (SqlConnection oconexion = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("sp_Eliminar", oconexion);
                cmd.Parameters.AddWithValue("IdContacto", IdContacto);
                cmd.CommandType = CommandType.StoredProcedure;
                oconexion.Open();
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Inicio", "Contacto");
        }
    }
}
