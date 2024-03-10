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
using Newtonsoft.Json;
using ProyectoMaylin;

namespace ProyectoMaylin.Controllers
{
    public class tblDocumentXCitizensController : ApiController
    {
        private bdProyectoWeb_MaylinCruzEntities db = new bdProyectoWeb_MaylinCruzEntities();

        // GET: api/tblDocumentXCitizens
        public IQueryable<tblDocumentXCitizenDTO> GettblDocumentXCitizens()
        {
            return db.tblDocumentXCitizens.Select(dc => new tblDocumentXCitizenDTO { 
                DoCi_id = dc.DoCi_id,
                doc_id = dc.doc_id,
                cit_id = dc.cit_id,
                DoCi_DateOfIssue = dc.DoCi_DateOfIssue,
                DoCi_plateNumber = dc.DoCi_plateNumber,
                DoCi_observations = dc.DoCi_observations,
                DoCi_number = dc.DoCi_number
            });
        }

        // GET: api/tblDocumentXCitizens/5
        [ResponseType(typeof(tblDocumentXCitizenDTO))]
        public async Task<IHttpActionResult> GettblDocumentXCitizen(int id)
        {
            //SE MODIFICO
            var doci = await db.tblDocumentXCitizens.FindAsync(id);
            if (doci == null)
            {
                return NotFound();
            }

            return Ok(new tblDocumentXCitizenDTO
            {
                DoCi_id = doci.DoCi_id,
                doc_id = doci.doc_id,
                cit_id = doci.cit_id,
                DoCi_DateOfIssue = doci.DoCi_DateOfIssue,
                DoCi_plateNumber = doci.DoCi_plateNumber,
                DoCi_observations = doci.DoCi_observations,
                DoCi_number = doci.DoCi_number
            });
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
        [ResponseType(typeof(tblDocumentXCitizenDTO))]
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
        [ResponseType(typeof(tblDocumentXCitizenDTO))]
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

    public partial class tblDocumentXCitizenDTO
    {
        public int DoCi_id { get; set; }
        public Nullable<int> doc_id { get; set; }
        public Nullable<int> cit_id { get; set; }
        public Nullable<System.DateTime> DoCi_DateOfIssue { get; set; }
        public string DoCi_plateNumber { get; set; }
        public string DoCi_observations { get; set; }
        public string DoCi_number { get; set; }
        [JsonIgnore]
        public tblCitizen tblCitizen { get; set; }
        [JsonIgnore]
        public tblTypeDocument tblTypeDocument { get; set; }
    }
}