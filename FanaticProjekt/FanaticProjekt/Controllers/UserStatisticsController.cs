using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using FanaticProjekt;
using Fanatic_GUI.Model;

namespace FanaticProjekt.Controllers
{
    public class UserStatisticsController : ApiController
    {
        private DbModel db = new DbModel();

        // GET: api/UserStatistics
        public IQueryable<UserStatistic> GetUserStatistics()
        {
            return db.UserStatistics;
        }

        // GET: api/UserStatistics/5
        [ResponseType(typeof(UserStatistic))]
        public async Task<IHttpActionResult> GetUserStatistic(int id)
        {
            UserStatistic userStatistic = await db.UserStatistics.FindAsync(id);
            if (userStatistic == null)
            {
                return NotFound();
            }

            return Ok(userStatistic);
        }

        // PUT: api/UserStatistics/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUserStatistic(int id, UserStatistic userStatistic)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userStatistic.ID)
            {
                return BadRequest();
            }

            db.Entry(userStatistic).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserStatisticExists(id))
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

        // POST: api/UserStatistics
        [ResponseType(typeof(StatisticsDTO))]
        public async Task<IHttpActionResult> PostUserStatistic(StatisticsDTO userStatistic)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // Tjekker om userID'et eksistere ellers er en entry ligegyldig
            var id = await db.UsersTables.FindAsync(userStatistic.ID);
            if (id == null)
            {
                return NotFound();
            }
            // samler et keyvalue par hvor values er true, det representere dem de er valgt
            var keyValuePairs = userStatistic.BooleanDic.Where(x => x.Value);
            // Hvis counten ikke er 0 så bliver de gemt, eller returneres en badrequest
            if (keyValuePairs.Count() != 0)
            {
                foreach (var keyValuePair in keyValuePairs)
                {
                    // Variabel navngivningen for "n" kunne lige så vel have været x som i en ligning eller et sigende navn
                    // opretter nye userstatistics objekter
                    var n = new UserStatistic()
                    {
                        USER_NAME = userStatistic.ID,
                        IR_TYPE = keyValuePair.Key,
                        DATE = DateTime.Now
                    };
                    // gemmer det oprettede objekt
                    db.UserStatistics.Add(n);
                }
                // sender ændringerne til databasen
                await db.SaveChangesAsync();
                // returnere at de er blevet oprettet, så appen ikke får en fejl
                return CreatedAtRoute("DefaultApi", new { id = userStatistic.ID }, userStatistic);
            }
            // fejlen der bliver returneret når count er 0
            return StatusCode(HttpStatusCode.BadRequest);
        }

        // DELETE: api/UserStatistics/5
        [ResponseType(typeof(UserStatistic))]
        public async Task<IHttpActionResult> DeleteUserStatistic(int id)
        {
            UserStatistic userStatistic = await db.UserStatistics.FindAsync(id);
            if (userStatistic == null)
            {
                return NotFound();
            }

            db.UserStatistics.Remove(userStatistic);
            await db.SaveChangesAsync();

            return Ok(userStatistic);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserStatisticExists(int id)
        {
            return db.UserStatistics.Count(e => e.ID == id) > 0;
        }
    }
}