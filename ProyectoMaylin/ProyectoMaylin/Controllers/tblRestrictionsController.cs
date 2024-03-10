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
    public class tblRestrictionsController : ApiController
    {
        private bdProyectoWeb_MaylinCruzEntities db = new bdProyectoWeb_MaylinCruzEntities();

        // GET: api/tblRestrictions
        public IQueryable<RestrictionDTO> GettblRestrictions()
        {
            return db.tblRestrictions.Select(r => new RestrictionDTO
            {
                res_id = r.res_id,
                res_ScreenID = r.res_ScreenID,
                res_RolID = r.res_RolID
            });
        }

        // GET: api/tblRestrictions/5
        [ResponseType(typeof(RestrictionDTO))]
        public async Task<IHttpActionResult> GettblRestriction(int id)
        {
            //SE MODIFICO
            var rest = await db.tblRestrictions.FindAsync(id);
            if (rest == null)
            {
                return NotFound();
            }

            return Ok(new RestrictionDTO
            {
                res_id = rest.res_id,
                res_ScreenID = rest.res_ScreenID,
                res_RolID = rest.res_RolID
            });
        }

        // PUT: api/tblRestrictions/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PuttblRestriction(int id, tblRestriction tblRestriction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblRestriction.res_id)
            {
                return BadRequest();
            }

            db.Entry(tblRestriction).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblRestrictionExists(id))
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

        // POST: api/tblRestrictions
        [ResponseType(typeof(RestrictionDTO))]
        public async Task<IHttpActionResult> PosttblRestriction(tblRestriction tblRestriction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tblRestrictions.Add(tblRestriction);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = tblRestriction.res_id }, tblRestriction);
        }

        // DELETE: api/tblRestrictions/5
        [ResponseType(typeof(RestrictionDTO))]
        public async Task<IHttpActionResult> DeletetblRestriction(int id)
        {
            tblRestriction tblRestriction = await db.tblRestrictions.FindAsync(id);
            if (tblRestriction == null)
            {
                return NotFound();
            }

            db.tblRestrictions.Remove(tblRestriction);
            await db.SaveChangesAsync();

            return Ok(tblRestriction);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblRestrictionExists(int id)
        {
            return db.tblRestrictions.Count(e => e.res_id == id) > 0;
        }
    }
    public class RestrictionDTO
    {
        public int res_id { get; set; }
        public Nullable<int> res_ScreenID { get; set; }
        public Nullable<int> res_RolID { get; set; }

        // No serializar tblRol para evitar referencia circular
        [JsonIgnore]
        public tblScreen tblScreen { get; set; }
        [JsonIgnore]
        public tblRol tblRol { get; set; }
    }
}