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
    public class tblDocumentsController : ApiController
    {
        private bdProyectoWeb_MaylinCruzEntities db = new bdProyectoWeb_MaylinCruzEntities();

        // GET: api/tblDocuments
        public IQueryable<tblDocument> GettblDocuments()
        {
            return db.tblDocuments;
        }

        // GET: api/tblDocuments/5
        [ResponseType(typeof(tblDocument))]
        public async Task<IHttpActionResult> GettblDocument(int id)
        {
            tblDocument tblDocument = await db.tblDocuments.FindAsync(id);
            if (tblDocument == null)
            {
                return NotFound();
            }

            return Ok(tblDocument);
        }

        // PUT: api/tblDocuments/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PuttblDocument(int id, tblDocument tblDocument)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblDocument.doc_id)
            {
                return BadRequest();
            }

            db.Entry(tblDocument).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblDocumentExists(id))
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

        // POST: api/tblDocuments
        [ResponseType(typeof(tblDocument))]
        public async Task<IHttpActionResult> PosttblDocument(tblDocument tblDocument)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tblDocuments.Add(tblDocument);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = tblDocument.doc_id }, tblDocument);
        }

        // DELETE: api/tblDocuments/5
        [ResponseType(typeof(tblDocument))]
        public async Task<IHttpActionResult> DeletetblDocument(int id)
        {
            tblDocument tblDocument = await db.tblDocuments.FindAsync(id);
            if (tblDocument == null)
            {
                return NotFound();
            }

            db.tblDocuments.Remove(tblDocument);
            await db.SaveChangesAsync();

            return Ok(tblDocument);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblDocumentExists(int id)
        {
            return db.tblDocuments.Count(e => e.doc_id == id) > 0;
        }
    }
}