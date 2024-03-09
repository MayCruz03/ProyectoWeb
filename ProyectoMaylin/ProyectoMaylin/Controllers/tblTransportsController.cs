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
    public class tblTransportsController : ApiController
    {
        private bdProyectoWeb_MaylinCruzEntities db = new bdProyectoWeb_MaylinCruzEntities();

        // GET: api/tblTransports
        public IQueryable<tblTransport> GettblTransports()
        {
            return db.tblTransports;
        }

        // GET: api/tblTransports/5
        [ResponseType(typeof(tblTransport))]
        public async Task<IHttpActionResult> GettblTransport(int id)
        {
            tblTransport tblTransport = await db.tblTransports.FindAsync(id);
            if (tblTransport == null)
            {
                return NotFound();
            }

            return Ok(tblTransport);
        }

        // PUT: api/tblTransports/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PuttblTransport(int id, tblTransport tblTransport)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblTransport.tra_id)
            {
                return BadRequest();
            }

            db.Entry(tblTransport).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblTransportExists(id))
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

        // POST: api/tblTransports
        [ResponseType(typeof(tblTransport))]
        public async Task<IHttpActionResult> PosttblTransport(tblTransport tblTransport)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tblTransports.Add(tblTransport);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = tblTransport.tra_id }, tblTransport);
        }

        // DELETE: api/tblTransports/5
        [ResponseType(typeof(tblTransport))]
        public async Task<IHttpActionResult> DeletetblTransport(int id)
        {
            tblTransport tblTransport = await db.tblTransports.FindAsync(id);
            if (tblTransport == null)
            {
                return NotFound();
            }

            db.tblTransports.Remove(tblTransport);
            await db.SaveChangesAsync();

            return Ok(tblTransport);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblTransportExists(int id)
        {
            return db.tblTransports.Count(e => e.tra_id == id) > 0;
        }
    }
}