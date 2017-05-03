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
using FanaticProjekt;

namespace FanaticProjekt.Controllers
{
    public class IRTypersController : ApiController
    {
        private DbModel db = new DbModel();

        // GET: api/IRTypers
        public IQueryable<IRTyper> GetIRTypers()
        {
            return db.IRTypers;
        }

        // GET: api/IRTypers/5
        [ResponseType(typeof(IRTyper))]
        public IHttpActionResult GetIRTyper(int id)
        {
            IRTyper iRTyper = db.IRTypers.Find(id);
            if (iRTyper == null)
            {
                return NotFound();
            }

            return Ok(iRTyper);
        }

        // PUT: api/IRTypers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutIRTyper(int id, IRTyper iRTyper)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != iRTyper.ID)
            {
                return BadRequest();
            }

            db.Entry(iRTyper).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IRTyperExists(id))
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

        // POST: api/IRTypers
        [ResponseType(typeof(IRTyper))]
        public IHttpActionResult PostIRTyper(IRTyper iRTyper)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.IRTypers.Add(iRTyper);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = iRTyper.ID }, iRTyper);
        }

        // DELETE: api/IRTypers/5
        [ResponseType(typeof(IRTyper))]
        public IHttpActionResult DeleteIRTyper(int id)
        {
            IRTyper iRTyper = db.IRTypers.Find(id);
            if (iRTyper == null)
            {
                return NotFound();
            }

            db.IRTypers.Remove(iRTyper);
            db.SaveChanges();

            return Ok(iRTyper);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool IRTyperExists(int id)
        {
            return db.IRTypers.Count(e => e.ID == id) > 0;
        }
    }
}