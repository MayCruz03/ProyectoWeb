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
    public class tblUsersController : ApiController
    {
        private bdProyectoWeb_MaylinCruzEntities db = new bdProyectoWeb_MaylinCruzEntities();

        // GET: api/tblUsers
        //SE MODIFICO
        public IQueryable<UserDTO> GettblUsers()
        {
            return db.tblUsers.Select(u => new UserDTO
            {
                use_id = u.use_id,
                use_idnumber = u.use_idnumber,
                use_name = u.use_name,
                use_lastname = u.use_lastname,
                use_nickname = u.use_nickname,
                use_password = u.use_password,
                use_gender = u.use_gender,
                use_email = u.use_email,
                use_telephone = u.use_telephone,
                use_rol = u.use_rol
            });
        }

        // GET: api/tblUsers/5
        [ResponseType(typeof(UserDTO))] //SE MODIFICO ESTO
        public async Task<IHttpActionResult> GettblUser(int id)
        {
            //SE MODIFICO
            var user = await db.tblUsers.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(new UserDTO {
                use_id = user.use_id,
                use_idnumber = user.use_idnumber,
                use_name = user.use_name,
                use_lastname = user.use_lastname,
                use_nickname = user.use_nickname,
                use_password = user.use_password,
                use_gender = user.use_gender,
                use_email = user.use_email,
                use_telephone = user.use_telephone,
                use_rol = user.use_rol
            });
        }

        // PUT: api/tblUsers/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PuttblUser(int id, tblUser tblUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblUser.use_id)
            {
                return BadRequest();
            }

            db.Entry(tblUser).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblUserExists(id))
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

        // POST: api/tblUsers
        [ResponseType(typeof(UserDTO))] //SE MODIFICO
        public async Task<IHttpActionResult> PosttblUser(tblUser tblUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tblUsers.Add(tblUser);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = tblUser.use_id }, tblUser);
        }

        // DELETE: api/tblUsers/5
        [ResponseType(typeof(UserDTO))] //SE MODIFICO
        public async Task<IHttpActionResult> DeletetblUser(int id)
        {
            tblUser tblUser = await db.tblUsers.FindAsync(id);
            if (tblUser == null)
            {
                return NotFound();
            }

            db.tblUsers.Remove(tblUser);
            await db.SaveChangesAsync();

            return Ok(tblUser);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblUserExists(int id)
        {
            return db.tblUsers.Count(e => e.use_id == id) > 0;
        }
    }
    public class UserDTO
    {
        public int use_id { get; set; }
        public string use_idnumber { get; set; }
        public string use_name { get; set; }
        public string use_lastname { get; set; }
        public string use_nickname { get; set; }
        public string use_password { get; set; }
        public string use_gender { get; set; }
        public string use_email { get; set; }
        public string use_telephone { get; set; }
        // Propiedad para el rol del usuario
        public Nullable<int> use_rol { get; set; }

        // No serializar tblRol para evitar referencia circular
        [JsonIgnore]
        public tblRol tblRol { get; set; }
    }
}