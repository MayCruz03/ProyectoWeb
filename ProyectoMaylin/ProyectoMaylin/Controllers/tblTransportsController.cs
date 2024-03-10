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
        public IQueryable<TransportDTO> GettblTransports()
        {
            return db.tblTransports.Select(t => new TransportDTO { 
                tra_id = t.tra_id,
                tra_description = t.tra_description,
                tra_type = t.tra_type,
                tra_category = t.tra_category
            });
        }

        // GET: api/tblTransports/5
        [ResponseType(typeof(TransportDTO))]
        public async Task<IHttpActionResult> GettblTransport(int id)
        {
            //SE MODIFICO
            var tran = await db.tblTransports.FindAsync(id);
            if (tran == null)
            {
                return NotFound();
            }

            return Ok(new TransportDTO
            {
                tra_id = tran.tra_id,
                tra_description = tran.tra_description,
                tra_type = tran.tra_type,
                tra_category = tran.tra_category
            });
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
        [ResponseType(typeof(TransportDTO))]
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
        [ResponseType(typeof(TransportDTO))]
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
    public class TransportDTO
    {
        public int tra_id { get; set; }
        public string tra_description { get; set; }
        public string tra_type { get; set; }
        public string tra_category { get; set; }
    }
}