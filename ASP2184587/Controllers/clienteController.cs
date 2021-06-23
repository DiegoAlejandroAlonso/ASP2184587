using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
//importando los modelos de base de datos
using ASP2184587.Models;
using Rotativa;
using System.IO;

namespace ASP2184587.Controllers
{
    public class clienteController : Controller
    {
        // GET: 
        public ActionResult Index()
        {
            using (var db = new inventarioEntities1())
            {
                return View(db.cliente.ToList());
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(cliente cliente)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                using (var db = new inventarioEntities1())
                {

                    db.cliente.Add(cliente);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "error " + ex);
                return View();
            }
        }



        public ActionResult Details(int id)
        {
            using (var db = new inventarioEntities1())
            {
                var findUser = db.cliente.Find(id);
                return View(findUser);
            }
        }

        public ActionResult Edit(int id)
        {
            try
            {
                using (var db = new inventarioEntities1())
                {
                    cliente findUser = db.cliente.Where(a => a.id == id).FirstOrDefault();
                    return View(findUser);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "error " + ex);
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(cliente editUser)
        {
            try
            {
                using (var db = new inventarioEntities1())
                {
                    cliente user = db.cliente.Find(editUser.id);

                    user.nombre = editUser.nombre;
                    user.documento = editUser.documento;
                    user.email = editUser.email;


                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "error " + ex);
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                using (var db = new inventarioEntities1())
                {
                    var findUser = db.cliente.Find(id);
                    db.cliente.Remove(findUser);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "error " + ex);
                return View();
            }
        }

        public ActionResult ImprimirReporte()
        {
            return new ActionAsPdf("Reporte") { FileName = "Reporte.pdf" };
        }





        public ActionResult uploadCSV()
        {
            return View();
        }


        [HttpPost]

        public ActionResult uploadCSV(HttpPostedFileBase fileform)
        {

            string filePath = string.Empty;

            if (fileform != null)
            {
                //ruta de la carpeta que caragara el archivo
                string path = Server.MapPath("~/Uploads/");

                //verificar si la ruta de la carpeta existe
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                //obtener el nombre del archivo
                filePath = path + Path.GetFileName(fileform.FileName);
                //obtener la extension del archivo
                string extension = Path.GetExtension(fileform.FileName);

                //guardando el archivo
                fileform.SaveAs(filePath);

                string csvData = System.IO.File.ReadAllText(filePath);
                foreach (string row in csvData.Split('\n'))
                {
                    if (!string.IsNullOrEmpty(row))
                    {
                        var newCliente = new cliente
                        {
                            nombre = row.Split(';')[0],
                            documento = row.Split(';')[1],
                            email = row.Split(';')[2],
                           
                        };

                        using (var db = new inventarioEntities1())
                        {
                            db.cliente.Add(newCliente);

                            db.SaveChanges();

                        }


                    }
                }

            }
            return View();


        }

        public ActionResult Reporte2()
        {
            var db = new inventarioEntities1();
            var query = from tabCompras in db.compra
                        join tabCliente in db.cliente on tabCompras.id equals tabCliente.id
                        select new Reporte2
                        {
                            fechaCompras = tabCompras.fecha,
                            totalCompras = tabCompras.total,

                            nombreCliente = tabCliente.nombre,
                            documentoCliente = tabCliente.documento,
                            

                        };

            return View(query);
        }

        public ActionResult ImprimirReporte1()
        {
            return new ActionAsPdf("Reporte2") { FileName = "Reporte.pdf" };
        }

    }

}