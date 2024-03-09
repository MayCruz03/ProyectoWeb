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
    public class tblTransportXCitizensController : ApiController
    {
        private bdProyectoWeb_MaylinCruzEntities db = new bdProyectoWeb_MaylinCruzEntities();

        // GET: api/tblTransportXCitizens
        public IQueryable<tblTransportXCitizen> GettblTransportXCitizens()
        {
            return db.tblTransportXCitizens;
        }

        // GET: api/tblTransportXCitizens/5
        [ResponseType(typeof(tblTransportXCitizen))]
        public async Task<IHttpActionResult> GettblTransportXCitizen(int id)
        {
            tblTransportXCitizen tblTransportXCitizen = await db.tblTransportXCitizens.FindAsync(id);
            if (tblTransportXCitizen == null)
            {
                return NotFound();
            }

            return Ok(tblTransportXCitizen);
        }

        // PUT: api/tblTransportXCitizens/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PuttblTransportXCitizen(int id, tblTransportXCitizen tblTransportXCitizen)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblTransportXCitizen.TrCi_id)
            {
                return BadRequest();
            }

            db.Entry(tblTransportXCitizen).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblTransportXCitizenExists(id))
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

        // POST: api/tblTransportXCitizens
        [ResponseType(typeof(tblTransportXCitizen))]
        public async Task<IHttpActionResult> PosttblTransportXCitizen(tblTransportXCitizen tblTransportXCitizen)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tblTransportXCitizens.Add(tblTransportXCitizen);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = tblTransportXCitizen.TrCi_id }, tblTransportXCitizen);
        }

        // DELETE: api/tblTransportXCitizens/5
        [ResponseType(typeof(tblTransportXCitizen))]
        public async Task<IHttpActionResult> DeletetblTransportXCitizen(int id)
        {
            tblTransportXCitizen tblTransportXCitizen = await db.tblTransportXCitizens.FindAsync(id);
            if (tblTransportXCitizen == null)
            {
                return NotFound();
            }

            db.tblTransportXCitizens.Remove(tblTransportXCitizen);
            await db.SaveChangesAsync();

            return Ok(tblTransportXCitizen);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblTransportXCitizenExists(int id)
        {
            return db.tblTransportXCitizens.Count(e => e.TrCi_id == id) > 0;
        }
    }
}