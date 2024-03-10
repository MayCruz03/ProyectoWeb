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
    public class tblCitizensController : ApiController
    {
        private bdProyectoWeb_MaylinCruzEntities db = new bdProyectoWeb_MaylinCruzEntities();

        // GET: api/tblCitizens
        public IQueryable<CitizenDTO> GettblCitizens()
        {
            return db.tblCitizens.Select(c => new CitizenDTO { 
                cit_id = c.cit_id,
                cit_identifyType = c.cit_identifyType,
                cit_idnumber = c.cit_idnumber,
                cit_name = c.cit_name,
                cit_lastname = c.cit_lastname,
                cit_birthdate = c.cit_birthdate,
                cit_gender = c.cit_gender,
                cit_telephone = c.cit_telephone,
                cit_email = c.cit_email,
                cit_address = c.cit_address,
                cit_civilStatus = c.cit_civilStatus,
                cit_bloodType = c.cit_bloodType,
                cit_country = c.cit_country
            });
        }

        // GET: api/tblCitizens/5
        [ResponseType(typeof(CitizenDTO))]
        public async Task<IHttpActionResult> GettblCitizen(int id)
        {
            //SE MODIFICO
            var citi = await db.tblCitizens.FindAsync(id);
            if (citi == null)
            {
                return NotFound();
            }

            return Ok(new CitizenDTO
            {
                cit_id = citi.cit_id,
                cit_identifyType = citi.cit_identifyType,
                cit_idnumber = citi.cit_idnumber,
                cit_name = citi.cit_name,
                cit_lastname = citi.cit_lastname,
                cit_birthdate = citi.cit_birthdate,
                cit_gender = citi.cit_gender,
                cit_telephone = citi.cit_telephone,
                cit_email = citi.cit_email,
                cit_address = citi.cit_address,
                cit_civilStatus = citi.cit_civilStatus,
                cit_bloodType = citi.cit_bloodType,
                cit_country = citi.cit_country
            });
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
        [ResponseType(typeof(CitizenDTO))]
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
        [ResponseType(typeof(CitizenDTO))]
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
    public class CitizenDTO
    {
        public int cit_id { get; set; }
        public string cit_identifyType { get; set; }
        public string cit_idnumber { get; set; }
        public string cit_name { get; set; }
        public string cit_lastname { get; set; }
        public Nullable<System.DateTime> cit_birthdate { get; set; }
        //public Nullable<int> cit_age { get; set; }
        public string cit_gender { get; set; }
        public string cit_telephone { get; set; }
        public string cit_email { get; set; }
        public string cit_address { get; set; }
        public string cit_civilStatus { get; set; }
        public string cit_bloodType { get; set; }
        public Nullable<int> cit_country { get; set; }

        // No serializar tblRol para evitar referencia circular
        [JsonIgnore]
        public tblCountry tblCountry { get; set; }
    }
}