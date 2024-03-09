using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ProyectoMaylin;

namespace ProyectoMaylin.Controllers
{
    public class tblRolsController : ApiController
    {
        private bdProyectoWeb_MaylinCruzEntities db = new bdProyectoWeb_MaylinCruzEntities();

        // GET: api/tblRols
        public IQueryable<RolDTO> GettblRols()
        {
            // Obtener los roles desde la base de datos
            var roles = db.tblRols.Select(r => new RolDTO
            {
                rol_id = r.rol_id,
                rol_descripcion = r.rol_descripcion,
                rol_observaciones = r.rol_observaciones
            }).ToList(); // Convertir a lista para evitar problemas de serialización circular

            return roles.AsQueryable();
        }

        // GET: api/tblRols/5
        [ResponseType(typeof(RolDTO))] //SE MODIFICO ESTO
        public async Task<IHttpActionResult> GettblRol(int id)
        {
            //SE MODIFICO
            var rol = await db.tblRols.FindAsync(id);
            if (rol == null)
            {
                return NotFound();
            }

            return Ok(new RolDTO
            {
                rol_id = rol.rol_id,
                rol_descripcion = rol.rol_descripcion,
                rol_observaciones = rol.rol_observaciones
            });
        }

        // PUT: api/tblRols/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PuttblRol(int id, tblRol tblRol)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblRol.rol_id)
            {
                return BadRequest();
            }

            db.Entry(tblRol).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblRolExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/tblRols
        [ResponseType(typeof(RolDTO))] //SE MODDIFICO
        public async Task<IHttpActionResult> PosttblRol(tblRol tblRol)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tblRols.Add(tblRol);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = tblRol.rol_id }, tblRol);
        }

        // DELETE: api/tblRols/5
        [ResponseType(typeof(RolDTO))] //SE MODIFICO
        public async Task<IHttpActionResult> DeletetblRol(int id)
        {
            tblRol tblRol = await db.tblRols.FindAsync(id);
            if (tblRol == null)
            {
                return NotFound();
            }

            db.tblRols.Remove(tblRol);
            await db.SaveChangesAsync();

            return Ok(tblRol);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblRolExists(int id)
        {
            return db.tblRols.Count(e => e.rol_id == id) > 0;
        }
    }
    public class RolDTO
    {
        public int rol_id { get; set; }
        public string rol_descripcion { get; set; }
        public string rol_observaciones { get; set; }
    }
}