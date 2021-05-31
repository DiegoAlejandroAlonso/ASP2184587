using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ASP2184587.Models;

namespace ASP2184587.Controllers
{
    public class productoController : Controller
    {
        private inventarioEntities1 db = new inventarioEntities1();

        // GET: producto
        public ActionResult Index()
        {
            var producto = db.producto.Include(p => p.proveedor);
            return View(producto.ToList());
        }

        // GET: producto/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            producto producto = db.producto.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // GET: producto/Create
        public ActionResult Create()
        {
            ViewBag.id_proveedor = new SelectList(db.proveedor, "id", "nombre");
            return View();
        }

        // POST: producto/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nombre,percio_unitario,descripcion,cantidad,id_proveedor")] producto producto)
        {
            if (ModelState.IsValid)
            {
                db.producto.Add(producto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_proveedor = new SelectList(db.proveedor, "id", "nombre", producto.id_proveedor);
            return View(producto);
        }

        // GET: producto/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            producto producto = db.producto.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_proveedor = new SelectList(db.proveedor, "id", "nombre", producto.id_proveedor);
            return View(producto);
        }

        // POST: producto/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nombre,percio_unitario,descripcion,cantidad,id_proveedor")] producto producto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(producto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_proveedor = new SelectList(db.proveedor, "id", "nombre", producto.id_proveedor);
            return View(producto);
        }

        // GET: producto/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            producto producto = db.producto.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // POST: producto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            producto producto = db.producto.Find(id);
            db.producto.Remove(producto);
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

        public ActionResult reporte()
        {
            var db = new inventarioEntities1();
            var query = from tabProvedor in db.proveedor
                        join tabProducto in db.producto on tabProvedor.id equals tabProducto.id_proveedor
                        select new reporte
                        {
                            nombreProveedor = tabProvedor.nombre,
                            telefonoProveedor = tabProvedor.telefono,
                            direccionProveedor = tabProvedor.direccion,
                            nombreProducto = tabProducto.nombre,
                            precioProducto = tabProducto.percio_unitario
                        };
            return View(query);
        }

    }

}
