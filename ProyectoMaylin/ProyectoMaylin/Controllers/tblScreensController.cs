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
    public class tblScreensController : ApiController
    {
        private bdProyectoWeb_MaylinCruzEntities db = new bdProyectoWeb_MaylinCruzEntities();

        // GET: api/tblScreens
        public IQueryable<ScreenDTO> GettblScreens()
        {
            return db.tblScreens.Select(s => new ScreenDTO
            {
                scr_id = s.scr_id,
                scr_description = s.scr_description,
                scr_active = s.scr_active,
                scr_controller = s.scr_action,
                scr_action = s.scr_action

            });
        }

        // GET: api/tblScreens/5
        [ResponseType(typeof(ScreenDTO))]
        public async Task<IHttpActionResult> GettblScreen(int id)
        {
            //SE MODIFICO
            var screen = await db.tblScreens.FindAsync(id);
            if (screen == null)
            {
                return NotFound();
            }

            return Ok(new ScreenDTO
            {
                scr_id = screen.scr_id,
                scr_description = screen.scr_description,
                scr_active = screen.scr_active,
                scr_controller = screen.scr_action,
                scr_action = screen.scr_action
            });
        }

        // PUT: api/tblScreens/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PuttblScreen(int id, tblScreen tblScreen)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblScreen.scr_id)
            {
                return BadRequest();
            }

            db.Entry(tblScreen).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblScreenExists(id))
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

        // POST: api/tblScreens
        [ResponseType(typeof(ScreenDTO))]
        public async Task<IHttpActionResult> PosttblScreen(tblScreen tblScreen)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tblScreens.Add(tblScreen);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = tblScreen.scr_id }, tblScreen);
        }

        // DELETE: api/tblScreens/5
        [ResponseType(typeof(ScreenDTO))]
        public async Task<IHttpActionResult> DeletetblScreen(int id)
        {
            tblScreen tblScreen = await db.tblScreens.FindAsync(id);
            if (tblScreen == null)
            {
                return NotFound();
            }

            db.tblScreens.Remove(tblScreen);
            await db.SaveChangesAsync();

            return Ok(tblScreen);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblScreenExists(int id)
        {
            return db.tblScreens.Count(e => e.scr_id == id) > 0;
        }
    }
    public class ScreenDTO
    {
        public int scr_id { get; set; }
        public string scr_description { get; set; }
        public Nullable<bool> scr_active { get; set; }
        public string scr_controller { get; set; }
        public string scr_action { get; set; }
    }
}