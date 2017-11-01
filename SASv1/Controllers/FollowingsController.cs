using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using SASDatabase;
using System.Web.Http.Cors;

namespace SASv1.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class FollowingsController : ApiController
    {
        private SASEntities db = new SASEntities();

        // GET: api/Followings
        public IQueryable<Following> GetFollowings()
        {
            return db.Followings;
        }

        // GET: api/Followings/5
        [ResponseType(typeof(Following))]
        public IHttpActionResult GetFollowing(int id)
        {
            Following following = db.Followings.Find(id);
            if (following == null)
            {
                return NotFound();
            }

            return Ok(following);
        }

        // PUT: api/Followings/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFollowing(int id, Following following)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != following.ID)
            {
                return BadRequest();
            }

            db.Entry(following).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FollowingExists(id))
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

        // POST: api/Followings
        [ResponseType(typeof(Following))]
        public IHttpActionResult PostFollowing(Following following)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Followings.Add(following);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = following.ID }, following);
        }

        // DELETE: api/Followings/5
        [ResponseType(typeof(Following))]
        public IHttpActionResult DeleteFollowing(int id)
        {
            Following following = db.Followings.Find(id);
            if (following == null)
            {
                return NotFound();
            }

            db.Followings.Remove(following);
            db.SaveChanges();

            return Ok(following);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FollowingExists(int id)
        {
            return db.Followings.Count(e => e.ID == id) > 0;
        }
    }
}