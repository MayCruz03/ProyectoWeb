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
    public class tblDocumentXCitizensController : ApiController
    {
        private bdProyectoWeb_MaylinCruzEntities db = new bdProyectoWeb_MaylinCruzEntities();

        // GET: api/tblDocumentXCitizens
        public IQueryable<tblDocumentXCitizen> GettblDocumentXCitizens()
        {
            return db.tblDocumentXCitizens;
        }

        // GET: api/tblDocumentXCitizens/5
        [ResponseType(typeof(tblDocumentXCitizen))]
        public async Task<IHttpActionResult> GettblDocumentXCitizen(int id)
        {
            tblDocumentXCitizen tblDocumentXCitizen = await db.tblDocumentXCitizens.FindAsync(id);
            if (tblDocumentXCitizen == null)
            {
                return NotFound();
            }

            return Ok(tblDocumentXCitizen);
        }

        // PUT: api/tblDocumentXCitizens/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PuttblDocumentXCitizen(int id, tblDocumentXCitizen tblDocumentXCitizen)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblDocumentXCitizen.DoCi_id)
            {
                return BadRequest();
            }

            db.Entry(tblDocumentXCitizen).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblDocumentXCitizenExists(id))
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

        // POST: api/tblDocumentXCitizens
        [ResponseType(typeof(tblDocumentXCitizen))]
        public async Task<IHttpActionResult> PosttblDocumentXCitizen(tblDocumentXCitizen tblDocumentXCitizen)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tblDocumentXCitizens.Add(tblDocumentXCitizen);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = tblDocumentXCitizen.DoCi_id }, tblDocumentXCitizen);
        }

        // DELETE: api/tblDocumentXCitizens/5
        [ResponseType(typeof(tblDocumentXCitizen))]
        public async Task<IHttpActionResult> DeletetblDocumentXCitizen(int id)
        {
            tblDocumentXCitizen tblDocumentXCitizen = await db.tblDocumentXCitizens.FindAsync(id);
            if (tblDocumentXCitizen == null)
            {
                return NotFound();
            }

            db.tblDocumentXCitizens.Remove(tblDocumentXCitizen);
            await db.SaveChangesAsync();

            return Ok(tblDocumentXCitizen);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblDocumentXCitizenExists(int id)
        {
            return db.tblDocumentXCitizens.Count(e => e.DoCi_id == id) > 0;
        }
    }
}