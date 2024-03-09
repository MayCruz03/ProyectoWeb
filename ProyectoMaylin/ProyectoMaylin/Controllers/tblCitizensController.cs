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
    public class tblCitizensController : ApiController
    {
        private bdProyectoWeb_MaylinCruzEntities db = new bdProyectoWeb_MaylinCruzEntities();

        // GET: api/tblCitizens
        public IQueryable<tblCitizen> GettblCitizens()
        {
            return db.tblCitizens;
        }

        // GET: api/tblCitizens/5
        [ResponseType(typeof(tblCitizen))]
        public async Task<IHttpActionResult> GettblCitizen(int id)
        {
            tblCitizen tblCitizen = await db.tblCitizens.FindAsync(id);
            if (tblCitizen == null)
            {
                return NotFound();
            }

            return Ok(tblCitizen);
        }

        // PUT: api/tblCitizens/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PuttblCitizen(int id, tblCitizen tblCitizen)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblCitizen.cit_id)
            {
                return BadRequest();
            }

            db.Entry(tblCitizen).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblCitizenExists(id))
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

        // POST: api/tblCitizens
        [ResponseType(typeof(tblCitizen))]
        public async Task<IHttpActionResult> PosttblCitizen(tblCitizen tblCitizen)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tblCitizens.Add(tblCitizen);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = tblCitizen.cit_id }, tblCitizen);
        }

        // DELETE: api/tblCitizens/5
        [ResponseType(typeof(tblCitizen))]
        public async Task<IHttpActionResult> DeletetblCitizen(int id)
        {
            tblCitizen tblCitizen = await db.tblCitizens.FindAsync(id);
            if (tblCitizen == null)
            {
                return NotFound();
            }

            db.tblCitizens.Remove(tblCitizen);
            await db.SaveChangesAsync();

            return Ok(tblCitizen);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblCitizenExists(int id)
        {
            return db.tblCitizens.Count(e => e.cit_id == id) > 0;
        }
    }
}