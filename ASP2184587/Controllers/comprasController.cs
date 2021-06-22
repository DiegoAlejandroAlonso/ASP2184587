using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ASP2184587.Models;
using Rotativa;
using System.IO;

namespace ASP2184587.Controllers
{
    public class comprasController : Controller
    {
        private inventarioEntities1 db = new inventarioEntities1();

        // GET: compras
        public ActionResult Index()
        {
            var compra = db.compra.Include(c => c.cliente).Include(c => c.usuario);
            return View(compra.ToList());
        }

        // GET: compras/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            compra compra = db.compra.Find(id);
            if (compra == null)
            {
                return HttpNotFound();
            }
            return View(compra);
        }

        // GET: compras/Create
        public ActionResult Create()
        {
            ViewBag.id_cliente = new SelectList(db.cliente, "id", "nombre");
            ViewBag.id_usuario = new SelectList(db.usuario, "id", "nombre");
            return View();

        }

        // POST: compras/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,fecha,total,id_usuario,id_cliente")] compra compra)
        {
                using (var db = new inventarioEntities1())
                if (ModelState.IsValid)
                {
                     db.compra.Add(compra);
                      db.SaveChanges();
                      return RedirectToAction("Index");
                 }

            ViewBag.id_cliente = new SelectList(db.cliente, "id", "nombre", compra.id_cliente);
            ViewBag.id_usuario = new SelectList(db.usuario, "id", "nombre", compra.id_usuario);
            return View(compra);
        }

        // GET: compras/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            compra compra = db.compra.Find(id);
            if (compra == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_cliente = new SelectList(db.cliente, "id", "nombre", compra.id_cliente);
            ViewBag.id_usuario = new SelectList(db.usuario, "id", "nombre", compra.id_usuario);
            return View(compra);
        }

        // POST: compras/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,fecha,total,id_usuario,id_cliente")] compra compra)
        {
            if (ModelState.IsValid)
            {
                db.Entry(compra).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_cliente = new SelectList(db.cliente, "id", "nombre", compra.id_cliente);
            ViewBag.id_usuario = new SelectList(db.usuario, "id", "nombre", compra.id_usuario);
            return View(compra);
        }

        // GET: compras/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            compra compra = db.compra.Find(id);
            if (compra == null)
            {
                return HttpNotFound();
            }
            return View(compra);
        }

        // POST: compras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            compra compra = db.compra.Find(id);
            db.compra.Remove(compra);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult ImprimirReporte()
        {
            return new ActionAsPdf("Reporte") { FileName = "Reporte.pdf" };
        }


        public ActionResult SubirCsv()
        {
            return View();
        }


        [HttpPost]

        public ActionResult SubirCsv(HttpPostedFileBase fileform)
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
                        var newCompra = new compra
                        {
                            //fecha = row.Split(';')[0],
                            //total = row.Split(';')[1],
                            
                            //id_usuario = row.Split(';')[2],
                            //id_cliente = row.Split(';')[3],
                        };

                        using (var db = new inventarioEntities1())
                        {
                            db.compra.Add(newCompra);

                            db.SaveChanges();

                        }


                    }
                }

            }
            return View();

        }


    }
}
