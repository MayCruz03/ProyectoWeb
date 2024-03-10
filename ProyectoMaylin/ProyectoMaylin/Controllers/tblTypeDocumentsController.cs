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
    public class tblTypeDocumentsController : ApiController
    {
        private bdProyectoWeb_MaylinCruzEntities db = new bdProyectoWeb_MaylinCruzEntities();

        // GET: api/tblTypeDocuments
        public IQueryable<TypeDocumentDTO> GettblTypeDocuments()
        {
            return db.tblTypeDocuments.Select(s => new TypeDocumentDTO { 
                doc_id = s.doc_id,
                doc_type = s.doc_type,
                doc_observations = s.doc_observations,
                doc_pricecost = s.doc_pricecost
            });
        }

        // GET: api/tblTypeDocuments/5
        [ResponseType(typeof(TypeDocumentDTO))]
        public async Task<IHttpActionResult> GettblTypeDocument(int id)
        {
            //SE MODIFICO
            var tydo = await db.tblTypeDocuments.FindAsync(id);
            if (tydo == null)
            {
                return NotFound();
            }

            return Ok(new TypeDocumentDTO
            {
                doc_id = tydo.doc_id,
                doc_type = tydo.doc_type,
                doc_observations = tydo.doc_observations,
                doc_pricecost = tydo.doc_pricecost
            });
        }

        // PUT: api/tblTypeDocuments/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PuttblTypeDocument(int id, tblTypeDocument tblTypeDocument)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblTypeDocument.doc_id)
            {
                return BadRequest();
            }

            db.Entry(tblTypeDocument).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblTypeDocumentExists(id))
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

        // POST: api/tblTypeDocuments
        [ResponseType(typeof(TypeDocumentDTO))]
        public async Task<IHttpActionResult> PosttblTypeDocument(tblTypeDocument tblTypeDocument)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tblTypeDocuments.Add(tblTypeDocument);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = tblTypeDocument.doc_id }, tblTypeDocument);
        }

        // DELETE: api/tblTypeDocuments/5
        [ResponseType(typeof(TypeDocumentDTO))]
        public async Task<IHttpActionResult> DeletetblTypeDocument(int id)
        {
            tblTypeDocument tblTypeDocument = await db.tblTypeDocuments.FindAsync(id);
            if (tblTypeDocument == null)
            {
                return NotFound();
            }

            db.tblTypeDocuments.Remove(tblTypeDocument);
            await db.SaveChangesAsync();

            return Ok(tblTypeDocument);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblTypeDocumentExists(int id)
        {
            return db.tblTypeDocuments.Count(e => e.doc_id == id) > 0;
        }
    }
    public class TypeDocumentDTO
    {
        public int doc_id { get; set; }
        public string doc_type { get; set; }
        public string doc_observations { get; set; }
        public Nullable<decimal> doc_pricecost { get; set; }
    }
}