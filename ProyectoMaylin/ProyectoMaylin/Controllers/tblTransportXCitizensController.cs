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
    public class tblTransportXCitizensController : ApiController
    {
        private bdProyectoWeb_MaylinCruzEntities db = new bdProyectoWeb_MaylinCruzEntities();

        // GET: api/tblTransportXCitizens
        public IQueryable<TransportXCitizenDTO> GettblTransportXCitizens()
        {
            return db.tblTransportXCitizens.Select(tc => new TransportXCitizenDTO { 
                TrCi_id = tc.TrCi_id,
                tra_id = tc.tra_id,
                cit_id = tc.cit_id,
                TrCi_brand = tc.TrCi_brand,
                TrCi_model = tc.TrCi_model,
                TrCi_year = tc.TrCi_year,
                TrCi_serialNumber = tc.TrCi_serialNumber,
                TrCi_vehicleDescription = tc.TrCi_vehicleDescription
            });
        }

        // GET: api/tblTransportXCitizens/5
        [ResponseType(typeof(TransportXCitizenDTO))]
        public async Task<IHttpActionResult> GettblTransportXCitizen(int id)
        {
            //SE MODIFICO
            var trci = await db.tblTransportXCitizens.FindAsync(id);
            if (trci == null)
            {
                return NotFound();
            }

            return Ok(new TransportXCitizenDTO
            {
                TrCi_id = trci.TrCi_id,
                tra_id = trci.tra_id,
                cit_id = trci.cit_id,
                TrCi_brand = trci.TrCi_brand,
                TrCi_model = trci.TrCi_model,
                TrCi_year = trci.TrCi_year,
                TrCi_serialNumber = trci.TrCi_serialNumber,
                TrCi_vehicleDescription = trci.TrCi_vehicleDescription
            });
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

        //// POST: api/tblTransportXCitizens
        //[ResponseType(typeof(TransportXCitizenDTO))]
        //public async Task<IHttpActionResult> PosttblTransportXCitizen(tblTransportXCitizen tblTransportXCitizen)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.tblTransportXCitizens.Add(tblTransportXCitizen);
        //    await db.SaveChangesAsync();

        //    return CreatedAtRoute("DefaultApi", new { id = tblTransportXCitizen.TrCi_id }, tblTransportXCitizen);
        //}

        // POST: api/tblTransportXCitizens
        [ResponseType(typeof(TransportXCitizenDTO))]
        public async Task<IHttpActionResult> PosttblTransportXCitizen(tblTransportXCitizen tblTransportXCitizen)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tblTransportXCitizens.Add(tblTransportXCitizen);
            await db.SaveChangesAsync();

            TransportXCitizenDTO transportXCitizenDTO = new TransportXCitizenDTO
            {
                TrCi_id = tblTransportXCitizen.TrCi_id,
                tra_id = tblTransportXCitizen.tra_id,
                cit_id = tblTransportXCitizen.cit_id,
                TrCi_brand = tblTransportXCitizen.TrCi_brand,
                TrCi_model = tblTransportXCitizen.TrCi_model,
                TrCi_year = tblTransportXCitizen.TrCi_year,
                TrCi_serialNumber = tblTransportXCitizen.TrCi_serialNumber,
                TrCi_vehicleDescription = tblTransportXCitizen.TrCi_vehicleDescription
            };

            return CreatedAtRoute("DefaultApi", new { id = tblTransportXCitizen.tra_id }, transportXCitizenDTO);
        }


        // DELETE: api/tblTransportXCitizens/5
        [ResponseType(typeof(TransportXCitizenDTO))]
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
    public class TransportXCitizenDTO
    {
        public int TrCi_id { get; set; }
        public Nullable<int> tra_id { get; set; }
        public Nullable<int> cit_id { get; set; }
        public string TrCi_brand { get; set; }
        public string TrCi_model { get; set; }
        public Nullable<int> TrCi_year { get; set; }
        public string TrCi_serialNumber { get; set; }
        public string TrCi_vehicleDescription { get; set; }

        [JsonIgnore]
        public tblCitizen tblCitizen { get; set; }
        [JsonIgnore]
        public tblTransport tblTransport { get; set; }
    }
}