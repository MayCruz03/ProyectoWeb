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
    public class tblCountriesController : ApiController
    {
        private bdProyectoWeb_MaylinCruzEntities db = new bdProyectoWeb_MaylinCruzEntities();

        // GET: api/tblCountries
        public IQueryable<CountryDTO> GettblCountries()
        {
            return db.tblCountries.Select(c => new CountryDTO { 
                cou_id = c.cou_id,
                cou_code = c.cou_code,
                cou_name = c.cou_name,
                cou_nationality = c.cou_nationality
            });
        }

        // GET: api/tblCountries/5
        [ResponseType(typeof(CountryDTO))]
        public async Task<IHttpActionResult> GettblCountry(int id)
        {
            //SE MODIFICO
            var coun = await db.tblCountries.FindAsync(id);
            if (coun == null)
            {
                return NotFound();
            }

            return Ok(new CountryDTO
            {
                cou_id = coun.cou_id,
                cou_code = coun.cou_code,
                cou_name = coun.cou_name,
                cou_nationality = coun.cou_nationality
            });
        }

        // PUT: api/tblCountries/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PuttblCountry(int id, tblCountry tblCountry)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblCountry.cou_id)
            {
                return BadRequest();
            }

            db.Entry(tblCountry).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblCountryExists(id))
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

        // POST: api/tblCountries
        [ResponseType(typeof(CountryDTO))]
        public async Task<IHttpActionResult> PosttblCountry(tblCountry tblCountry)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tblCountries.Add(tblCountry);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = tblCountry.cou_id }, tblCountry);
        }

        // DELETE: api/tblCountries/5
        [ResponseType(typeof(CountryDTO))]
        public async Task<IHttpActionResult> DeletetblCountry(int id)
        {
            tblCountry tblCountry = await db.tblCountries.FindAsync(id);
            if (tblCountry == null)
            {
                return NotFound();
            }

            db.tblCountries.Remove(tblCountry);
            await db.SaveChangesAsync();

            return Ok(tblCountry);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblCountryExists(int id)
        {
            return db.tblCountries.Count(e => e.cou_id == id) > 0;
        }
    }
    public class CountryDTO
    {
        public int cou_id { get; set; }
        public string cou_code { get; set; }
        public string cou_name { get; set; }
        public string cou_nationality { get; set; }
    }
}